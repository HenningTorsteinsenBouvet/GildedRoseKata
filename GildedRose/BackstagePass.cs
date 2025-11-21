namespace GildedRoseKata;

public class BackstagePass : Item
{
    public BackstagePass(int sellIn, int quality)
    {
        Name = "Backstage passes to a TAFKAL80ETC concert";
        SellIn = sellIn;
        Quality = quality;
    }

    public override void UpdateItem()
    {
        UpdateItemQuality();
        UpdateItemSellIn();
    }

    private void UpdateItemQuality()
    {
        IncreaseQuality();

        if (SellIn < 11)
        {
            IncreaseQuality();
        }

        if (SellIn < 6)
        {
            IncreaseQuality();
        }
    }

    // Handle regular items
    private void UpdateItemSellIn()
    {
        DecreaseSellIn();

        if (SellIn < 0)
        {
            Quality = 0;
        }
    }
}
