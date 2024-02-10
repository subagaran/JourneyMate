using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JourneyMate.MVVM.Views.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyMate.MVVM.ViewModels.User
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        string userName;

        [ObservableProperty]
        string password;

        public LoginViewModel()
        {
                
        }

        [RelayCommand]
        public async Task Login()
        {
            IsBusy = true;
           await Shell.Current.GoToAsync($"{nameof(HomePage)}");
            IsBusy = false;
        }
    }
}
