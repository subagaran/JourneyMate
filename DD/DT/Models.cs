public class Request
{
    public string action { get; set; }
    public int itemId { get; set; }
    public int qty { get; set; }
}

public class Item
{
    public int ItemId { get; set; }
    public string ItemName { get; set; }
    public int Qty { get; set; }
}
