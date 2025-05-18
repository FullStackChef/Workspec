namespace Workspec.Domain.AI;

public record FunctionProperty(string Type, string Description, ICollection<string>? Enum = null);
