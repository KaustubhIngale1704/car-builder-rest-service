using CarFactory.Utilities;
using CarFactory_Domain;
using CarFactory_Domain.Engine;
using CarFactory_Domain.Engine.EngineSpecifications;
using CarFactory_Factory;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading;

namespace CarFactory_Storage
{
    public class GetEngineSpecificationQuery : IGetEngineSpecificationQuery
    {
        private readonly IStorageProvider _storageProvider;

        public GetEngineSpecificationQuery(IStorageProvider storageProvider)
        {
            _storageProvider = storageProvider;
        }

        public EngineSpecification GetForManufacturer(Manufacturer manufacturer)
        {
            using var conn = _storageProvider.GetConnection();
            using var cmd = new SQLiteCommand(conn);

            cmd.CommandText = @$"Select * from engine_specification where manufacturerId = {(int)manufacturer}";
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            var specifications = new List<EngineSpecification>();

            while (rdr.Read())
            {
                specifications.Add(new EngineSpecification { CylinderCount = rdr.GetInt32(2), PropulsionType = (Propulsion)rdr.GetInt32(3), Name = rdr.GetString(4) });
            }

            SlowWorker.FakeWorkingForMillis(350);

            return specifications.Single();
        }
    }
}
