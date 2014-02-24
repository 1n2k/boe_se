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


            [JsonProperty("listing_datetime")]
            private string listingDatetime
            {
                get;
                set;
            }
            public DateTime ListingDatetime {
                get
                {
                    return Convert.ToDateTime(listingDatetime.TrimEnd(new[] { ' ', 'U', 'T', 'C' }));
                }
            }
        }

		public class GItem
		{
            [JsonProperty("name")]
			public string Name{ get; private set; }

            [JsonProperty("data_id")]
			public int DataId{ get; private set; }

            [JsonProperty("rarity")]
			public int Rarity{ get; private set; }

            [JsonProperty("restriction_level")]
			public int RestrictionLevel{ get; private set; }

            [JsonProperty("img")]
			public string ImageURL{ get; private set; }

            [JsonProperty("type_id")]
			public int TypeId{ get; private set; }

            [JsonProperty("sub_type_id")]
			public int SubTypeId{ get; private set; }

            [JsonProperty("price_last_changed")]
            private string priceLastChanged
            {
                get; set;
            }
            public DateTime PriceLastChanged
            {
                get
                {
                    return Convert.ToDateTime(priceLastChanged.TrimEnd(new [] {' ' ,'U', 'T', 'C'}));
                }
            }

			/// <summary>
			/// Returns a <see cref="System.String"/> that represents the current <see cref="boe_se.Item"/>.
			/// </summary>
			/// <returns>
			/// A <see cref="System.String"/> that represents the current <see cref="boe_se.Item"/>.
			/// </returns>
			public override string ToString ()
			{
				return string.Format ("[Item: Name={0}, DataId={1}, Rarity={2}, RestrictionLevel={3}, ImageURL={4}, TypeId={5}, SubTypeId={6}, PriceLastChanged={7}]", Name, DataId, Rarity, RestrictionLevel, ImageURL, TypeId, SubTypeId, PriceLastChanged);
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
			public override bool Equals (object obj)
			{
				return this.DataId == ((GItem)obj).DataId;
			}
		
			/// <summary>
			/// Serves as a hash function for a <see cref="boe_se.Item"/> object.
			/// </summary>
			/// <returns>
			/// A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.
			/// </returns>
			public override int GetHashCode ()
			{
				return this.DataId;
			}

			/// <summary>
			/// Get most recent sell and buy data.
			/// </summary>
			public void Refresh(){

			}

			private List<Tuple<DateTime,int,int>> sellList;
			public List<Tuple<DateTime,int,int>> SellList {
				get {
                    return null;
				}
			}
			
			private List<Tuple<DateTime,int,int>> buyList;
			public List<Tuple<DateTime,int,int>> BuyList {
				get {
                    return null;
				}
			}

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
                    return new Tuple<int, int>(0, 0);
                }

            }
		}
	}
}

	