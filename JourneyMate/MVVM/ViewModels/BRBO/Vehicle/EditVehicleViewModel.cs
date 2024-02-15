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

namespace JourneyMate.MVVM.ViewModels.BRBO.Vehicle
{
    public partial class EditVehicleViewModel : BaseViewModel
    {
        private readonly HttpClient _httpClient;
        private readonly DatabaseContext _databaseContext;
        private const string ApiBaseUrl = "https://guidtourism.azurewebsites.net/api/Vehicle/";
        private int _Id;

        [ObservableProperty]
        string make;
        [ObservableProperty]
        string model;
        [ObservableProperty]
        int year;
        [ObservableProperty]
        string color;
        [ObservableProperty]
        double price;
        [ObservableProperty]
        string isActive = "N";
        [ObservableProperty]
        string vehicleName;
        [ObservableProperty]
        string vehicleNo;
        [ObservableProperty]
        string brand;
        [ObservableProperty]
        string driverName;

        public EditVehicleViewModel()
        {
            _httpClient = new HttpClient();
            _databaseContext = new DatabaseContext();
            _Id = GlobalVariable.GetGuideId();

            Task.Run(() => PageLoad()).Wait();

        }

        public async Task PageLoad()
        {
            var Vehicles = await _databaseContext.GetAllAsync<VehicleModelcs>();

            var List = Vehicles.Where(a => a.Id == _Id);
            Make = List.FirstOrDefault().Make;
;           Year = List.FirstOrDefault().Year;
            Color = List.FirstOrDefault().Color;
            Price = List.FirstOrDefault().Price; 
        }

        [RelayCommand]
        public async Task Update()
        {
            var response = await UpdateVehicleAsync();

          
        }

        public async Task<bool> UpdateVehicleAsync()
        {
            try
            { 
                string imageKeysJson = await SecureStorage.GetAsync("ImageKeys");
                var userid = GlobalVariable.GetUserId();
                var GuideId = GlobalVariable.GetGuideId();
                var model = new VehicleModelcs
                {
                    Id = _Id,
                    Make = Make,
                    Model = Model,
                    Year = Year,
                    Color = Color,
                    Price = Price,
                    IsActive = "Y",

                };

                var json = System.Text.Json.JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(ApiBaseUrl + "UpdateVehicle", content);

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
