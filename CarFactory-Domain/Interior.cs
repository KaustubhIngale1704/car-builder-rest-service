using System.Collections.Generic;

namespace CarFactory_Domain
{
    public class Interior
    {
        public IEnumerable<Speaker> FrontWindowSpeakers { get; set; }
        public IEnumerable<Speaker> DoorSpeakers { get; set; }
        public IEnumerable<Seat> Seats { get; set; }
        public Dashboard Dashboard { get; set; }
    }
}