using Microsoft.Extensions.Logging;
using Workspec.Domain.Modules.Diagrams.Models;

namespace Workspec.Domain.Modules.Diagrams.Services
{
    public class ElementService(ILogger<ElementService> logger)
    {
        public async Task<IReadOnlyCollection<ElementRecord>> GetElementsAsync()
        {
            logger.LogInformation("Getting elements");
            var elements = new List<ElementRecord>
            {
                new(Guid.NewGuid(), "Element 1", "Type 1"),
                new(Guid.NewGuid(), "Element 2", "Type 2"),
                new(Guid.NewGuid(), "Element 3", "Type 3"),
            };
            return await Task.FromResult(elements).ConfigureAwait(false);
        }
    }
}
