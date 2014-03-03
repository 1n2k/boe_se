using System;
using boe_se.Engine;

namespace boe_se
{
	class MainClass
	{
		public static void Main (string[] args)
		{
            foreach (var item in Market.GetInstance.Items)
                if(item.DataId == 440)
                    Console.WriteLine(item.kurs);

            Console.ReadLine();
		}
	}
}
