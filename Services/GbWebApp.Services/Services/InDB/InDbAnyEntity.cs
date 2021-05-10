using System;
using System.Linq;
using GbWebApp.DAL.Context;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using GbWebApp.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using GbWebApp.Domain.Entities.Base.Interfaces;

namespace GbWebApp.Services.Services.InDB
{
    public class InDbAnyEntity<T> : IAnyEntityCRUD<T>
        where T : class, IEntity
    {
        private readonly GbWebAppDB _db;
        private readonly DbSet<T> _tbl;
        private readonly ILogger _logger;

        public InDbAnyEntity(GbWebAppDB db, ILogger<InDbAnyEntity<T>> logger)
        {
            _db = db;
            _logger = logger;
            _tbl = db.Set<T>();
        }

        public int Add(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            if (_tbl.Contains(entity)) return entity.Id;

            _logger.LogInformation($"Adding new '{typeof(T)}' entity to DB...");
            using (_logger.BeginScope("*** ADDING NEW ENTITY SCOPE ***"))
            {
                entity.Id = 0; // for that reason to ensure that 'Id' is 0 and avoid DB-exception
                _tbl.Add(entity);
                _db.SaveChanges();
                _logger.LogInformation($"...completed successfully! id={entity.Id}");
            }
            return entity.Id;
        }

        public bool Delete(int id)
        {
            var dbItem = Get(id);
            if (dbItem is null) return false;

            _logger.LogInformation($"Removing '{typeof(T)}' entity with id={dbItem.Id} from DB...");
            using (_logger.BeginScope("*** REMOVING ENTITY SCOPE ***"))
            {
                _tbl.Remove(dbItem);
                _db.SaveChanges();
                _logger.LogInformation("...completed successfully!");
            }
            return true;
        }

        public IEnumerable<T> Get() => _tbl;

        public T Get(int id) => _tbl.FirstOrDefault(e => e.Id == id);

        public void Update(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            _logger.LogInformation($"Updating '{typeof(T)}' entity with id={entity.Id}...");
            using (_logger.BeginScope("*** UPDATING ENTITY SCOPE ***"))
            {
                _db.Entry(entity).State = EntityState.Modified;
                _db.SaveChanges();
                _logger.LogInformation("...completed successfully!");
            }
        }
    }
}
