using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Workspec.Architecture.SmartMocks.ApplicationModel;

namespace Workspec.Architecture.SmartMocks;

public static class IServiceCollectionSmartMockBuilderExtensions
{
    /// <summary>
    /// Registers the Feature Registry and Startup Filter.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddSmartMockFeatures(this IServiceCollection services)
    {
        services.AddSingleton<FeatureRegistry>();
        //services.AddSingleton<IStartupFilter, FeatureStartupFilter>();
        return services;
    }

    // You may or may not need references to your "SmartMock" namespace
    /// <summary>
    /// Registers a *scoped* SmartMock of TInterface. 
    /// Each request (scope) will get a fresh SmartMock, 
    /// and you can optionally configure it via the provided callback.
    /// Example usage:
    ///   services.AddScopedSmartMock<IAuthenticationService>((mock, httpContext) =>
    ///   {
    ///       var header = httpContext?.Request.Headers["X-AUTH"].FirstOrDefault();
    ///       mock.SetupGet(s => s.Authenticated).Returns(!string.IsNullOrEmpty(header));
    ///   });
    /// </summary>
    public static IServiceCollection AddScopedSmartMock<TInterface>(
        this IServiceCollection services,
        Action<SmartMock<TInterface>, HttpContext>? configure = null)
        where TInterface : class
    {
        services.AddHttpContextAccessor();

        // 1) Register the SmartMock with SCOPED lifetime
        services.AddScoped(sp =>
        {
            var mock = new SmartMock<TInterface>();

            var httpCtx = sp.GetRequiredService<IHttpContextAccessor>().HttpContext;
            configure?.Invoke(mock, httpCtx!);

            return mock;
        });

        // 2) Register TInterface so that we can inject TInterface -> mock.Object
        services.AddScoped(sp => sp.GetRequiredService<SmartMock<TInterface>>().Object);

        return services;
    }


    /// <summary>
    /// Registers a *transient* SmartMock of TInterface. 
    /// Each time TInterface or SmartMock<TInterface> is requested, 
    /// a brand-new SmartMock is created. 
    /// Optional configure callback can apply default setups.
    /// </summary>
    public static IServiceCollection AddTransientSmartMock<TInterface>(
        this IServiceCollection services,
        Action<SmartMock<TInterface>>? configure = null)
        where TInterface : class
    {
        services.AddTransient(sp =>
        {
            var mock = new SmartMock<TInterface>();
            configure?.Invoke(mock);
            return mock;
        });

        services.AddTransient(sp =>
            sp.GetRequiredService<SmartMock<TInterface>>().Object);

        return services;
    }


    /// <summary>
    /// Registers a *singleton* SmartMock of TInterface.
    /// The same SmartMock instance is used for the entire app lifetime.
    /// Optional configure callback for default setups.
    /// </summary>
    public static IServiceCollection AddSingletonSmartMock<TInterface>(
        this IServiceCollection services,
        Action<SmartMock<TInterface>>? configure = null)
        where TInterface : class
    {
        // Create one SmartMock and keep it
        var mock = new SmartMock<TInterface>();
        configure?.Invoke(mock);

        // Register the SmartMock itself as singleton
        services.AddSingleton(mock);

        // Also TInterface
        services.AddSingleton(sp =>
            sp.GetRequiredService<SmartMock<TInterface>>().Object);

        return services;
    }
}