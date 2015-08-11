using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Ecobici.WP.Common;
using Ecobici.WP.Enumerators;
using Ecobici.WP.Model.Entities;
using Ecobici.WP.Model.EventArgs;
using Ecobici.WP.Services.Stations;

namespace Ecobici.WP.Model
{
    public class MapItemsModel
    {
        #region Event args

        public EventHandler<MapItemEventArgs> GetMapItemsCompleted;

        public EventHandler<NearMapItemEventArgs> GetNearMapItemsCompleted;

        #endregion Event args

        #region fields

        private readonly ICicloStationService _cicloStationService;

        private IEnumerable<CicloStation> _currentElements;

        #endregion fields

        #region Constructor

        public MapItemsModel(ICicloStationService cicloStationService)
        {
            _cicloStationService = cicloStationService;
        }

        #endregion Constructor

        #region Methods

        public async Task GetAllMapItems()
        {
            if (_currentElements == null)
            {
                _currentElements = await _cicloStationService.GetCicloStationsAsync();
            }

            var result = _currentElements.Select(pre => new MapItem()
            {
                Id = pre.id,
                Geopoint = new Geopoint(new BasicGeoposition()
                {
                    Latitude = pre.location.Lat,
                    Longitude = pre.location.Lon
                }),
                ItemType = ItemType.EcobiciStation,
                Name = pre.name
            });

            if (GetMapItemsCompleted != null) GetMapItemsCompleted(this, new MapItemEventArgs(result));
        }

        public async Task GetNearMapItems(Geopoint currentGeopoint)
        {
            if (_currentElements == null)
            {
                _currentElements = await _cicloStationService.GetCicloStationsAsync();
            }

            var result = _currentElements.Select(pre => new MapItem()
            {
                Id = pre.id,
                Geopoint = new Geopoint(new BasicGeoposition()
                {
                    Latitude = pre.location.Lat,
                    Longitude = pre.location.Lon
                }),
                ItemType = ItemType.EcobiciStation,
                Name = pre.name
            });

            result = result
                .OrderBy(pre => pre.Geopoint.CalculateDistance(currentGeopoint))
                .Where(pre => pre.Geopoint.CalculateDistance(currentGeopoint) <= 1)
                .Take(5);

            GetNearMapItemsCompleted?.Invoke(this, new NearMapItemEventArgs(result));
        }

        #endregion Methods
    }
}