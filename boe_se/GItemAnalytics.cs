using System;
using System.Collections.Generic;

namespace boe_se
{
	namespace Engine
	{
		public partial class GItem
		{
            private Tuple<int, int>[] compiledItemValues;
			public int kurs {
				get {
                    compiledItemValues = GetCompiledItemValues();
                    if (compiledItemValues.Length <= 50)
                    {
                        return -2;
                    }
					//Bedeutungen der Vergleichswerte:
					// 1 Kurs steigend => (jetzt) Kaufen
					// 0 nicht Handeln
					//-1 Kurs fallend => (jetzt) Verkaufen
					//-2 keine Aussage (z.B. wegen zu wenig Werten)

					int a = getAverage ();
					int b = getFormation ();
					if (a == -2 || a == 0) {
						return b;
					}
					if (b == -2 || b == 0) {
						return a;
					}
					if (a == b) {
						return a;
					}
					return 0;
				}
			}

			private int getAverage ()
			{
				int a = movingAverage ();
				int b = expAverage ();
				if (a == -2 || a == 0) {
					return b;
				}
				if (b == -2 || b == 0) {
					return a;
				}
				if (a == b) {
					return a;
				}
				return 0;
			}

			private Tuple<int,int>[] GetCompiledItemValues (bool sell = true)
			{
				var ret = new List<Tuple<int,int> > ();

				int idx = 0;
				Tuple<int,int> ac = this [idx++, sell];
				while (ac != null) {
					ret.Add(ac);
					ac = this[idx++,sell];
				}

				return ret.ToArray();
			}

			// -2  keine Angabe (z.B. nicht genug Werte), -1  fallender Kurs => Verkaufen, 0 nicht Handeln, 1 steigender Kurs => Kaufen
			private int getFormation ()//Parameter: 1. Verkaufszeit 2. Preis 3. Menge // -2  keine Angabe (z.B. nicht genug Werte), -1  fallender Kurs => Verkaufen, 0 nicht Handeln, 1 steigender Kurs => Kaufen
			{
				Tuple<bool, Tuple<int, int>[], Tuple<int, int>> werte = mm ();
				int length = 0;
				foreach (Tuple<int, int> t in werte.Item2) {
					if (t != null) {
						length++;
					}
				}
				if (length < 3) {
					return -2;
				}
				bool a = getM (werte);
				bool b = getW (werte);
				bool c = getKS (werte);
				bool d = getUKS (werte);
				if (!b && !d) {
					if (a || c) {
						return -1;
					} else {
						return 0;
					}
				} else {
					if (!a && !c) {
						return 1;
					} else {
						return 0;
					}
				}
			}

			private Tuple<bool, Tuple<int, int>[], Tuple<int, int>> mm () //bool = true => vorletzter Punkt ist ein Hochpunkt
			{
				bool b = false;
                var verkaufswerte = compiledItemValues;
				Tuple<int, int>[] verkaufswerteUnveraendert = (Tuple<int, int>[]) verkaufswerte.Clone();
                Tuple<int, int> vorletzter = verkaufswerteUnveraendert[verkaufswerteUnveraendert.Length - 2];
                for (int i = 1; i <= verkaufswerteUnveraendert.Length - 2; i++)
                {
					if (i - 1 >= 0) {
                        if ((verkaufswerteUnveraendert[i].Item1 > verkaufswerteUnveraendert[(i - 1)].Item1 && verkaufswerteUnveraendert[i].Item1 > verkaufswerteUnveraendert[(i + 1)].Item1))
                        {
							//Console.WriteLine("Nope");
							b = true;

						} else {
                            if (((verkaufswerteUnveraendert[i].Item1 < verkaufswerteUnveraendert[(i - 1)].Item1) && (verkaufswerteUnveraendert[i].Item1 < verkaufswerteUnveraendert[(i + 1)].Item1)))
                            {
								//Console.WriteLine("Nope");
								b = false;
							} else {
								verkaufswerte [i] = null;
							}
						}
					}
				}
				return new Tuple<bool, Tuple<int, int>[], Tuple<int, int>> (b, verkaufswerte, vorletzter);
			}

