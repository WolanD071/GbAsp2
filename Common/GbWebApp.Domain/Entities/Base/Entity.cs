using GbWebApp.Domain.Entities.Base.Interfaces;
//using System.ComponentModel.DataAnnotations;

namespace GbWebApp.Domain.Entities.Base
{
    public abstract class Entity : IEntity
    {
        //[Key] // when field is int and named 'Id', the system itself will take it as primary key
        public int Id { get; set; }
    }
}
