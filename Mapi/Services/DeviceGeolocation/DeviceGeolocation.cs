using System;
using Microsoft.Maui.Devices.Sensors;
namespace Mapi.Services.DeviceLocation
{
	public class DeviceGeolocation : IDeviceGeolocation
    {
        public async Task<Location> GetLocationAsync(CancellationTokenSource cts, GeolocationAccuracy accuracy = GeolocationAccuracy.Default)
        {
            var request = new GeolocationRequest(accuracy, TimeSpan.FromSeconds(10));

            return await Geolocation.GetLocationAsync(request, cts.Token);
        }
    }
}