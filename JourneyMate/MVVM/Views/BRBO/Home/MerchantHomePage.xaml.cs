namespace JourneyMate.MVVM.Views.BRBO.Home;

public partial class MerchantHomePage : ContentPage
{
	public MerchantHomePage()
	{
		InitializeComponent();
	}


    protected override bool OnBackButtonPressed()
    {
        Shell.Current.Navigation.PopAsync();
        return true;
    }

}