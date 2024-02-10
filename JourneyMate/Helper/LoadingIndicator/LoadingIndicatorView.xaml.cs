using CommunityToolkit.Maui.Views;

namespace JourneyMate.Helper.LoadingIndicator
{
    public partial class LoadingIndicatorView : Popup
{
    [Obsolete]
    public LoadingIndicatorView()
    { 
        InitializeComponent();
 
    }

    public void CloseLoadingIndicatorC()
    {
        this.Close();
    }
  }
}