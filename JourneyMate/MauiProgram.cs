using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using JourneyMate.Database;
using JourneyMate.MVVM.Views.BRUS.Home;
using JourneyMate.MVVM.Views.User;
using JourneyMate.MVVM.ViewModels.User;
using SkiaSharp.Views.Maui.Controls.Hosting;
using UraniumUI;
using JourneyMate.MVVM.Views.BRBO.Hotel;
using JourneyMate.MVVM.Views.BRUS.Bookings;
using JourneyMate.MVVM.Views.BRBO.Vehicle;
using JourneyMate.MVVM.Views.BRBO.Home;
using JourneyMate.MVVM.ViewModels.BRBO.Hotel;
using JourneyMate.MVVM.Views.OnBoarding;
using JourneyMate.MVVM.ViewModels.OnBoardingPage;
using JourneyMate.MVVM.ViewModels;
using JourneyMate.MVVM.ViewModels.BRUS.Home;
using JourneyMate.MVVM.ViewModels.BRBO.Vehicle;
using JourneyMate.MVVM.ViewModels.BRBO.Guide;
using JourneyMate.MVVM.ViewModels.BRBO.Home;
using JourneyMate.MVVM.Views.BRBO.Guide;
using JourneyMate.MVVM.Views.BRBO.Booking;
using JourneyMate.MVVM.ViewModels.BRBO.Payment;
using JourneyMate.MVVM.ViewModels.BRUS.Booking;

namespace JourneyMate
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        { 
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .UseSkiaSharp()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Kanit-Regular.ttf", "Kanit");
                    fonts.AddFont("Kanit-Bold.ttf", "KanitB");
                    fonts.AddFont("fontello.ttf", "Icons");
                });
#if DEBUG
            builder.Logging.AddDebug();
#endif
            //Database
            builder.Services.AddTransient<DatabaseContext>();

            //View           
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<OnBoardingPage>();
            builder.Services.AddTransient<OnboardingPage2>();
            builder.Services.AddTransient<StartingPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<RegistrationPage>();
            builder.Services.AddTransient<AddHotelsPage>();
            builder.Services.AddTransient<PaymentPage>();
            builder.Services.AddTransient<VehiclePage>();
            builder.Services.AddTransient<MerchantHomePage>();
            builder.Services.AddTransient<GuidePage>();
            builder.Services.AddTransient<ViewUpdateAndDeleteGuidePage>();
            builder.Services.AddTransient<EditGuidePage>();
            builder.Services.AddTransient<ViewAllPayments>();
            builder.Services.AddTransient<ViewHotelPage>();
            builder.Services.AddTransient<EditVehiclePage>();
            builder.Services.AddTransient<HotelBookingPage>(); 
            builder.Services.AddTransient<ViewVehicles>();

            //viewModels
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<MainHomeViewModel>();
            builder.Services.AddTransient<OnBoardingPageViewModel>();
            builder.Services.AddTransient<StartingVIewModel>();
            builder.Services.AddTransient<VehicleViewModel>();
            builder.Services.AddTransient<HotelViewModel>();
            builder.Services.AddTransient<GuideViewModel>();
            builder.Services.AddTransient<MerchantHomeViewModel>();
            builder.Services.AddTransient<EditVehicleViewModel>();
            builder.Services.AddTransient<EditGuideViewModel>();
            builder.Services.AddTransient<AllPaymentViewModel>(); 
            builder.Services.AddTransient<PaymentViewModel>(); 
       


            return builder.Build();
        }
    }
}