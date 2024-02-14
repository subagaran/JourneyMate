using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JourneyMate.Database;
using JourneyMate.MVVM.Models;
using JourneyMate.MVVM.Views.BRUS.Home;
using Newtonsoft.Json;
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
            Hotels = new ObservableCollection<HotelModel>();
            LoadHotelsAsync();
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
                var model = new RestaurantModel
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
                    HeaderName = "-",
                    SubHeaderName = "",
                    Price = Price,
                    HasMoreInfo = "Y"
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

        public async Task LoadHotelsAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(ApiURL);

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var responseObject = JsonConvert.DeserializeObject<ApiResponseModel>(json);

                        var hotels = responseObject.Result; // Assuming ApiResponseModel has a property 'Result' of type List<HotelModel>

                        Hotels.Clear(); // Clear the existing collection

                        foreach (var hotel in hotels)
                        {
                            Hotels.Add(hotel);
                        }
                    }
                    else
                    {
                        // Handle non-success status code, e.g., log or display an error message
                    }
                }
            }
            catch (HttpRequestException httpEx)
            {
                // Handle HTTP request exceptions
                Console.WriteLine($"HTTP request error: {httpEx.Message}");
            }
            //catch (JsonException jsonEx)
            //{
            //    // Handle JSON deserialization exceptions
            //    Console.WriteLine($"JSON deserialization error: {jsonEx.Message}");
            //}
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Error: {ex.Message}");
            }
        }



    }
}
