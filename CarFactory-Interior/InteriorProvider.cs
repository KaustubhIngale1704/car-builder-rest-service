using CarFactory_Domain;
using CarFactory_Factory;
using CarFactory_Interior.Interfaces;

namespace CarFactory_Interior
{
    public class InteriorProvider : IInteriorProvider
    {
        private IDashboardBuilder _dashboardBuilder;
        private ISeatBuilder _seatBuilder;
        private ISpeakerBuilder _speakerBuilder;

        public InteriorProvider(
            IDashboardBuilder dashboardBuilder, 
            ISeatBuilder seatBuilder, 
            ISpeakerBuilder speakerBuilder)
        {
            _dashboardBuilder = dashboardBuilder;
            _seatBuilder = seatBuilder;
            _speakerBuilder = speakerBuilder;
        }

        public Interior GetInterior(CarSpecification specification)
        {
            
            return new Interior
            {
                Dashboard = _dashboardBuilder.Build(),
                Seats = _seatBuilder.Build(),
                FrontWindowSpeakers = _speakerBuilder.BuildFrontWindowSpeakers(specification.FrontWindowSpeakers)
            };
        }
    }
}