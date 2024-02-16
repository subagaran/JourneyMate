using JourneyMate.Database;
using JourneyMate.Helper;
using JourneyMate.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyMate.MVVM.ViewModels.BRBO.Payment
{
    public partial class AllPaymentViewModel : BaseViewModel
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "https://guidtourism.azurewebsites.net/api/Payment/";
        public readonly DatabaseContext _databaseContext;
        public ObservableCollection<PaymentModel> Payments { get; set; }
        public ObservableCollection<PaymentModel> UserWisePayments { get; set; }

        public AllPaymentViewModel(DatabaseContext databaseContext)
        {
            _httpClient = new HttpClient();
            _databaseContext = databaseContext;
            Task.Run(() => GetAllPaymentsFromApiToLocalAsync()).Wait();
            Task.Run(() => GetAllPaymentFromLocalDB()).Wait(); 
        }

        public async void GetAllPaymentFromLocalDB()
        {
            var PaymentsList = await _databaseContext.GetAllAsync<PaymentModel>();

            Payments.Clear();
            foreach (var item in PaymentsList)
            {
                Payments.Add(item);
            }
        }

        public async void GetPaymentsBasedOnuser()
        {
            var PaymentsList = await _databaseContext.GetAllAsync<PaymentModel>();
            Payments.Clear();
            foreach (var item in PaymentsList.Where(a=>a.UserId == GlobalVariable.GetUserId()))
            {
                UserWisePayments.Add(item);
            }
        }

    }
}
