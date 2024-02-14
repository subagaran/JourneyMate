using JourneyMate.MVVM.ViewModels.BRBO.Home;

namespace JourneyMate.MVVM.Views.BRBO.Home;

public partial class MerchantHomePage : ContentPage
{
    private readonly MerchantHomeViewModel _merchantHomeViewModel;

    public MerchantHomePage(MerchantHomeViewModel merchantHomeViewModel)
    {
        InitializeComponent();
        _merchantHomeViewModel = merchantHomeViewModel;
        BindingContext = _merchantHomeViewModel;
    }

    protected override bool OnBackButtonPressed()
    {
        Shell.Current.Navigation.PopAsync();
        return true;
    }



}