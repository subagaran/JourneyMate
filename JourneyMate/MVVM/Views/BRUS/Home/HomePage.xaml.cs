using JourneyMate.MVVM.ViewModels.User;

namespace JourneyMate.MVVM.Views.BRUS.Home;

public partial class HomePage : ContentPage
{
    private readonly LoginViewModel _loginViewModel;

    public HomePage(LoginViewModel loginViewModel)
	{
		InitializeComponent();
        _loginViewModel = loginViewModel;
        BindingContext = _loginViewModel;
    }
}