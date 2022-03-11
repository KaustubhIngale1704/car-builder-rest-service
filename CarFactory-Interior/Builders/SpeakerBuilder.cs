using System;
using System.Collections.Generic;
using System.Linq;
using CarFactory_Domain;
using CarFactory_Interior.Interfaces;
using static CarFactory_Factory.CarSpecification;

namespace CarFactory_Interior.Builders
{
    public class SpeakerBuilder : ISpeakerBuilder
    {
        public List<Speaker> BuildFrontWindowSpeakers(IEnumerable<SpeakerSpecification> specification)
        {
            if (specification.ToArray().Length > 2) throw new ArgumentException("More than 2 speakers aren't supported");
            return specification.Select(spec =>
                new Speaker { IsSubwoofer = spec.IsSubwoofer }
            )
                .ToList();
        }
    }
}