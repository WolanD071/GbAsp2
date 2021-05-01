using System.ComponentModel.DataAnnotations;

namespace GbWebApp.Domain.ViewModels
{
    public record ProductViewModel
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string ImageUrl { get; init; }
        [Range(minimum: 1, maximum: int.MaxValue, ErrorMessage = "Product price must be from 1 to 2 147 483 647.00$ !!!")]
        public decimal Price { get; init; }
        public int BrandId { get; init; }
        public string Brand { get; init; }
        public int SectionId { get; init; }
        public string Section { get; init; }
    }
}