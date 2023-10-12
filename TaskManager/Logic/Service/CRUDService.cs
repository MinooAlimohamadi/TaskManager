using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using TaskManager.Logic.Data;
using TaskManager.Logic.Infrastructure;

namespace TaskManager.Logic.Service
{
    public class CRUDService : IDisposable
    {
        private IDbContextTransaction? dbTr;
        private readonly DbContext _dbContext;

        public CRUDService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> GET<T>() where T : BaseDomain
        {
            var entity = _dbContext.Set<T>();
            var query = entity.Where(a => !a.IsDelete);

            return query;
        }

        public async Task<T?> GET<T>(object id) where T : BaseDomain
        {
            try
            {
                var entity = _dbContext.Set<T>();

                var item = await entity.FindAsync(id);

                if (item != null && item.IsDelete) { return null; }

                return item;
            }

            catch { return null; }
        }

        public async Task<T?> POST<T>(T dbo) where T : BaseDomain
        {
            try
            {
                dbo.CreateDate = DateTime.Now;
                _dbContext.Entry(dbo).State = EntityState.Added;
                if (dbTr == null) { await _dbContext.SaveChangesAsync(); }

                return dbo;
            }

            catch { return null; }
        }

        public async Task<T?> PUT<T>(T dbo) where T : BaseDomain
        {
            try
            {
                _dbContext.Entry(dbo).State = EntityState.Modified;
                if (dbTr == null) { await _dbContext.SaveChangesAsync(); }

                return dbo;
            }

            catch { return null; }
        }

        public async Task<T?> DELETE<T>(object id, bool PhysicalDelete = false) where T : BaseDomain
        {
            try
            {
                var entity = await GET<T>(id);

                if (entity != null)
                {
                    return await DELETE(entity, PhysicalDelete);
                }
            }
            catch { }
            return null;
        }

        public async Task<T?> DELETE<T>(T dbo, bool PhysicalDelete = false) where T : BaseDomain
        {
            try
            {
                if (PhysicalDelete)
                {
                    _dbContext.Entry(dbo).State = EntityState.Deleted;

                    if (dbTr == null) { await _dbContext.SaveChangesAsync(); }
                }
                else
                {
                    dbo.IsDelete = true;

                    await PUT(dbo);
                }

                return dbo;
            }
            catch { return null; }
        }
      
        public void Dispose()
        {
            dbTr?.Dispose();
        }
    }
}
