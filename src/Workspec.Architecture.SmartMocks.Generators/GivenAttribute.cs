using System;

namespace Workspec.Architecture.SmartMocks.Generators;

/// <summary>
/// Specifies a given condition for a property. The attribute accepts an object value and a descriptive message.
/// </summary>
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public sealed class GivenAttribute(object value, string message) : Attribute
{
    public object Value { get; } = value;
    public string Message { get; } = message;
}
