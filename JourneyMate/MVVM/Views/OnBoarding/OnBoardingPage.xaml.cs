using JourneyMate.MVVM.ViewModels.OnBoardingPage;

namespace JourneyMate.MVVM.Views.OnBoarding;

public partial class OnBoardingPage : ContentPage
{
    private OnBoardingPageViewModel viewModel;
    public OnBoardingPage()
	{
		InitializeComponent();
        viewModel = new OnBoardingPageViewModel();

        // Call the InitializeOnBoardings method
        viewModel.InitializeOnBoardings();

        // Set the BindingContext to the view model
        BindingContext = viewModel;
    }

    private void OnNextPageClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new OnboardingPage2());
    }
}