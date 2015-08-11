using Autofac;
using Ecobici.WP.Common;
using Ecobici.WP.Model;
using Ecobici.WP.PhoneServices;
using Ecobici.WP.Services.Authentication;
using Ecobici.WP.Services.Availability;
using Ecobici.WP.Services.HttpCommunicator;
using Ecobici.WP.Services.Localization;
using Ecobici.WP.Services.PhoneCall;
using Ecobici.WP.Services.Stations;
using Ecobici.WP.Services.TokenManager;

namespace Ecobici.WP.ViewModel.ViewModelBase
{
    public class VMLocator
    {
        private readonly IContainer _container;

        public VMLocator()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterType<VMMainPage>();
            builder.RegisterType<LocalizationService>().As<ILocalizationService>();
            builder.RegisterType<EcobiciAuthenticator>().As<IAuthenticator>();
            builder.RegisterType<HttpCommunicator>().As<IHttpCommunicator>();
            builder.RegisterType<TokenManager>().As<ITokenManager>();
            builder.RegisterType<CicloStationService>().As<ICicloStationService>();
            builder.RegisterType<PhoneCallService>().As<IPhoneCallService>();
            builder.RegisterType<MapItemsModel>();
            builder.RegisterType<VibrationService>().As<IVibrationService>();
            builder.RegisterType<CicloStationsManager>().As<ICicloStationsManager>().SingleInstance();
            builder.RegisterType<AvailabilityService>().As<IAvailabilityService>().SingleInstance();
            builder.RegisterType<ConnectionManager>().As<IConnectionManager>();


            _container = builder.Build();
        }

        #region Properties

        public VMMainPage VmMainPage => _container.Resolve<VMMainPage>();

        public IContainer Container => _container;

        #endregion Properties
    }
}