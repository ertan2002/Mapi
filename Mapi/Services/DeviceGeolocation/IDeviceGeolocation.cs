using System;
namespace Mapi.Services.DeviceLocation
{
	public interface IDeviceGeolocation
    {
        public Task<Location> GetLocationAsync(CancellationTokenSource cts, GeolocationAccuracy accuracy = GeolocationAccuracy.Default);
    }
}
