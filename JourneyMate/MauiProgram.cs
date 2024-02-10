﻿using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using JourneyMate.Database;
using JourneyMate.MVVM.Views.Home;
using JourneyMate.MVVM.Views.User;
using JourneyMate.MVVM.ViewModels.User;

namespace JourneyMate
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Kanit-Regular.ttf", "Kanit");
                fonts.AddFont("Kanit-Bold.ttf", "KanitB");
                fonts.AddFont("fontello.ttf", "Icons");
            }).UseMauiCommunityToolkit();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            //Database
            builder.Services.AddTransient<DatabaseContext>();

            //View           
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<StartingPage>();
            builder.Services.AddTransient<LoginPage>();

            //viewModels
            builder.Services.AddTransient<LoginViewModel>();

            return builder.Build();
        }
    }
}