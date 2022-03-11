using System.Collections.Generic;
using CarFactory_Domain;

namespace CarFactory_Storage
{
    public interface IGetRubberQuery
    {
        IEnumerable<Part> Get();
    }
}