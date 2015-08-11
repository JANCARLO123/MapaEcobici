using System;
using Windows.Phone.Devices.Notification;

namespace Ecobici.WP.PhoneServices
{
    public class VibrationService : IVibrationService
    {
        public void Vibrate(int seconds)
        {
            VibrationDevice testVibrationDevice = VibrationDevice.GetDefault();
            testVibrationDevice.Vibrate(TimeSpan.FromSeconds(seconds));
        }
    }
}