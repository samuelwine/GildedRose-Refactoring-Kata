using System.Collections.Generic;

namespace GildedRoseKata
{
    public class GildedRose
    {
        IList<Item> Items;
        public GildedRose(IList<Item> Items)
        {
            this.Items = Items;
        }

        public void UpdateQuality()
        {
            const string AgedBrie = "Aged Brie";
            const string BackStage = "Backstage passes to a TAFKAL80ETC concert";
            const string Sulfuras = "Sulfuras, Hand of Ragnaros";

            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name == Sulfuras)
                {
                    return;
                }

                if (Items[i].Name != AgedBrie && Items[i].Name != BackStage)
                {
                    if (Items[i].Quality > 0)
                    {
                        if (Items[i].Name != Sulfuras)
                        {
                            Items[i].Quality -= 1;
                        }
                    }
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality += 1;

                        if (Items[i].Name == BackStage)
                        {
                            if (Items[i].SellIn < 11)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality += 1;
                                }
                            }

                            if (Items[i].SellIn < 6)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality += 1;
                                }
                            }
                        }
                    }
                }

                if (Items[i].Name != Sulfuras)
                {
                    Items[i].SellIn -= 1;
                }

                if (Items[i].SellIn < 0)
                {
                    if (Items[i].Name != AgedBrie)
                    {
                        if (Items[i].Name != BackStage)
                        {
                            if (Items[i].Quality > 0)
                            {
                                if (Items[i].Name != Sulfuras)
                                {
                                    Items[i].Quality -= 1;
                                }
                            }
                        }
                        else
                        {
                            Items[i].Quality = 0;
                        }
                    }
                    else
                    {
                        if (Items[i].Quality < 50)
                        {
                            Items[i].Quality += 1;
                        }
                    }
                }
            }
        }
    }
}
