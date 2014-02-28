using System;
using System.Net;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace boe_se
{
    namespace Engine
    {
        public class Market
        {
            /// <summary>
            /// The items.
            /// </summary>
            public List<GItem> Items;
            private WebClient WClient;

            private static Lazy<Market> Instance = new Lazy<Market>(() => new Market());
            public static Market GetInstance
            {
                get
                {
                    return Instance.Value;
                }
            }

            class GAllItemsResponse
            {
                [JsonProperty("results")]
                public List<GItem> Result;
            }

            private Market()
            {
                WClient = new WebClient();
                Items = JsonConvert.DeserializeObject<GAllItemsResponse>(WClient.DownloadString("http://www.gw2spidy.com/api/v0.9/json/all-items/all")).Result;

            }

            public GItem GetItem(int id)
            {
                return Items.Find((GItem g) => g.DataId == id);
                //return JsonConvert.DeserializeObject<Dictionary<string, GItem>>(WClient.DownloadString("http://www.gw2spidy.com/api/v0.9/json/item/" + id))["result"];
            }

            public GItem GetItem(string name)
            {
                return Items.Find((GItem g) => g.Name == name);
            }
        }
    }
}