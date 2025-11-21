using GildedRoseKata;
using System.Collections.Generic;
using Xunit;

namespace GildedRoseTests
{
    /// <summary>
    /// Additional tests to improve mutation coverage and validate all requirements
    /// </summary>
    public class GildedRoseAdditionalTests
    {
        private const string BackStagePassName = "Backstage passes to a TAFKAL80ETC concert";
        private const string Sulfuras = "Sulfuras, Hand of Ragnaros";
        private const string AgedBrie = "Aged Brie";

        #region Normal Items Tests

        [Theory]
        [InlineData("Normal Item", 10, 10, 9, 9)]
        [InlineData("+5 Dexterity Vest", 10, 20, 19, 9)]
        [InlineData("Elixir of the Mongoose", 5, 7, 6, 4)]
        [InlineData("Conjured Mana Cake", 3, 6, 5, 2)]
        public void NormalItem_Should_DecreaseQualityBy1_BeforeSellDate(
            string itemName, int initialSellIn, int initialQuality,
            int expectedQuality, int expectedSellIn)
        {
            // Arrange
            IList<Item> items = new List<Item>
            {
                Item.Create(itemName, initialSellIn, initialQuality)
            };
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(expectedQuality, items[0].Quality);
            Assert.Equal(expectedSellIn, items[0].SellIn);
        }

        [Theory]
        [InlineData("Normal Item", -1, 10, 8)]
        [InlineData("Normal Item", 0, 10, 8)]
        [InlineData("Normal Item", -5, 10, 8)]
        [InlineData("+5 Dexterity Vest", 0, 20, 18)]
        public void NormalItem_Should_DecreaseQualityBy2_AfterSellDate(
            string itemName, int initialSellIn, int initialQuality, int expectedQuality)
        {
            // Arrange
            IList<Item> items = new List<Item>
            {
                Item.Create(itemName, initialSellIn, initialQuality)
            };
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(expectedQuality, items[0].Quality);
        }

        [Theory]
        [InlineData("Normal Item", 10, 0, 0)]
        [InlineData("Normal Item", -1, 0, 0)]
        [InlineData("Normal Item", -1, 1, 0)]
        [InlineData("+5 Dexterity Vest", 0, 1, 0)]
        public void NormalItem_Quality_Should_NeverBeNegative(
            string itemName, int initialSellIn, int initialQuality, int expectedQuality)
        {
            // Arrange
            IList<Item> items = new List<Item>
            {
                Item.Create(itemName, initialSellIn, initialQuality)
            };
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(expectedQuality, items[0].Quality);
        }

        #endregion

        #region Aged Brie Tests

        [Theory]
        [InlineData(10, 10, 11, 9)]
        [InlineData(5, 0, 1, 4)]
        [InlineData(1, 49, 50, 0)]
        public void AgedBrie_Should_IncreaseQualityBy1_BeforeSellDate(
            int initialSellIn, int initialQuality, int expectedQuality, int expectedSellIn)
        {
            // Arrange
            IList<Item> items = new List<Item>
            {
                Item.Create(AgedBrie, initialSellIn, initialQuality)
            };
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(expectedQuality, items[0].Quality);
            Assert.Equal(expectedSellIn, items[0].SellIn);
        }

        [Theory]
        [InlineData(-1, 10, 12)]
        [InlineData(-10, 10, 12)]
        [InlineData(0, 20, 22)]
        public void AgedBrie_Should_IncreaseQualityBy2_AfterSellDate(
            int initialSellIn, int initialQuality, int expectedQuality)
        {
            // Arrange
            IList<Item> items = new List<Item>
            {
                Item.Create(AgedBrie, initialSellIn, initialQuality)
            };
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(expectedQuality, items[0].Quality);
        }

