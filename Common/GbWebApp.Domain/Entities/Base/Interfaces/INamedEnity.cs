using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GbWebApp.Domain.Entities.Base.Interfaces
{
    public interface INamedEntity : IEntity
    {
        string Name { get; set; }
    }
}
