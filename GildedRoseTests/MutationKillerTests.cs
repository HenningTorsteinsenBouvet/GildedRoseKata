using GildedRoseKata;
using System.Collections.Generic;
using Xunit;

namespace GildedRoseTests
{
    /// <summary>
    /// Additional tests to kill survived mutants from Stryker mutation testing
    /// These tests specifically target boundary conditions at quality = 50
    /// </summary>
    public class MutationKillerTests
    {
        private readonly string _backStagePassName = "Backstage passes to a TAFKAL80ETC concert";

        /// <summary>
        /// Kills Mutant #36: Tests that backstage pass quality at exactly 50 
        /// does not increase when SellIn is between 6 and 10 days
        /// </summary>
        [Theory]
        [InlineData(10, 50, 50)]
        [InlineData(8, 50, 50)]
        [InlineData(6, 50, 50)]
        public void Should_NotIncreaseBackstagePassQualityOver50_WhenSellInBetween6And10(
            int initialSellIn, int initialQuality, int expectedQuality)
        {
            // Arrange
            IList<Item> Items = new List<Item> 
            { 
                new Item 
                { 
                    Name = _backStagePassName, 
                    SellIn = initialSellIn, 
                    Quality = initialQuality 
                } 
            };

            // Act
            GildedRose app = new(Items);
            app.UpdateQuality();

            // Assert
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        /// <summary>
        /// Kills Mutant #45: Tests that backstage pass quality at exactly 50 
        /// does not increase when SellIn is between 1 and 5 days
        /// </summary>
        [Theory]
        [InlineData(5, 50, 50)]
        [InlineData(3, 50, 50)]
        [InlineData(1, 50, 50)]
        public void Should_NotIncreaseBackstagePassQualityOver50_WhenSellInBetween1And5(
            int initialSellIn, int initialQuality, int expectedQuality)
        {
            // Arrange
            IList<Item> Items = new List<Item> 
            { 
                new Item 
                { 
                    Name = _backStagePassName, 
                    SellIn = initialSellIn, 
                    Quality = initialQuality 
                } 
            };

            // Act
            GildedRose app = new(Items);
            app.UpdateQuality();

            // Assert
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        /// <summary>
        /// Kills Mutants #63, #64, #65: Tests that regular items degrade quality 
        /// twice as fast (by 2) when the sell-by date has passed (SellIn < 0)
        /// Also verifies quality cannot go below 0
        /// </summary>
        [Theory]
        [InlineData(-1, 10, 8)]   // Quality degrades by 2 when expired
        [InlineData(-1, 2, 0)]    // Quality degrades by 2 but stops at 0
        [InlineData(-1, 1, 0)]    // Quality degrades but can't go negative
        [InlineData(-5, 20, 18)]  // Multiple days expired still degrades by 2
        public void Should_DegradeQualityTwiceAsFast_WhenItemIsExpired(
            int initialSellIn, int initialQuality, int expectedQuality)
        {
            // Arrange
            IList<Item> Items = new List<Item> 
            { 
                new Item 
                { 
                    Name = "Regular Item", 
                    SellIn = initialSellIn, 
                    Quality = initialQuality 
                } 
            };

            // Act
            GildedRose app = new(Items);
            app.UpdateQuality();

            // Assert
            Assert.Equal(expectedQuality, Items[0].Quality);
        }

        /// <summary>
        /// Additional test: Verifies backstage pass quality approaches 50 correctly
        /// and then stays at 50 across multiple updates
        /// </summary>
        [Fact]
        public void Should_KeepBackstagePassQualityAt50_AfterMultipleUpdates()
        {
            // Arrange
            IList<Item> Items = new List<Item> 
            { 
                new Item 
                { 
                    Name = _backStagePassName, 
                    SellIn = 10, 
                    Quality = 49 
                } 
            };

            GildedRose app = new(Items);

            // Act & Assert - First update should reach 50
            app.UpdateQuality();
            Assert.Equal(50, Items[0].Quality);

            // Act & Assert - Second update should stay at 50
            app.UpdateQuality();
            Assert.Equal(50, Items[0].Quality);

            // Act & Assert - Third update should stay at 50
            app.UpdateQuality();
            Assert.Equal(50, Items[0].Quality);
        }
    }
}
