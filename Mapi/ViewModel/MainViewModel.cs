using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Mapi.Models;
using Mapi.Models.CustomLocationInfo;
using Mapi.Pages;
using Mapi.Services.DeviceLocation;
using Mapi.Services.GoogleLocation;

namespace Mapi.ViewModel
{
	public class MainViewModel : ObservableObject
    {
        private readonly IGoogleLocationService _locationService;
        private readonly IDeviceGeolocation _geolocation;
        
        private Location _location;

        public MainViewModel(IGoogleLocationService locationService, IDeviceGeolocation geolocation)
		{
            _locationService = locationService;
            _geolocation = geolocation;
        }

      
        public ICommand StartSearchboxTextChangedCommand => new Command<string>(async (text) =>
        {
            await Search(text, AddressType.StartAddress);
        });

        public ICommand DestinationSearchboxTextChangedCommand => new Command<string>(async (text) =>
        {
            await Search(text, AddressType.DestinationAddress);
        });

        public ICommand InitializeValuesCommand => new Command( () =>
        {
            InitializeValues();
        });

        public ICommand SelectPlaceCommand => new Command(async (selectedItemObj) =>
        {
            CustomLocationInfo tmp = selectedItemObj as CustomLocationInfo;
            if (tmp.AddressType == AddressType.StartAddress)
                StartAddress = tmp;
            else
                DestinationAddress = tmp;

            SearchResultList.Clear();
            if (StartAddress != null && DestinationAddress != null)
                await ShowRoute();
        });

        private CustomLocationInfo _startAddress;

        public CustomLocationInfo StartAddress
        {
            get => _startAddress;
            set { SetProperty(ref _startAddress, value); }
        }

        private CustomLocationInfo _destinationAddress;

        public CustomLocationInfo DestinationAddress
        {
            get => _destinationAddress;
            set { SetProperty(ref _destinationAddress, value); }
        }        

        private ObservableCollection<CustomLocationInfo> _searchResultList;
        public ObservableCollection<CustomLocationInfo> SearchResultList
        {
            get => _searchResultList;
            set { SetProperty(ref _searchResultList, value);  }
        }

        public async void InitializeValues()
        {
            var tcs = new CancellationTokenSource();
            _location = await _geolocation.GetLocationAsync(tcs);
        }


         private async Task ShowRoute()
        {
            // var tuple = Tuple.Create<CustomLocationInfo, CustomLocationInfo>(StartAddress, DestinationAddress);

            var locations = new Dictionary<string, object>();
            locations.Add(nameof(StartAddress), StartAddress);
            locations.Add(nameof(DestinationAddress), DestinationAddress);
            await Shell.Current.GoToAsync(nameof(SelectedRoutePage), locations);
        }

        public async Task Search(string text, AddressType addressType)
        {
            var tcs = new CancellationTokenSource();
          var result = await _locationService.GetPlaces(text, _location);
           SearchResultList = new ObservableCollection<CustomLocationInfo>();
            
            SearchResultList.Add(GetCurrentLocationInfo(addressType)); // add current address

            foreach (var place in result.results)
            {
                var item = new CustomLocationInfo()
                {
                    Address = place.formatted_address,
                    Longitude = place.geometry.location.lng,
                    Latitude = place.geometry.location.lat,
                    AddressType = addressType
                };
                SearchResultList.Add(item);
            }
        }

        private CustomLocationInfo GetCurrentLocationInfo(AddressType addressType)
        {            
            return new CustomLocationInfo()
            {
                Address = "Your current location",
                Longitude = _location.Longitude,
                Latitude = _location.Latitude,
                AddressType = addressType
            };
        }
    }
}