			private bool getM (Tuple<bool, Tuple<int, int>[], Tuple<int, int>> verkaufswerte) //false kein M oder zu wenig Hoch/Tiefpunkte; true ist ein M => verkaufen
			{
				if (!verkaufswerte.Item1) {
					return false;
				}
				Tuple<int, int>[] ac = new Tuple<int, int>[verkaufswerte.Item2.Length - 1];
				ac = verkaufswerte.Item2;
				Tuple<int, int, int>[] werte = new Tuple<int, int, int>[3];
				int j = 2;
				int temp = 0;

				for (int i = verkaufswerte.Item2.Length - 2; i > 0; i--) {
					if (ac [i] != null) {
						werte [j] = new Tuple<int, int, int> (ac [i].Item1, ac [i].Item2, temp + 1);
						j--;
						temp = 0;
						if (j < 0) {
							break;
						}
					} else {
						temp++;
					}
				}
				if (j >= 0) {
					return false;
				}
				if (werte [0].Item1 >= werte [2].Item1) {
					return false;
				}
				if ((werte [1].Item1 < verkaufswerte.Item3.Item1) != (werte [1].Item1 < verkaufswerte.Item2 [verkaufswerte.Item2.Length - 1].Item1)) {
					return true;
				}
				return false;
			}

			private bool getW (Tuple<bool, Tuple<int, int>[], Tuple<int, int>> verkaufswerte) //false kein W oder zu wenig Hoch/Tiefpunkte; true ist ein W => kaufen
			{
				if (verkaufswerte.Item1) {
					return false;
				}
				Tuple<int, int>[] ac = new Tuple<int, int>[verkaufswerte.Item2.Length - 1];
				ac = verkaufswerte.Item2;
				Tuple<int, int, int>[] werte = new Tuple<int, int, int>[3];
				int j = 2;
				int temp = 0;

				for (int i = verkaufswerte.Item2.Length - 2; i > 0; i--) {
					if (ac [i] != null) {
						werte [j] = new Tuple<int, int, int> (ac [i].Item1, ac [i].Item2, temp + 1);
						j--;
						temp = 0;
						if (j < 0) {
							break;
						}
					} else {
						temp++;
					}
				}
				if (j >= 0) {
					return false;
				}
				if (werte [0].Item1 <= werte [2].Item1) {
					return false;
				}
				if ((werte [1].Item1 > verkaufswerte.Item3.Item1) != (werte [1].Item1 > verkaufswerte.Item2 [verkaufswerte.Item2.Length - 1].Item1)) {
					return true;
				}
				return false;
			}

			private bool getKS (Tuple<bool, Tuple<int, int>[], Tuple<int, int>> verkaufswerte) //false keine KS oder zu wenig Hoch/Tiefpunkte; true ist eine Ks => verkaufen
			{
				if (!verkaufswerte.Item1) {
					return false;
				}
				Tuple<int, int>[] ac = new Tuple<int, int>[verkaufswerte.Item2.Length - 1];
				ac = verkaufswerte.Item2;
				Tuple<int, int, int>[] werte = new Tuple<int, int, int>[4];
				int j = 3;
				int temp = 0;

				for (int i = verkaufswerte.Item2.Length - 2; i > 0; i--) {
					if (ac [i] != null) {
						werte [j] = new Tuple<int, int, int> (ac [i].Item1, ac [i].Item2, temp + 1);
						j--;
						temp = 0;
						if (j < 0) {
							break;
						}
					} else {
						temp++;
					}
				}
				if (j >= 0) {
					return false;
				}
				//Gerade durch (a,b), (c,d): (d-b) = m*(c-a) <=> y= (d-b)/(c-a)*x - (d-b)*a/(c-a) + b
				int a = 0;
				int b = werte [0].Item1;
				int c = werte [0].Item3 + werte [1].Item3;
				int d = werte [2].Item1;
				Tuple<int, int> lastLinePoint = new Tuple<int, int> (werte [0].Item3 + werte [1].Item3 + werte [2].Item3 + werte [3].Item3, (d - b) / (c - a) * (werte [0].Item3 + werte [1].Item3 + werte [2].Item3 + werte [3].Item3) - (d - b) * (werte [0].Item3 + werte [1].Item3 + werte [2].Item3 + werte [3].Item3) / (c - a) + b);
				Tuple<int, int> newLinePoint = new Tuple<int, int> (werte [0].Item3 + werte [1].Item3 + werte [2].Item3 + werte [3].Item3 + 1, (d - b) / (c - a) * (werte [0].Item3 + werte [1].Item3 + werte [2].Item3 + werte [3].Item3 + 1) - (d - b) * (werte [0].Item3 + werte [1].Item3 + werte [2].Item3 + werte [3].Item3 + 1) / (c - a) + b);
				if ((lastLinePoint.Item2 > verkaufswerte.Item3.Item1) != (newLinePoint.Item2 > verkaufswerte.Item2 [verkaufswerte.Item2.Length - 1].Item1)) {
					return true;
				}
				return false;
			}

