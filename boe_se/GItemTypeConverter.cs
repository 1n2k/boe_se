using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Newtonsoft.Json;

namespace boe_se
{
    namespace Engine
    {
        public static class GItemTypeConverter
        {
            public class GType
            {
                public class GSubType
                {
                    [JsonProperty("name")]
                    public string Name;
                    [JsonProperty("id")]
                    public int Id;
                }
                [JsonProperty("name")]
                public string Name;
                [JsonProperty("id")]
                public int Id;
                [JsonProperty("subtypes")]
                public List<GSubType> Subtypes;
            }

            private class GAllType
            {
                [JsonProperty("results")]
                public List<GType> Results;
            }

            public static List<GType> AllItems = null;

            public static Tuple<string,string> TypeToName(this GItem item)
            {
                if(AllItems == null)
                    AllItems = JsonConvert.DeserializeObject<GAllType>(new WebClient().DownloadString("http://www.gw2spidy.com/api/v0.9/json/types")).Results;
                return new Tuple<string, string>(AllItems[item.TypeId].Name, AllItems[item.TypeId].Subtypes[item.SubTypeId].Name);
            }
        }
    }
}
