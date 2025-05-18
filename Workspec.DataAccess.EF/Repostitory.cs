using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Workspec.DataAccess.EF
{
    public class Repostitory<DbRecord>(
        IDbContextFactory<WorkspecDbContext> _dbContextFactory
        ) : IRepository<DbRecord> where DbRecord : class
    {
        private WorkspecDbContext? _dbContext;
        private bool disposedValue;

        public async ValueTask<DbRecord> CreateAsync(DbRecord record)
        {
            await CreateDbContextInstanceAsync(_dbContextFactory).ConfigureAwait(false);
            ArgumentNullException.ThrowIfNull(_dbContext);
            var id = _dbContext.Set<DbRecord>().Add(record);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            return id.Entity;
        }

        public async ValueTask CreateAsync(IEnumerable<DbRecord> records)
        {
            await CreateDbContextInstanceAsync(_dbContextFactory).ConfigureAwait(false);

            ArgumentNullException.ThrowIfNull(_dbContext);
            _dbContext.Set<DbRecord>().AddRange(records);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async ValueTask DeleteAsync(object key)
        {
            await CreateDbContextInstanceAsync(_dbContextFactory).ConfigureAwait(false);

            ArgumentNullException.ThrowIfNull(_dbContext);
            var record = await _dbContext.Set<DbRecord>().FindAsync(key).ConfigureAwait(false);

            ArgumentNullException.ThrowIfNull(record, nameof(record));

            _dbContext.Set<DbRecord>().Remove(record);
            await _dbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async ValueTask<DbRecord?> GetAsync(object id)
        {
            await CreateDbContextInstanceAsync(_dbContextFactory).ConfigureAwait(false);
            ArgumentNullException.ThrowIfNull(_dbContext);
            return await _dbContext.Set<DbRecord>().FindAsync(id).ConfigureAwait(false);
        }

        public async ValueTask<IQueryable<DbRecord>> QueryAsync()
        {
            await CreateDbContextInstanceAsync(_dbContextFactory).ConfigureAwait(false);
            ArgumentNullException.ThrowIfNull(_dbContext);
            return _dbContext.Set<DbRecord>().AsQueryable();
        }

        public async ValueTask UpdateAsync(object key, DbRecord record)
        {
            await CreateDbContextInstanceAsync(_dbContextFactory).ConfigureAwait(false);
            ArgumentNullException.ThrowIfNull(_dbContext);
            var existingRecord = await _dbContext.Set<DbRecord>().FindAsync(key).ConfigureAwait(false);

            ArgumentNullException.ThrowIfNull(existingRecord, nameof(existingRecord));

            if (existingRecord != record)
            {
                _dbContext.Entry(existingRecord).State = EntityState.Detached;

                _dbContext.Set<DbRecord>().Update(record);
                
                await _dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }


        private async Task CreateDbContextInstanceAsync(IDbContextFactory<WorkspecDbContext> _dbContextFactory)
        {
            _dbContext = await _dbContextFactory.CreateDbContextAsync().ConfigureAwait(false);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_dbContextFactory is IDisposable disposable)
                    {
                        disposable.Dispose();
                    }
                    if (_dbContext is not null)
                    {
                        _dbContext.Dispose();
                    }
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Repostitory()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
