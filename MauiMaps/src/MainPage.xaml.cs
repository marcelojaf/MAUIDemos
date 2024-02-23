using Microsoft.Maui.Maps;

namespace MauiMaps;

public partial class MainPage : ContentPage
{
	public MainPage(MainPageViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
		var villagePin = new CustomPin()
		{
            Label = "Cupertino Village",
            Location = new Location(37.33613966724056, -122.01534299825563),
            Address = "10869 N Wolfe Rd, Cupertino, CA 95014, United States",
            ImageSource = "pin_blue.png"
        };

        MyMap.Pins.Add(villagePin);

        Location appleLocation = new Location(37.334859987318374, -122.00972110805554);
        MapSpan mapSpan = new MapSpan(appleLocation, 0.01, 0.01);
        MyMap.MoveToRegion(mapSpan);

        base.OnAppearing();
    }
}


