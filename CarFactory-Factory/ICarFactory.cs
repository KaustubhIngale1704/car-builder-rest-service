using CarFactory_Domain;
using System.Collections.Generic;

namespace CarFactory_Factory
{
    public interface ICarFactory
    {
        IEnumerable<Car> BuildCars(IEnumerable<CarSpecification> specs);
    }
}