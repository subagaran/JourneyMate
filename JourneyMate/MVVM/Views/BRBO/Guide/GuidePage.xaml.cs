using JourneyMate.MVVM.ViewModels.BRBO.Guide;
using Newtonsoft.Json;

namespace JourneyMate.MVVM.Views.BRBO.Guide;

public partial class GuidePage : ContentPage
{
    private readonly GuideViewModel _guideViewModel;
	public GuidePage(GuideViewModel guideViewModel)
	{
		InitializeComponent();
        _guideViewModel = guideViewModel;
        BindingContext = _guideViewModel;
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
        await SecureStorage.SetAsync("ImageVehicle", JsonConvert.SerializeObject(imageKeys));

        // Retrieve the list of image keys from SecureStorage
        string imageKeysJson = await SecureStorage.GetAsync("ImageVehicle");


        // Update your UI with the selected photos
        UpdateImageCollection();
    }

    private async Task<List<string>> GetImageKeys()
    {
        string keysString = await SecureStorage.GetAsync("ImageVehicle");

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


        myImageCollection.ItemsSource = imageSources;
    }
}