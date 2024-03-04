using Hotel.Core.Application.Interfaces.Infrastructure.Persistence;
using Hotel.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Hotel.Infrastructure.Persistence.Repositories
{
    public class GenericRepositoryAsync<TEntity> : IGenericRepositoryAsync<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<TEntity> _entity;

        public GenericRepositoryAsync(AppDbContext context)
        {
            _context = context;
            _entity = context.Set<TEntity>();
        }

        public void Delete(TEntity data)
        {
            _entity.Remove(data);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _entity.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id) => await _entity.FindAsync(id)!;

        public async Task<List<TEntity>> GetAllWithIncludeAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            var list = await query.ToListAsync();

            return list;
        }

        public async Task<TEntity> SaveAsync(TEntity data)
        {
            var entity = await _entity.AddAsync(data);

            return entity.Entity;
        }

        public void Update(TEntity data)
        {
            _entity.Attach(data);
            _context.Entry(data).State = EntityState.Modified;
        }

        public async Task<List<TEntity>> Where(Func<TEntity, bool> predicate)
        {
            return await Task.Run(() => _entity.Where(predicate).ToList());
        }
    }
}
