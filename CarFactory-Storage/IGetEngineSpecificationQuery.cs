using CarFactory_Domain;
using CarFactory_Domain.Engine.EngineSpecifications;

namespace CarFactory_Storage
{
    public interface IGetEngineSpecificationQuery
    {
        EngineSpecification GetForManufacturer(Manufacturer manufacturer);
    }
}
