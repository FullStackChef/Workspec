using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Workspec.DataAccess;
using Workspec.DataAccess.Models.Projects;
using Workspec.Domain.Modules.Projects.API;
using Workspec.Domain.Modules.Projects.Models;
using Workspec.Domain.Modules.SubscriptionManagement.Enum;
using Workspec.Domain.Modules.SubscriptionManagement.Services;

namespace Workspec.Domain.Modules.Projects.Services;

/// <summary>
/// Represents a service for managing projects.
/// </summary>
public interface IProjectService
{
    /// <summary>
    /// Retrieves all projects.
    /// </summary>
    /// <returns>The result of the operation.</returns>
    Task<IResult> Query();

    /// <summary>
    /// Retrieves a project by its ID.
    /// </summary>
    /// <param name="projectId">The ID of the project.</param>
    /// <returns>The result of the operation.</returns>
    Task<IResult> Get(int projectId);

    /// <summary>
    /// Creates a new project.
    /// </summary>
    /// <param name="projectRecord">The project record.</param>
    /// <returns>The result of the operation.</returns>
    Task<IResult> Create(CreateProjectRecord projectRecord);

    /// <summary>
    /// Updates an existing project.
    /// </summary>
    /// <param name="projectId">The ID of the project.</param>
    /// <param name="projectRecord">The project record.</param>
    /// <returns>The result of the operation.</returns>
    Task<IResult> Update(int projectId, ProjectUpdateRecord projectRecord);
}
/// <summary>
/// Represents a service for managing projects.
/// </summary>
public class ProjectService(
    ILogger<ProjectService> logger,
    IRepository<ProjectDbRecord> projectRepo,
    ISubscriptionLimitService subscriptionLimits,
    ITenantResolutionService tenantResolutionService
    ) : IProjectService
{
    /// <inheritdoc/>
    public async Task<IResult> Query()
    {
        var projects = await projectRepo.QueryAsync().ConfigureAwait(false);
        return Results.Ok(projects);
    }

    /// <inheritdoc/>
    public async Task<IResult> Get(int projectId)
    {
        var project = await projectRepo.GetAsync(projectId).ConfigureAwait(false);
        return project == null ? Results.NotFound() : Results.Ok(project);
    }

    /// <inheritdoc/>
    public async Task<IResult> Create(CreateProjectRecord projectRecord)
    {
        Guid tenantId = tenantResolutionService.GetTenantId();
        var checkLimitResult = await subscriptionLimits.CheckLimitAsync(tenantId, FeatureType.MaxProjects, 1).ConfigureAwait(false);
        if (checkLimitResult is not Ok)
        {
            logger.LogWarning("Project limit reached for tenant {tenantId}", tenantId);
            return checkLimitResult;
        }

        var dbProject = projectRecord.ToDbRecord(tenantId);
        var createResult = await projectRepo.CreateAsync(dbProject).ConfigureAwait(false);

        return Results.Created($"{ProjectApi.BaseRoute}/{createResult.Id}", createResult);
    }

    /// <inheritdoc/>
    public async Task<IResult> Update(int projectId, ProjectUpdateRecord projectRecord)
    {
        Guid tenantId = tenantResolutionService.GetTenantId();
        var project = await projectRepo.GetAsync(projectId).ConfigureAwait(false);
        if (project == null)
        {
            return Results.NotFound();
        }

        await projectRepo.UpdateAsync(projectId, project.UpdateFrom(projectRecord)).ConfigureAwait(false);
        return Results.Accepted();
    }
}
