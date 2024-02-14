using JourneyMate.Database;
using JourneyMate.MVVM.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JourneyMate.Helper.Services
{
    public class HotelListService
    {
        private readonly HttpClient _httpClient;
        private readonly DatabaseContext _databaseContext;
        private const string ApiBaseUrl = "https://guidtourism.azurewebsites.net/api/Hotel/";

        public ObservableCollection<Hotel> HotelList { get; set; } = new ObservableCollection<Hotel>();

        public HotelListService()
        {
            _httpClient = new HttpClient();
            _databaseContext = new DatabaseContext();

           //Task.Run(() => GetAllHotelsFromApiToLocalAsync()).Wait();
        }

        public async Task<bool> GetAllHotelsFromApiToLocalAsync()
        {
            var response = await _httpClient.GetAsync(ApiBaseUrl + "GetAllHotels");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponses>(jsonString);
                if (apiResponse.isSuccess)
                { 
                    foreach (var stockElement in apiResponse.result)
                        { 
                            HotelList.Add(stockElement);
                        }
 
                    var LocalHotel = await _databaseContext.GetAllAsync<Hotel>();
                    if (LocalHotel.Any()) { await _databaseContext.DeleteAllAsync<Hotel>(); }
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
