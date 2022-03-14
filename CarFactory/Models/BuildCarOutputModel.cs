using System.Collections.Generic;
using CarFactory_Domain;

namespace CarFactory.Models
{
    public class BuildCarOutputModel
    {
        public long RunTime { get; set; }
        public IEnumerable<Car> Cars { get; set; }
    }
}
