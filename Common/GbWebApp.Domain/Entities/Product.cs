using GbWebApp.Domain.Entities.Base;
using GbWebApp.Domain.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace GbWebApp.Domain.Entities
{
    public class Product : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int SectionId { get; set; }

        [ForeignKey(nameof(SectionId))]
        public Section Section { get; set; } // so called 'navigation property'

        public int? BrandId { get; set; }

        [ForeignKey(nameof(BrandId))]
        public Brand Brand { get; set; } // so called 'navigation property'

        public string ImageUrl { get; set; }

        [Column(TypeName = "decimal(18,2)")] // otherwise EF will swear!
        public decimal Price { get; set; }
    }
}
