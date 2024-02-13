using JourneyMate.MVVM.ViewModels.OnBoardingPage;

namespace JourneyMate.MVVM.Views.OnBoarding;

public partial class OnBoardingPage : ContentPage
{
    private OnBoardingPageViewModel viewModel;
    public OnBoardingPage()
	{
		InitializeComponent();
        viewModel = new OnBoardingPageViewModel();
         
        BindingContext = viewModel;
        viewModel.InitializeOnBoardings(); 
    }
     
}