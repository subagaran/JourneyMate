using JourneyMate.MVVM.LocalModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyMate.MVVM.ViewModels.OnBoardingPage
{
    public class OnBoardingPageViewModel
    {
        public ObservableCollection<OnBoarding> OnBoardings { get; set; }

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
    }


}
