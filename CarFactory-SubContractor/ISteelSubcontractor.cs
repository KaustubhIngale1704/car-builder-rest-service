using System;
using System.Collections.Generic;
using System.Text;

namespace CarFactory_SubContractor
{
    public interface ISteelSubcontractor
    {
        List<SteelDelivery> OrderSteel(int amount);
    }
}
