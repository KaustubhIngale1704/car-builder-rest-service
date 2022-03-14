using CarFactory_Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace CarFactory_Factory
{
    public class CarFactory : ICarFactory
    {
        private IChassisProvider _chassisProvider;
        private IEngineProvider _engineProvider;
        private IPainter _painter;
        private IInteriorProvider _interiorProvider;
        private IWheelProvider _wheelProvider;
        private ICarAssembler _carAssembler;
        private IStorageProvider _storageProvider;

        public CarFactory(
            IChassisProvider chassisProvider, 
            IEngineProvider engineProvider, 
            IPainter painter, 
            IInteriorProvider interiorProvider, 
            IWheelProvider wheelProvider, 
            ICarAssembler carAssembler,
            IStorageProvider storageProvider)
        {
            _chassisProvider = chassisProvider;
            _engineProvider = engineProvider;
            _painter = painter;
            _interiorProvider = interiorProvider;
            _wheelProvider = wheelProvider;
            _carAssembler = carAssembler;
            _storageProvider = storageProvider;
        }

        public IEnumerable<Car> BuildCars(IEnumerable<CarSpecification> specs)
        {
            var cars = new List<Car>();

            var s = specs.FirstOrDefault();
            var chassis = _chassisProvider.GetChassis(s.Manufacturer, s.NumberOfDoors);
            var engine = _engineProvider.GetEngine(s.Manufacturer);
            var interior = _interiorProvider.GetInterior(s);
            var wheels = _wheelProvider.GetWheels();
            foreach (var spec in specs)
            {
                var car = _carAssembler.AssembleCar(chassis, engine, interior, wheels);
                var paintedCar = _painter.PaintCar(car, spec.PaintJob);
                cars.Add(paintedCar);
            }
            return cars;
        }
    }
}