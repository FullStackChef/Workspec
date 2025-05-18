using System.Text.Json;
using System.Text.Json.Serialization;

namespace Workspec.Domain.AI;

public record FunctionParameters(string Type = "object")
{
    public Dictionary<string, FunctionProperty> Properties { get; init; } = [];
    public ICollection<string> Required { get; init; } = [];
}

public static class FunctionParametersExtensions
{
    public static BinaryData ToBinaryData(this FunctionParameters parameters)
    {
        return BinaryData.FromObjectAsJson(parameters, new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });
    }
    public static FunctionParameters WithProperties(this FunctionParameters parameters, params (string Key, FunctionProperty Value)[] properties)
    {
        return parameters with { Properties = properties.ToDictionary() };
    }

    public static FunctionParameters WithRequired(this FunctionParameters parameters, params string[] required)
    {
        return parameters with { Required = required };
    }
}