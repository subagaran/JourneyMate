using JourneyMate.MVVM.ViewModels;

namespace JourneyMate.MVVM.Views.BRUS.Home;

public partial class StartingPage : ContentPage
{
	private readonly StartingVIewModel _startingVIewModel;
	public StartingPage(StartingVIewModel startingVIewModel)
	{
		InitializeComponent();
		_startingVIewModel = startingVIewModel;
		BindingContext = _startingVIewModel;
	}
}