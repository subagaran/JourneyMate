using JourneyMate.Helper;
using JourneyMate.MVVM.ViewModels.BRBO.Guide;
using Newtonsoft.Json;

namespace JourneyMate.MVVM.Views.BRBO.Guide;

public partial class ViewUpdateAndDeleteGuidePage : ContentPage
{
    private readonly GuideViewModel _guideViewModel;
	public ViewUpdateAndDeleteGuidePage(GuideViewModel guideViewModel)
	{
		InitializeComponent();
        _guideViewModel = guideViewModel;
        BindingContext = _guideViewModel;
	}

    private void Label_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
       
    }

    public List<ImageSource> LoadImageSources()
    {
        string json = GlobalVariable.GetHotelImage();
        var A = JsonConvert.DeserializeObject<List<ImageSource>>(json);
        return A;

    }
}