using System.Collections.Generic;
using GbWebApp.Domain.ViewModels;

namespace GbWebApp.ViewModels
{
    public class BrandsViewModelId
    {
        public IEnumerable<BrandsViewModel> Brands { get; set; }

        public int? BrandId { get; set; }
    }
}
