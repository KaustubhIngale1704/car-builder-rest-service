using System;
using System.Collections.Generic;
using System.Linq;
using CarFactory_Domain;
using CarFactory_Factory;
using CarFactory_Storage;

namespace CarFactory_Wheels
{
    public class WheelProvider : IWheelProvider
    {
        private readonly IGetRubberQuery _getRubberQuery;

        public WheelProvider(IGetRubberQuery getRubberQuery)
        {
            _getRubberQuery = getRubberQuery;
        }

        public IEnumerable<Wheel> GetWheels()
        {
            var rubber = _getRubberQuery.Get();
            
            return new[]
            {
                CreateWheel(ref rubber),
                CreateWheel(ref rubber),
                CreateWheel(ref rubber),
                CreateWheel(ref rubber)
            };
        }

        private Wheel CreateWheel(ref IEnumerable<Part> allRubber)
        {
            var rubber = allRubber.Take(50);
            
            if (rubber.Any(x => x.PartType != PartType.Rubber))
            {
                throw new Exception("parts must be rubber");
            }
            
            return new Wheel(){Manufacturer = rubber.First().Manufacturer};
        }
    }
}