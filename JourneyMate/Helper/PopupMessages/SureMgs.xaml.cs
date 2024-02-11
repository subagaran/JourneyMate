using CommunityToolkit.Maui.Views;
using JourneyMate.Helper;
using JourneyMate.MVVM.Models;

namespace JourneyMate.Helper.PopupMessages;

public partial class SureMgs : Popup
{
    public SureMgs(PopUpMessagesLocalModel popUpMessagesModel)
    {
        InitializeComponent();
        this.BindingContext = popUpMessagesModel;
    }

    private void OnClickedClosebtn(object sender, EventArgs e)
    {
        Answer(false);

    }

    private void OnClickedYesBtn(object sender, EventArgs e)
    {
        Answer(true);
    }

    public void Answer(bool Answer)
    {
        if (Answer)
        {
            GlobalVariable.SetSureMgsVlaue(true);
        }
        else
        {
            GlobalVariable.SetSureMgsVlaue(false);
        }
        this.Close();
    }
}