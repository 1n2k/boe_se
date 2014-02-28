using System;
using boe_se.Engine;

namespace boe_se
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            GItem a = Market.getInstance.getItem(443);
            GItem b = Market.getInstance.Items[443];

			Console.WriteLine ("Hello World! " + b.Name + ", last changed: " + b.PriceLastChanged);
            Console.ReadLine();
		}
	}
}
