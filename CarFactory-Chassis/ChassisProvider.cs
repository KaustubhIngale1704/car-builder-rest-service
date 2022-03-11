using CarFactory_Domain;
using CarFactory_Factory;
using CarFactory_Storage;
using CarFactory_SubContractor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_Chasis
{
    public class ChassisProvider : IChassisProvider
    {
        private readonly ISteelSubcontractor _steelSubcontractor;
        private readonly IGetChassisRecipeQuery _chassisRecipeQuery;

        public ChassisProvider(ISteelSubcontractor steelSubcontractor, IGetChassisRecipeQuery chassisRecipeQuery)
        {
            _steelSubcontractor = steelSubcontractor;
            _chassisRecipeQuery = chassisRecipeQuery;
        }
        public Chassis GetChassis(Manufacturer manufacturer, int numberOfDoors)
        {
            var chassisRecipe = _chassisRecipeQuery.Get(manufacturer);

            var chassisParts = new List<ChassisPart>();
            chassisParts.Add(new ChassisBack(chassisRecipe.BackId));
            chassisParts.Add(new ChassisCabin(chassisRecipe.CabinId));
            chassisParts.Add(new ChassisFront(chassisRecipe.FrontId));

            CheckChassisParts(chassisParts);

            SteelInventory += _steelSubcontractor.OrderSteel(chassisRecipe.Cost).Select(d => d.Amount).Sum();
            CheckForMaterials(chassisRecipe.Cost);
            SteelInventory -= chassisRecipe.Cost;

            var chassisWelder = new ChassisWelder();

            chassisWelder.StartWeld(chassisParts[0]);
            chassisWelder.ContinueWeld(chassisParts[1], numberOfDoors);
            chassisWelder.FinishWeld(chassisParts[2]);
 
            return chassisWelder.GetChassis();
        }

        public int SteelInventory { get; private set; }

        private void CheckForMaterials(int cost)
        {
            if (SteelInventory < cost)
            {
                throw new Exception("Not enough chassis material");
            }
        }

        private void CheckChassisParts(List<ChassisPart> parts)
        {
            if (parts == null)
            {
                throw new Exception("No chassis parts");
            }

            if (parts.Count > 3)
            {
                throw new Exception("Chassis parts missing");
            }

            if (parts.Count < 3)
            {
                throw new Exception("To many chassis parts");
            }
        }
    }
}
