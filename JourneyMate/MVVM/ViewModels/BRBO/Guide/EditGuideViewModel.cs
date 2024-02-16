using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JourneyMate.Database;
using JourneyMate.Helper;
using JourneyMate.Helpers;
using JourneyMate.MVVM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyMate.MVVM.ViewModels.BRBO.Guide
{
    public partial class EditGuideViewModel : ObservableObject
    {
        private readonly HttpClient _httpClient;
        private readonly DatabaseContext _databaseContext;
        private const string ApiBaseUrl = "https://guidtourism.azurewebsites.net/api/Guide/";
        private int _Id;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string telephoneNo;

        [ObservableProperty]
        string description;

        [ObservableProperty]
        string language;

        [ObservableProperty]
        string email;

        public EditGuideViewModel()
        {
            _httpClient = new HttpClient();
            _databaseContext = new DatabaseContext();
            _Id = GlobalVariable.GetGuideId();
            Task.Run(() => PageLoad()).Wait();
        }

        public async Task PageLoad()
        {
            var Vehicles = await _databaseContext.GetAllAsync<GuideModel>();

            var List = Vehicles.Where(a => a.Id == _Id);
            Name = List.FirstOrDefault().Name;
                    TelephoneNo = List.FirstOrDefault().TpNo;
                    Description = List.FirstOrDefault().Descriptiohn; 
                    Email = List.FirstOrDefault().Email;
            Language = List.FirstOrDefault().Language;
        }

        [RelayCommand]
        public async Task UpdateGuide()
        {
            var response = await UpdateGuideAsync();
        }

        public async Task<bool> UpdateGuideAsync()
        {
            try
            { 
                var userid = GlobalVariable.GetUserId();
                var GuideId = GlobalVariable.GetGuideId();
                var model = new GuideModel
                {
                    Id = Convert.ToInt32(GuideId),
                    UserId = userid,
                    Name = Name,
                    TpNo = TelephoneNo,
                    Image = "",
                    Descriptiohn = Description,
                    IsActive = "Y",
                    Email = Email,
                    Language = Language,
                    Password = "",
                    Username = "-",
                };

                var json = System.Text.Json.JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(ApiBaseUrl + "UpdateGuide", content);

                if (response.IsSuccessStatusCode)
                {
                    PopUpMessage.SuccessMessage("Updated Successfully");
                    await Shell.Current.Navigation.PopAsync();
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
