﻿using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using JourneyMate.Database;
using JourneyMate.Helper.NoInternetConnectionIndicator;
using JourneyMate.Helper.Services;
using JourneyMate.Helpers;
using JourneyMate.MVVM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JourneyMate.MVVM.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {

        private readonly HttpClient _httpClient;
        private readonly DatabaseContext _databaseContext;
        private const string ApiBaseUrl = "https://guidtourism.azurewebsites.net/api/Hotel/";
        private readonly HotelListService _hotelListService;
        [ObservableProperty]
        string showMessage;

        [ObservableProperty]
        bool isBusy;

        [ObservableProperty]
        bool isInternet;

        public ObservableCollection<Hotel> HotelList { get; set; } = new ObservableCollection<Hotel>();

        public BaseViewModel()
        {
            _httpClient = new HttpClient();
            _databaseContext = new DatabaseContext();
            _hotelListService = new();
            Task.Run(() => GetHotel()).Wait();
        }

        public Task GetHotel()
        {
            HotelList = _hotelListService.HotelList;
            return Task.CompletedTask;
        }


        partial void OnIsBusyChanged(bool value)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (value)
                {
                    LoadingIndicator.StartLoading();
                }
                else
                {
                    LoadingIndicator.CloseLoading();
                }
            });
        }

        public static Task<bool> CheckInternetConnection()
        {
            var current = Connectivity.NetworkAccess;

            if (current == NetworkAccess.Internet)
            {
                // Internet connection is available
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);

            }
        }

        public async Task<bool> GetAllHotelsFromApiToLocalAsync()
        {
            var response = await _httpClient.GetAsync(ApiBaseUrl + "GetAllHotels");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonString);
                if (apiResponse.isSuccess)
                {

                    using (JsonDocument document = JsonDocument.Parse(apiResponse.result.ToString()))
                    {
                        JsonElement root = document.RootElement;

                        foreach (JsonElement stockElement in root.EnumerateArray())
                        {
                            var stock = System.Text.Json.JsonSerializer.Deserialize<Hotel>(stockElement.GetRawText());
                            HotelList.Add(stock);
                        }

                    }

                    var Local_CDMStockDto = await _databaseContext.GetAllAsync<Hotel>();
                    if (Local_CDMStockDto.Any()) { await _databaseContext.DeleteAllAsync<Hotel>(); }
                    await _databaseContext.AddRangeAsync(HotelList); 
                    return true;
                }
                else
                {
                    return false; 
                }
            }
            else
            {
                return false;
            }
             
        }
    }
}
