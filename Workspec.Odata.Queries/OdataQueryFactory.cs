using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using System.Runtime.Intrinsics.X86;
using Workspec.Contracts.Queries;

namespace Workspec.Odata.Queries;

public static class OdataQueryFactory
{
    public static WebApplicationBuilder AddOdataQueries(this WebApplicationBuilder builder)
    {
        string? WorkspecDb = builder.Configuration.GetConnectionString(nameof(WorkspecDb));
        ArgumentException.ThrowIfNullOrWhiteSpace(WorkspecDb, nameof(WorkspecDb));
        builder.Services.AddDbContextFactory<OdataQueryContext>(options => options.UseSqlServer(WorkspecDb,
            options => options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));



        builder.Services.AddControllers()
            .AddOData(options =>
                options
                .EnableQueryFeatures()
                .AddRouteComponents("odata", GetEdmModel())
            );
        return builder;
    }

    public static IEdmModel GetEdmModel()
    {
        var builder = new ODataConventionModelBuilder();

        builder.EntitySet<ProjectQueryModel>("Projects");

        builder.EntitySet<ProductQueryModel>("Products");
        builder.EntitySet<ModuleQueryModel>("Modules");
        builder.EntitySet<FeatureQueryModel>("Features");
        builder.EntitySet<UserRequirementQueryModel>("UserRequirements");

        builder.EnableLowerCamelCase();

        IEdmModel edmModel = builder.GetEdmModel();
        var projectQueryModel = edmModel
            .SchemaElements
            .OfType<IEdmEntityType>()
            .First(c => c.Name == nameof(ProjectQueryModel));
        var path = projectQueryModel.FindProperty("path");
        ((EdmModel)edmModel)
            .AddAlternateKeyAnnotation(projectQueryModel,
            new Dictionary<string, IEdmProperty>
            {
                {"Path", path}
            });



        return edmModel;
    }
}
