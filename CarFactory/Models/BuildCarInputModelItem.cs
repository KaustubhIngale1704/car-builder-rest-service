using System.ComponentModel.DataAnnotations;

namespace CarFactory.Models
{
    public class BuildCarInputModelItem
    {
        [Required]
        public int Amount { get; set; }
        [Required]
        public CarSpecificationInputModel Specification { get; set; }
    }
}
