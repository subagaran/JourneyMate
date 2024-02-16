using JourneyMate.MVVM.ViewModels.BRUS.Booking;

namespace JourneyMate.MVVM.Views.BRUS.Bookings;

public partial class HotelBookingPage : ContentPage
{
	private BookingViewModel _bookingViewModel;
	public HotelBookingPage(BookingViewModel bookingViewModel)
	{
		InitializeComponent();
		_bookingViewModel = bookingViewModel;
		BindingContext = _bookingViewModel;
	}

    private void OnConfirmBookingClicked(object sender, EventArgs e)
    {

    }
}