			private bool getUKS (Tuple<bool, Tuple<int, int>[], Tuple<int, int>> verkaufswerte) //false keine UKS oder zu wenig Hoch/Tiefpunkte; true ist eine UKS => kaufen
			{
				if (verkaufswerte.Item1) {
					return false;
				}
				Tuple<int, int>[] ac = new Tuple<int, int>[verkaufswerte.Item2.Length - 1];
				ac = verkaufswerte.Item2;
				Tuple<int, int, int>[] werte = new Tuple<int, int, int>[4];
				int j = 3;
				int temp = 0;

				for (int i = verkaufswerte.Item2.Length - 2; i > 0; i--) {
					if (ac [i] != null) {
						werte [j] = new Tuple<int, int, int> (ac [i].Item1, ac [i].Item2, temp + 1);
						j--;
						temp = 0;
						if (j < 0) {
							break;
						}
					} else {
						temp++;
					}
				}
				if (j >= 0) {
					return false;
				}
				//Gerade durch (a,b), (c,d): (d-b) = m*(c-a) <=> y= (d-b)/(c-a)*x - (d-b)*a/(c-a) + b
				int a = 0;
				int b = werte [0].Item1;
				int c = werte [0].Item3 + werte [1].Item3;
				int d = werte [2].Item1;
				Tuple<int, int> lastLinePoint = new Tuple<int, int> (werte [0].Item3 + werte [1].Item3 + werte [2].Item3 + werte [3].Item3, (d - b) / (c - a) * (werte [0].Item3 + werte [1].Item3 + werte [2].Item3 + werte [3].Item3) - (d - b) * (werte [0].Item3 + werte [1].Item3 + werte [2].Item3 + werte [3].Item3) / (c - a) + b);
				Tuple<int, int> newLinePoint = new Tuple<int, int> (werte [0].Item3 + werte [1].Item3 + werte [2].Item3 + werte [3].Item3 + 1, (d - b) / (c - a) * (werte [0].Item3 + werte [1].Item3 + werte [2].Item3 + werte [3].Item3 + 1) - (d - b) * (werte [0].Item3 + werte [1].Item3 + werte [2].Item3 + werte [3].Item3 + 1) / (c - a) + b);
				if ((lastLinePoint.Item2 >= verkaufswerte.Item3.Item1) != (newLinePoint.Item2 >= verkaufswerte.Item2 [verkaufswerte.Item2.Length - 1].Item1)) {
					return true;
				}
				if ((lastLinePoint.Item2 <= verkaufswerte.Item3.Item1) != (newLinePoint.Item2 <= verkaufswerte.Item2 [verkaufswerte.Item2.Length - 1].Item1)) {
					return true;
				}
				return false;
			}

			private int[] movingAverageShortGraph () //Parameter: 1. Preis, 2. Menge //-2 keine Aussage (z.B. wegen zu wenig Werten); -1 Kurs fallend => (jetzt) Verkaufen, 0 nicht Handeln, 1 Kurs steigend => (jetzt) Kaufen
			{//Graph vom kurzfristigen gleitenden Durhschnitt

				var verkaufspreise = compiledItemValues;
                int shortma = 50;
                if (verkaufspreise.Length < shortma)
                {
					return null;
				}
                int[] shortMA = new int[verkaufspreise.Length - shortma];

                for (int j = shortma - 1; j < verkaufspreise.Length; j++)
                {
					int temp = 0;
					for (int i = 0; i < shortma; i++) {
						temp = temp + (verkaufspreise [j - i]).Item1;
					}
					shortMA [j - shortma + 1] = temp / shortma;
				}
				return shortMA;
			}

