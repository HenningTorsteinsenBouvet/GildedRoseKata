using GildedRoseKata;
using System.Collections.Generic;
using Xunit;

namespace GildedRoseTests
{
    public class UpdateQualityTest
    {
        private readonly string _backStagePassName = "Backstage passes to a TAFKAL80ETC concert";
        private readonly string _agedBrie = "Aged Brie";

        [Theory]
        [InlineData(10, 10, 12)]
        [InlineData(6, 10, 12)]
        public void Should_IncreaseBackStageQualityBy2_GivenSellInIs10DaysOrLess(int initialSellIn, int initialQuality, int expectedQuality)
        {
            IList<Item> Items = new List<Item> { Item.Create(_backStagePassName, initialSellIn, initialQuality) };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(5, 10, 13)]
        [InlineData(1, 10, 13)]
        public void Should_IncreaseBackStageQualityBy3_GivenSellInIs5DaysOrLess(int initialSellIn, int initialQuality, int expectedQuality)
        {
            IList<Item> Items = new List<Item> { Item.Create(_backStagePassName, initialSellIn, initialQuality) };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(0, 10, 0)]
        public void Should_SetBackStageQualityToZero_GivenSellInIsBelow0(int initialSellIn, int initialQuality, int expectedQuality)
        {
            IList<Item> Items = new List<Item> { Item.Create(_backStagePassName, initialSellIn, initialQuality) };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(42, 10, 11)]
        [InlineData(-10, 10, 12)]
        public void Should_IncreaseAgedBrieQuality_GivenSellInIncreases(int initialSellIn, int initialQuality, int expectedQuality)
        {
            IList<Item> Items = new List<Item> { Item.Create(_agedBrie, initialSellIn, initialQuality) };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(11, 10, 11)]
        public void Should_IncreaseBackStageQualityBy1_GivenSellInAbove10(int initialSellIn, int initialQuality, int expectedQuality)
        {
            IList<Item> Items = new List<Item> { Item.Create(_backStagePassName, initialSellIn, initialQuality) };

            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedQuality, Items[0].Quality);
        }
    }
}
