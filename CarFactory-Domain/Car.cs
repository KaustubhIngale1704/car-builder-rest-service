using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarFactory_Domain
{
    public class Car
    {
        public Chassis Chassis { get; set; }
        public Engine.Engine Engine { get; }
        public PaintJob PaintJob { get; set; }
        public Interior Interior { get; set; }
        public IEnumerable<Wheel> Wheels { get; set; }
        public long CarLockSetting { get; private set; }
        public Car(Chassis chassis, Engine.Engine engine, Interior interior, IEnumerable<Wheel> wheels)
        {
            Chassis = chassis;
            Engine = engine;
            Interior = interior;
            Wheels = wheels;
        }

        public void SetCarLockSettings(long setting)
        {
            CarLockSetting = setting;
        }
    }
}
