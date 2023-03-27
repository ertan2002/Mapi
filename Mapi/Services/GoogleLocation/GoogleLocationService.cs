using System;
using Mapi.Models.Exceptions;
using Mapi.Models.Google;
using Mapi.Models.Settings;
using Newtonsoft.Json;

namespace Mapi.Services.GoogleLocation
{
    public class GoogleLocationService : IGoogleLocationService
    {
        private readonly ISettings _settings;
        HttpClient _httpClient;
        public GoogleLocationService(ISettings settings)
        {
            _httpClient = new HttpClient() { BaseAddress = new Uri(settings.GoogleApiUrl) };
            this._settings = settings;
        }


        public async Task<PlaceApiResult> GetPlaces(string searchTerm, Microsoft.Maui.Devices.Sensors.Location location)
        {
                var response = await _httpClient.GetAsync(GenerateGetPlaceUrl(searchTerm, location)).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<PlaceApiResult>(json);
                }
                else
                    throw new GooglePlaceApiException("Invalid query. Status code: " + response.StatusCode);
          
        }


        private string GenerateGetPlaceUrl(string searchTerm, Microsoft.Maui.Devices.Sensors.Location location) => $"api/place/textsearch/json?query={Uri.EscapeDataString(searchTerm)}&location={location.Latitude},{location.Longitude}&rankby=distance&key={_settings.GoogleApiKey}";
    }

}