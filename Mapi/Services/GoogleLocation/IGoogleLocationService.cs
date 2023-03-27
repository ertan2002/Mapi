using System;
using Mapi.Models.Google;

namespace Mapi.Services.GoogleLocation
{
	public interface IGoogleLocationService
	{
        public Task<PlaceApiResult> GetPlaces(string searchTerm, Microsoft.Maui.Devices.Sensors.Location location);
    }
}