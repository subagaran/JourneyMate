using JourneyMate.MVVM.ViewModels.BRUS.Home;
using JourneyMate.MVVM.ViewModels.User;

namespace JourneyMate.MVVM.Views.BRUS.Home;

public partial class HomePage : ContentPage
{
    private readonly MainHomeViewModel _mainHomeViewModel;

    public HomePage(MainHomeViewModel mainHomeViewModel)
	{
		InitializeComponent();
        _mainHomeViewModel = mainHomeViewModel;
        BindingContext = _mainHomeViewModel;
    }
}