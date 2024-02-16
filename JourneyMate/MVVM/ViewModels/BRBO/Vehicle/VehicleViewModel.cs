using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JourneyMate.Database;
using JourneyMate.Helper;
using JourneyMate.Helpers;
using JourneyMate.MVVM.Models;
using JourneyMate.MVVM.Views.BRBO.Vehicle;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private readonly DatabaseContext _databaseContext;
        private const string ApiBaseUrl = "https://guidtourism.azurewebsites.net/api/Vehicle/";

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

        public ObservableCollection<VehicleModelcs> Vehicle { get; set; } = new();


        public VehicleViewModel()
        {
            _httpClient = new HttpClient();
            _databaseContext = new DatabaseContext();
            Task.Run(() => GetAllVehicleFromApiToLocalAsync()).Wait();

        }

        [RelayCommand]
        public async Task CreateVehicle()
        {
            var response = await CreateVehicleAsync();
        }

        public async Task<bool> CreateVehicleAsync()
        {
            try
            {
                var model = new VehicleModelcs
                {
                    Id = 1,
                    Make = Make,
                    Model= Model,
                    Year = Year,
                    Color = Color,
                    Price = Price,
                    IsActive = "Y",                  
                };

                var json = JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(ApiBaseUrl + "CreateVehicle", content);

                if (response.IsSuccessStatusCode)
                { 
                    await Shell.Current.Navigation.PopAsync();
                    return true;
                }
                else
                {
                    PopUpMessage.WarningMessage("Something went wrong.");
                    return false;
                }

            }
            catch (Exception ex)
            { 
                return false;  
            }
        }

        public async void GetAllVehicleFromLocalDB()
        {
            var List = await _databaseContext.GetAllAsync<VehicleModelcs>();

            foreach (var item in List)
            {
                Vehicle.Add(item);
            }
        }

        public async Task<bool> DeleteAsync()
        {
            try
            {
                var guideId = GlobalVariable.GetGuideId();
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://guidtourism.azurewebsites.net/api/Vehicle/");


                    client.DefaultRequestHeaders.Add("Id", guideId.ToString());

                    HttpResponseMessage response = await client.PostAsync("DeleteVehicle", null);

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
        public async Task GotoEditPage(GuideModel guideModel)
        {
            IsBusy = true;
            GlobalVariable.SetGuideId(guideModel.Id);
            await Shell.Current.GoToAsync($"{nameof(EditVehiclePage)}");
            IsBusy = false;
        }

        [RelayCommand]
        public async Task Delete(GuideModel guideModel)
        {
            GlobalVariable.SetGuideId(guideModel.Id);

            bool answer = await PopUpMessage.SureMessage("Confirmation", "Do you want to Delete?");
            if (!answer)
            {
                return;
            }
            await DeleteAsync();
        }
    }
}
