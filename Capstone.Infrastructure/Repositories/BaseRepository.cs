using Capstone.Core.Entities;
using Capstone.Core.Interfaces;
using Capstone.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly CapstoneContext _context;
        protected readonly DbSet<T> _entities;
        public BaseRepository(CapstoneContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }
        public void Add(T entity)
        {
            entity.IsDeleted = false;
            _entities.AddAsync(entity);
        }

        public void Delete(int?[] id)
        {
            var entities = _entities.Where(f => id.Contains(f.Id)).ToList();
            entities.ForEach(a => a.IsDeleted = true);
            _context.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll()
        {
            return _entities.Where(x => x.IsDeleted == false).AsEnumerable().ToList();
        }

        public T GetById(int? id)
        {
            return _entities.Find(id);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }
    }
}
