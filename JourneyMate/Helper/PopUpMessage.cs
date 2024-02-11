using CommunityToolkit.Maui.Views;
using JourneyMate.Helper;
using JourneyMate.Helper.NoInternetConnectionIndicator;
using JourneyMate.Helper.PopupMessages;
using JourneyMate.MVVM.Models;

namespace JourneyMate.Helpers
{
    public static class PopUpMessage
    {

        public static void SuccessMessage(string mgs)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                PopUpMessagesLocalModel popUpMessagesModel = new PopUpMessagesLocalModel();
                popUpMessagesModel.Tile = "Successfull";
                popUpMessagesModel.Mgs = mgs;

                var successMgs = new SuccessMgs(popUpMessagesModel);
                Shell.Current.ShowPopup(successMgs);
            });
        }

        public static void ErrorMessage(string mgs)
        {
            Device.InvokeOnMainThreadAsync(() =>
            {
                PopUpMessagesLocalModel popUpMessagesModel = new PopUpMessagesLocalModel();
                popUpMessagesModel.Tile = "Error";
                popUpMessagesModel.Mgs = mgs;

                var errorMgs = new ErrorMgs(popUpMessagesModel);
                Shell.Current.ShowPopup(errorMgs);
            });
        }

        public static void WarningMessage(string mgs)
        {
            Device.InvokeOnMainThreadAsync(() =>
            {
                PopUpMessagesLocalModel popUpMessagesModel = new PopUpMessagesLocalModel();
                popUpMessagesModel.Tile = "Warning";
                popUpMessagesModel.Mgs = mgs;

                var warningMgs = new WarningMgs(popUpMessagesModel);
                Shell.Current.ShowPopupAsync(warningMgs);
            });
        }


        public static async Task<bool> SureMessage(string? tile, string mgs)
        {
            if (tile == null) tile = "Attention";
            await Device.InvokeOnMainThreadAsync(async () =>
              {
                  PopUpMessagesLocalModel popUpMessagesModel = new PopUpMessagesLocalModel
                  {
                      Tile = tile,
                      Mgs = mgs
                  };

                  var sureMgs = new SureMgs(popUpMessagesModel);
                  await Shell.Current.ShowPopupAsync(sureMgs);
              });

            bool tmpAnswer = GlobalVariable.GetSureMgsVlaue();
            return tmpAnswer;
        }

        public static void NoInternetMessage()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var internet = new NoInternetConnectionIndicatorView();
                Shell.Current.ShowPopup(internet);
            });
        }
    }
}
