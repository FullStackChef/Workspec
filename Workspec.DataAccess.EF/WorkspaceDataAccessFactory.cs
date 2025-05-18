using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Workspec.DataAccess.EF
{
    public static class WorkspaceDataAccessFactory
    {
        public static WebApplicationBuilder AddWorkspaceDataAccess(this WebApplicationBuilder builder)
        {
            string? WorkspecDb = builder.Configuration.GetConnectionString(nameof(WorkspecDb));
            ArgumentException.ThrowIfNullOrWhiteSpace(WorkspecDb, nameof(WorkspecDb));
            builder.Services.AddDbContextFactory<WorkspecDbContext>(options =>
            options.UseSqlServer(WorkspecDb,
                options => options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

            builder.Services.AddTransient(typeof(IRepository<>), typeof(Repostitory<>));

            return builder;
        }
    }
}
