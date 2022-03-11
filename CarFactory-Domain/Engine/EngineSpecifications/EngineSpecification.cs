
namespace CarFactory_Domain.Engine.EngineSpecifications
{
    public class EngineSpecification
    {
        public virtual string Name { get; set; }
        public virtual int CylinderCount { get; set; }
        public virtual Propulsion PropulsionType { get; set; }
    }
}
