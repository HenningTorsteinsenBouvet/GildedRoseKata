namespace GildedRoseKata;

public class AgedBrie : Item
{
    public AgedBrie(int sellIn, int quality)
    {
        Name = "Aged Brie";
        SellIn = sellIn;
        Quality = quality;
    }

    public override void UpdateItem()
    {
        IncreaseQuality();
        UpdateItemSellIn();
    }

    // Handle regular items
    private void UpdateItemSellIn()
    {
        DecreaseSellIn();

        if (SellIn < 0)
        {
            IncreaseQuality();
        }
    }
}
