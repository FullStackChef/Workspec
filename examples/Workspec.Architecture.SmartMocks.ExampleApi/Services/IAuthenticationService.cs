using Workspec.Architecture.SmartMocks.Generators;

namespace Workspec.Architecture.SmartMocks.ExampleApi.Services
{
    public interface IAuthenticationService
    {
        [Given(true, "the user is authenticated")]
        [Given(false, "the user is not authenticated")]
        bool Authenticated { get; }
    }
}
