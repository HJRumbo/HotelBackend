using System.Linq.Expressions;

namespace Hotel.Core.Application.Interfaces.Infrastructure.Persistence
{
    public interface IGenericRepositoryAsync<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(int id);
        Task<List<TEntity>> GetAllWithIncludeAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<List<TEntity>> Where(Func<TEntity, bool> predicate);
        Task<TEntity> SaveAsync(TEntity data);
        void Update(TEntity data);
        void Delete(TEntity data);
    }
}
