using SharpnadoTabs.ViewModels;

namespace SharpnadoTabs;

public partial class MainPage : ContentPage
{
	MainPageViewModel viewModel;
	public MainPage(MainPageViewModel vm)
	{
		viewModel = vm;
		InitializeComponent();
		BindingContext = viewModel;
		NavigationPage.SetHasNavigationBar(this, false);
	}
}


