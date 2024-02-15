using JourneyMate.MVVM.ViewModels.BRBO.Payment;
using JourneyMate.MVVM.ViewModels.BRUS.Booking;

namespace JourneyMate.MVVM.Views.BRBO.Booking;

public partial class ViewAllPayments : ContentPage
{
    private readonly AllPaymentViewModel _paymentViewModel;

    public ViewAllPayments(AllPaymentViewModel paymentViewModel)
	{
		InitializeComponent();
        _paymentViewModel = paymentViewModel;
        BindingContext = _paymentViewModel;
    }
}