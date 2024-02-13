using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyMate.MVVM.ViewModels.BRBO.Vehicle
{
    public partial class VehicleViewModel : BaseViewModel
    {
        private const string ApiBaseUrl = "https://guidtourism.azurewebsites.net/api/Vehicle/";

        [ObservableProperty]
        string make;
        [ObservableProperty]
        string model;
        [ObservableProperty]
        string year;
        [ObservableProperty]
        string color;
        [ObservableProperty]
        string price;
        [ObservableProperty]
        string isActive;
        [ObservableProperty]
        string vehcicleName;
        [ObservableProperty]
        string vehcileNo;
        [ObservableProperty]
        string brand;
        [ObservableProperty]
        string driverName;
    }
}