        [Theory]
        [InlineData(10, 50, 50)]
        [InlineData(-10, 50, 50)]
        [InlineData(-1, 49, 50)]
        public void AgedBrie_Quality_Should_NeverExceed50(
            int initialSellIn, int initialQuality, int expectedQuality)
        {
            // Arrange
            IList<Item> items = new List<Item>
            {
                Item.Create(AgedBrie, initialSellIn, initialQuality)
            };
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(expectedQuality, items[0].Quality);
        }

        #endregion

        #region Backstage Pass Tests

        [Theory]
        [InlineData(15, 10, 11, 14)]
        [InlineData(20, 20, 21, 19)]
        [InlineData(11, 0, 1, 10)]
        public void BackstagePass_Should_IncreaseQualityBy1_When_MoreThan10Days(
            int initialSellIn, int initialQuality, int expectedQuality, int expectedSellIn)
        {
            // Arrange
            IList<Item> items = new List<Item>
            {
                Item.Create(BackStagePassName, initialSellIn, initialQuality)
            };
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(expectedQuality, items[0].Quality);
            Assert.Equal(expectedSellIn, items[0].SellIn);
        }

        [Theory]
        [InlineData(10, 10, 12)]
        [InlineData(9, 10, 12)]
        [InlineData(6, 20, 22)]
        public void BackstagePass_Should_IncreaseQualityBy2_When_10DaysOrLess(
            int initialSellIn, int initialQuality, int expectedQuality)
        {
            // Arrange
            IList<Item> items = new List<Item>
            {
                Item.Create(BackStagePassName, initialSellIn, initialQuality)
            };
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(expectedQuality, items[0].Quality);
        }

        [Theory]
        [InlineData(5, 10, 13)]
        [InlineData(4, 10, 13)]
        [InlineData(1, 20, 23)]
        public void BackstagePass_Should_IncreaseQualityBy3_When_5DaysOrLess(
            int initialSellIn, int initialQuality, int expectedQuality)
        {
            // Arrange
            IList<Item> items = new List<Item>
            {
                Item.Create(BackStagePassName, initialSellIn, initialQuality)
            };
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(expectedQuality, items[0].Quality);
        }

        [Theory]
        [InlineData(0, 10, 0)]
        [InlineData(-1, 50, 0)]
        [InlineData(-10, 30, 0)]
        public void BackstagePass_Quality_Should_DropToZero_AfterConcert(
            int initialSellIn, int initialQuality, int expectedQuality)
        {
            // Arrange
            IList<Item> items = new List<Item>
            {
                Item.Create(BackStagePassName, initialSellIn, initialQuality)
            };
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(expectedQuality, items[0].Quality);
        }

        [Theory]
        [InlineData(11, 50, 50)]
        [InlineData(10, 50, 50)]
        [InlineData(5, 50, 50)]
        [InlineData(10, 49, 50)]
        [InlineData(5, 48, 50)]
        public void BackstagePass_Quality_Should_NeverExceed50(
            int initialSellIn, int initialQuality, int expectedQuality)
        {
            // Arrange
            IList<Item> items = new List<Item>
            {
                Item.Create(BackStagePassName, initialSellIn, initialQuality)
            };
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(expectedQuality, items[0].Quality);
        }

        #endregion

        #region Sulfuras Tests

        [Theory]
        [InlineData(10, 80, 80, 0)]
        [InlineData(0, 80, 80, 0)]
        [InlineData(-1, 80, 80, 0)]
        [InlineData(100, 80, 80, 0)]
        public void Sulfuras_Should_NeverChangeQualityOrSellIn(
            int initialSellIn, int initialQuality, int expectedQuality, int expectedSellIn)
        {
            // Arrange
            IList<Item> items = new List<Item>
            {
                Item.Create(Sulfuras, initialSellIn, initialQuality)
            };
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(expectedQuality, items[0].Quality);
            Assert.Equal(expectedSellIn, items[0].SellIn);
        }

        #endregion

        #region Multiple Items Tests

