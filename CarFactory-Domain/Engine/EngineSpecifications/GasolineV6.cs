
namespace CarFactory_Domain.Engine.EngineSpecifications
{
    public class GasolineV6 : EngineSpecification
    {
        public override string Name => "Gasoline V6";
        public override int CylinderCount => 6;
        public override Propulsion PropulsionType => Propulsion.Gasoline;
    }
}
