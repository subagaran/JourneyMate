using JourneyMate.MVVM.Views.BRUS.Home;
using JourneyMate.MVVM.Views.User;

namespace JourneyMate.MVVM.Views.OnBoarding;

public partial class OnboardingPage2 : ContentPage
{
	public OnboardingPage2()
	{
		InitializeComponent();
	}

    private void OnNextPageClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"{nameof(LoginPage)}");
    }
}