using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JourneyMate.Database;
using JourneyMate.Helper;
using JourneyMate.Helpers;
using JourneyMate.MVVM.Models;
using JourneyMate.MVVM.Views.BRBO.Hotel;
using JourneyMate.MVVM.Views.BRUS.Bookings;
using JourneyMate.MVVM.Views.BRUS.Home; 
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static JourneyMate.MVVM.Models.HotelModel;

namespace JourneyMate.MVVM.ViewModels.BRBO.Hotel
{
    public partial class HotelViewModel : BaseViewModel
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "https://guidtourism.azurewebsites.net/api/Hotel/";
        private const string ApiURL = "https://guidtourism.azurewebsites.net/api/Hotel/GetAllHotels";
        public readonly DatabaseContext _databaseContext;
        public ObservableCollection<HotelModel> Hotels { get; set; }

        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;
        private double _lat = 0.00;
        private double _lng = 0.00;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string email;

        [ObservableProperty]
        double latitude;

        [ObservableProperty]
        double longitude;

        [ObservableProperty]
        string address;

        [ObservableProperty]
        string price;

        [ObservableProperty]
        string city;

        [ObservableProperty]
        string phoneNumber;      
        
        [ObservableProperty]
        string noOfRooms;   
        
        [ObservableProperty]
        string noOfToitents;

        [ObservableProperty]
        string starRating;

        [ObservableProperty]
        string descriptiohn;

        public HotelViewModel(DatabaseContext databaseContext)
        {
            _httpClient = new HttpClient();  
            _databaseContext = databaseContext;
            Hotels = new ObservableCollection<HotelModel>();
            Task.Run(() => GetAllHotelFromApiToLocalAsync()).Wait();
            Task.Run(() => GetAllHotelFromLocal()).Wait();
        }

        public async Task GetAllHotelFromLocal()
        {
            var db = await _databaseContext.GetAllAsync<HotelModel>();
            int i = 1;
            foreach (var item in db)
            {
                Hotels.Add(item);
            
            }
        }

        [RelayCommand]
        public async Task CreateHotel()
        {
            var response =  await CreateHotelAsync();
        }

        public async Task<bool> GetCurrentLocation()
        {
            try
            { 
                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(10));
                _isCheckingLocation = true;
                _cancelTokenSource = new CancellationTokenSource();
                Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);


                if (location != null)
                {
                    _lat = location.Latitude;
                    _lng = location.Longitude;
                    return true;
                }
                return false;
            }

            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                _isCheckingLocation = false;
            }
        }

        public async Task<bool> CreateHotelAsync()
        {
            try
            {
                string imageKeysJson = await SecureStorage.GetAsync("ImageKeys");

                var model = new RestaurantModel
                {
                    UserId = GlobalVariable.GetUserId(),
                    Longitude = _lng,
                    Latitude = _lat,
                    Name = Name,
                    Address = Address,
                    Area = City,
                    CreatedOn = DateTime.Now,
                    IsActive = "Y",
                    Reason = "-",
                    ImageURl = "d.png",
                    Description = "",
                    HeaderName = "-",
                    SubHeaderName = "",
                    Price = Price,
                    HasMoreInfo = "Y",
                    Room1ImgUrl = "",
                    Room2mgUrl = "",
                    Room3mgUrl = "",
                    Room4ImgUrl = "",
                    Room6mgUrl = "",
                    Room5mgUrl = ""                    
                };

                var json = System.Text.Json.JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(ApiBaseUrl + "CreateHotel", content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                } 
                 
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine($"Error creating while creating hotel: {ex.Message}");
                return false; // Hotel creation failed
            }
        }

        [RelayCommand]
        public async Task GotoBack()
        {
            await Shell.Current.Navigation.PopAsync();
        }

        public async Task<bool> DeleteAsync()
        {
            try
            {
                var guideId = GlobalVariable.GetGuideId();

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://guidtourism.azurewebsites.net/api/Hotel/");


                    client.DefaultRequestHeaders.Add("Id", guideId.ToString());

                    HttpResponseMessage response = await client.PostAsync("DeleteHotel", null);

                    if (response.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }                 
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [RelayCommand]
        public async Task GotoEditPage(HotelModel hotelModel)
        {
            IsBusy = true;
            GlobalVariable.SetGuideId(hotelModel.Id);
            await Shell.Current.GoToAsync($"{nameof(EditHotel)}");
            IsBusy = false;
        }

        [RelayCommand]
        public async Task Delete(HotelModel model)
        {
            GlobalVariable.SetGuideId(model.Id);

            bool answer = await PopUpMessage.SureMessage("Confirmation", "Do you want to Delete?");
            if (!answer)
            {
                return;
            }
            await DeleteAsync();
        }


        [RelayCommand]
        public async Task GoToBooking(HotelModel model)
        {
            GlobalVariable.SetGuideId(model.Id);
            IsBusy = true; 
            await Shell.Current.GoToAsync($"{nameof(HotelBookingPage)}");
            IsBusy = false;

        }

    }
}
