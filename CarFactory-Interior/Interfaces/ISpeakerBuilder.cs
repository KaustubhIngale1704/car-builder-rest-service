using System.Collections.Generic;
using CarFactory_Domain;
using static CarFactory_Factory.CarSpecification;

namespace CarFactory_Interior.Interfaces
{
    public interface ISpeakerBuilder
    {
        List<Speaker> BuildFrontWindowSpeakers(IEnumerable<SpeakerSpecification> specification);
    }
}