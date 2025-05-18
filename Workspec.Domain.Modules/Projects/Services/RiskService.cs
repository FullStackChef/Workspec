using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Workspec.DataAccess;
using Workspec.DataAccess.Models.Projects;
using Workspec.Domain.Modules.Projects.API;
using Workspec.Domain.Modules.Projects.Models;
using Workspec.Domain.Modules.SubscriptionManagement.Services;

namespace Workspec.Domain.Modules.Projects.Services;

/// <summary>
/// Represents a service for managing risks associated with projects.
/// </summary>
public interface IRiskService
{
    /// <summary>
    /// Retrieves all risks.
    /// </summary>
    /// <returns>The result of the operation.</returns>
    Task<IResult> Query();

    /// <summary>
    /// Retrieves a risk by its ID.
    /// </summary>
    /// <param name="riskId">The ID of the risk.</param>
    /// <returns>The result of the operation.</returns>
    Task<IResult> Get(int riskId);

    /// <summary>
    /// Creates a new risk.
    /// </summary>
    /// <param name="riskRecord">The risk record to create.</param>
    /// <returns>The result of the operation.</returns>
    Task<IResult> Create(CreateRiskRecord riskRecord);

    /// <summary>
    /// Updates an existing risk.
    /// </summary>
    /// <param name="riskId">The ID of the risk.</param>
    /// <param name="riskRecord">The risk record to update.</param>
    /// <returns>The result of the operation.</returns>
    Task<IResult> Update(int riskId, RiskUpdateRecord riskRecord);
}

public class RiskService(
    ILogger<RiskService> logger,
    IRepository<RiskDbRecord> riskRepo,
    ITenantResolutionService tenantResolutionService
    ) : IRiskService
{

    /// <inheritdoc/>
    public async Task<IResult> Query()
    {
        var risks = await riskRepo.QueryAsync().ConfigureAwait(false);
        return Results.Ok(risks);
    }

    /// <inheritdoc/>
    public async Task<IResult> Get(int riskId)
    {
        var risk = await riskRepo.GetAsync(riskId).ConfigureAwait(false);
        return risk == null ? Results.NotFound() : Results.Ok(risk);
    }

    /// <inheritdoc/>
    public async Task<IResult> Create(CreateRiskRecord riskRecord)
    {
        Guid tenantId = tenantResolutionService.GetTenantId();
        var dbRisk = riskRecord.ToDbRecord(tenantId);
        var createResult = await riskRepo.CreateAsync(dbRisk).ConfigureAwait(false);
        return Results.Created($"{RiskApi.BaseRoute}/{createResult.Id}", createResult);
    }

    /// <inheritdoc/>
    public async Task<IResult> Update(int riskId, RiskUpdateRecord riskRecord)
    {
        var risk = await riskRepo.GetAsync(riskId).ConfigureAwait(false);
        if (risk == null)
        {
            return Results.NotFound();
        }

        await riskRepo.UpdateAsync(riskId, risk.UpdateFrom(riskRecord)).ConfigureAwait(false);
        return Results.Accepted();
    }
}
