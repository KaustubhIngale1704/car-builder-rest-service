using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_Domain
{
    public class Chassis
    {
        public Chassis(string description, bool valid)
        {
            Description = description;
            ValidConstruction = valid;
        }
        public string Description { get; }
        public bool ValidConstruction { get; }
    }
}
