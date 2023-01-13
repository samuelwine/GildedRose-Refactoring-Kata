using GildedRoseKata;
using System.Collections.Generic;
using Xunit;

namespace GildedRoseTests
{
    public class GildedRoseTests
    {
        [Fact]
        public void DegradesDaily()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Item", SellIn = 10, Quality = 15 } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.True(Items[0].Quality < 15);
        }

        [Fact]
        public void DegradesDoubleIfSellInIsZeroOrLess()
        {
            IList<Item> Items = new List<Item>
            {
                new Item { Name = "Item", SellIn = 5, Quality = 15 },
                new Item { Name = "Item", SellIn = 0, Quality = 15 },
            };

            GildedRose app = new(Items);
            app.UpdateQuality();
            var decreaseOfBeforeSellIn = 15 - Items[0].Quality;
            var decreaseOfAfterSellIn = 15 - Items[1].Quality;
            Assert.True(decreaseOfBeforeSellIn * 2 == decreaseOfAfterSellIn);
        }

        [Theory]
        [InlineData(-1, 1)]
        [InlineData(15, 0)]
        public void NeverDegradesToLessThanZero(int sellIn, int quality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Item", SellIn = sellIn, Quality = quality } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(0, Items[0].Quality);
        }

        [Theory]
        [InlineData(-1, 5)]
        [InlineData(15, 5)]
        public void AgedBrieIncreasesInQuality(int sellIn, int quality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = sellIn, Quality = quality } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.True(Items[0].Quality > quality);
        }

        [Theory]
        [InlineData("Aged Brie", 15, 50)]
        public void QualityCannotBeMoreThanFifty(string name, int sellIn, int quality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = name, SellIn = sellIn, Quality = quality } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.True(Items[0].Quality <= 50);
        }

        [Theory]
        [InlineData(15, 80)]
        [InlineData(10, 80)]
        [InlineData(5, 80)]
        [InlineData(0, 80)]
        [InlineData(-1, 80)]
        [InlineData(-10, 80)]
        public void SulfurasQualityIsConstant(int sellIn, int quality)
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = sellIn, Quality = quality } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.True(Items[0].Quality == 80);
        }

        [Fact]
        public void SulfurasSellInDoesNotDecrease()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 15, Quality = 80 } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.True(Items[0].SellIn == 15);
        }

        [Theory]
        [InlineData(9, 15, 17)]
        [InlineData(10, 15, 17)]
        [InlineData(11, 15, 16)]
        public void BackstagePassesIncreasesQualityByTwoWhenTenDaysOrLess(int sellIn, int quality, int expectedResult)
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = quality } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedResult, Items[0].Quality);
        }

        [Theory]
        [InlineData(4, 15, 18)]
        [InlineData(5, 15, 18)]
        [InlineData(6, 15, 17)]
        public void BackstagePassesIncreasesQualityByThreeWhenFiveDaysOrLess(int sellIn, int quality, int expectedResult)
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = quality } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedResult, Items[0].Quality);
        }

        [Theory]
        [InlineData(0, 15, 0)]
        [InlineData(0, 15, 0)]
        public void BackstagePassesDecreasesQualityToZeroAfterTheConcert(int sellIn, int quality, int expectedResult)
        {
            IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = sellIn, Quality = quality } };
            GildedRose app = new(Items);
            app.UpdateQuality();
            Assert.Equal(expectedResult, Items[0].Quality);
        }


    }
}
