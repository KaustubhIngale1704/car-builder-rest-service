using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_Chasis
{
    public class ChassisCabin : ChassisPart
    {
        public ChassisCabin(int typeId) : base(typeId)
        {

        }

        public override string GetChassisType()
        {
            switch (_typeId)
            {
                case 0:
                    return "Two Door";
                case 1:
                    return "Four Door";
                default:
                    throw new Exception("Unknown cabin type");
            }
        }

        public override string GetType()
        {
            return "ChassisCabin";
        }
    }

}
