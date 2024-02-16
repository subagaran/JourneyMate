using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using JourneyMate.Database;
using JourneyMate.Helper;
using JourneyMate.Helpers; 
using JourneyMate.MVVM.Models;
using JourneyMate.MVVM.Views.BRBO.Guide; 
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace JourneyMate.MVVM.ViewModels.BRBO.Guide
{
    public partial class GuideViewModel : BaseViewModel
    {
        private readonly HttpClient _httpClient;
        private readonly DatabaseContext _databaseContext;
        private const string ApiBaseUrl = "https://guidtourism.azurewebsites.net/api/Guide/DeleteGuide/";

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

        public ObservableCollection<GuideModel> Guide { get; set; } = new();

        public GuideViewModel()
        {
             _httpClient = new HttpClient();
            _databaseContext = new DatabaseContext();
            GetAllGuidessFromLocalDB();
        }

       

        [RelayCommand]
        public async Task CreateGuide()
        {
            bool answer = await PopUpMessage.SureMessage("Confirmation", "Do you want to save?");
            if (!answer)
            {
                return;
            }

            var response = await CreateGuideAsync();

            PopUpMessage.SuccessMessage("Guide Created Successfully");
            
        }

        public async void GetAllGuidessFromLocalDB()
        {
            var GuidList = await _databaseContext.GetAllAsync<GuideModel>();

            foreach (var item in GuidList)
            {
                Guide.Add(item);
            }
        }

        public async Task<bool> CreateGuideAsync()
        {
            try
            {
                // Retrieve the list of image keys from SecureStorage
                string imageKeysJson = await SecureStorage.GetAsync("ImageKeys");
 
                
                var model = new GuideModel
                {
                    UserId = GlobalVariable.GetUserId(),
                    Name = Name,
                    TpNo = TelephoneNo, 
                    Image = imageKeysJson,
                    Descriptiohn = Description, 
                    IsActive = "Y",
                    Email = Email,
                    Language = Language,
                    Password = "",
                    Username = "-"
                };

                var json = System.Text.Json.JsonSerializer.Serialize(model);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(ApiBaseUrl + "CreateGuide", content);

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
                return false;
            }
        }

        public async Task<bool> DeleteGuideAsync()
        {
            try
            {
                var guideId = GlobalVariable.GetGuideId();

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://guidtourism.azurewebsites.net/api/Guide/");


                    client.DefaultRequestHeaders.Add("Id", guideId.ToString());

                    HttpResponseMessage response = await client.PostAsync("DeleteGuide", null);

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
        public async Task GotoEditGuidePage(GuideModel guideModel)
        {
            IsBusy = true;
            GlobalVariable.SetGuideId(guideModel.Id);
            await Shell.Current.GoToAsync($"{nameof(EditGuidePage)}");
            IsBusy = false;
        }

        [RelayCommand]
        public async Task DeleteGuide(GuideModel guideModel)
        {
            GlobalVariable.SetGuideId(guideModel.Id);

            bool answer = await PopUpMessage.SureMessage("Confirmation", "Do you want to Delete?");
            if (!answer)
            {
                return;
            }
           await DeleteGuideAsync();
        }
    }
}
