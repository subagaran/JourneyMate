﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JourneyMate.Helper;
using JourneyMate.Helpers;
using JourneyMate.MVVM.Views.BRBO.Guide;
using JourneyMate.MVVM.Views.BRBO.Home;
using JourneyMate.MVVM.Views.BRBO.Hotel;
using JourneyMate.MVVM.Views.BRBO.Vehicle;
using JourneyMate.MVVM.Views.BRUS.Bookings;
using JourneyMate.MVVM.Views.User;
using JourneyMate.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyMate.MVVM.ViewModels.BRBO.Home
{
    public partial class MerchantHomeViewModel : BaseViewModel
    {
        [ObservableProperty]
        string userName;

        public MerchantHomeViewModel()
        {
            UserName = GlobalVariable.GetUserName();
        }

        [RelayCommand]
        public async Task GotoHotelPage()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync($"{nameof(AddHotelsPage)}");
            IsBusy = false;
        }

        [RelayCommand]
        public async Task GotoPlacesPage()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync($"{nameof(PaymentPage)}");
            IsBusy = false;
        }

        [RelayCommand]
        public async Task GotoBookForFriendPage()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync($"{nameof(MerchantHomePage)}");
            IsBusy = false;
        }

        [RelayCommand]
        public async Task GotoVehicleBookingPage()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync($"{nameof(VehiclePage)}");
            IsBusy = false;
        }

        [RelayCommand]
        public async Task GotoGuidPage()
        {
            IsBusy = true;
            await Shell.Current.GoToAsync($"{nameof(ViewUpdateAndDeleteGuidePage)}");
            IsBusy = false;
        }


        [RelayCommand]
        public async Task Logout()
        {
            await UserLogOut();
        }

        public static async Task UserLogOut()
        {
            bool answer = await PopUpMessage.SureMessage(null, "Are you sure you want to exit?");
            if (answer)
            {
                try
                {
                    GlobalVariable.SetUserLogedIn(false);
                    SecureStorage.Default.Remove(SD.UserName);
                    SecureStorage.Default.Remove(SD.UserRole);
                    SecureStorage.Default.Remove(SD.UserLogedIn);
                    await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
                }
                catch (Exception)
                {
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }


            }
            else
            {
                return;
            }
        }
    }
}
