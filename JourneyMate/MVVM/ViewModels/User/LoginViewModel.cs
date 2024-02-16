using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JourneyMate.MVVM.Views.BRUS.Home;
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
using System.Text.Json;
using JourneyMate.Helper;
using System.Data;

namespace JourneyMate.MVVM.ViewModels.User
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly DatabaseContext _databaseContext;
        static NoInternetConnectionIndicatorView popup;
        public static bool IsLoadingActive = false;

        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "https://guidtourism.azurewebsites.net/api/Users/";

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
            _httpClient = new HttpClient();
        }

        [RelayCommand]
        public async Task Login()
        {
            try
            {
                IsBusy = true;
                bool Connection = await CheckInternetConnection();
                if (!Connection)
                {
                    PopUpMessage.NoInternetMessage();
                    await Task.CompletedTask;
                    IsBusy = false;
                    return;
                }

                var model = new LoginModel
                {
                    Username = UserName,
                    Password = Password
                };

                if (string.IsNullOrEmpty(UserName)|| string.IsNullOrEmpty(Password))
                {
                    PopUpMessage.WarningMessage("Username or password cannot be empty");
                    IsBusy = false;
                    return;
                }

                var json = JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(ApiBaseUrl + "login", content); 
                 
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var responseObject = JsonSerializer.Deserialize<LoginResponseDto>(responseBody);                     
                    if (responseObject != null && responseObject.isSuccess)
                    {
                        // Extract user information from the response
                        var user = responseObject.result.user;
                        
                        string username = user.userName;
                        string roleName = responseObject.result.role;
                        string token = responseObject.result.token;
                        string userId = responseObject.result.user.id;
                        GlobalVariable.SetUserName(username);
                        GlobalVariable.SetUserRole(roleName);  
                        GlobalVariable.SetUserId(userId);  
                    }

                    GlobalVariable.SetUserLogedIn(true); 
                    DateTime loginDate = DateTime.Now;
                    await SecureStorage.SetAsync("LoginDate", loginDate.ToString());
                   
                    var AppRole = GlobalVariable.GetUserRole();
                   
                    MenuBuilder.BuildMenu();

                }
                else
                {
                    PopUpMessage.WarningMessage("Username or password is incorrect");
                    IsBusy = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                PopUpMessage.WarningMessage("Somethimg went wrong.");
                throw;
            }
            finally
            {
                IsBusy = false;

            }

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
        public async Task Register()
        {
            try
            {
                var model = new RegistrationModel
                {
                    UserName = "Demo",
                    Password = "Demo@123",
                    Role = "BZUS",
                    Name = "Demo"
                };

                var json = JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(ApiBaseUrl + "register", content);

                if (response.IsSuccessStatusCode)
                {
                    
                }
                else
                {
                   
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
