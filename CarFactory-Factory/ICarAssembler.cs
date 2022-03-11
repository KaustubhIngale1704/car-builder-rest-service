using CarFactory_Domain;
using CarFactory_Domain.Engine;
using System.Collections.Generic;

namespace CarFactory_Factory
{
    public interface ICarAssembler
    {
        Car AssembleCar(Chassis chassis, Engine engine, Interior interior, IEnumerable<Wheel> wheels);
    }
}