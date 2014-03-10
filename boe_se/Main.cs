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
            GItem i = Market.GetInstance.GetItem(12355);
            Console.WriteLine( i.Name + ": " + i.TypeToName().Item1 + ", " + i.TypeToName().Item2 );
            Console.WriteLine(i.kurs);
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds + " ms ohne Ausgabe");
            Console.ReadLine();
		}
	}
}
