using CarFactory.Utilities;
using CarFactory_Domain;
using CarFactory_Domain.Engine;
using CarFactory_Domain.Engine.EngineSpecifications;
using CarFactory_Factory;
using CarFactory_Storage;
using CarFactory_SubContractor;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;
using System.Threading;

namespace CarFactory_Engine
{
    public class EngineProvider : IEngineProvider
    {
        private readonly IGetPistons _getPistons;
        private readonly ISteelSubcontractor _steelSubContractor;
        private int SteelInventory = 0;
        private readonly IGetEngineSpecificationQuery _getEngineSpecification;
        private readonly IMemoryCache _cache;

        public EngineProvider(IGetPistons getPistons, ISteelSubcontractor steelSubContractor, 
            IGetEngineSpecificationQuery getEngineSpecification, IMemoryCache cache)
        {
            _getPistons = getPistons;
            _steelSubContractor = steelSubContractor;
            _getEngineSpecification = getEngineSpecification ;
            _cache = cache;
        }


        public Engine GetEngine(Manufacturer manufacturer)
        {
            var specification = _getEngineSpecification.GetForManufacturer(manufacturer);
                        
            var engineBlock = MakeEngineBlock(specification.CylinderCount);

            var engine = new Engine(engineBlock, specification.Name);

            var pistons = _getPistons.Get(specification.CylinderCount);

            InstallPistons(engine, pistons);

            InstallFuelInjectors(engine, specification.PropulsionType);

            InstallSparkPlugs(engine);

            if (!engine.IsFinished)
                throw new InvalidOperationException("Cannot return an unfinished engine");

            return engine;
        }

        private EngineBlock MakeEngineBlock(int cylinders)
        {
            var requiredSteel = cylinders * EngineBlock.RequiredSteelPerCylinder;

            var steel = GetSteel(requiredSteel);

            var engineBlock = new EngineBlock(steel);

            if (cylinders != engineBlock.CylinderCount)
                throw new InvalidOperationException("Engine block does not have the required amount of cylinders");

            return engineBlock;
        }


        private int GetSteel(int amount)
        {
            if(amount > SteelInventory)
            {
                var missingSteel = amount - SteelInventory;
                SteelInventory += _steelSubContractor.OrderSteel(missingSteel).Sum(sd => sd.Amount);
            }

            SteelInventory -= amount;

            return amount;
        }

        private void InstallFuelInjectors(Engine engine, Propulsion propulsionType)
        {
            //Do work
            SlowWorker.FakeWorkingForMillis(25 * engine.EngineBlock.CylinderCount);

            engine.PropulsionType = propulsionType;
        }

        private void InstallPistons(Engine engine, int pistons)
        {
            //Do work
            SlowWorker.FakeWorkingForMillis(25 * pistons);

            engine.PistonsCount = pistons;
        }

        private void InstallSparkPlugs(Engine engine)
        {
            if (!engine.PropulsionType.HasValue)
                throw new InvalidOperationException($"Propulsion type must be known before installing spark plugs");

            if(engine.PropulsionType.Value == Propulsion.Gasoline)
            {
                //Do work 
                SlowWorker.FakeWorkingForMillis(engine.EngineBlock.CylinderCount * 15);
                engine.HasSparkPlugs = true;
            }   
        }


    }
}