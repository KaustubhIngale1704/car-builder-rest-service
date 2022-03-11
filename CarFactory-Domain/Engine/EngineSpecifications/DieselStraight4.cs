
namespace CarFactory_Domain.Engine.EngineSpecifications
{
    public class DieselStraight4 : EngineSpecification
    {
        public override string Name => "Diesel Straight 4";
        public override int CylinderCount => 4;
        public override Propulsion PropulsionType => Propulsion.Diesel;
    }
}
