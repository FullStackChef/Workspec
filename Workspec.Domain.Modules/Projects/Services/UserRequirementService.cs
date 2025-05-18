using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Workspec.DataAccess;
using Workspec.DataAccess.Models.Projects;
using Workspec.Domain.Modules.Projects.API;
using Workspec.Domain.Modules.Projects.Models;
using Workspec.Domain.Modules.SubscriptionManagement.Services;

namespace Workspec.Domain.Modules.Projects.Services;

/// <summary>
/// Represents a service for managing user requirements associated with projects.
/// </summary>
public interface IUserRequirementService
{
    /// <summary>
    /// Retrieves all user requirements.
    /// </summary>
    /// <returns>The result of the operation.</returns>
    Task<IResult> Query();

    /// <summary>
    /// Retrieves a user requirement by its ID.
    /// </summary>
    /// <param name="requirementId">The ID of the user requirement.</param>
    /// <returns>The result of the operation.</returns>
    Task<IResult> Get(int requirementId);

    /// <summary>
    /// Creates a new user requirement.
    /// </summary>
    /// <param name="requirementRecord">The user requirement record to create.</param>
    /// <returns>The result of the operation.</returns>
    Task<IResult> Create(CreateUserRequirementRecord requirementRecord);

    /// <summary>
    /// Updates an existing user requirement.
    /// </summary>
    /// <param name="requirementId">The ID of the user requirement.</param>
    /// <param name="requirementRecord">The user requirement update record.</param>
    /// <returns>The result of the operation.</returns>
    Task<IResult> Update(int requirementId, UserRequirementUpdateRecord requirementRecord);
}
public class UserRequirementService(
    ILogger<UserRequirementService> logger,
    IRepository<UserRequirementDbRecord> userRequirementRepo,
    ITenantResolutionService tenantResolutionService) : IUserRequirementService
{

    /// <inheritdoc/>
    public async Task<IResult> Query()
    {
        var requirements = await userRequirementRepo.QueryAsync().ConfigureAwait(false);
        return Results.Ok(requirements);
    }

    /// <inheritdoc/>
    public async Task<IResult> Get(int requirementId)
    {
        var requirement = await userRequirementRepo.GetAsync(requirementId).ConfigureAwait(false);
        return requirement == null ? Results.NotFound() : Results.Ok(requirement);
    }

    /// <inheritdoc/>
    public async Task<IResult> Create(CreateUserRequirementRecord requirementRecord)
    {
        Guid tenantId = tenantResolutionService.GetTenantId();
        var dbRequirement = requirementRecord.ToDbRecord(tenantId);
        var createResult = await userRequirementRepo.CreateAsync(dbRequirement).ConfigureAwait(false);
        return Results.Created($"{UserRequirementApi.BaseRoute}/{createResult.Id}", createResult);
    }

    /// <inheritdoc/>
    public async Task<IResult> Update(int requirementId, UserRequirementUpdateRecord requirementRecord)
    {
        var requirement = await userRequirementRepo.GetAsync(requirementId).ConfigureAwait(false);
        if (requirement == null)
        {
            return Results.NotFound();
        }

        var updatedRequirement = requirement.UpdateFrom(requirementRecord);
        await userRequirementRepo.UpdateAsync(requirementId, requirement).ConfigureAwait(false);
        return Results.Accepted();
    }
}
