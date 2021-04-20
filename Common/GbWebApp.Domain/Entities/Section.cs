using GbWebApp.Domain.Entities.Base;
using GbWebApp.Domain.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace GbWebApp.Domain.Entities
{
    public class Section : NamedEntity, IOrderedEntity
    {
        public int Order { get; set; }

        public int? ParentId { get; set; }

        // following properties are for dbms...

        [ForeignKey(nameof(ParentId))]
        public Section Parent { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
