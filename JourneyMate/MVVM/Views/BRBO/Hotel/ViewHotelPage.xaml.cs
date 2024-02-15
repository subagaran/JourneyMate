using JourneyMate.MVVM.ViewModels.BRBO.Hotel;

namespace JourneyMate.MVVM.Views.BRBO.Hotel;

public partial class ViewHotelPage : ContentPage
{
    private readonly HotelViewModel _hotelViewModel;
    public ViewHotelPage(HotelViewModel hotelViewModel)
	{
		InitializeComponent();
        _hotelViewModel = hotelViewModel;
        BindingContext = _hotelViewModel;
    }
}