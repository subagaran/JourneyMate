using JourneyMate.Helper;
using JourneyMate.MVVM.ViewModels.BRBO.Hotel;
using JourneyMate.Utilities;
using Microsoft.Maui.Storage;
using Newtonsoft.Json;

namespace JourneyMate.MVVM.Views.BRBO.Hotel;

public partial class AddHotelsPage : ContentPage
{
    private readonly HotelViewModel _hotelViewModel;
	public AddHotelsPage(HotelViewModel hotelViewModel)
	{
		InitializeComponent();
        _hotelViewModel = hotelViewModel;
        BindingContext = _hotelViewModel;
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

        List<string> imageKeys = new List<string>();

        foreach (var file in results)
        {
            var stream = await file.OpenReadAsync();
            byte[] bytes;

            using (MemoryStream ms = new MemoryStream())
            {
                await stream.CopyToAsync(ms);
                bytes = ms.ToArray();
            }

            // Use a unique key for each photo
            string key = Guid.NewGuid().ToString();
            await SecureStorage.SetAsync(key, Convert.ToBase64String(bytes));

            // Store the key for each image in the list
            imageKeys.Add(key);
        }

        // Store the list of image keys after all images are stored
        await SecureStorage.SetAsync("ImageKeys", JsonConvert.SerializeObject(imageKeys));

        // Retrieve the list of image keys from SecureStorage
        string imageKeysJson = await SecureStorage.GetAsync("ImageKeys");


        // Update your UI with the selected photos
        UpdateImageCollection();
    }

    private async Task<List<string>> GetImageKeys()
    {
        string keysString = await SecureStorage.GetAsync("ImageKeys");

        if (keysString != null)
            return JsonConvert.DeserializeObject<List<string>>(keysString);
        else
            return new List<string>();
    }

    private async void UpdateImageCollection()
    {
        // Retrieve images from SecureStorage using stored keys and display them in the image collection
        List<ImageSource> imageSources = new List<ImageSource>();
        List<string> imageKeys = await GetImageKeys();
        foreach (var key in imageKeys)
        {
            string base64String = await SecureStorage.GetAsync(key);
            byte[] bytes = Convert.FromBase64String(base64String);
            imageSources.Add(ImageSource.FromStream(() => new MemoryStream(bytes)));
        }
         
      //  myImageCollection.ItemsSource = imageSources;
    }
     

    public async Task Add()
    {
        var imageSource = await GetImageFromSecureStorage("Image_1"); 
    }
    private async Task<ImageSource> GetImageFromSecureStorage(string key)
    {
        try
        {
            // Retrieve the Base64-encoded string from secure storage
            string base64String = await SecureStorage.GetAsync(SD.ImgUrl);

            if (string.IsNullOrEmpty(base64String))
                return null;

            // Convert the Base64-encoded string back to a byte array
            byte[] bytes = Convert.FromBase64String(base64String);

            // Convert the byte array to an ImageSource
            return ImageSource.FromStream(() => new MemoryStream(bytes));
        }
        catch (Exception ex)
        {
            // Handle any exceptions
            Console.WriteLine($"Error retrieving image from secure storage: {ex.Message}");
            return null;
        }
    } 

}