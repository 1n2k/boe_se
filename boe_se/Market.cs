using System;
using System.Collections.Generic;

namespace boe_se
{
	namespace Engine{
		public class Market
		{			
			/// <summary>
			/// The items.
			/// </summary>
			public List<Item> Items;

			private static Lazy<Market> instance;
			public Market getInstance {
				get {
					return instance.Value;
				}
			}

			private Market ()
			{
				WebClient wc = new WebClient ();
				ParseInfo (wc.DownloadString ("http://www.gw2spidy.com/api/v0.9/json/all-items/all")
			}
			
			public Item getItem(int ID) {
				
			}
			
			public Item getItem(string Name){
				
			}
			
			


		}
	}
}

