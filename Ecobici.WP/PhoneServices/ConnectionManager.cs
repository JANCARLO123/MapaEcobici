using Windows.Networking.Connectivity;

namespace Ecobici.WP.PhoneServices
{
    public class ConnectionManager : IConnectionManager
    {
        public bool IsConnectionAvailable()
        {
            bool result = false;
            ConnectionProfile internetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();

            if (internetConnectionProfile != null)
            {
                switch (internetConnectionProfile.GetNetworkConnectivityLevel())
                {
                    case NetworkConnectivityLevel.InternetAccess:
                        result = true;
                        break;
                }
            }

            return result;
        }
    }
}
