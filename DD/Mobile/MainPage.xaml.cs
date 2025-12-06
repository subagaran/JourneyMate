
namespace MauiTcpClient;

public partial class MainPage : ContentPage
{
    private readonly SyncService syncService = new SyncService();
    private List<Item> items = new List<Item>();

    public MainPage()
    {
        InitializeComponent();
        LoadItems();
    }

    private async void LoadItems()
    {
        try
        {
            lblStatus.Text = "Loading items...";
            items = await syncService.GetItemsAsync();
            ItemsCollection.ItemsSource = items;
            lblStatus.Text = $"Loaded {items.Count} items.";
        }
        catch (Exception ex)
        {
            lblStatus.Text = "Error loading items: " + ex.Message;
        }
    }

    private void OnRefreshClicked(object sender, EventArgs e)
    {
        LoadItems();
    }

    private async void OnQtyEdited(object sender, EventArgs e)
    {
        try
        {
            Entry entry = sender as Entry;
            if (entry?.BindingContext is Item item)
            {
                int newQty = int.TryParse(entry.Text, out var q) ? q : item.Qty;
                bool updated = await syncService.UpdateQtyAsync(item.ItemId, newQty);
                if (updated)
                {
                    lblStatus.Text = $"Updated {item.ItemName} Qty to {newQty}.";
                    // Optional: refresh items from server
                    await Task.Delay(200);
                    LoadItems();
                }
                else
                {
                    lblStatus.Text = $"Failed to update {item.ItemName}.";
                }
            }
        }
        catch (Exception ex)
        {
            lblStatus.Text = "Error updating qty: " + ex.Message;
        }
    }
}
