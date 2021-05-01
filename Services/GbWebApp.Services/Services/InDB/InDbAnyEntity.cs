using System;
using System.Linq;
using GbWebApp.DAL.Context;
using GbWebApp.Domain.Entities.Base.Interfaces;
using GbWebApp.Interfaces.Services;
using Microsoft.EntityFrameworkCore;

namespace GbWebApp.Services.Services.InDB
{
    public class InDbAnyEntity<T> : IAnyEntityCRUD<T>
        where T : class, IEntity
    {
        readonly GbWebAppDB __db;
        readonly DbSet<T> _tbl;

        public InDbAnyEntity(GbWebAppDB db)
        {
            __db = db;
            _tbl = db.Set<T>();
        }

        public int Add(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            if (_tbl.Contains(entity)) return entity.Id;

            entity.Id = 0; // for that reason to ensure that 'Id' is 0 and avoid DB-exception

            //////////////////////////////////////////
            //// stub for further app development...
            //using (__db.Database.BeginTransaction())
            //{   _tbl.Add(entity);
            //    __db.SaveChanges();
            //    __db.Database.CommitTransaction(); }
            //////////////////////////////////////////
            _tbl.Add(entity);
            __db.SaveChanges();
            return entity.Id;
        }

        public bool Delete(int id)
        {
            var db_item = Get(id);
            if (db_item is null) return false;
            _tbl.Remove(db_item);
            __db.SaveChanges();
            return true;
        }

        public IQueryable<T> Get() => _tbl;

        public T Get(int id) => _tbl.FirstOrDefault(e => e.Id == id);

        public void Update(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            ////// the following commented code doesn't work with db!
            //if (_tbl.Contains(entity)) return;
            //var db_item = Get(entity.Id);
            //if (db_item is null) return;
            //_tbl.Update(db_item);
            __db.Entry(entity).State = EntityState.Modified;
            __db.SaveChanges();
        }
    }
}
