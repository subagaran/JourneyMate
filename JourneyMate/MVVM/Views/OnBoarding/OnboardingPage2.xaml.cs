using JourneyMate.MVVM.ViewModels.OnBoardingPage;
using JourneyMate.MVVM.ViewModels.User;
using JourneyMate.MVVM.Views.BRUS.Home;
using JourneyMate.MVVM.Views.User;

namespace JourneyMate.MVVM.Views.OnBoarding;

public partial class OnboardingPage2 : ContentPage
{
    private readonly OnBoardingPageViewModel _loginViewModel;
	public OnboardingPage2(OnBoardingPageViewModel loginViewModel)
	{
		InitializeComponent();

        _loginViewModel = loginViewModel;
        BindingContext = _loginViewModel;
	}

    private void OnNextPageClicked(object sender, EventArgs e)
    { 
        
    }
}