using CarFactory_Domain;

namespace CarFactory_Factory
{
    public interface IPainter
    {
        Car PaintCar(Car car, PaintJob paint);
    }
}