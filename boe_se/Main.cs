using System;
using boe_se.Engine;

namespace boe_se
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            /*foreach (var item in Market.GetInstance.Items)
                if(item.DataId == 440)
                    Console.WriteLine(item.kurs);

            Console.ReadLine();*/
            System.Collections.Generic.List<GItem> items = Market.GetInstance.Items;
            Console.WriteLine("Die Daten wurden heruntergeladen.");
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            foreach (GItem item in items)
            {
                if (item.DataId == 787)
                {
                    Console.WriteLine(item.kurs);
                }
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds + " ms mit Ausgabe");
            Console.ReadLine();
            watch.Start();
            int a;
            foreach (GItem item in items)
            {
                a = item.kurs;
            }
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds + " ms ohne Ausgabe");
		}
	}
}
