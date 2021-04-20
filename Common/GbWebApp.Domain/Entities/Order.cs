using System;
using System.Collections.Generic;
using GbWebApp.Domain.Entities.Base;
using GbWebApp.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace GbWebApp.Domain.Entities
{
    public class Order : NamedEntity
    {
        [Required]
        public virtual User User { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public virtual ICollection<OrderItem> Items { get; init; } = new HashSet<OrderItem>();
    }
}
