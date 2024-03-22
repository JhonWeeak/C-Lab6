public class InventoryItem
{
    public int ItemNo { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public string FormattedInfo => $"{ItemNo}|{Description}|{Price}";
}
