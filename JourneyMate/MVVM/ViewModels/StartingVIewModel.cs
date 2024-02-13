using JourneyMate.Helper;
using JourneyMate.Helpers;
using JourneyMate.MVVM.Views.BRUS.Home;
using JourneyMate.MVVM.Views.OnBoarding;
using JourneyMate.MVVM.Views.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyMate.MVVM.ViewModels
{
    public class StartingVIewModel : BaseViewModel
    {
        public StartingVIewModel()
        {
            CheckUserStatus();
        }
        public async Task CheckUserStatus()
        {
            try
            {
                IsBusy = true;

                string loginDateStr = await SecureStorage.GetAsync("LoginDate");
                if (!string.IsNullOrEmpty(loginDateStr))
                {
                    DateTime loginDate = DateTime.Parse(loginDateStr);

                    if (DateTime.Now <= loginDate.AddDays(0))
                    {
                        await GoToMainPage();
                    }
                    else
                    {
                        await GoToLoginPage();
                    }
                }
                else
                {
                    await GoToLoginPage();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                IsBusy = false;
            }
           


        }

        private async Task GoToLoginPage()
        {
            await Shell.Current.GoToAsync($"{nameof(OnBoardingPage)}");
        }

        private static async Task GoToMainPage()
        {
            MenuBuilder.BuildMenu();

            //await Shell.Current.GoToAsync($"{nameof(HomePage)}");
        }

    }
}
