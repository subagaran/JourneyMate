using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JourneyMate.Database;
using JourneyMate.Helper;
using JourneyMate.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyMate.MVVM.ViewModels.BRBO.Hotel
{
    public partial class EditHotelViewModel : BaseViewModel
    {
        private readonly HttpClient _httpClient;
        private readonly DatabaseContext _databaseContext;
        private const string ApiBaseUrl = "https://guidtourism.azurewebsites.net/api/Hotel/";
        private int _Id;
        private double _lat;
        private double _lng;

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

        public EditHotelViewModel()
        {
            _httpClient = new HttpClient();
            _databaseContext = new DatabaseContext();
            _Id = GlobalVariable.GetGuideId();
            Task.Run(() => PageLoad()).Wait();
        }
        public async Task PageLoad()
        {
            var list = await _databaseContext.GetAllAsync<HotelModel>();

            var List = list.Where(a => a.Id == _Id);
            Longitude = List.FirstOrDefault().Longitude;
            Latitude = List.FirstOrDefault().Latitude; 
                    Name = List.FirstOrDefault().Name;
            Address = List.FirstOrDefault().Address; 
                    City = List.FirstOrDefault().City;  
                    Price = List.FirstOrDefault().Price;  
        }

        [RelayCommand]
        public async Task Update()
        {
            var response = await UpdateAsync();
        }

        public async Task<bool> UpdateAsync()
        {
            try
            {
                string imageKeysJson = await SecureStorage.GetAsync("ImageKeys");
                var userid = GlobalVariable.GetUserId();
                var GuideId = GlobalVariable.GetGuideId();
                var model = new RestaurantModel
                {
                    UserId = Convert.ToInt32(GlobalVariable.GetUserId()),
                    Longitude = _lng,
                    Latitude = _lat,
                    Name = Name,
                    Address = Address,
                    Area = City,
                    CreatedOn = DateTime.Now,
                    IsActive = "Y",
                    Reason = "-",
                    ImageURl = "",
                    Description = "",
                    HeaderName = "-",
                    SubHeaderName = "",
                    Price = Price,
                    HasMoreInfo = "Y",
                    Room1ImgUrl = imageKeysJson
                };

                var json = System.Text.Json.JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(ApiBaseUrl + "UpdateHotel", content);

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
    }
}
