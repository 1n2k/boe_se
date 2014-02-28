using System;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace boe_se
{
	namespace Engine{
		public class Market
		{			
			/// <summary>
			/// The items.
			/// </summary>
            public List<GItem> Items;
            private WebClient WClient;

			private static Lazy<Market> Instance = new Lazy<Market>(() => new Market());
			public static Market GetInstance {
				get {
					return Instance.Value;
				}
			}

			private Market ()
			{
                WClient = new WebClient();
                string allItems = WClient.DownloadString("http://www.gw2spidy.com/api/v0.9/json/all-items/all");
                Items=JsonConvert.DeserializeObject<Dictionary<string, List<GItem>>>(allItems)["result"];
			}
			
			public GItem GetItem(int id) {
                return JsonConvert.DeserializeObject<Dictionary<string, GItem>>(WClient.DownloadString("http://www.gw2spidy.com/api/v0.9/json/item/" + id))["result"];
			}
			
			public GItem GetItem(string name){
                return Items.Find((GItem g) => g.Name == name);
			}
		}
	}
}