        [Fact]
        public void Should_HandleMultipleItems_Correctly()
        {
            // Arrange
            IList<Item> items = new List<Item>
            {
                Item.Create("Normal Item", 10, 10),
                Item.Create(AgedBrie, 10, 10),
                Item.Create(Sulfuras, 10, 80),
                Item.Create(BackStagePassName, 10, 10)
            };
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(9, items[0].Quality);   // Normal item decreases by 1
            Assert.Equal(9, items[0].SellIn);

            Assert.Equal(11, items[1].Quality);  // Aged Brie increases by 1
            Assert.Equal(9, items[1].SellIn);

            Assert.Equal(80, items[2].Quality);  // Sulfuras stays the same
            Assert.Equal(0, items[2].SellIn);

            Assert.Equal(12, items[3].Quality);  // Backstage pass increases by 2 (10 days)
            Assert.Equal(9, items[3].SellIn);
        }

        [Fact]
        public void Should_HandleEmptyItemsList()
        {
            // Arrange
            IList<Item> items = new List<Item>();
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Empty(items);
        }

        [Fact]
        public void Should_UpdateAllItems_InCorrectOrder()
        {
            // Arrange
            IList<Item> items = new List<Item>
            {
                Item.Create("+5 Dexterity Vest", 10, 20),
                Item.Create(AgedBrie, 2, 0),
                Item.Create("Elixir of the Mongoose", 5, 7),
                Item.Create(Sulfuras, 0, 80),
                Item.Create(BackStagePassName, 15, 20)
            };
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(19, items[0].Quality);
            Assert.Equal(9, items[0].SellIn);

            Assert.Equal(1, items[1].Quality);
            Assert.Equal(1, items[1].SellIn);

            Assert.Equal(6, items[2].Quality);
            Assert.Equal(4, items[2].SellIn);

            Assert.Equal(80, items[3].Quality);
            Assert.Equal(0, items[3].SellIn);

            Assert.Equal(21, items[4].Quality);
            Assert.Equal(14, items[4].SellIn);
        }

        #endregion

        #region Edge Cases

        [Theory]
        [InlineData("Normal Item", 1, 1, 0, 0)]
        [InlineData("Normal Item", 0, 2, 0, -1)]
        public void NormalItem_Should_HandleQualityAtBoundary(
            string itemName, int initialSellIn, int initialQuality,
            int expectedQuality, int expectedSellIn)
        {
            // Arrange
            IList<Item> items = new List<Item>
            {
                Item.Create(itemName, initialSellIn, initialQuality)
            };
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(expectedQuality, items[0].Quality);
            Assert.Equal(expectedSellIn, items[0].SellIn);
        }

        [Fact]
        public void Should_HandleMultipleUpdates()
        {
            // Arrange
            IList<Item> items = new List<Item>
            {
                Item.Create("Normal Item", 3, 10)
            };
            var app = new GildedRose(items);

            // Act - Run multiple days
            app.UpdateQuality(); // Day 1: Quality=9, SellIn=2
            app.UpdateQuality(); // Day 2: Quality=8, SellIn=1
            app.UpdateQuality(); // Day 3: Quality=7, SellIn=0
            app.UpdateQuality(); // Day 4: Quality=5, SellIn=-1 (degrades twice)

            // Assert
            Assert.Equal(5, items[0].Quality);
            Assert.Equal(-1, items[0].SellIn);
        }

        [Theory]
        [InlineData(11, 49, 50)]
        [InlineData(10, 48, 50)]
        public void BackstagePass_Should_NotExceed50_WhenIncreasingByMultiple(
            int initialSellIn, int initialQuality, int expectedQuality)
        {
            // Arrange
            IList<Item> items = new List<Item>
            {
                Item.Create(BackStagePassName, initialSellIn, initialQuality)
            };
            var app = new GildedRose(items);

            // Act
            app.UpdateQuality();

            // Assert
            Assert.Equal(expectedQuality, items[0].Quality);
            Assert.True(items[0].Quality <= 50, "Quality should never exceed 50");
        }

        #endregion
    }
}


