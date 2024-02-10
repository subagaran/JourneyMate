using CommunityToolkit.Mvvm.ComponentModel;
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
    }
}
