using CarFactory_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarFactory_Chasis
{
    public class ChassisWelder
    {
        private ChassisPart _firstPart;
        private ChassisPart _secondPart;
        private ChassisPart _thirdPart;
        public bool StartWeld(ChassisPart firstPart)
        {
            if(_firstPart == null)
            {
                _firstPart = firstPart;
                return true;
            }
            return false;
        }

        public bool ContinueWeld(ChassisPart secondPart, int numberOfDoors)
        {
            if (_secondPart == null)
            {
                _secondPart = secondPart;
                return true;
            }
            for(var i = 0; i != numberOfDoors; i++)
            {
                //TODO: Weld door
            }
            return false;
        }

        public bool FinishWeld(ChassisPart thirdPart)
        {
            if (_thirdPart == null)
            {
                _thirdPart = thirdPart;
                return true;
            }
            return false;
        }

        public Chassis GetChassis()
        {
            if(_firstPart == null || _secondPart == null || _thirdPart == null)
            {
                throw new Exception("Chassis not finished");
            }
            var isValid = false;
            if(_firstPart.GetType() == "ChassisBack" && _secondPart.GetType() == "ChassisCabin" && _thirdPart.GetType() == "ChassisFront")
            {
                isValid = true;
            }


            var description = _secondPart.GetChassisType()+ " " + _firstPart.GetChassisType() + " " + _thirdPart.GetChassisType();
            return new Chassis(description, isValid);
        }
    }
}
