using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Workspec.DataAccess;
using Workspec.DataAccess.Models.Systems;
using Workspec.Domain.Modules.SubscriptionManagement.Services;
using Workspec.Domain.Modules.Systems.API;
using Workspec.Domain.Modules.Systems.Models;

namespace Workspec.Domain.Modules.Systems.Services;

/// <summary>
/// Represents a service for managing records.
/// </summary>
public interface IComponentTypeService
{
    /// <summary>
    /// Retrieves all records.
    /// </summary>
    /// <returns>The result of the operation.</returns>
    Task<IResult> Query();

    /// <summary>
    /// Retrieves a record by its ID.
    /// </summary>
    /// <param name="recordId">The ID of the record.</param>
    /// <returns>The result of the operation.</returns>
    Task<IResult> Get(int recordId);

    /// <summary>
    /// Creates a new record.
    /// </summary>
    /// <param name="createRecord">The create record.</param>
    /// <returns>The result of the operation.</returns>
    Task<IResult> Create(CreateComponentTypeRecord createRecord);

    /// <summary>
    /// Updates an existing record.
    /// </summary>
    /// <param name="recordId">The ID of the record.</param>
    /// <param name="updateRecord">The update record.</param>
    /// <returns>The result of the operation.</returns>
    Task<IResult> Update(int recordId, UpdateComponentTypeRecord updateRecord);
}
/// <summary>
/// Represents a service for managing records.
/// </summary>
public class ComponentTypeService(
    ILogger<ComponentTypeService> logger,
    IRepository<ComponentTypeDbRecord> repository,
    ISubscriptionLimitService subscriptionLimits,
    ITenantResolutionService tenantResolutionService
    ) : IComponentTypeService
{
    /// <inheritdoc/>
    public async Task<IResult> Query()
    {
        var records = await repository.QueryAsync().ConfigureAwait(false);
        return Results.Ok(records);
    }

    /// <inheritdoc/>
    public async Task<IResult> Get(int recordId)
    {
        var record = await repository.GetAsync(recordId).ConfigureAwait(false);
        return record == null ? Results.NotFound() : Results.Ok(record);
    }

    /// <inheritdoc/>
    public async Task<IResult> Create(CreateComponentTypeRecord recordRecord)
    {
        Guid tenantId = tenantResolutionService.GetTenantId();

        //var checkLimitResult = await subscriptionLimits.CheckLimitAsync(tenantId, FeatureType.Maxrecords, 1).ConfigureAwait(false);
        //if (checkLimitResult is not Ok)
        //{
        //    logger.LogWarning("record limit reached for tenant {tenantId}", tenantId);
        //    return checkLimitResult;
        //}

        var dbrecord = recordRecord.ToDbRecord(tenantId);
        var createResult = await repository.CreateAsync(dbrecord).ConfigureAwait(false);

        return Results.Created($"{ComponentTypeApi.BaseRoute}/{createResult.Id}", createResult);
    }

    /// <inheritdoc/>
    public async Task<IResult> Update(int recordId, UpdateComponentTypeRecord recordRecord)
    {
        Guid tenantId = tenantResolutionService.GetTenantId();
        var record = await repository.GetAsync(recordId).ConfigureAwait(false);
        if (record == null)
        {
            return Results.NotFound();
        }

        await repository.UpdateAsync(recordId, record.UpdateFrom(recordRecord)).ConfigureAwait(false);
        return Results.Accepted();
    }
}
