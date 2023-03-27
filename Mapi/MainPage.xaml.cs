using Mapi.ViewModel;

namespace Mapi;

public partial class MainPage : ContentPage
{
    MainViewModel _viewModel;

    public MainPage(MainViewModel mainVm)
	{
        InitializeComponent();
        BindingContext = _viewModel = mainVm;
    }
}


