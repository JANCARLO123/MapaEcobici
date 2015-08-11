namespace Ecobici.WP.Services.PhoneCall
{
    public class PhoneCallService : IPhoneCallService
    {
        public void Call()
        {
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            var phoneNumber = loader.GetString("EmergencyPhone");
            Windows.ApplicationModel.Calls.PhoneCallManager.ShowPhoneCallUI("5005-2424", "Emergencia");
        }
    }
}