using CarFactory_Domain;

namespace CarFactory_Factory
{
    public interface IChassisProvider
    {
        Chassis GetChassis(Manufacturer manufacturer, int numberOfDoors);
    }
}