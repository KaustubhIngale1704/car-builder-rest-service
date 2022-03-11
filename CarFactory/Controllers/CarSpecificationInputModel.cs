using CarFactory_Domain;

namespace CarFactory.Controllers
{
    public class CarSpecificationInputModel
    {
        public int NumberOfDoors { get; set; }
        public CarPaintSpecificationInputModel Paint { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public SpeakerSpecificationInputModel[] FrontWindowSpeakers { get; set; }
        public SpeakerSpecificationInputModel[] DoorSpeakers { get; set; }
    }
}
