using Ecobici.WP.Common;
using Ecobici.WP.Enumerators;
using Ecobici.WP.Model;
using Ecobici.WP.Model.EventArgs;
using Ecobici.WP.PhoneServices;
using Ecobici.WP.Services.Availability;
using Ecobici.WP.Services.Localization;
using Ecobici.WP.Services.PhoneCall;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Devices.Geolocation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Ecobici.WP.Model.Entities;

namespace Ecobici.WP.ViewModel
{
    public class VMMainPage : NotifyPropertyBase
    {

        public VMMainPage(ILocalizationService localizationService, IPhoneCallService phoneCallService, MapItemsModel mapsItemModel, IVibrationService vibrationService, IAvailabilityService availabilityService, IConnectionManager connectionManager)
        {
            _localizationService = localizationService;
            _phoneCallService = phoneCallService;
            _mapsItemModel = mapsItemModel;
            _vibrationService = vibrationService;
            _availabilityService = availabilityService;
            _connectionManager = connectionManager;

            _dispatcherTimerChronometer = new DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 1) };
            _dispatcherTimerChronometer.Tick += _dispatcherTimer_Tick;

            var dispatcherTimerAvailability = new DispatcherTimer()
            {
                Interval = new TimeSpan(0, 0, 0, 30)
            };
            dispatcherTimerAvailability.Tick += _dispatcherTimerAvailability_Tick;
            dispatcherTimerAvailability.Start();

            _mapsItemModel.GetNearMapItemsCompleted += GetNearMapItemsCompleted;

            _localizationService.Geolocator.PositionChanged += _geolocator_PositionChanged;

            SetCurrentPositionAsync();
            ZoomLevel = 15;

            _lazyPhoneCallCommand = new Lazy<DelegateCommand>(() => new DelegateCommand(PhoneCallCommandExecute));

            _lazyGetNearStationsCommand = new Lazy<DelegateCommand>(() => new DelegateCommand(GetNearStationsCommandExecute));

