using System;
using System.Collections.Generic;
using System.Net;
using System.Diagnostics;
using System.Runtime.Serialization.Json;

namespace boe_se
{
	namespace Engine
	{
		public class Item
		{
			public string Name{ get; private set; }

			public int DataId{ get; private set; }

			public int Rarity{ get; private set; }

			public int RestrictionLevel{ get; private set; }

			public string ImageURL{ get; private set; }

			public int TypeId{ get; private set; }

			public int SubTypeId{ get; private set; }

			public DateTime PriceLastChanged { get; private set; }

			private void ParseInfo (string resultString)
			{
				Console.WriteLine (resultString);
				var splitData = resultString.Trim (new [] {'{','}'}).Split (',');
				foreach (var item in splitData) {
					var splItem = item.Split (':');
					switch (splItem [0].Trim ('\"')) {
					case "data_id":
						DataId = Convert.ToInt32 (splItem [1]);
						break;
					case "name":
						Name = splItem [1].Trim ('\"');
						break;
					case "rarity":
						Rarity = Convert.ToInt32 (splItem [1]);
						break;
					case "restriction_level":
						RestrictionLevel = Convert.ToInt32 (splItem [1]);
						break;
					case "img":
						ImageURL = splItem [1].Trim ('\"');
						break;
					case "type_id":
						TypeId = Convert.ToInt32 (splItem [1]);
						break;
					case "sub_type_id":
						SubTypeId = Convert.ToInt32 (splItem [1]);
						break;
					case "price_last_changed":
						PriceLastChanged = DateTime.Parse ((splItem [1].Trim ('\"')).Split (' ') [0]);
						break;
					}
				}
				return;
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="boe_se.Item"/> class.
			/// </summary>
			/// <param name='dataId'>
			/// Data identifier.
			/// </param>
			public Item (int dataId)
			{
				WebClient wc = new WebClient ();
				ParseInfo (wc.DownloadString ("http://www.gw2spidy.com/api/v0.9/json/item/" + dataId)
			          .Replace ("\n", "")
			          .Replace ("\t", "")
			          .Replace ("\r", "")
			          .Replace ("{\"result\":", "")
			          .TrimEnd ('}') + "}"
				);
			}

			public Item (string name)
			{

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
				return this.DataId == ((Item)obj).DataId;
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

				}
			}
			
			private List<Tuple<DateTime,int,int>> buyList;
			public List<Tuple<DateTime,int,int>> BuyList {
				get {

				}
			}
		}
	}
}

	