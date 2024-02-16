using JourneyMate.MVVM.ViewModels.BRBO.Payment;

namespace JourneyMate.MVVM.Views.BRUS.Bookings;

public partial class ViewPayments : ContentPage
{
	private readonly AllPaymentViewModel _allPaymentViewModel;
    public ViewPayments(AllPaymentViewModel allPaymentViewModel)
	{
		InitializeComponent();
		_allPaymentViewModel = allPaymentViewModel;
		BindingContext = _allPaymentViewModel;
	}
}