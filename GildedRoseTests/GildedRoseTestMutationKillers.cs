using Xunit;
using System.Collections.Generic;
using GildedRoseKata;

namespace GildedRoseTests
{
    /// <summary>
    /// Tests specifically designed to kill mutation testing mutants
    /// </summary>
    public class GildedRoseTestMutationKillers
    {
        private readonly string _backStagePassName = "Backstage passes to a TAFKAL80ETC concert";
        private readonly string _sulfuras = "Sulfuras, Hand of Ragnaros";
        private readonly string _agedBrie = "Aged Brie";

        [Fact]
        public void Should_DecreaseQualityByExactlyOne_ForNormalItem_WhenSellInIsPositive()
        {
            // This test kills mutants that try to change the quality decrement value
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 5, Quality = 10 } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            Assert.Equal(9, Items[0].Quality); // Must be exactly 9, not 8 or 11
            Assert.Equal(4, Items[0].SellIn);
        }

        [Fact]
        public void Should_DecreaseQualityByExactlyTwo_ForNormalItem_WhenSellInIsZeroOrNegative()
        {
            // This test kills mutants that try to change the quality decrement rate after sell date
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 10 } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            Assert.Equal(8, Items[0].Quality); // Must be exactly 8, not 9 or 7
            Assert.Equal(-1, Items[0].SellIn);
        }

        [Fact]
        public void Should_IncreaseAgedBrieQualityByExactlyOne_WhenSellInIsPositive()
        {
            // This test kills mutants that try to change Aged Brie quality increment
            IList<Item> Items = new List<Item> { new Item { Name = _agedBrie, SellIn = 5, Quality = 10 } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            Assert.Equal(11, Items[0].Quality); // Must be exactly 11, not 12 or 10
            Assert.Equal(4, Items[0].SellIn);
        }

        [Fact]
        public void Should_IncreaseAgedBrieQualityByExactlyTwo_WhenSellInIsZeroOrNegative()
        {
            // This test kills mutants that try to change Aged Brie quality increment after expiration
            IList<Item> Items = new List<Item> { new Item { Name = _agedBrie, SellIn = 0, Quality = 10 } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            Assert.Equal(12, Items[0].Quality); // Must be exactly 12, not 11 or 13
            Assert.Equal(-1, Items[0].SellIn);
        }

        [Fact]
        public void Should_IncreaseBackstagePassQualityByExactlyOne_WhenSellInIsAbove10()
        {
            // This test kills mutants that try to change backstage pass increment when > 10 days
            IList<Item> Items = new List<Item> { new Item { Name = _backStagePassName, SellIn = 15, Quality = 10 } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            Assert.Equal(11, Items[0].Quality); // Must be exactly 11, not 12 or 13
            Assert.Equal(14, Items[0].SellIn);
        }

        [Fact]
        public void Should_IncreaseBackstagePassQualityByExactlyTwo_WhenSellInIs10()
        {
            // This test kills mutants that try to change the quality increment at exactly 10 days
            IList<Item> Items = new List<Item> { new Item { Name = _backStagePassName, SellIn = 10, Quality = 10 } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            Assert.Equal(12, Items[0].Quality); // Must be exactly 12, not 11 or 13
            Assert.Equal(9, Items[0].SellIn);
        }

        [Fact]
        public void Should_IncreaseBackstagePassQualityByExactlyTwo_WhenSellInIs6()
        {
            // This test kills mutants that try to change quality increment between 6-10 days
            IList<Item> Items = new List<Item> { new Item { Name = _backStagePassName, SellIn = 6, Quality = 10 } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            Assert.Equal(12, Items[0].Quality); // Must be exactly 12, not 11 or 13
            Assert.Equal(5, Items[0].SellIn);
        }

        [Fact]
        public void Should_IncreaseBackstagePassQualityByExactlyThree_WhenSellInIs5()
        {
            // This test kills mutants that try to change the threshold from 5 days
            IList<Item> Items = new List<Item> { new Item { Name = _backStagePassName, SellIn = 5, Quality = 10 } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            Assert.Equal(13, Items[0].Quality); // Must be exactly 13, not 12 or 14
            Assert.Equal(4, Items[0].SellIn);
        }

        [Fact]
        public void Should_SetBackstagePassQualityToExactlyZero_WhenConcertPasses()
        {
            // This test kills mutants that try to keep some quality after concert
            IList<Item> Items = new List<Item> { new Item { Name = _backStagePassName, SellIn = 0, Quality = 50 } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            Assert.Equal(0, Items[0].Quality); // Must be exactly 0, not any other value
            Assert.Equal(-1, Items[0].SellIn);
        }

        [Fact]
        public void Should_NotChangeSulfurasQualityOrSellIn_EverythingMustRemainExactly()
        {
            // This test kills mutants that try to modify Sulfuras in any way
            IList<Item> Items = new List<Item> { new Item { Name = _sulfuras, SellIn = 5, Quality = 80 } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            Assert.Equal(80, Items[0].Quality); // Must be exactly 80
            Assert.Equal(5, Items[0].SellIn); // Must be exactly 5
        }

        [Fact]
        public void Should_RespectQualityNeverNegative_ForAllItems()
        {
            // This test kills mutants that allow negative quality
            IList<Item> Items = new List<Item> 
            { 
                new Item { Name = "foo", SellIn = -10, Quality = 1 }
            };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            Assert.True(Items[0].Quality >= 0, "Quality went negative!");
            Assert.Equal(0, Items[0].Quality);
        }

        [Fact]
        public void Should_RespectQualityNeverAbove50_ForAgedBrie()
        {
            // This test kills mutants that allow quality > 50 for Aged Brie
            IList<Item> Items = new List<Item> 
            { 
                new Item { Name = _agedBrie, SellIn = -10, Quality = 49 }
            };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            Assert.True(Items[0].Quality <= 50, "Quality went above 50!");
            Assert.Equal(50, Items[0].Quality);
        }

        [Fact]
        public void Should_RespectQualityNeverAbove50_ForBackstagePasses()
        {
            // This test kills mutants that allow quality > 50 for backstage passes
            IList<Item> Items = new List<Item> 
            { 
                new Item { Name = _backStagePassName, SellIn = 5, Quality = 49 }
            };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            Assert.True(Items[0].Quality <= 50, "Quality went above 50!");
            Assert.Equal(50, Items[0].Quality);
        }

        [Fact]
        public void Should_DecreaseSellInByExactlyOne_ForNormalItems()
        {
            // This test kills mutants that change the SellIn decrement rate
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 10, Quality = 10 } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            Assert.Equal(9, Items[0].SellIn); // Must be exactly 9, not 10 or 8
        }

        [Fact]
        public void Should_DecreaseSellInByExactlyOne_ForAgedBrie()
        {
            // This test kills mutants that change Aged Brie SellIn behavior
            IList<Item> Items = new List<Item> { new Item { Name = _agedBrie, SellIn = 10, Quality = 10 } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            Assert.Equal(9, Items[0].SellIn); // Must be exactly 9
        }

        [Fact]
        public void Should_DecreaseSellInByExactlyOne_ForBackstagePasses()
        {
            // This test kills mutants that change backstage pass SellIn behavior
            IList<Item> Items = new List<Item> { new Item { Name = _backStagePassName, SellIn = 10, Quality = 10 } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            Assert.Equal(9, Items[0].SellIn); // Must be exactly 9
        }

        [Theory]
        [InlineData(11, 10, 11)] // Boundary: 11 days should increment by 1
        [InlineData(10, 10, 12)] // Boundary: 10 days should increment by 2
        public void Should_UseCorrectBoundary_ForBackstagePass10DayThreshold(int sellIn, int quality, int expectedQuality)
        {
            // This test kills mutants that change the comparison operators for the 10-day threshold
            IList<Item> Items = new List<Item> { new Item { Name = _backStagePassName, SellIn = sellIn, Quality = quality } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(6, 10, 12)] // Boundary: 6 days should increment by 2
        [InlineData(5, 10, 13)] // Boundary: 5 days should increment by 3
        public void Should_UseCorrectBoundary_ForBackstagePass5DayThreshold(int sellIn, int quality, int expectedQuality)
        {
            // This test kills mutants that change the comparison operators for the 5-day threshold
            IList<Item> Items = new List<Item> { new Item { Name = _backStagePassName, SellIn = sellIn, Quality = quality } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Theory]
        [InlineData(1, 10, 9)] // SellIn = 1: quality should decrease by 1
        [InlineData(0, 10, 8)] // SellIn = 0: quality should decrease by 2
        public void Should_UseCorrectBoundary_ForNormalItemExpiration(int sellIn, int quality, int expectedQuality)
        {
            // This test kills mutants that change the comparison operators for normal item expiration
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = sellIn, Quality = quality } };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        [Fact]
        public void Should_ProcessEmptyList_WithoutErrors()
        {
            // This test ensures the loop bounds are correct
            IList<Item> Items = new List<Item>();

            GildedRose app = new(Items);
            app.UpdateQuality(); // Should not throw
            
            Assert.Empty(Items);
        }

        [Fact]
        public void Should_ProcessExactlyAllItems_InOrderWithoutSkipping()
        {
            // This test kills mutants that change the loop increment/decrement
            IList<Item> Items = new List<Item> 
            { 
                new Item { Name = "Item1", SellIn = 10, Quality = 10 },
                new Item { Name = "Item2", SellIn = 10, Quality = 10 },
                new Item { Name = "Item3", SellIn = 10, Quality = 10 }
            };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            // All items should be processed exactly once
            Assert.Equal(9, Items[0].Quality);
            Assert.Equal(9, Items[1].Quality);
            Assert.Equal(9, Items[2].Quality);
        }

        [Fact]
        public void Should_NotModifyWrongItem_WhenProcessingList()
        {
            // This test ensures the correct item index is used
            IList<Item> Items = new List<Item> 
            { 
                new Item { Name = _sulfuras, SellIn = 10, Quality = 80 },
                new Item { Name = "foo", SellIn = 10, Quality = 10 }
            };

            GildedRose app = new(Items);
            app.UpdateQuality();
            
            Assert.Equal(80, Items[0].Quality); // Sulfuras unchanged
            Assert.Equal(10, Items[0].SellIn);  // Sulfuras SellIn unchanged
            Assert.Equal(9, Items[1].Quality);  // Normal item decreased
            Assert.Equal(9, Items[1].SellIn);   // Normal item SellIn decreased
        }
    }
}
