
namespace CarFactory_Domain.Engine.EngineSpecifications
{
    public class GasolineV8 : EngineSpecification
    {
        public override string Name => "Gasoline V8";
        public override int CylinderCount => 8;
        public override Propulsion PropulsionType => Propulsion.Gasoline;
    }
}
