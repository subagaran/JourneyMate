using JourneyMate.Utilities;

namespace JourneyMate.MVVM.Views.BRBO.Vehicle;

public partial class VehiclePage : ContentPage
{
	public VehiclePage()
	{
		InitializeComponent();
	}

    private async void OnCounterClicked(object sender, EventArgs e)
    {
        var results = await FilePicker.PickMultipleAsync(new PickOptions
        {
            PickerTitle = "Pick multiple photos",
            FileTypes = FilePickerFileType.Images
        });

        if (results == null || results.Count() == 0)
            return;

        List<ImageSource> imageSources = new List<ImageSource>();

        foreach (var file in results)
        {
            var stream = await file.OpenReadAsync();
            imageSources.Add(ImageSource.FromStream(() => stream));
        }

        foreach (var file in results)
        {
            var stream = await file.OpenReadAsync();
            byte[] bytes;
            using (MemoryStream ms = new MemoryStream())
            {
                await stream.CopyToAsync(ms);
                bytes = ms.ToArray();
            }
            await SecureStorage.SetAsync(SD.VehicleImgUrl, Convert.ToBase64String(bytes));
        } 

        myImageCollection.ItemsSource = imageSources;
    }
}