using Mapi.Models.CustomLocationInfo;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using static Microsoft.Maui.ApplicationModel.Permissions;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace Mapi.Pages;

[QueryProperty(nameof(StartAddress), nameof(StartAddress))]
[QueryProperty(nameof(DestinationAddress), nameof(DestinationAddress))]
public partial class SelectedRoutePage : ContentPage
{
    public SelectedRoutePage()
    {
        InitializeComponent();
    }

    public CustomLocationInfo StartAddress { get; set; }
    public CustomLocationInfo DestinationAddress { get; set; }

    protected override void OnAppearing()
    {

        map.MapElements.Clear();
        
        Polyline polyline = new Polyline
        {
            StrokeColor = Colors.Blue,
            StrokeWidth = 12,
            Geopath =
    {
             new Location(StartAddress.Latitude,StartAddress.Longitude),
                new Location(DestinationAddress.Latitude,DestinationAddress.Longitude)
            }
        };



        map.MapElements.Add(polyline);
        map.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(polyline.Geopath[0].Latitude, polyline.Geopath[0].Longitude), Distance.FromKilometers(1)));

        var pin1 = new Pin
        {
            Type = PinType.Place,
            Location = new Location(polyline.Geopath.First().Latitude, polyline.Geopath.First().Longitude),
            Label = StartAddress.Address,
            Address = StartAddress.Address
        };
        map.Pins.Add(pin1);

        var pin2 = new Pin
        {
            Type = PinType.Place,
            Location = new Location(polyline.Geopath.Last().Latitude, polyline.Geopath.Last().Longitude),
            Label = DestinationAddress.Address,
            Address = DestinationAddress.Address,

        };
        map.Pins.Add(pin2);
        base.OnAppearing();

    }
}
