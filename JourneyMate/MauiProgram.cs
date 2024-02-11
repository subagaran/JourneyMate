﻿using Microsoft.Extensions.Logging;
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
            builder.Services.AddTransient<StartingPage>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<HomePage>();
            builder.Services.AddTransient<RegistrationPage>();
            builder.Services.AddTransient<AddHotelsPage>();
            builder.Services.AddTransient<PaymentPage>();
            builder.Services.AddTransient<VehiclePage>();
            builder.Services.AddTransient<MerchantHomePage>();

            //viewModels
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<HotelViewModel>();

            return builder.Build();
        }
    }
}