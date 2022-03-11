using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_Chasis
{
    public class ChassisBack : ChassisPart
    {
        public ChassisBack(int typeId) : base(typeId)
        {}

        public override string GetChassisType()
        {
            switch (_typeId)
            {
                case 0:
                    return "Sedan";
                case 1:
                    return "Pickup";
                case 2:
                    return "Hatchback";
                default:
                    throw new Exception("Unknown trunk type");
            }
        }

        public override string GetType()
        {
            return "ChassisBack";
        }

    }
}
