namespace GildedRoseKata;

public class Item
{
    public string Name { get; set; }
    public int SellIn { get; set; }
    public int Quality { get; set; }

    /// <summary>
    /// Factory method to create the appropriate Item subtype based on the item name
    /// </summary>
    /// <param name="name">The name of the item</param>
    /// <param name="sellIn">The sell-in value</param>
    /// <param name="quality">The quality value</param>
    /// <returns>An instance of the appropriate Item subtype</returns>
    public static Item Create(string name, int sellIn, int quality)
    {
        return name switch
        {
            "Sulfuras, Hand of Ragnaros" => new Sulfaras(),
            "Aged Brie" => new AgedBrie(sellIn, quality),
            "Backstage passes to a TAFKAL80ETC concert" => new BackstagePass(sellIn, quality),
            _ => new Item { Name = name, SellIn = sellIn, Quality = quality }
        };
    }

    public virtual void UpdateItem()
    {
        DecreaseQuality();
        UpdateItemSellIn();
    }

    // Handle regular items
    private void UpdateItemSellIn()
    {
        DecreaseSellIn();

        if (SellIn < 0)
        {
            // Handle regular items
            DecreaseQuality();
        }
    }

    protected void DecreaseSellIn()
    {
        SellIn--;
    }

    protected void DecreaseQuality()
    {
        if (Quality > 0)
        {
            Quality--;
        }
    }

    protected void IncreaseQuality()
    {
        if (Quality < 50)
        {
            Quality++;
        }
    }

    private bool IsBackstagePass => Name == "Backstage passes to a TAFKAL80ETC concert";
    private bool IsSulfuras => Name == "Sulfuras, Hand of Ragnaros";
    private bool IsAgedBrie => Name == "Aged Brie";
}
