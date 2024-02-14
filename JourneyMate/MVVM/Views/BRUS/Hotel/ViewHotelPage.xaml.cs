using JourneyMate.Database;
using JourneyMate.MVVM.ViewModels.BRBO.Hotel;

namespace JourneyMate.MVVM.Views.BRUS.Hotel;

public partial class ViewHotelPage : ContentPage
{
    private readonly DatabaseContext _databaseContext;
    public ViewHotelPage()
	{
		InitializeComponent();
        _databaseContext = new DatabaseContext(); // Instantiate or inject your DatabaseContext here
        BindingContext = new HotelViewModel(_databaseContext);

    }
}