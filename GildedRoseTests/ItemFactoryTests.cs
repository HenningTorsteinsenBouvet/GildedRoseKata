using GildedRoseKata;
using Xunit;

namespace GildedRoseTests
{
    /// <summary>
    /// Tests for the Item.Create factory method
    /// </summary>
    public class ItemFactoryTests
    {
        [Fact]
        public void Create_Should_ReturnSulfaras_WhenNameIsSulfuras()
        {
            // Act
            var item = Item.Create("Sulfuras, Hand of Ragnaros", 10, 50);

            // Assert
            Assert.IsType<Sulfaras>(item);
            Assert.Equal("Sulfuras, Hand of Ragnaros", item.Name);
            Assert.Equal(0, item.SellIn);    // Sulfuras always has SellIn=0
            Assert.Equal(80, item.Quality);  // Sulfuras always has Quality=80
        }

        [Fact]
        public void Create_Should_ReturnAgedBrie_WhenNameIsAgedBrie()
        {
            // Act
            var item = Item.Create("Aged Brie", 10, 20);

            // Assert
            Assert.IsType<AgedBrie>(item);
            Assert.Equal("Aged Brie", item.Name);
            Assert.Equal(10, item.SellIn);
            Assert.Equal(20, item.Quality);
        }

        [Fact]
        public void Create_Should_ReturnBackstagePass_WhenNameIsBackstagePass()
        {
            // Act
            var item = Item.Create("Backstage passes to a TAFKAL80ETC concert", 15, 30);

            // Assert
            Assert.IsType<BackstagePass>(item);
            Assert.Equal("Backstage passes to a TAFKAL80ETC concert", item.Name);
            Assert.Equal(15, item.SellIn);
            Assert.Equal(30, item.Quality);
        }

        [Theory]
        [InlineData("Normal Item", 5, 10)]
        [InlineData("+5 Dexterity Vest", 10, 20)]
        [InlineData("Elixir of the Mongoose", 5, 7)]
        [InlineData("Conjured Mana Cake", 3, 6)]
        public void Create_Should_ReturnBaseItem_WhenNameIsNotSpecial(string name, int sellIn, int quality)
        {
            // Act
            var item = Item.Create(name, sellIn, quality);

            // Assert
            Assert.IsType<Item>(item);
            Assert.IsNotType<Sulfaras>(item);
            Assert.IsNotType<AgedBrie>(item);
            Assert.IsNotType<BackstagePass>(item);
            Assert.Equal(name, item.Name);
            Assert.Equal(sellIn, item.SellIn);
            Assert.Equal(quality, item.Quality);
        }

        [Fact]
        public void Create_Should_CreateItemsThatUpdateCorrectly()
        {
            // Arrange
            var items = new System.Collections.Generic.List<Item>
            {
                Item.Create("Normal Item", 10, 10),
                Item.Create("Aged Brie", 10, 10),
                Item.Create("Sulfuras, Hand of Ragnaros", 10, 50),
                Item.Create("Backstage passes to a TAFKAL80ETC concert", 10, 10)
            };
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(9, items[0].Quality);   // Normal item decreases by 1
            Assert.Equal(11, items[1].Quality);  // Aged Brie increases by 1
            Assert.Equal(80, items[2].Quality);  // Sulfuras stays at 80
            Assert.Equal(12, items[3].Quality);  // Backstage pass increases by 2 (10 days)
        }
    }
}
