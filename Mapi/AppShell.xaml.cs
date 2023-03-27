using Mapi.Pages;

namespace Mapi;

public partial class AppShell : Shell
{
    private readonly Dictionary<string, Type> routes = new Dictionary<string, Type>();

    public AppShell()
	{
		InitializeComponent();
        RegisterRoutes();
    }

    private void RegisterRoutes()
    {
        routes.Add(nameof(SelectedRoutePage), typeof(SelectedRoutePage));
        
        // Register routes
        foreach (var route in routes)
        {
            Routing.RegisterRoute(route.Key, route.Value);
        }
    }
}