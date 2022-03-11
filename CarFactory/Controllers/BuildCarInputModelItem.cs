using System.ComponentModel.DataAnnotations;

namespace CarFactory.Controllers
{
    public class BuildCarInputModelItem
    {
        [Required]
        public int Amount { get; set; }
        [Required]
        public CarSpecificationInputModel Specification { get; set; }
    }
}
