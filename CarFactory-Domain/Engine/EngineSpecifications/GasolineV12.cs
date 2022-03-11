
namespace CarFactory_Domain.Engine.EngineSpecifications
{
    public class GasolineV12 : EngineSpecification
    {
        public override string Name => "Gasoline V12";
        public override int CylinderCount => 12;
        public override Propulsion PropulsionType => Propulsion.Gasoline;
    }
}
