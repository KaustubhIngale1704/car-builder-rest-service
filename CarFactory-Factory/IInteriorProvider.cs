using CarFactory_Domain;

namespace CarFactory_Factory
{
    public interface IInteriorProvider
    {
        Interior GetInterior(CarSpecification specification);
    }
}