using GbWebApp.Domain.Entities.Base;
using GbWebApp.Domain.Entities.Base.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GbWebApp.Domain.Entities
{
    [Table("Brands")] // if not written, system will generate this name automatically
    public class Brand : NamedEntity, IOrderedEntity
    {
        [Column("BrandOrder")]
        public int Order { get; set; }

        // following property is for dbms...

        public ICollection<Product> Products { get; set; }
    }
}
