using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using CarFactory_Domain;
using CarFactory_Factory;

namespace CarFactory_Storage
{
    public class GetChassisRecipeQuery : IGetChassisRecipeQuery
    {
        private readonly IStorageProvider _storageProvider;

        public GetChassisRecipeQuery(IStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
        }

        public ChassisRecipe Get(Manufacturer manufacturer)
        {
            using var conn = _storageProvider.GetConnection();
            using var cmd = new SQLiteCommand(conn);
            
            cmd.CommandText = @"Select * from chassis_recipe";
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            var recipes = new List<ChassisRecipe>();
            while (rdr.Read())
            {
                recipes.Add(new ChassisRecipe((Manufacturer)rdr.GetInt32(1), rdr.GetInt32(2), rdr.GetInt32(3), rdr.GetInt32(4), rdr.GetInt32(5), rdr.GetInt32(6), rdr.GetInt32(7)));
            }

            return recipes.First(x => x.Manufacturer == manufacturer);
        }
    }
}