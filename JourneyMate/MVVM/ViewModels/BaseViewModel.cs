using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using JourneyMate.Helper.NoInternetConnectionIndicator;
using JourneyMate.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyMate.MVVM.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {

        [ObservableProperty]
        string showMessage;

        [ObservableProperty]
        bool isBusy;

        [ObservableProperty]
        bool isInternet;

        public BaseViewModel()
        {
           
        }

        partial void OnIsBusyChanged(bool value)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (value)
                {
                    LoadingIndicator.StartLoading();
                }
                else
                {
                    LoadingIndicator.CloseLoading();
                }
            });
        }

        public static Task<bool> CheckInternetConnection()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                // Internet connection is available
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);

            }
        }
    }
}
