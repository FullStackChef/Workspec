namespace Workspec.Domain.AI.Core;

public class AiAssistantOptions
{
    public required string Model { get; set; }
    public required string Endpoint { get; set; }
    public required string ApiKey { get; set; }
}
