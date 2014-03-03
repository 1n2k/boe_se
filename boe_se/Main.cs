using System;
using System.Diagnostics;
using boe_se.Engine;

namespace boe_se
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            GItem[] items = new GItem[1000];
            Stopwatch watch = new Stopwatch();
            for (int i = 0; i < 1000; i++)
            {
                items[i] = Market.GetInstance.GetItem(i + 1000);
            }
            int a;
            Console.WriteLine("Die Daten wurden heruntergeladen.");
            watch.Start();
            for (int i = 0; i < 1000; i++)
            {
                a = items[i].kurs;
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
            Console.ReadLine();
            /*GItem a = Market.GetInstance.GetItem(443);

			Console.WriteLine ("Hello World! " + a.Name + ", last changed: " + a.PriceLastChanged);

            a.Refresh();

            Console.WriteLine("Hello World! " + a.Name + ", last changed: " + a.PriceLastChanged);
            Console.ReadLine();*/
		}
	}
}
