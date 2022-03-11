using System.Collections.Generic;
using CarFactory_Domain;

namespace CarFactory_Storage
{
    public interface IGetChassisRecipeQuery
    {
        ChassisRecipe Get(Manufacturer manufacturer);
    }
}