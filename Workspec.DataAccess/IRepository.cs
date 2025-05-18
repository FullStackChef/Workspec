namespace Workspec.DataAccess;

/// <summary>
/// Represents a generic repository interface for accessing and manipulating data in a database.
/// </summary>
/// <typeparam name="TDbRecord">The type of the database record.</typeparam>
public interface IRepository<TDbRecord> : IDisposable where TDbRecord : class
{
    /// <summary>
    /// Asynchronously creates a new record in the database.
    /// </summary>
    /// <param name="record">The record to create.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the newly created record.</returns>
    ValueTask<TDbRecord> CreateAsync(TDbRecord record);

    /// <summary>
    /// Asynchronously creates multiple new records in the database.
    /// </summary>
    /// <param name="records">The records to create.</param>
    /// <returns>A task that represents the asynchronous creation operation.</returns>
    ValueTask CreateAsync(IEnumerable<TDbRecord> records);

    /// <summary>
    /// Asynchronously retrieves a record by its identifier.
    /// </summary>
    /// <param name="id">The identifier of the record to retrieve.</param>
    /// <returns>A task that represents the asynchronous retrieval operation. The task result contains the retrieved record or null if not found.</returns>
    ValueTask<TDbRecord?> GetAsync(object id);

    /// <summary>
    /// Asynchronously queries records from the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous query operation. The task result contains an IQueryable of retrieved records.</returns>
    ValueTask<IQueryable<TDbRecord>> QueryAsync();

    /// <summary>
    /// Asynchronously updates an existing record identified by a key.
    /// </summary>
    /// <param name="key">The key of the record to update.</param>
    /// <param name="record">The updated record.</param>
    /// <returns>A task that represents the asynchronous update operation.</returns>
    ValueTask UpdateAsync(object key, TDbRecord record);

    /// <summary>
    /// Asynchronously deletes a record identified by a key.
    /// </summary>
    /// <param name="key">The key of the record to delete.</param>
    /// <returns>A task that represents the asynchronous delete operation.</returns>
    ValueTask DeleteAsync(object key);
}
