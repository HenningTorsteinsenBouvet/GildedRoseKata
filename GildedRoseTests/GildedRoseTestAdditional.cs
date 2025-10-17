using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests
{
    public class UpdateQualityTestAdditional
    {
        private readonly string _backStagePassName = "Backstage passes to a TAFKAL80ETC concert";
        private readonly string _sulfuras = "Sulfuras, Hand of Ragnaros";
        private readonly string _agedBrie = "Aged Brie";

        [Theory]
        [InlineData(10, 48, 50)]
        [InlineData(9, 48, 50)]
        [InlineData(8, 48, 50)]
        [InlineData(7, 48, 50)]
        [InlineData(6, 48, 50)]
        public void Should_NotIncreaseBackStageQualityOver50_GivenSellInIs10DaysOrLessAndQualityIs48(int initialSellIn, int initialQuality, int expectedQuality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = _backStagePassName, SellIn = initialSellIn, Quality = initialQuality } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(5, 48, 50)]
        [InlineData(4, 48, 50)]
        [InlineData(3, 48, 50)]
        [InlineData(2, 48, 50)]
        [InlineData(1, 48, 50)]
        public void Should_NotIncreaseBackStageQualityOver50_GivenSellInIs5DaysOrLessAndQualityIs48(int initialSellIn, int initialQuality, int expectedQuality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = _backStagePassName, SellIn = initialSellIn, Quality = initialQuality } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(42, 50, 50)]
        [InlineData(1, 50, 50)]
        public void Should_NotIncreaseAgedBrieQualityOver50_GivenQualityIsAlready50BeforeExpiration(int initialSellIn, int initialQuality, int expectedQuality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = _agedBrie, SellIn = initialSellIn, Quality = initialQuality } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(11)]
        [InlineData(10)]
        [InlineData(5)]
        [InlineData(1)]
        public void Should_DecreaseBackStageSellIn_GivenWeRunUpdateQuality(int initialSellIn)
        {
            IList<Item> Items = new List<Item> { new Item { Name = _backStagePassName, SellIn = initialSellIn, Quality = 10 } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(initialSellIn - 1, Items[0].SellIn);
        }

        [Theory]
        [InlineData(11)]
        [InlineData(1)]
        public void Should_DecreaseAgedBrieSellIn_GivenWeRunUpdateQuality(int initialSellIn)
        {
            IList<Item> Items = new List<Item> { new Item { Name = _agedBrie, SellIn = initialSellIn, Quality = 10 } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(initialSellIn - 1, Items[0].SellIn);
        }

        [Fact]
        public void Should_ProcessAllItems_GivenMultipleItemsInList()
        {
            IList<Item> Items = new List<Item> 
            { 
                new Item { Name = "foo", SellIn = 10, Quality = 10 },
                new Item { Name = "bar", SellIn = 5, Quality = 5 },
                new Item { Name = "baz", SellIn = 3, Quality = 3 }
            };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            Assert.Equal(9, Items[0].Quality);
            Assert.Equal(4, Items[1].Quality);
            Assert.Equal(2, Items[2].Quality);
            Assert.Equal(9, Items[0].SellIn);
            Assert.Equal(4, Items[1].SellIn);
            Assert.Equal(2, Items[2].SellIn);
        }

        [Fact]
        public void Should_ProcessDifferentItemTypesCorrectly_GivenMixedItemsInList()
        {
            IList<Item> Items = new List<Item> 
            { 
                new Item { Name = "foo", SellIn = 10, Quality = 10 },
                new Item { Name = _agedBrie, SellIn = 10, Quality = 10 },
                new Item { Name = _backStagePassName, SellIn = 10, Quality = 10 },
                new Item { Name = _sulfuras, SellIn = 10, Quality = 80 }
            };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            Assert.Equal(9, Items[0].Quality);
            Assert.Equal(11, Items[1].Quality);
            Assert.Equal(12, Items[2].Quality);
            Assert.Equal(80, Items[3].Quality);
            Assert.Equal(9, Items[0].SellIn);
            Assert.Equal(9, Items[1].SellIn);
            Assert.Equal(9, Items[2].SellIn);
            Assert.Equal(10, Items[3].SellIn);
        }

        [Theory]
        [InlineData(1, 50, 49)]
        [InlineData(0, 50, 48)]
        [InlineData(-1, 50, 48)]
        public void Should_DecreaseNormalItemQuality_GivenQualityIs50(int sellIn, int initialQuality, int expectedQuality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = sellIn, Quality = initialQuality } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(1, 60, 59)]
        [InlineData(0, 60, 58)]
        public void Should_DecreaseNormalItemQuality_GivenQualityIsAbove50(int sellIn, int initialQuality, int expectedQuality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = sellIn, Quality = initialQuality } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(11, 50, 50)]
        public void Should_NotIncreaseBackStageQualityOver50_GivenQualityIsAlready50(int initialSellIn, int initialQuality, int expectedQuality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = _backStagePassName, SellIn = initialSellIn, Quality = initialQuality } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedQuality, Items[0].Quality);
        }
    }
}
