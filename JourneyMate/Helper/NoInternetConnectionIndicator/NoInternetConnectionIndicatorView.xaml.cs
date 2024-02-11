using CommunityToolkit.Maui.Views;
using JourneyMate.MVVM.Models;

namespace JourneyMate.Helper.NoInternetConnectionIndicator;

public partial class NoInternetConnectionIndicatorView : Popup
{  
    public NoInternetConnectionIndicatorView()
    {
        InitializeComponent(); 
    }

    private  void OnClickedClosebtn(object sender, EventArgs e)
    {
        this.Close();
    }
}