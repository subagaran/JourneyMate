using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JourneyMate.Database;
using JourneyMate.Helper;
using JourneyMate.MVVM.Models;
using JourneyMate.MVVM.Views.BRUS.Home;
using Newtonsoft.Json;
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

        private CancellationTokenSource _cancelTokenSource;
        private bool _isCheckingLocation;
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

        public HotelViewModel(DatabaseContext databaseContext)
        {
            _httpClient = new HttpClient();  
            _databaseContext = databaseContext;

            Task.Run(() => GetCurrentLocation()).Wait();

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
                // Retrieve the list of image keys from SecureStorage
                string imageKeysJson = await SecureStorage.GetAsync("ImageKeys");

                //if (imageKeysJson != null)
                //{
                //    List<string> imageKeys = JsonConvert.DeserializeObject<List<string>>(imageKeysJson);

                //    // Create a list to store image data for each image
                //    List<byte[]> images = new List<byte[]>();

                //    // Iterate over each image key and retrieve the corresponding image data
                //    foreach (string key in imageKeys)
                //    {
                //        // Retrieve image data from SecureStorage using the key
                //        string imageDataBase64 = await SecureStorage.GetAsync(key);
                //        if (imageDataBase64 != null)
                //        {
                //            // Convert Base64 string back to byte array and add it to the list of images
                //            byte[] imageData = Convert.FromBase64String(imageDataBase64);
                //            images.Add(imageData);
                //        }
                //    }

                //    // Now you have each image data stored in separate lists (images)
                //    // You can further process these lists as needed
                //}

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
    }
}
