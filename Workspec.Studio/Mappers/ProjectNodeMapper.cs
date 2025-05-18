using Workspec.Contracts.Queries;
using Workspec.Studio.Models;

namespace Workspec.Studio.Mappers;
public static class ProjectNodeMapper
{
    public static List<RequirementNode> MapProjectToRequirementNodes(ProjectQueryModel project)
    {
        if (project.Product?.Modules == null)
            return [];

        return [.. project.Product.Modules.Select(module => new RequirementNode
        {
            Id = $"module_{module.Id}",
            Title = module.Name,
            Description = module.Description,
            Type = "Module",
            ParentId = null,
            HasChildren = module.Features.Count != 0,
            Children = [.. module.Features.Select(feature => new RequirementNode
            {
                Id = $"feature_{feature.Id}",
                Title = feature.Name,
                Description = feature.Description,
                Type = "Feature",
                ParentId = $"module_{module.Id}",
                HasChildren = feature.UserRequirements.Count != 0,
                Children = [.. feature.UserRequirements.Select(requirement => new RequirementNode
                {
                    Id = $"requirement_{requirement.Id}",
                    Title = requirement.Title,
                    Description = requirement.Description,
                    Type = "Requirement",
                    ParentId = $"feature_{feature.Id}",
                    HasChildren = false,
                    Children = []
                })]
            })]
        })];
    }
}
