using System.Collections.Generic;

namespace GildedRoseKata
{
    public class GildedRose
    {
        IList<Item> Items; // Poor encapsulation
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        // Dead code
        private void DeadCode()
        {
            var theAnswer = 42; // Unused variable
        }

        // Long method
        // Feature envy
        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++) // Deep nesting
            {
                if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert") // Magic string, Complicated logic
                {
                    if (Items[i].Quality > 0)
                    {
                        if (Items[i].Name != "Sulfuras, Hand of Ragnaros") // Magic string, Nested code
                        {
                            Items[i].Quality = Items[i].Quality - 1; // Duplicate code
                        }
                    }
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1; // Duplicate code

                        if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert") // Magic string, Complicated logic
                        {
                            if (Items[i].SellIn < 11)
                            {
                                if (Items[i].Quality < 50) // Nested code
                                {
                                    Items[i].Quality = Items[i].Quality + 1; // Duplicate code, Nested code
                                }
                            }

                            if (Items[i].SellIn < 6)
                            {
                                if (Items[i].Quality < 50) // Nested code
                                {
                                    Items[i].Quality = Items[i].Quality + 1; // Duplicate code
                                }
                            }
                        }
                    }
                }

                if (Items[i].Name != "Sulfuras, Hand of Ragnaros") // Magic string, Complicated logic
                {
                    Items[i].SellIn = Items[i].SellIn - 1; // Duplicate code
                }

                if (Items[i].SellIn < 0)
                {
                    if (Items[i].Name != "Aged Brie") // Magic string
                    {
                        if (Items[i].Name != "Backstage passes to a TAFKAL80ETC concert") // Magic string
                        {
                            if (Items[i].Quality > 0)
                            {
                                if (Items[i].Name != "Sulfuras, Hand of Ragnaros") // Magic string
                                {
                                    Items[i].Quality = Items[i].Quality - 1; // Duplicate code
                                }
                            }
                        }
                        else
                        {
                            Items[i].Quality = Items[i].Quality - Items[i].Quality; // Complicated code, set to 0
                        }
                    }
                    else
                    {
                        if (Items[i].Quality < 50)
                        {
                            Items[i].Quality = Items[i].Quality + 1; // Duplicate code
                        }
                    }
                }
            }
        }
    }
}
