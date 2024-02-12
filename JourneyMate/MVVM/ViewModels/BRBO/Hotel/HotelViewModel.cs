using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JourneyMate.Database;
using JourneyMate.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JourneyMate.MVVM.ViewModels.BRBO.Hotel
{
    public partial class HotelViewModel : BaseViewModel
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "https://guidtourism.azurewebsites.net/api/Hotel/";
        public readonly DatabaseContext _databaseContext;

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

        public HotelViewModel(DatabaseContext databaseContext)
        {
            _httpClient = new HttpClient();  
            _databaseContext = databaseContext;
        }

        [RelayCommand]
        public async Task CreateHotel()
        {
            var response =  await CreateHotelAsync();
        }

        public async Task<bool> CreateHotelAsync()
        {
            try
            {
                var registrationModel = new RestaurantModel
                {
                    UserId = 1,
                    Longitude = 1.02586,
                    Latitude = 1.2586,
                    Name = Name,
                    Address = Address,
                    CreatedOn = DateTime.Now,
                    IsActive = "Y",
                    Reason = "-",
                    ImageURl = "",
                    Description = "",
                    HeaderName = "",
                    SubHeaderName = "",
                    Price = Price,
                    HasMoreInfo = "Y"
                };

                var json = JsonSerializer.Serialize(registrationModel);
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
    }
}
