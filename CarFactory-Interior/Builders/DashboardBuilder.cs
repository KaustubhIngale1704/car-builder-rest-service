using System;
using CarFactory_Domain;
using CarFactory_Interior.Interfaces;

namespace CarFactory_Interior.Builders
{
    public class DashboardBuilder : IDashboardBuilder
    {
        public Dashboard Build()
        {
            return new Dashboard()
            {
                PartType = PartType.Wood,
                HasTouchScreen = true,
                HasGPS = true
            };
        }
    }
}