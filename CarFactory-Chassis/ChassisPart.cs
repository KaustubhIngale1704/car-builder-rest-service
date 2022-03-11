using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_Chasis
{
    public abstract class ChassisPart
    {
        public readonly int _typeId;
        public ChassisPart(int typeId)
        {
            _typeId = typeId;
        }
        public new abstract string GetType();

        public abstract string GetChassisType();
    }

    
}
