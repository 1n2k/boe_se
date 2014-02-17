using System;
using System.Net;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Xml;

namespace boe_se
{
	namespace Engine{
		public class Market
		{			
			/// <summary>
			/// The items.
			/// </summary>
            public List<Item> Items;
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
			
			public Item getItem(int ID) {
                string s = wc.DownloadString("http://www.gw2spidy.com/api/v0.9/json/item/" + ID);

                var reader = JsonReaderWriterFactory.CreateJsonReader(wc.Encoding.GetBytes(s), new XmlDictionaryReaderQuotas());
                Console.WriteLine(wc.Encoding.GetBytes(s));
                //reader.Get;
                reader.Read();
                //var g = reader.GetAttribute(0);
                reader.MoveToFirstAttribute();
                reader.ReadAttributeValue();
                //var r2 = reader.ReadSubtree();
                //r2.Read();
                //var r3 = r2.ReadSubtree();
                return new Item( reader );

			}
			
			public Item getItem(string Name){
                return null;
			}
		}
	}
}

