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
            private WebClient wc;

			private static Lazy<Market> instance = new Lazy<Market>(() => new Market());
			public static Market getInstance {
				get {
					return instance.Value;
				}
			}

			private Market ()
			{
                wc = new WebClient();
			}
			
			public GItem getItem(int ID) {
                return JsonConvert.DeserializeObject<Dictionary<string, GItem>>(wc.DownloadString("http://www.gw2spidy.com/api/v0.9/json/item/" + ID))["result"];
			}
			
			public GItem getItem(string Name){
                return Items.Find((GItem g) => g.Name == Name);
			}
		}
	}
}

