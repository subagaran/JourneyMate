using JourneyMate.MVVM.ViewModels.BRBO.Vehicle;

namespace JourneyMate.MVVM.Views.BRBO.Vehicle;

public partial class ViewVehicles : ContentPage
{
	private readonly VehicleViewModel _vehicleViewModel;
	public ViewVehicles(VehicleViewModel vehicleViewModel)
	{
		InitializeComponent();
		_vehicleViewModel = vehicleViewModel;
		BindingContext = _vehicleViewModel;
	}

    private void Label_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {

    }
}