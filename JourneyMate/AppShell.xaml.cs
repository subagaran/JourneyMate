using JourneyMate.MVVM.Views.BRBO.Booking;
using JourneyMate.MVVM.Views.BRBO.Guide;
using JourneyMate.MVVM.Views.BRBO.Home;
using JourneyMate.MVVM.Views.BRBO.Hotel;
using JourneyMate.MVVM.Views.BRBO.Vehicle;
using JourneyMate.MVVM.Views.BRUS.Bookings;
using JourneyMate.MVVM.Views.BRUS.Home;
using JourneyMate.MVVM.Views.OnBoarding;
using JourneyMate.MVVM.Views.User;

namespace JourneyMate
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(StartingPage), typeof(StartingPage));
            Routing.RegisterRoute(nameof(OnBoardingPage), typeof(OnBoardingPage));
            Routing.RegisterRoute(nameof(OnboardingPage2), typeof(OnboardingPage2));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(AddHotelsPage), typeof(AddHotelsPage));
            Routing.RegisterRoute(nameof(PaymentPage), typeof(PaymentPage));
            Routing.RegisterRoute(nameof(VehiclePage), typeof(VehiclePage));
            Routing.RegisterRoute(nameof(ViewUpdateAndDeleteGuidePage), typeof(ViewUpdateAndDeleteGuidePage));
            Routing.RegisterRoute(nameof(MerchantHomePage), typeof(MerchantHomePage));
            Routing.RegisterRoute(nameof(GuidePage), typeof(GuidePage));
            Routing.RegisterRoute(nameof(EditGuidePage), typeof(EditGuidePage));
            Routing.RegisterRoute(nameof(ViewAllPayments), typeof(ViewAllPayments));
            Routing.RegisterRoute(nameof(HotelBookingPage), typeof(HotelBookingPage));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }
    }
}