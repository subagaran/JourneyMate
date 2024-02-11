using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JourneyMate.Database;
using JourneyMate.Helper.NoInternetConnectionIndicator;
using JourneyMate.Helpers;
using JourneyMate.MVVM.Models;
using JourneyMate.MVVM.Views.BRBO.Home;
using JourneyMate.MVVM.Views.BRBO.Hotel;
using JourneyMate.MVVM.Views.BRBO.Vehicle;
using JourneyMate.MVVM.Views.BRUS.Bookings;
using JourneyMate.MVVM.Views.BRUS.Home;
using JourneyMate.MVVM.Views.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyMate.MVVM.ViewModels.User
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly DatabaseContext _databaseContext;
        static NoInternetConnectionIndicatorView popup;
        public static bool IsLoadingActive = false;

        [ObservableProperty]
        string firstName;

        [ObservableProperty]
        string lastName;

        [ObservableProperty]
        string city;

        [ObservableProperty]
        string email;

        [ObservableProperty]
        string phoneNumber;

        [ObservableProperty]
        string userName;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        string confirmPassword; 

        public LoginViewModel(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        [RelayCommand]
        public async Task Login()
        {
            bool Connection = await CheckInternetConnection();
            if (!Connection)
            {
                PopUpMessage.NoInternetMessage();
                await Task.CompletedTask;
                return;
            }

            IsBusy = true;
      await Shell.Current.GoToAsync($"{nameof(HomePage)}");
                // await Shell.Current.GoToAsync($"{nameof(AddHotelsPage)}");
             //await Shell.Current.GoToAsync($"{nameof(PaymentPage)}");
            IsBusy = false;
        }

        [RelayCommand]
        public async Task SignUp()
        {
            var GetEditProductCartLocalModel = await _databaseContext.GetAllAsync<RegistrationModel>();

            IsBusy = true;
            await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}");
            IsBusy = false;
        }

        [RelayCommand]
        public async Task GotoBack()
        {
            IsBusy = true;
            await Shell.Current.Navigation.PopAsync();
            IsBusy = false;
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
        public async Task Register()
        {
            try
            {
                var registrationModel = new RegistrationModel
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    City = City,
                    PhoneNumber = PhoneNumber,
                    EmailAddress = Email,
                    UserName = UserName,
                    Password = Password,
                    ConfirmPassword = ConfirmPassword
                };
                 
                var registrationModels = new List<RegistrationModel> { registrationModel };                 
                await _databaseContext.AddRangeAsync(registrationModels);
 

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
