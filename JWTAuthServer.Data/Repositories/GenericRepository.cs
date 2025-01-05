using JWTAuthServer.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthServer.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        public GenericRepository(DbContext dbContext)
        {
            _context = dbContext;
        }
        private DbSet<TEntity> Table { get => _context.Set<TEntity>(); }
        public async Task AddAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Table.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Table.ToListAsync();
        }

        public TEntity GetById(int id)
        {
            var entity = Table.Find(id);
            if (entity != null)
                _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            var entity = await Table.FindAsync(id);
            if (entity != null)
                _context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public void Remove(TEntity entity)
        {
            Table.Remove(entity);
        }

        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IQueryable<TEntity> Where(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return Table.Where(predicate);
        }
    }
}
