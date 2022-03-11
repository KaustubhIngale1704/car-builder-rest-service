using System.Collections.Generic;
using CarFactory_Domain;

namespace CarFactory_Factory
{
    public interface IWheelProvider
    {
        IEnumerable<Wheel> GetWheels();
    }
}