using CarFactory.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarFactory_SubContractor
{
    public class SteelSubcontractor : ISteelSubcontractor
    {
        public List<SteelDelivery> OrderSteel(int amount)
        {
            var delivery = new List<SteelDelivery>();
            do
            {
                delivery.Add(new SteelDelivery());
                SlowWorker.FakeWorkingForMillis(100);
            }
            while (delivery.Select(d => d.Amount).Sum(a => a) < amount);

            if (delivery.Sum(x => x.Amount) == 25)
            {
                Console.Write("Wait here");
            }
            return delivery;
        }
    }
}
