using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JourneyMate.Database;
using JourneyMate.Helper;
using JourneyMate.Helpers;
using JourneyMate.MVVM.Models;
using JourneyMate.MVVM.Views.BRUS.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyMate.MVVM.ViewModels.BRUS.Booking
{
    public partial class BookingViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        public readonly DatabaseContext _databaseContext;
        private const string ApiBaseUrl = "https://guidtourism.azurewebsites.net/api/Booking/";

        private int _Id;

        [ObservableProperty]
        string hotelName;

        [ObservableProperty]
        string price;

        [ObservableProperty]
        double noOfDays;

        [ObservableProperty]
        string address;

        [ObservableProperty]
        string customerName;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string numberOfGuests;

        public BookingViewModel(DatabaseContext databaseContext)
        {
            _httpClient = new HttpClient();
            _databaseContext = databaseContext;
            _Id = GlobalVariable.GetGuideId();
            Task.Run(() => GetHotelDetails()).Wait();

        }

        public async Task GetHotelDetails()
        {
            var hotels = await _databaseContext.GetAllAsync<HotelModel>();

            var details = hotels.Where(a => a.Id == _Id);

            HotelName = details.FirstOrDefault().Name;
            Price = details.FirstOrDefault().Price;
            Address = details.FirstOrDefault().Address;
        }

        [RelayCommand]
        public async Task Booking()
        {
            var response = await BookHotel();
            await Shell.Current.GoToAsync($"{nameof(PaymentPage)}");         

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
        public async Task<bool> BookHotel()
        {
            bool Connection = await CheckInternetConnection();
            if (!Connection)
            {
                PopUpMessage.NoInternetMessage();
                await Task.CompletedTask; 
                return false;
            }

            var model = new BookingModel
            {
                BookingTypeId = 1,
                UserId = GlobalVariable.GetUserId(),
                BookingDate = DateTime.Now,
                CustomerName = Name,
                ContactNumber = "0766316787",
                CheckInDate = DateTime.Now.AddDays(1),
                CheckOutDate = DateTime.Now.AddDays(2),
                NumberOfGuests = 3,
                Amount = 0,
                BookingType = "",
                CustomerId = 0,
                GuideId = 0, 
                RoomId = 0,
                TotalPaid = 0,
                VehicleId = 0
            };

            var json = System.Text.Json.JsonSerializer.Serialize(model);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(ApiBaseUrl + "CreateBooking", content);

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
}
