using JourneyMate.MVVM.ViewModels.User;

namespace JourneyMate.MVVM.Views.User;

public partial class LoginPage : ContentPage
{
	private readonly LoginViewModel _loginViewModel;
	public LoginPage(LoginViewModel loginViewModel)
	{
		InitializeComponent();
		_loginViewModel = loginViewModel;
		BindingContext = _loginViewModel;
	}
}