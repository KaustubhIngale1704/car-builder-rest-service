using CarFactory_Domain;
using CarFactory_Domain.Engine;

namespace CarFactory_Factory
{
    public interface IEngineProvider
    {
        Engine GetEngine(Manufacturer manufacturer);
    }
}