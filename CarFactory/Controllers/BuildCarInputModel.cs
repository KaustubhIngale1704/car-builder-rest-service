using System.Collections.Generic;

namespace CarFactory.Controllers
{
    public class BuildCarInputModel
    {
        public IEnumerable<BuildCarInputModelItem> Cars { get; set; }
    }
}
