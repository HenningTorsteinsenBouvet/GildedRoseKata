using GildedRoseKata;
using System.Collections.Generic;
using Xunit;

namespace GildedRoseTests
{
    public class UpdateQualityTest
    {
        private readonly string _backStagePassName = "Backstage passes to a TAFKAL80ETC concert";
        private readonly string _sulfuras = "Sulfuras, Hand of Ragnaros";
        private readonly string _agedBrie = "Aged Brie";

        [Theory]
        [InlineData(1, 0)]
        [InlineData(-42, -43)]
        public void Should_DecreaseSellInValue_GivenAnySellInValue(int initialSellIn, int expected)
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = initialSellIn, Quality = 0 } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expected, Items[0].SellIn);
        }

        [Theory]
        [InlineData(10, 10, 12)]
        [InlineData(6, 10, 12)]
        public void Should_IncreaseBackStageQualityBy2_GivenSellInIs10DaysOrLess(int initialSellIn, int initialQuality, int expectedQuality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = _backStagePassName, SellIn = initialSellIn, Quality = initialQuality } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(5, 10, 13)]
        [InlineData(1, 10, 13)]
        public void Should_IncreaseBackStageQualityBy3_GivenSellInIs5DaysOrLess(int initialSellIn, int initialQuality, int expectedQuality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = _backStagePassName, SellIn = initialSellIn, Quality = initialQuality } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(0, 10, 0)]
        public void Should_SetBackStageQualityToZero_GivenSellInIsBelow0(int initialSellIn, int initialQuality, int expectedQuality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = _backStagePassName, SellIn = initialSellIn, Quality = initialQuality } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(42, 10, 11)]
        [InlineData(-10, 10, 12)]
        public void Should_IncreaseAgedBrieQuality_GivenSellInIncreases(int initialSellIn, int initialQuality, int expectedQuality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = _agedBrie, SellIn = initialSellIn, Quality = initialQuality } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(-10, 50, 50)]
        public void Should_NotIncreaseAgedBrieQualityOver50_GivenSellInIncreases(int initialSellIn, int initialQuality, int expectedQuality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = _agedBrie, SellIn = initialSellIn, Quality = initialQuality } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(42, 80, 80)]
        [InlineData(-10, 80, 80)]
        public void Should_NotChangeQualityOfSulfuras_GivenSellInIncreases(int initialSellIn, int initialQuality, int expectedQuality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = _sulfuras, SellIn = initialSellIn, Quality = initialQuality } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(42, 42)]
        [InlineData(-10, -10)]
        public void Should_NotChangeSellInOfSulfuras_GivenWeRunUpdateQuality(int initialSellIn, int expectedSellIn)
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = initialSellIn, Quality = 80 } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedSellIn, Items[0].SellIn);
        }

        [Theory]
        [InlineData(1, 10, 9)]
        public void Should_DecreaseQuality_GivenWeRunOpdateQuality(int sellIn, int initialQuality, int expectedQuality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = sellIn, Quality = initialQuality } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(11, 10, 11)]
        public void Should_IncreaseBackStageQualityBy1_GivenSellInAbove10(int initialSellIn, int initialQuality, int expectedQuality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = _backStagePassName, SellIn = initialSellIn, Quality = initialQuality } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(1, 0, 0)]
        public void Should_NotDecreaseQualityBelowZero_GivenQualityIsZero(int sellIn, int initialQuality, int expectedQuality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = sellIn, Quality = initialQuality } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedQuality, Items[0].Quality);
        }
    }
}
