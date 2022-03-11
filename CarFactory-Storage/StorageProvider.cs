using CarFactory_Factory;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using CarFactory_Domain;
using System.Threading;
using CarFactory_Domain.Engine.EngineSpecifications;

namespace CarFactory
{
    public class StorageProvider : IStorageProvider
    {
        public SQLiteConnection GetConnection()
        {
            var firstRun = !File.Exists("factory.sqlite");
            var conn = new SQLiteConnection(@"Data Source=factory.sqlite;Pooling=true;FailIfMissing=false");
            conn.Open();
            if (firstRun)
            {
                CreateMissingTables(conn);
                AddData(conn);
            }
            return conn;
        }

        private static void CreateMissingTables(SQLiteConnection conn)
        {
            using var cmd = new SQLiteCommand(conn);
            
            cmd.CommandText = @"CREATE TABLE part(id INTEGER PRIMARY KEY, type TEXT)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE manufacturer(id INTEGER PRIMARY KEY, name TEXT)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE manufacturer_part(Id INTEGER PRIMARY KEY, manufacturerId INTEGER, partId INTEGER INTEGER)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE chassis_recipe(Id INTEGER PRIMARY KEY, manufacturerId INTEGER, frontId INTEGER, frontCost INTEGER, cabinId INTEGER, cabinCost INTEGER, backId INTEGER, backCost INTEGER )";
            cmd.ExecuteNonQuery();

            cmd.CommandText = @"CREATE TABLE engine_specification(Id INTEGER PRIMARY KEY, manufacturerId INTEGER, cylinderCount INTEGER, propulsion INTEGER, specificationName TEXT)";
            cmd.ExecuteNonQuery();
        }

        private static void AddData(SQLiteConnection conn)
        {
            using var cmd = new SQLiteCommand(conn);

            foreach (PartType partType in Enum.GetValues<PartType>())
            {
                cmd.CommandText = $"INSERT INTO part(id,type) VALUES({(int)partType},'{partType.ToString()}')";
                cmd.ExecuteNonQuery();
            }

            foreach (Manufacturer manufacturer in Enum.GetValues<Manufacturer>())
            {
                cmd.CommandText = $"INSERT INTO manufacturer(id,name) VALUES({(int)manufacturer},'{manufacturer.ToString()}')";
                cmd.ExecuteNonQuery();
            }

            InsertPart(conn, Manufacturer.Plandrover, PartType.Rubber, 200);
            InsertPart(conn, Manufacturer.Plandrover, PartType.Leather, 900);
            InsertPart(conn, Manufacturer.Plandrover, PartType.Cotton, 700);
            InsertPart(conn, Manufacturer.Plandrover, PartType.Wood, 1000);
            InsertPart(conn, Manufacturer.Plandrover, PartType.Plastic, 1100);

            InsertChassisRecipe(conn, Manufacturer.Plandrover, 1, 60, 1, 75, 2, 75);
            InsertChassisRecipe(conn, Manufacturer.Planborghini, 0, 100, 0, 100, 0, 100);
            InsertChassisRecipe(conn, Manufacturer.PlanfaRomeo, 2, 50, 1, 75, 0, 50);
            InsertChassisRecipe(conn, Manufacturer.PlandayMotorWorks, 1, 60, 0, 75, 1, 75);
            InsertChassisRecipe(conn, Manufacturer.AstonPlanday, 0, 100, 1, 75, 0, 75);

            InsertEngineSpecification(conn, Manufacturer.AstonPlanday, new GasolineV12());
            InsertEngineSpecification(conn, Manufacturer.Planborghini, new GasolineV12());
            InsertEngineSpecification(conn, Manufacturer.PlandayMotorWorks, new DieselStraight4());
            InsertEngineSpecification(conn, Manufacturer.Plandrover, new GasolineV8());
            InsertEngineSpecification(conn, Manufacturer.PlanfaRomeo, new GasolineV6());

        }

        private static void InsertPart(SQLiteConnection conn, Manufacturer manufacturer, PartType partType, int amount)
        {
            using var cmd = new SQLiteCommand(conn);
            cmd.CommandText = $"INSERT INTO manufacturer_part(manufacturerId,partId) VALUES({(int)manufacturer},{(int)partType})";

            for (int i = 0; i < amount; i++)
            {
                cmd.ExecuteNonQuery();
            }
        }

        private static void InsertChassisRecipe(SQLiteConnection conn, Manufacturer manufacturer, int frontId, int frontCost, int cabinId, int cabinCost, int backId, int backCost)
        {
            using var cmd = new SQLiteCommand(conn);
            cmd.CommandText = $"INSERT INTO chassis_recipe(manufacturerId, frontId, frontCost, cabinId, cabinCost, backId, backCost ) VALUES({(int)manufacturer},{frontId}, {frontCost}, {cabinId}, {cabinCost}, {backId}, {backCost})";
            cmd.ExecuteNonQuery();
        }

        private static void InsertEngineSpecification(SQLiteConnection conn, Manufacturer manufacturer, EngineSpecification specification)
        {
            using var cmd = new SQLiteCommand(conn);
            cmd.CommandText = $"INSERT INTO engine_specification(manufacturerId, cylinderCount, propulsion, specificationName) VALUES({(int) manufacturer},{specification.CylinderCount},{(int) specification.PropulsionType},'{specification.Name}')";
            cmd.ExecuteNonQuery();
        }
    }
}
