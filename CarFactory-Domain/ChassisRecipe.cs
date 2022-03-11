using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_Domain
{
    public class ChassisRecipe
    {
        public ChassisRecipe(Manufacturer manufacturer, int frontId, int frontCost, int cabinId, int cabinCost, int backId, int backCost)
        {
            Manufacturer = manufacturer;
            FrontId = frontId;
            CabinId = cabinId;
            BackId = backId;
            Cost = frontCost + cabinCost + backCost;
        }
        public Manufacturer Manufacturer { get; }
        public int FrontId { get; }
        public int CabinId { get; }
        public int BackId { get; }
        public int Cost { get; }
    }
}
