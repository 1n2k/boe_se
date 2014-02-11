using System;
using System.Collections.Generic;
using System.Net;
using System.Diagnostics;

namespace boe_se
{
	public class Item
	{
		public string Name{get;private set;}
		public int DataId{get;private set;}
		public int Rarity{get;private set;}
		public int RestrictionLevel{get;private set;}
		public string ImageURL{get;private set;}
		public int TypeId{get;private set;}
		public int SubTypeId{get;private set;}
		public DateTime PriceLastChanged { get; private set; }

		private void ParseInfo (string resultString)
		{
			Console.WriteLine (resultString);
			var splitData = resultString.Trim (new [] {'{','}'}).Split (',');
			foreach (var item in splitData) {
				var splItem = item.Split(':');
				switch(splItem[0].Trim ('\"')){
				case "data_id":
					DataId = Convert.ToInt32 (splItem[1]);
					break;
				case "name":
					Name = splItem[1].Trim ('\"');
					break;
				case "rarity":
					Rarity = Convert.ToInt32 (splItem[1]);
					break;
				case "restriction_level":
					RestrictionLevel = Convert.ToInt32 (splItem[1]);
					break;
				case "img":
					ImageURL = splItem[1].Trim ('\"');
					break;
				case "type_id":
					TypeId =  Convert.ToInt32 (splItem[1]);
					break;
				case "sub_type_id":
					SubTypeId =  Convert.ToInt32 (splItem[1]);
					break;
				case "price_last_changed":
					PriceLastChanged = DateTime.Parse(splItem[1].Trim('\"'));
					break;
				}
			}
			return;
		}

		public Item (int dataId)
		{
			WebClient wc = new WebClient();
			ParseInfo(wc.DownloadString("http://www.gw2spidy.com/api/v0.9/json/item/" + dataId)
			          .Replace ("\n","")
			          .Replace("\t","")
			          .Replace("\r","")
			          .Replace("{\"result\":","")
			          .TrimEnd('}') + "}");
		}
		public Item (string name)
		{

		}


		public static List<Item> Items;
	}

}

