namespace CarFactory.Controllers
{
    public class CarPaintSpecificationInputModel
    {
        public string Type { get; set; }
        public string BaseColor { get; set; }
        public string? StripeColor { get; set; }
        public string? DotColor { get; set; }
    }
}
