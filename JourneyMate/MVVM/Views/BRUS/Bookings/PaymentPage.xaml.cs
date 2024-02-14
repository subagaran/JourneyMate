using JourneyMate.MVVM.ViewModels.BRUS.Booking;

namespace JourneyMate.MVVM.Views.BRUS.Bookings;

public partial class PaymentPage : ContentPage
{
    private readonly PaymentViewModel _paymentViewModel;
    public PaymentPage(PaymentViewModel paymentViewModel)
	{
		InitializeComponent();
        _paymentViewModel = paymentViewModel;
        BindingContext = _paymentViewModel;
    }
     

    private void OnMonthPickerSelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void OnYearPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
}