			private int[] movingAverageLongGraph () //Parameter: 1. Preis, 2. Menge //-2 keine Aussage (z.B. wegen zu wenig Werten); -1 Kurs fallend => (jetzt) Verkaufen, 0 nicht Handeln, 1 Kurs steigend => (jetzt) Kaufen
			{//Graph vom langfristigen gleitenden Durhschnitt
				var verkaufspreise = compiledItemValues;
                int longma = 200;
                if (verkaufspreise.Length < longma)
                {
					return null;
				}
                int[] longMA = new int[verkaufspreise.Length - longma];
				
				for (int j = longma - 1; j < verkaufspreise.Length; j++) {
					int temp = 0;
					for (int i = 0; i < longma; i++) {
						temp = temp + (verkaufspreise [j - i]).Item1;
					}
                    longMA[j - longma + 1] = temp / longma;
				}
				return longMA;
			}

			private int movingAverage () //Parameter: 1. Preis, 2. Menge //-2 keine Aussage (z.B. wegen zu wenig Werten); -1 Kurs fallend => (jetzt) Verkaufen, 0 nicht Handeln, 1 Kurs steigend => (jetzt) Kaufen
			{//Ergebnis des Analyseinstruments gleit. Durchschnitt
				
				var verkaufspreise = compiledItemValues;

                int longma = 200;
                if (verkaufspreise.Length < longma)
                {
					return -2;
				}
				int lastSMA;
				int nowSMA;
				int lastLMA;
				int nowLMA;
				int shortma = 50;
				int temp = 0;
				for (int i = 0; i < shortma; i++) {
					temp = temp + (verkaufspreise [verkaufspreise.Length - i - 2]).Item1;
				}
				lastSMA = temp / shortma;
				temp = 0;
				for (int i = 0; i < shortma; i++) {
					temp = temp + (verkaufspreise [verkaufspreise.Length - i - 1]).Item1;
				}
				nowSMA = temp / shortma;
				temp = 0;
				for (int i = 0; i < longma; i++) {
					temp = temp + (verkaufspreise [verkaufspreise.Length - i - 2]).Item1;
				}
				lastLMA = temp / longma;
				temp = 0;
				for (int i = 0; i < longma; i++) {
					temp = temp + (verkaufspreise [verkaufspreise.Length - i - 1]).Item1;
				}
				nowLMA = temp / longma;

				if (lastLMA >= lastSMA && nowLMA < nowSMA) {
					return 1;
				}
				if (lastLMA <= lastSMA && nowLMA > nowSMA) {
					return -1;
				}
				return 0;
			}

			private int[] expAverageGraph ()//Graph vom expotentiellen gleitenden Durhschnitt
			{
				var verkaufspreise = compiledItemValues;
				int[] expMA = new int[verkaufspreise.Length];
				expMA [0] = (verkaufspreise [0]).Item1;
				expMA [1] = expMA [0] + (2 / 3) * ((verkaufspreise [1]).Item1 - expMA [0]);
				for (int j = 2; j < verkaufspreise.Length; j++) {
					expMA [j] = expMA [j - 1] + (2 / (j + 1)) * ((verkaufspreise [j]).Item1 - expMA [j - 1]);
				}
				return expMA;
			}

			private int expAverage ()//Ergebnis des Analyseinstruments exp. gleit. Durchschnitt
			{
				var verkaufspreise = compiledItemValues;
                if (verkaufspreise.Length < 50)
                {
                    return -2;
                }
				int[] expMA = new int[verkaufspreise.Length];
				expMA [0] = (verkaufspreise [0]).Item1;
				expMA [1] = expMA [0] + (2 / 3) * ((verkaufspreise [1]).Item1 - expMA [0]);
				for (int j = 2; j < verkaufspreise.Length; j++) {
					expMA [j] = expMA [j - 1] + (2 / (j + 1)) * ((verkaufspreise [j]).Item1 - expMA [j - 1]);
				}
				int stocknow = (verkaufspreise [verkaufspreise.Length - 1]).Item1;
				int stocklast = (verkaufspreise [verkaufspreise.Length - 2]).Item1;
				int expnow = expMA [expMA.Length - 1];
				int explast = expMA [expMA.Length - 2];
				if (explast > stocklast && expnow < stocknow) {
					return 1;
				}
				if (explast < stocklast && expnow > stocknow) {
					return -1;
				}
				return 0;
			}

		}
	}
}

