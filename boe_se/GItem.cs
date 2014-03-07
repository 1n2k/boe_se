using System;
using System.Collections.Generic;
using System.Net;
using System.Diagnostics;
using System.Runtime.Serialization.Json;
using System.Xml;

using Newtonsoft.Json;

namespace boe_se
{
    namespace Engine
    {
        public class GListing
        {
            [JsonProperty("unit_price")]
            public int UnitPrice { get; set; }

            [JsonProperty("quantity")]
            public int Quantity { get; set; }

            [JsonProperty("listings")]
            public int Listings { get; set; }

            [JsonProperty("listing_datetime")]
            private string listingDatetime
            {
                get;
                set;
            }
            public DateTime ListingDatetime
            {
                get
                {
                    return Convert.ToDateTime(listingDatetime.TrimEnd(new[] { ' ', 'U', 'T', 'C' }));
                }
            }

            public static implicit operator Tuple<DateTime, int, int>(GListing gListing)
            {
                return new Tuple<DateTime, int, int>(gListing.ListingDatetime, gListing.Quantity, gListing.UnitPrice);
            }
        }

        public partial class GItem
        {
            [JsonProperty("name")]
            public string Name { get; private set; }

            [JsonProperty("data_id")]
            public int DataId { get; private set; }

            [JsonProperty("rarity")]
            public int Rarity { get; private set; }

            [JsonProperty("restriction_level")]
            public int RestrictionLevel { get; private set; }

            [JsonProperty("img")]
            public string ImageURL { get; private set; }

            [JsonProperty("type_id")]
            public int TypeId { get; private set; }

            [JsonProperty("sub_type_id")]
            public int SubTypeId { get; private set; }

            [JsonProperty("price_last_changed")]
            private string priceLastChanged
            {
                get;
                set;
            }

            public DateTime PriceLastChanged
            {
                get
                {
                    return Convert.ToDateTime(priceLastChanged.TrimEnd(new[] { ' ', 'U', 'T', 'C' }));
                }
            }

            /// <summary>
            /// Returns a <see cref="System.String"/> that represents the current <see cref="boe_se.Item"/>.
            /// </summary>
            /// <returns>
            /// A <see cref="System.String"/> that represents the current <see cref="boe_se.Item"/>.
            /// </returns>
            public override string ToString()
            {
                return string.Format("[Item: Name={0}, DataId={1}, Rarity={2}, RestrictionLevel={3}, ImageURL={4}, TypeId={5}, SubTypeId={6}, PriceLastChanged={7}]", Name, DataId, Rarity, RestrictionLevel, ImageURL, TypeId, SubTypeId, PriceLastChanged);
            }

            /// <summary>
            /// Determines whether the specified <see cref="System.Object"/> is equal to the current <see cref="boe_se.Item"/>.
            /// </summary>
            /// <param name='obj'>
            /// The <see cref="System.Object"/> to compare with the current <see cref="boe_se.Item"/>.
            /// </param>
            /// <returns>
            /// <c>true</c> if the specified <see cref="System.Object"/> is equal to the current <see cref="boe_se.Item"/>;
            /// otherwise, <c>false</c>.
            /// </returns>
            public override bool Equals(object obj)
            {
                return this.DataId == ((GItem)obj).DataId;
            }

            /// <summary>
            /// Serves as a hash function for a <see cref="boe_se.Item"/> object.
            /// </summary>
            /// <returns>
            /// A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.
            /// </returns>
            public override int GetHashCode()
            {
                return this.DataId;
            }
            
            class GListingsResult
            {
                [JsonProperty("page")]
                public int Page;
                [JsonProperty("last_page")]
                public int LastPage;
                [JsonProperty("results")]
                public List<GListing> Results;
            }

            /// <summary>
            /// Get most recent sell and buy data.
            /// </summary>
            public void Refresh()
            {
                WebClient wc = new WebClient();
                List<Tuple<DateTime, int, int>> list;

                string mode = "sell";
                list = SellList;
                goto NEXT;
            BUY:
                mode = "buy";
                list = BuyList;
            NEXT:
                GListingsResult g = JsonConvert.DeserializeObject<GListingsResult>(wc.DownloadString("http://www.gw2spidy.com/api/v0.9/json/listings/" + this.DataId + "/" + mode + "/1"));

                list = new List<Tuple<DateTime, int, int>>();

                foreach (var item in g.Results)
                    list.Add(item);

                while (g.Page < g.LastPage)
                {
                    g = JsonConvert.DeserializeObject<GListingsResult>(wc.DownloadString("http://www.gw2spidy.com/api/v0.9/json/listings/" + this.DataId + "/"+ mode + "/" + (++g.Page)));

                    foreach (var item in g.Results)
                        list.Add(item);
                }

                if (mode == "sell")
                {
                    SellList = list;
                    goto BUY;
                }
                else
                    BuyList = list;
                BuyList.Sort();
                SellList.Sort();
            }

            public List<Tuple<DateTime, int, int>> SellList { private set; get; }

            public List<Tuple<DateTime, int, int>> BuyList { private set; get; }


            public DateTime First;
            public int IntervalLength = 15;

            /// <summary>
            /// Gets the normalized sell/buy price at time time.
            /// </summary>
            /// <param name="time"></param>
            /// <param name="sell">Specifies whether the result is a sell tuple or a buy tuple</param>
            /// <returns>Tuple of price and </returns>
            public Tuple<int, int> this[DateTime time, bool sell]
            {
                get
                {

                    if (SellList == null || BuyList == null)
                    {
                        Debug.WriteLine("Getting Data for Item " + this.DataId + "\n");
                        this.Refresh();
                    }
                    if (First == default(DateTime))
                        First = sell ? SellList[0].Item1 : BuyList[0].Item1;

                    var list = sell ? SellList : BuyList;
                    DateTime nTime = time.AddMinutes(15);

                    if (time.CompareTo(list[list.Count - 1].Item1) > 0)
                        return null;

                    int i = list.FindIndex((Tuple<DateTime, int, int> a) =>
                        a.Item1.CompareTo(time) >= 0 && a.Item1.CompareTo(nTime) <= 0);

					if(i < 0)
						return new Tuple<int,int>(0,0);

                    if (i >= list.Count)
                        return null;

                    int quantity = 0;
                    decimal price = 0;

                    for (; i < list.Count && list[i].Item1.CompareTo(nTime) <= 0; i++)
                    {
                        price += list[i].Item2 * list[i].Item3;
                        quantity += list[i].Item3;
                    }
                    if (quantity != 0)
                        price /= quantity * (decimal)1.0;
                    else
                        price = 0;

                    return new Tuple<int, int>((int)price, quantity);
                }

            }

            public Tuple<int, int> this[int time, bool sell]
            {
                get
                {
                    if (First == default(DateTime))
                    {
                        var t = this[First, sell];
                    }
                    return this[First.AddMinutes(time * IntervalLength), sell];
                }
            }
		}
    }
}