            _lazyStartChronometerCommand = new Lazy<DelegateCommand>(() => new DelegateCommand(StartChronometerCommandExecute));
        }

        #region Properties

        private int _zoomLevel;

        public int ZoomLevel
        {
            get { return _zoomLevel; }
            set
            {
                _zoomLevel = value;
                OnPropertyChanged();
            }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        private Chronometer _chronometer;

        public Chronometer Chronometer
        {
            get
            {
                return _chronometer ?? (_chronometer = new Chronometer()
                {
                    Minutes = 45
                });
            }
            set
            {
                _chronometer = value;
                OnPropertyChanged();
            }
        }

        private Geopoint _currentGeopoint;

        private Geopoint _lastCenter;

        public Geopoint CurrentGeopoint
        {
            get
            {
                return _currentGeopoint ?? (_currentGeopoint = new Geopoint(new BasicGeoposition()
                {
                    Latitude = 19.432223858311772,
                    Longitude = -99.1304486989975,
                    Altitude = 1.5027934804138715E+200
                }));
            }
            set
            {
                _currentGeopoint = value;
                OnPropertyChanged();
            }
        }

        private ChronometerType _chronometerType;

        public ChronometerType ChronometerType
        {
            get { return _chronometerType; }
            set
            {
                _chronometerType = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<MapItem> _mapItems;

        public ObservableCollection<MapItem> MapItems
        {
            get
            {
                return _mapItems ?? (_mapItems = new ObservableCollection<MapItem>());
            }
            set
            {
                _mapItems = value;
                OnPropertyChanged();
            }
        }

        private List<AvailabilityStatus> _availabilityStatus;

        public List<AvailabilityStatus> AvailabilityStatus
        {
            get { return _availabilityStatus ?? (new List<AvailabilityStatus>()); }
            set
            {
                _availabilityStatus = value;
            }
        }

        #endregion Properties

        #region Fields

        private readonly ILocalizationService _localizationService;
        private readonly IPhoneCallService _phoneCallService;
        private readonly MapItemsModel _mapsItemModel;
        private readonly IVibrationService _vibrationService;
        private readonly IAvailabilityService _availabilityService;
        private readonly IConnectionManager _connectionManager;
        private readonly DispatcherTimer _dispatcherTimerChronometer;

        #endregion Fields

        #region Commands

        private readonly Lazy<DelegateCommand> _lazyPhoneCallCommand;

        public ICommand PhoneCallCommand => _lazyPhoneCallCommand.Value;

        private readonly Lazy<DelegateCommand> _lazyGetNearStationsCommand;

        public ICommand GetNearStationsCommand => _lazyGetNearStationsCommand.Value;

        private readonly Lazy<DelegateCommand> _lazyStartChronometerCommand;

        public ICommand StartChronometerCommand => _lazyStartChronometerCommand.Value;

        #endregion Commands

        #region CommandExecuteMethods

        private async void GetNearStationsCommandExecute(object parameter)
        {
            var currentGeopoint = parameter as Geopoint;
            if (currentGeopoint != null)
            {
                if (_lastCenter == null)
                {
                    _lastCenter = currentGeopoint;
                }
                if (_lastCenter.CalculateDistance(currentGeopoint)>.5)
                {
                    await _mapsItemModel.GetNearMapItems(currentGeopoint);
                    _lastCenter = currentGeopoint;
                }
            }
        }

        private void PhoneCallCommandExecute(object parameter)
        {
            _phoneCallService.Call();
        }

        private void StartChronometerCommandExecute(object parameter)
        {
            StartChronometer();
        }

        #endregion CommandExecuteMethods

        #region Methods

        private async void SetCurrentPositionAsync()
        {
            Geoposition result = null;
            result = await _localizationService.GetGeopositionAsync();
            if (result != null)
            {
                var mapItemResult = MapItems.FirstOrDefault(pre => pre.ItemType == ItemType.Position);

                if (mapItemResult == null)
                {
                    CurrentGeopoint = result.Coordinate.Point;
                    MapItems.Add(new MapItem
                    {
                        Geopoint = CurrentGeopoint,
                        ItemType = ItemType.Position
                    });
                    ZoomLevel = 15;
                }
                else
                {
                    CurrentGeopoint = result.Coordinate.Point;
                    mapItemResult.Geopoint = CurrentGeopoint;
                    ZoomLevel = 15;
                }
                await SetNeerEcobiciStations();
            }
        }

        private async Task SetNeerEcobiciStations()
        {
            GetNearStationsCommandExecute(CurrentGeopoint);
            if (_connectionManager.IsConnectionAvailable())
            {
                await _availabilityService.GetAvailabilityAsync();
                if (_availabilityService.AvailabilityStatuses != null)
                    AvailabilityStatus = new List<AvailabilityStatus>(_availabilityService.AvailabilityStatuses);
            }
        }

        private void StartChronometer()
        {
            switch (ChronometerType)
            {
                case ChronometerType.Inicial:
                    _dispatcherTimerChronometer.Start();
                    ChronometerType = ChronometerType.Stop;
                    break;

                case ChronometerType.Stop:
                    _dispatcherTimerChronometer.Stop();
                    ChronometerType = ChronometerType.Refresh;
                    break;

                case ChronometerType.Refresh:
                    ChronometerType = ChronometerType.Inicial;
                    Chronometer.Minutes = 45;
                    Chronometer.Seconds = 0;
                    break;
            }
        }

        #endregion Methods

        #region Event handlers

        private async void _geolocator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            if (args.Position.Coordinate != null)
                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher
                                    .RunAsync(CoreDispatcherPriority.Normal, () =>
                                    {
                                        var mapItemResult = MapItems.FirstOrDefault(pre => pre.ItemType == ItemType.Position);

                                        if (mapItemResult == null)
                                        {
                                            MapItems.Add(new MapItem
                                            {
                                                Geopoint = args.Position.Coordinate.Point,
                                                ItemType = ItemType.Position
                                            });
                                        }
                                        else
                                        {
                                            mapItemResult.Geopoint = args.Position.Coordinate.Point;
                                        }
                                    });
        }

        private void _dispatcherTimer_Tick(object sender, object e)
        {
            if (Chronometer.Minutes == 0 && Chronometer.Seconds == 0)
            {
                _vibrationService.Vibrate(5);
                _dispatcherTimerChronometer.Stop();
            }
            else if (Chronometer.Seconds == 0)
            {
                if (Chronometer.Minutes == 5)
                    _vibrationService.Vibrate(3);
                Chronometer.Minutes -= 1;
                Chronometer.Seconds -= 1;
            }
            else
            {
                Chronometer.Seconds -= 1;
            }
        }

        private async void _dispatcherTimerAvailability_Tick(object sender, object e)
        {
            if (_connectionManager.IsConnectionAvailable())
            {
                await _availabilityService.GetAvailabilityAsync();
                if (_availabilityService.AvailabilityStatuses != null)
                    AvailabilityStatus =
                        new List<AvailabilityStatus>(_availabilityService.AvailabilityStatuses);

                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher
                    .RunAsync(CoreDispatcherPriority.Normal, () =>
                    {
                        foreach (var mapItem in MapItems)
                        {
                            var availabilityResult =
                                AvailabilityStatus.FirstOrDefault(pre => pre.id == mapItem.Id);
                            if (availabilityResult?.availability != null)
                            {
                                mapItem.Bikes = availabilityResult.availability.bikes;
                                mapItem.Slots = availabilityResult.availability.slots;
                            }
                        }
                    });
            }
        }

        private async void GetNearMapItemsCompleted(object sender, NearMapItemEventArgs nearMapItemEventArgs)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher
                .RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    foreach (var item in nearMapItemEventArgs.Result)
                    {
                        MapItem selectedItem = MapItems.FirstOrDefault(pre => pre.Id == item.Id);
                        if (selectedItem == null) MapItems.Add(item);
                    }
                    foreach (var mapItem in MapItems)
                    {
                        var availabilityResult = AvailabilityStatus.FirstOrDefault(pre => pre.id == mapItem.Id);
                        if (availabilityResult != null)
                        {
                            mapItem.Bikes = availabilityResult.availability.bikes;
                            mapItem.Slots = availabilityResult.availability.slots;
                        }
                    }
                    IsBusy = false;
                });
        }

        #endregion Event handlers

    }
}