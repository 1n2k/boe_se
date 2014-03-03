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
            [JsonProperty("unit_price")]
            public int UnitPrice { get; private set; }

            [JsonProperty("quantity")]
            public int Quantity { get; private set; }

            [JsonProperty("listings")]
            public int Listings { get; private set; }

            [JsonProperty("listing_datetime")]
            private string listingDatetime
            {
                get;
                set;
            }
            public DateTime ListingDatetime
            {
                get
                {
                    return Convert.ToDateTime(listingDatetime.TrimEnd(new[] { ' ', 'U', 'T', 'C' }));
                }
            }

            public static implicit operator Tuple<DateTime, int, int>(GListing gListing)
            {
                return new Tuple<DateTime, int, int>(gListing.ListingDatetime, gListing.Quantity, gListing.UnitPrice);
            }
        }

        public class GItem
        {
            [JsonProperty("name")]
            public string Name { get; private set; }

            [JsonProperty("data_id")]
            public int DataId { get; private set; }

            [JsonProperty("rarity")]
            public int Rarity { get; private set; }

            [JsonProperty("restriction_level")]
            public int RestrictionLevel { get; private set; }

            [JsonProperty("img")]
            public string ImageURL { get; private set; }

            [JsonProperty("type_id")]
            public int TypeId { get; private set; }

            [JsonProperty("sub_type_id")]
            public int SubTypeId { get; private set; }

            [JsonProperty("price_last_changed")]
            private string priceLastChanged
            {
                get;
                set;
            }

            public DateTime PriceLastChanged
            {
                get
                {
                    return Convert.ToDateTime(priceLastChanged.TrimEnd(new[] { ' ', 'U', 'T', 'C' }));
                }
            }

            /// <summary>
            /// Returns a <see cref="System.String"/> that represents the current <see cref="boe_se.Item"/>.
            /// </summary>
            /// <returns>
            /// A <see cref="System.String"/> that represents the current <see cref="boe_se.Item"/>.
            /// </returns>
            public override string ToString()
            {
                return string.Format("[Item: Name={0}, DataId={1}, Rarity={2}, RestrictionLevel={3}, ImageURL={4}, TypeId={5}, SubTypeId={6}, PriceLastChanged={7}]", Name, DataId, Rarity, RestrictionLevel, ImageURL, TypeId, SubTypeId, PriceLastChanged);
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
            public override bool Equals(object obj)
            {
                return this.DataId == ((GItem)obj).DataId;
            }

            /// <summary>
            /// Serves as a hash function for a <see cref="boe_se.Item"/> object.
            /// </summary>
            /// <returns>
            /// A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.
            /// </returns>
            public override int GetHashCode()
            {
                return this.DataId;
            }



            class GListingsResult
            {
                [JsonProperty("page")]
                public int Page;
                [JsonProperty("last_page")]
                public int LastPage;
                [JsonProperty("results")]
                public List<Tuple<DateTime, int, int>> Results;
            }

            /// <summary>
            /// Get most recent sell and buy data.
            /// </summary>
            public void Refresh()
            {
                WebClient wc = new WebClient();
                List<Tuple<DateTime, int, int>> list;

                string mode = "sell";
                list = SellList;
                goto NEXT;
            BUY:
                mode = "buy";
                list = BuyList;
            NEXT:
                GListingsResult g = JsonConvert.DeserializeObject<GListingsResult>(wc.DownloadString("http://www.gw2spidy.com/api/v0.9/json/listings/" + this.DataId + "/" + mode + "/1"));
                list = g.Results;

                while (g.Page < g.LastPage)
                {
                    g = JsonConvert.DeserializeObject<GListingsResult>(wc.DownloadString("http://www.gw2spidy.com/api/v0.9/json/listings/" + this.DataId + "/"+ mode + "/" + g.Page + 1));
                    list.AddRange(g.Results);
                }

                if (mode == "sell")
                    goto BUY;
            }

            public List<Tuple<DateTime, int, int>> SellList { private set; get; }

            public List<Tuple<DateTime, int, int>> BuyList { private set; get; }


            public DateTime First = new DateTime();
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
                    var list = sell ? SellList : BuyList;
                    DateTime nTime = time.AddMinutes(15);

                    int i = list.FindIndex((Tuple<DateTime, int, int> a) =>
                        a.Item1.CompareTo(time) >= 0 && a.Item1.CompareTo(nTime) <= 0);

                    int quantity = 0;
                    double price = 0;

                    for (; list[i].Item1.CompareTo(nTime) <= 0; i++)
                    {
                        price += list[i].Item2 * list[i].Item3;
                        quantity += list[i].Item3;
                    }
                    price /= quantity * 1.0;

                    return new Tuple<int, int>((int)price, quantity);
                }

            }

            public Tuple<int, int> this[int time, bool sell]
            {
                get
                {
                    return this[First.AddMinutes(time * IntervalLength), sell];
                }
            }
        
            public Tuple<int, int>[] verkaufspreis;
        
            public GItem(Tuple<int, int>[] _verkaufspreis)
            {
                verkaufspreis = _verkaufspreis;
            }
            public int kurs
            {
                get
                {
                    //Bedeutungen der Vergleichswerte:
                    // 1 Kurs steigend => (jetzt) Kaufen
                    // 0 nicht Handeln
                    //-1 Kurs fallend => (jetzt) Verkaufen
                    //-2 keine Aussage (z.B. wegen zu wenig Werten)

                    int a = getAverage(verkaufspreis);
                    int b = getFormation(verkaufspreis);
                    if (a == -2 || a == 0)
                    {
                        return b;
                    }
                    if (b == -2 || b == 0)
                    {
                        return a;
                    }
                    if (a == b)
                    {
                        return a;
                    }
                    return 0;
                }
            }
            private int getAverage(Tuple<int, int>[] verkaufswerte)
            {
                int a = movingAverage(verkaufswerte);
                int b = expAverage(verkaufswerte);
                if (a == -2 || a == 0)
                {
                    return b;
                }
                if (b == -2 || b == 0)
                {
                    return a;
                }
                if (a == b)
                {
                    return a;
                }
                return 0;
            }// -2  keine Angabe (z.B. nicht genug Werte), -1  fallender Kurs => Verkaufen, 0 nicht Handeln, 1 steigender Kurs => Kaufen
            private int getFormation(Tuple<int, int>[] verkaufswerte)//Parameter: 1. Verkaufszeit 2. Preis 3. Menge // -2  keine Angabe (z.B. nicht genug Werte), -1  fallender Kurs => Verkaufen, 0 nicht Handeln, 1 steigender Kurs => Kaufen
            {
                Tuple<int, int>[] verkaufswerteTemp = (Tuple<int, int>[])verkaufswerte.Clone();
                Tuple<bool, Tuple<int, int>[], Tuple<int, int>> werte = mm(verkaufswerteTemp);
                int length = 0;
                foreach (Tuple<int, int> t in werte.Item2)
                {
                    if (t != null)
                    {
                        length++;
                    }
                }
                if (length < 3)
                {
                    return -2;
                }
                bool a = getM(werte);
                bool b = getW(werte);
                bool c = getKS(werte);
                bool d = getUKS(werte);
                if (!b && !d)
                {
                    if (a || c)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    if (!a && !c)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            private Tuple<bool, Tuple<int, int>[], Tuple<int, int>> mm(Tuple<int, int>[] verkaufswerte) //bool = true => vorletzter Punkt ist ein Hochpunkt
            {
                bool b = false;
                Tuple<int, int>[] verkaufswerteUnveraendert = (Tuple<int, int>[])verkaufswerte.Clone();
                Tuple<int, int> vorletzter = verkaufswerteUnveraendert[verkaufswerteUnveraendert.Length - 2];
                for (int i = 1; i <= verkaufswerteUnveraendert.Length - 2; i++)
                {
                    if (i - 1 >= 0)
                    {
                        if ((verkaufswerteUnveraendert[i].Item1 > verkaufswerteUnveraendert[(i - 1)].Item1 && verkaufswerteUnveraendert[i].Item1 > verkaufswerteUnveraendert[(i + 1)].Item1))
                        {
                            //Console.WriteLine("Nope");
                            b = true;

                        }
                        else
                        {
                            if (((verkaufswerteUnveraendert[i].Item1 < verkaufswerteUnveraendert[(i - 1)].Item1) && (verkaufswerteUnveraendert[i].Item1 < verkaufswerteUnveraendert[(i + 1)].Item1)))
                            {
                                //Console.WriteLine("Nope");
                                b = false;
                            }
                            else
                            {
                                verkaufswerte[i] = null;
                            }
                        }
                    }
                }
                return new Tuple<bool, Tuple<int, int>[], Tuple<int, int>>(b, verkaufswerte, vorletzter);
            }
            private bool getM(Tuple<bool, Tuple<int, int>[], Tuple<int, int>> verkaufswerte) //false kein M oder zu wenig Hoch/Tiefpunkte; true ist ein M => verkaufen
            {
                if (!verkaufswerte.Item1)
                {
                    return false;
                }
                Tuple<int, int>[] ac = new Tuple<int, int>[verkaufswerte.Item2.Length - 1];
                ac = verkaufswerte.Item2;
                Tuple<int, int, int>[] werte = new Tuple<int, int, int>[3];
                int j = 2;
                int temp = 0;

                for (int i = verkaufswerte.Item2.Length - 2; i > 0; i--)
                {
                    if (ac[i] != null)
                    {
                        werte[j] = new Tuple<int, int, int>(ac[i].Item1, ac[i].Item2, temp + 1);
                        j--;
                        temp = 0;
                        if (j < 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        temp++;
                    }
                }
                if (j >= 0)
                {
                    return false;
                }
                if (werte[0].Item1 <= werte[2].Item1)
                {
                    return false;
                }
                if ((werte[1].Item1 < verkaufswerte.Item3.Item1) != (werte[1].Item1 < verkaufswerte.Item2[verkaufswerte.Item2.Length - 1].Item1))
                {
                    return true;
                }
                return false;
            }
            private bool getW(Tuple<bool, Tuple<int, int>[], Tuple<int, int>> verkaufswerte) //false kein W oder zu wenig Hoch/Tiefpunkte; true ist ein W => kaufen
            {
                if (verkaufswerte.Item1)
                {
                    return false;
                }
                Tuple<int, int>[] ac = new Tuple<int, int>[verkaufswerte.Item2.Length - 1];
                ac = verkaufswerte.Item2;
                Tuple<int, int, int>[] werte = new Tuple<int, int, int>[3];
                int j = 2;
                int temp = 0;

                for (int i = verkaufswerte.Item2.Length - 2; i > 0; i--)
                {
                    if (ac[i] != null)
                    {
                        werte[j] = new Tuple<int, int, int>(ac[i].Item1, ac[i].Item2, temp + 1);
                        j--;
                        temp = 0;
                        if (j < 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        temp++;
                    }
                }
                if (j >= 0)
                {
                    return false;
                }
                if (werte[0].Item1 >= werte[2].Item1)
                {
                    return false;
                }
                if ((werte[1].Item1 > verkaufswerte.Item3.Item1) != (werte[1].Item1 > verkaufswerte.Item2[verkaufswerte.Item2.Length - 1].Item1))
                {
                    return true;
                }
                return false;
            }
            private bool getKS(Tuple<bool, Tuple<int, int>[], Tuple<int, int>> verkaufswerte) //false keine KS oder zu wenig Hoch/Tiefpunkte; true ist eine Ks => verkaufen
            {
                if (!verkaufswerte.Item1)
                {
                    return false;
                }
                Tuple<int, int>[] ac = new Tuple<int, int>[verkaufswerte.Item2.Length - 1];
                ac = verkaufswerte.Item2;
                Tuple<int, int, int>[] werte = new Tuple<int, int, int>[4];
                int j = 3;
                int temp = 0;

                for (int i = verkaufswerte.Item2.Length - 2; i > 0; i--)
                {
                    if (ac[i] != null)
                    {
                        werte[j] = new Tuple<int, int, int>(ac[i].Item1, ac[i].Item2, temp + 1);
                        j--;
                        temp = 0;
                        if (j < 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        temp++;
                    }
                }
                if (j >= 0)
                {
                    return false;
                }
                //Gerade durch (a,b), (c,d): (d-b) = m*(c-a) <=> y= (d-b)/(c-a)*x - (d-b)*a/(c-a) + b
                int a = 0;
                int b = werte[0].Item1;
                int c = werte[0].Item3 + werte[1].Item3;
                int d = werte[2].Item1;
                Tuple<int, int> lastLinePoint = new Tuple<int, int>(werte[0].Item3 + werte[1].Item3 + werte[2].Item3 + werte[3].Item3, (d - b) / (c - a) * (werte[0].Item3 + werte[1].Item3 + werte[2].Item3 + werte[3].Item3) - (d - b) * (werte[0].Item3 + werte[1].Item3 + werte[2].Item3 + werte[3].Item3) / (c - a) + b);
                Tuple<int, int> newLinePoint = new Tuple<int, int>(werte[0].Item3 + werte[1].Item3 + werte[2].Item3 + werte[3].Item3 + 1, (d - b) / (c - a) * (werte[0].Item3 + werte[1].Item3 + werte[2].Item3 + werte[3].Item3 + 1) - (d - b) * (werte[0].Item3 + werte[1].Item3 + werte[2].Item3 + werte[3].Item3 + 1) / (c - a) + b);
                if ((lastLinePoint.Item2 > verkaufswerte.Item3.Item1) != (newLinePoint.Item2 > verkaufswerte.Item2[verkaufswerte.Item2.Length - 1].Item1))
                {
                    return true;
                }
                return false;
            }
            private bool getUKS(Tuple<bool, Tuple<int, int>[], Tuple<int, int>> verkaufswerte) //false keine UKS oder zu wenig Hoch/Tiefpunkte; true ist eine UKS => kaufen
            {
                if (verkaufswerte.Item1)
                {
                    return false;
                }
                Tuple<int, int>[] ac = new Tuple<int, int>[verkaufswerte.Item2.Length - 1];
                ac = verkaufswerte.Item2;
                Tuple<int, int, int>[] werte = new Tuple<int, int, int>[4];
                int j = 3;
                int temp = 0;

                for (int i = verkaufswerte.Item2.Length - 2; i > 0; i--)
                {
                    if (ac[i] != null)
                    {
                        werte[j] = new Tuple<int, int, int>(ac[i].Item1, ac[i].Item2, temp + 1);
                        j--;
                        temp = 0;
                        if (j < 0)
                        {
                            break;
                        }
                    }
                    else
                    {
                        temp++;
                    }
                }
                if (j >= 0)
                {
                    return false;
                }
                //Gerade durch (a,b), (c,d): (d-b) = m*(c-a) <=> y= (d-b)/(c-a)*x - (d-b)*a/(c-a) + b
                int a = 0;
                int b = werte[0].Item1;
                int c = werte[0].Item3 + werte[1].Item3;
                int d = werte[2].Item1;
                Tuple<int, int> lastLinePoint = new Tuple<int, int>(werte[0].Item3 + werte[1].Item3 + werte[2].Item3 + werte[3].Item3, (d - b) / (c - a) * (werte[0].Item3 + werte[1].Item3 + werte[2].Item3 + werte[3].Item3) - (d - b) * (werte[0].Item3 + werte[1].Item3 + werte[2].Item3 + werte[3].Item3) / (c - a) + b);
                Tuple<int, int> newLinePoint = new Tuple<int, int>(werte[0].Item3 + werte[1].Item3 + werte[2].Item3 + werte[3].Item3 + 1, (d - b) / (c - a) * (werte[0].Item3 + werte[1].Item3 + werte[2].Item3 + werte[3].Item3 + 1) - (d - b) * (werte[0].Item3 + werte[1].Item3 + werte[2].Item3 + werte[3].Item3 + 1) / (c - a) + b);
                if ((lastLinePoint.Item2 >= verkaufswerte.Item3.Item1) != (newLinePoint.Item2 >= verkaufswerte.Item2[verkaufswerte.Item2.Length - 1].Item1))
                {
                    return true;
                }
                if ((lastLinePoint.Item2 <= verkaufswerte.Item3.Item1) != (newLinePoint.Item2 <= verkaufswerte.Item2[verkaufswerte.Item2.Length - 1].Item1))
                {
                    return true;
                }
                return false;
            }
            private int[] movingAverageShortGraph(Tuple<int, int>[] verkaufspreise) //Parameter: 1. Preis, 2. Menge //-2 keine Aussage (z.B. wegen zu wenig Werten); -1 Kurs fallend => (jetzt) Verkaufen, 0 nicht Handeln, 1 Kurs steigend => (jetzt) Kaufen 
            {//Graph vom kurzfristigen gleitenden Durhschnitt
                int shortma = 50;
                if (verkaufspreise.Length < shortma)
                {
                    return null;
                }
                int[] shortMA = new int[verkaufspreise.Length - shortma];
                for (int j = shortma - 1; j < verkaufspreise.Length; j++)
                {
                    int temp = 0;
                    for (int i = 0; i < shortma; i++)
                    {
                        temp = temp + (verkaufspreise[j - i]).Item1;
                    }
                    shortMA[j-shortma+1] = temp / shortma;
                }
                return shortMA;
            }
            private int[] movingAverageLongGraph(Tuple<int, int>[] verkaufspreise) //Parameter: 1. Preis, 2. Menge //-2 keine Aussage (z.B. wegen zu wenig Werten); -1 Kurs fallend => (jetzt) Verkaufen, 0 nicht Handeln, 1 Kurs steigend => (jetzt) Kaufen 
            {//Graph vom langfristigen gleitenden Durhschnitt
                int longma = 200;
                if (verkaufspreise.Length < longma)
                {
                    return null;
                }
                int[] longMA = new int[verkaufspreise.Length - longma];
                for (int j = longma - 1; j < verkaufspreise.Length; j++)
                {
                    int temp = 0;
                    for (int i = 0; i < longma; i++)
                    {
                        temp = temp + (verkaufspreise[j - i]).Item1;
                    }
                    longMA[j-longma+1] = temp / longma;
                }
                return longMA;
            }
            private int movingAverage(Tuple<int, int>[] verkaufspreise) //Parameter: 1. Preis, 2. Menge //-2 keine Aussage (z.B. wegen zu wenig Werten); -1 Kurs fallend => (jetzt) Verkaufen, 0 nicht Handeln, 1 Kurs steigend => (jetzt) Kaufen 
            {//Ergebnis des Analyseinstruments gleit. Durchschnitt
                if (verkaufspreise.Length < 200)
                {
                    return -2;
                }
                int lastSMA;
                int nowSMA;
                int lastLMA;
                int nowLMA;
                int shortma = 50;
                int longma = 200;
                int temp = 0;
                for (int i = 0; i < shortma; i++)
                {
                    temp = temp + (verkaufspreise[verkaufspreise.Length - i - 2]).Item1;
                }
                lastSMA = temp / shortma;
                temp = 0;
                for (int i = 0; i < shortma; i++)
                {
                    temp = temp + (verkaufspreise[verkaufspreise.Length - i - 1]).Item1;
                }
                nowSMA = temp / shortma;
                temp = 0;
                for (int i = 0; i < longma; i++)
                {
                    temp = temp + (verkaufspreise[verkaufspreise.Length - i - 2]).Item1;
                }
                lastLMA = temp / longma;
                temp = 0;
                for (int i = 0; i < longma; i++)
                {
                    temp = temp + (verkaufspreise[verkaufspreise.Length - i - 1]).Item1;
                }
                nowLMA = temp / longma;

                if (lastLMA > lastSMA && nowLMA < nowSMA)
                {
                    return 1;
                }
                if (lastLMA < lastSMA && nowLMA > nowSMA)
                {
                    return -1;
                }
                return 0;
            }
            private int[] expAverageGraph(Tuple<int, int>[] verkaufspreise)//Graph vom expotentiellen gleitenden Durhschnitt
            {
                int[] expMA = new int[verkaufspreise.Length];
                expMA[0] = (verkaufspreise[0]).Item1;
                expMA[1] = expMA[0] + (2 / 3) * ((verkaufspreise[1]).Item1 - expMA[0]);
                for (int j = 2; j < verkaufspreise.Length; j++)
                {
                    expMA[j] = expMA[j - 1] + (2 / (j + 1)) * ((verkaufspreise[j]).Item1 - expMA[j - 1]);
                }
                return expMA;
            }
            private int expAverage(Tuple<int, int>[] verkaufspreise)//Ergebnis des Analyseinstruments exp. gleit. Durchschnitt
            {
                int[] expMA = new int[verkaufspreise.Length];
                expMA[0] = (verkaufspreise[0]).Item1;
                expMA[1] = expMA[0] + (2 / 3) * ((verkaufspreise[1]).Item1 - expMA[0]);
                for (int j = 2; j < verkaufspreise.Length; j++)
                {
                    expMA[j] = expMA[j - 1] + (2 / (j + 1)) * ((verkaufspreise[j]).Item1 - expMA[j - 1]);
                }
                int stocknow = (verkaufspreise[verkaufspreise.Length - 1]).Item1;
                int stocklast = (verkaufspreise[verkaufspreise.Length - 2]).Item1;
                int expnow = expMA[expMA.Length - 1];
                int explast = expMA[expMA.Length - 2];
                if (explast >= stocklast && expnow < stocknow)
                {
                    return 1;
                }
                if (explast <= stocklast && expnow > stocknow)
                {
                    return -1;
                }
                return 0;
            }
        }
    }
}

