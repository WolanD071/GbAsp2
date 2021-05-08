using GbWebApp.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GbWebApp.Domain.Entities
{
    public class OrderItem : Entity
    {
        [Required]
        public Order Order { get; set; }

        public int ProductId { get; set; }  // will read exactly int value from the field, not an object

        [Required]
        [ForeignKey(nameof(ProductId))]     // attribute 'ForeignKey' is required in this case
        public Product Product { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [NotMapped]
        public decimal TotalItemPrice => Price * Quantity;
    }
}
