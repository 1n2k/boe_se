using System;
using boe_se.Engine;

namespace boe_se
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            GItem a = Market.getInstance.getItem(443);

			Console.WriteLine ("Hello World! " + a.Name + ", last changed: " + a.PriceLastChanged);
            Console.ReadLine();
		}
	}
}
