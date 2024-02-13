using CommunityToolkit.Mvvm.Input;
using JourneyMate.Database;
using JourneyMate.MVVM.LocalModels;
using JourneyMate.MVVM.ViewModels.User;
using JourneyMate.MVVM.Views.OnBoarding;
using JourneyMate.MVVM.Views.User;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyMate.MVVM.ViewModels.OnBoardingPage
{
    public partial class OnBoardingPageViewModel
    {
        private readonly DatabaseContext _databaseContext;
        public ObservableCollection<OnBoarding> OnBoardings { get; set; }

        public OnBoardingPageViewModel()
        {
                _databaseContext = new DatabaseContext();
        }
        public void InitializeOnBoardings()
        {
            OnBoardings = new ObservableCollection<OnBoarding>
            {
                new OnBoarding
                {
                    // Assuming OnBoarding class has properties like Title, Description, ImageUrl, etc.
                    Location = "Find Your Destination",
                    Details = "All Tourist Destinations are in your hands ",
                    ImageUrl = "backgroundimage.png"
                },
                new OnBoarding
                {
                    Location = "Plan Your Journey",
                    Details = "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    ImageUrl = "backgroundimage.png"
                },
                // Add more OnBoarding instances for additional slides if needed
            };
        }

        [RelayCommand]
        public async Task GotoOnboardingNextPage()
        {
            await Shell.Current.GoToAsync($"{nameof(OnboardingPage2)}");
        }

        [RelayCommand]
        public async Task GotoLoginPage()
        {
            LoginViewModel loginViewModel = new LoginViewModel(_databaseContext);
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage(loginViewModel));
        }
    }


}
