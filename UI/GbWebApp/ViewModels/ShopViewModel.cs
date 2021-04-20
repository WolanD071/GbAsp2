using System.Collections.Generic;

namespace GbWebApp.ViewModels
{
    public record ShopViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; init; }

        public int? SectionId { get; init; }

        public int? BrandId { get; set; }

        public PaginationViewModel PagingInfo { get; set; }
    }
}
