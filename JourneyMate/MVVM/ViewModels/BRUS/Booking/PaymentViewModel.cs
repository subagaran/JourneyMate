using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JourneyMate.Database;
using JourneyMate.Helper;
using JourneyMate.MVVM.Models;
using JourneyMate.MVVM.Views.BRUS.Home;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JourneyMate.MVVM.ViewModels.BRUS.Booking
{
    public partial class PaymentViewModel : BaseViewModel
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "https://guidtourism.azurewebsites.net/api/Payment/";
        public readonly DatabaseContext _databaseContext;

        [ObservableProperty]
        string nameOnCard;

        [ObservableProperty]
        string bankName;

        [ObservableProperty]
        decimal amount;

        [ObservableProperty]
        int bookingId;

        [ObservableProperty]
        string cardNo;

        [ObservableProperty]
        Double price;

        [ObservableProperty]
        string cardExpiryDate;

        [ObservableProperty]
        string cardExpiryMonth;

        [ObservableProperty]
        string cvcNo;

        public ObservableCollection<PaymentModel> Payments { get; set; }

        public PaymentViewModel(DatabaseContext databaseContext)
        {
            _httpClient = new HttpClient();
            _databaseContext = databaseContext;

            Task.Run(() => GetAllPaymentsFromApiToLocalAsync()).Wait();
        }

        public async void GetAllPaymentFromLocalDB()
        {
            var PaymentsList = await _databaseContext.GetAllAsync<PaymentModel>();

            foreach (var item in PaymentsList)
            {
                Payments.Add(item);
            }
        }

        [RelayCommand]
        public async Task CreatePayment()
        {
            var response = await CreatePaymentAsync();
        }

        public async Task<bool> CreatePaymentAsync()
        {
            try
            {
                var model = new PaymentModel
                {
                    UserId = GlobalVariable.GetUserId(),
                    Amount = amount,
                    NameOnCard = nameOnCard,
                    BankName = bankName,
                    PaymentDate = DateTime.Now,
                    CardNo = cardNo,
                    CardExpiryNo = cardExpiryDate + "   " + cardExpiryMonth,
                    CVCNo = cvcNo
                };

                var json = JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(ApiBaseUrl + "CreatePayment", content);

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
