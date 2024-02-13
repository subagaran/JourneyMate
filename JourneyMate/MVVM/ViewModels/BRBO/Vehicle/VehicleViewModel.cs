using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JourneyMate.Database;
using JourneyMate.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JourneyMate.MVVM.ViewModels.BRBO.Vehicle
{
    public partial class VehicleViewModel : BaseViewModel
    {
        private readonly HttpClient _httpClient;

        private const string ApiBaseUrl = "https://guidtourism.azurewebsites.net/api/Vehicle/";

        [ObservableProperty]
        string make;
        [ObservableProperty]
        string model;
        [ObservableProperty]
        string year;
        [ObservableProperty]
        string color;
        [ObservableProperty]
        string price;
        [ObservableProperty]
        string isActive = "N";
        [ObservableProperty]
        string vehcicleName;
        [ObservableProperty]
        string vehcileNo;
        [ObservableProperty]
        string brand;
        [ObservableProperty]
        string driverName;


        public VehicleViewModel()
        {
            _httpClient = new HttpClient(); 
        }

        [RelayCommand]
        public async Task CreateVehicle()
        {
            var response = await CreateHotelAsync();
        }

        public async Task<bool> CreateHotelAsync()
        {
            try
            {
                var model = new VehicleModelcs
                {
                    Id = 1,
                    Make = "Toyota",
                    Model= "Car",
                    Year = 2023,
                    Color = "Green",
                    Price = 20000.00,
                    IsActive = "Y",
                  
                };

                var json = JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(ApiBaseUrl + "CreateVehicle", content);

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
