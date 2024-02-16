using JourneyMate.MVVM.ViewModels.BRBO.Hotel;
using JourneyMate.MVVM.ViewModels.BRBO.Vehicle;

namespace JourneyMate.MVVM.Views.BRBO.Hotel;

public partial class EditHotel : ContentPage
{
	private readonly EditHotelViewModel _viewModel;
	public EditHotel(EditHotelViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}

    private void OnCounterClicked(object sender, EventArgs e)
    {

    }
}