using System;
using System.Collections.Generic;
using CarFactory_Domain;
using CarFactory_Interior.Interfaces;

namespace CarFactory_Interior.Builders
{
    public class SeatBuilder : ISeatBuilder
    {
        public IEnumerable<Seat> Build()
        {
            return new List<Seat>()
            {
                new Seat(){PartType = PartType.Leather},
                new Seat(){PartType = PartType.Leather},
                new Seat(){PartType = PartType.Leather},
                new Seat(){PartType = PartType.Leather}
            };
        }
    }
}