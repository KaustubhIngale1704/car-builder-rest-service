using System.Collections.Generic;
using CarFactory_Domain;

namespace CarFactory_Interior.Interfaces
{
    public interface ISeatBuilder
    {
        IEnumerable<Seat> Build();
    }
}