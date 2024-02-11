using JourneyMate.MVVM.ViewModels.User;

namespace JourneyMate.MVVM.Views.User;

public partial class RegistrationPage : ContentPage
{
    private readonly LoginViewModel _loginViewModel;

    public RegistrationPage(LoginViewModel loginViewModel)
	{
		InitializeComponent();
		_loginViewModel = loginViewModel;
		this.BindingContext = _loginViewModel;
	}
}