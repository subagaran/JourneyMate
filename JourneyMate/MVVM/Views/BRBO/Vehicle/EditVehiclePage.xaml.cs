using JourneyMate.MVVM.ViewModels.BRBO.Vehicle;

namespace JourneyMate.MVVM.Views.BRBO.Vehicle;

public partial class EditVehiclePage : ContentPage
{
	private readonly EditVehicleViewModel _editVehicleViewModel;
    public EditVehiclePage(EditVehicleViewModel editVehicleViewModel)
	{
		InitializeComponent();
		_editVehicleViewModel = editVehicleViewModel;
		BindingContext = _editVehicleViewModel;

	}

    private void OnCounterClicked(object sender, EventArgs e)
    {

    }
}