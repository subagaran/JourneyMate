using JourneyMate.MVVM.ViewModels.BRBO.Guide;
using JourneyMate.MVVM.ViewModels.BRBO.Vehicle;
using Newtonsoft.Json;

namespace JourneyMate.MVVM.Views.BRBO.Guide;

public partial class EditGuidePage : ContentPage
{
    private readonly EditGuideViewModel _guideViewModel;
	public EditGuidePage(EditGuideViewModel guideViewModel)
	{
		InitializeComponent();
        _guideViewModel = guideViewModel;
        BindingContext = _guideViewModel;
	}

   
}