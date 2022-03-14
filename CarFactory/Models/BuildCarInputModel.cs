using System.Collections.Generic;

namespace CarFactory.Models
{
    public class BuildCarInputModel
    {
        public IEnumerable<BuildCarInputModelItem> Cars { get; set; }
    }
}
