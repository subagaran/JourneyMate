using CommunityToolkit.Maui.Views;
using JourneyMate.MVVM.Models; 

namespace JourneyMate.Helper.PopupMessages;

public partial class ErrorMgs : Popup
{
    public ErrorMgs(PopUpMessagesLocalModel popUpMessagesModel)
    {
        InitializeComponent();
        this.BindingContext = popUpMessagesModel;
    }

    private void OnClickedClosebtn(object sender, EventArgs e)
    {
        this.Close();
    }

}