using JourneyMate.MVVM.ViewModels.BRBO.Vehicle;

namespace JourneyMate.MVVM.Views.BRBO.Hotel;

public partial class EditHotel : ContentPage
{
	private readonly EditVehicleViewModel _viewModel;
	public EditHotel(EditVehicleViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}

    private void OnCounterClicked(object sender, EventArgs e)
    {

    }
}