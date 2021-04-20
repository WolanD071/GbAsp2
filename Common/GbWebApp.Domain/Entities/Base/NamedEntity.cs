using GbWebApp.Domain.Entities.Base.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace GbWebApp.Domain.Entities.Base
{
    public abstract class NamedEntity : Entity, INamedEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
