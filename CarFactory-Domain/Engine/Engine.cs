
namespace CarFactory_Domain.Engine
{
    public class Engine
    {
        public Engine(EngineBlock engineBlock, string specification)
        {
            EngineBlock = engineBlock;
            EngineSpecification = specification;
        }

        public EngineBlock EngineBlock { get; }

        public string EngineSpecification { get; }

        public Propulsion? PropulsionType { get; set; }
        
        public int? PistonsCount { get; set; }

        public bool HasSparkPlugs { get; set; }

        public bool IsFinished => PropulsionType.HasValue && PistonsCount.HasValue && 
            (PropulsionType != Propulsion.Gasoline || HasSparkPlugs);
    }
}
