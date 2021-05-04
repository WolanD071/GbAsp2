using System.Collections.Generic;
using System.Linq;
using GbWebApp.Domain.Entities.Base.Interfaces;

namespace GbWebApp.Interfaces.Services
{
    public interface IAnyEntityCRUD<T> where T : class, IEntity
    {
        IEnumerable<T> Get();
        T Get(int id);
        int Add(T emp);
        void Update(T emp);
        bool Delete(int id);
    }
}
