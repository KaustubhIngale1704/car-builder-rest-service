using System;
using System.Collections.Generic;
using System.Linq;
using CarFactory_Domain;
using CarFactory_Domain.Engine;
using CarFactory_Factory;

namespace CarFactory_Assembly
{
    public class CarAssembler : ICarAssembler
    {
        /*
         * 
         * When working with this file, please do not 
         * modify existing logic in the constructor nor the CalibrateLocks method
         */
        public Car AssembleCar(Chassis chassis, Engine engine, Interior interior, IEnumerable<Wheel> wheels)
        {
            if (chassis == null || engine == null || interior == null || wheels == null) throw new ArgumentNullException();
            if (wheels.Count() != 4) throw new Exception("Common cars must have 4 wheels");
            var car = new Car(chassis, engine, interior, wheels);
            CalibrateLocks(car);
            return car;
        }

        private void CalibrateLocks(Car car)
        {
            var partsHash = car.Chassis.GetHashCode() + car.Engine.GetHashCode() + car.Interior.GetHashCode() + car.Wheels.GetHashCode();
            var lockSetting = GoBig(partsHash % 3, partsHash % 10);
            car.SetCarLockSettings(lockSetting);
        }

        private long GoBig(long m, long n)
        {
            if (m > 0)
            {
                if (n > 0)
                    return GoBig(m - 1, GoBig(m, n - 1));
                else if (n == 0)
                    return GoBig(m - 1, 1);
            }
            else if (m == 0)
            {
                if (n >= 0)
                    return n + 1;
            }
            throw new ArgumentOutOfRangeException();
        }
    }
    
    
}