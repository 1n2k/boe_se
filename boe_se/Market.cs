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
			public Market Instance {
				get {
					return instance.Value;
				}
			}

			private Market ()
			{

			}


		}
	}
}

