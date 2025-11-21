namespace GildedRoseKata;

public class Sulfaras : Item
{
    public Sulfaras()
    {
        Name = "Sulfuras, Hand of Ragnaros";
        SellIn = 0;
        Quality = 80;
    }

    public override void UpdateItem()
    {
        // Method intentionally left empty because Sulfaras is a legendary item.
    }
}
