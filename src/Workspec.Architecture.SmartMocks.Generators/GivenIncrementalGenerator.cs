using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Workspec.Architecture.SmartMocks.Generators;

[Generator]
[SuppressMessage(
    "MicrosoftCodeAnalysisCorrectness",
    "RS1041:Compiler extensions should be implemented in assemblies targeting netstandard2.0",
    Justification = "Relies on functionality in Workspec.Architecture.SmartMocks (Currently built on .net 9.0")]
public class GivenIncrementalGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Create a provider that finds all property declarations with attributes.
        var propertyDeclarations = context.SyntaxProvider.CreateSyntaxProvider(
            predicate: static (s, _) => IsCandidateProperty(s),
            transform: static (ctx, _) => GetPropertySymbol(ctx))
            .Where(static m => m != null);

        // Combine the property symbols with the overall compilation.
        var compilationAndProperties = context.CompilationProvider.Combine(propertyDeclarations.Collect());

        context.RegisterSourceOutput(compilationAndProperties, (spc, source) =>
        {
            Execute(source.Left, source.Right, spc);
        });
    }

    private static bool IsCandidateProperty(SyntaxNode node)
    {
        return node is PropertyDeclarationSyntax propertyDeclaration &&
               propertyDeclaration.AttributeLists.Count > 0;
    }

    private static IPropertySymbol GetPropertySymbol(GeneratorSyntaxContext context)
    {
        if (context.Node is PropertyDeclarationSyntax propertyDeclaration)
        {
            return context.SemanticModel.GetDeclaredSymbol(propertyDeclaration) as IPropertySymbol;
        }
        return null;
    }

    private static void Execute(Compilation compilation, IEnumerable<IPropertySymbol> properties, SourceProductionContext context)
    {
        // Dictionary to collect Given attribute info grouped by containing interface.
        var givenData = new Dictionary<INamedTypeSymbol, List<GivenInfo>>(SymbolEqualityComparer.Default);
        var givenAttributeType = compilation.GetTypeByMetadataName("Workspec.Architecture.SmartMocks.Generators.GivenAttribute");
        if (givenAttributeType == null)
        {
            // If the GivenAttribute isn't found, do nothing.
            return;
        }

        // Process each property symbol found.
        foreach (var propertySymbol in properties)
        {
            if (propertySymbol == null)
            {
                continue;
            }

            foreach (var attributeData in propertySymbol.GetAttributes())
            {
                if (!SymbolEqualityComparer.Default.Equals(attributeData.AttributeClass, givenAttributeType))
                    continue;

                // Expect two constructor arguments: expected value (bool) and message (string)
                if (attributeData.ConstructorArguments.Length == 2 &&
                    attributeData.ConstructorArguments[0].Value is bool expectedValue &&
                    attributeData.ConstructorArguments[1].Value is string message)
                {
                    // Group by the containing interface.
                    var interfaceSymbol = propertySymbol.ContainingType;
                    if (!givenData.TryGetValue(interfaceSymbol, out var list))
                    {
                        list = new List<GivenInfo>();
                        givenData[interfaceSymbol] = list;
                    }

                    list.Add(new GivenInfo
                    {
                        PropertyName = propertySymbol.Name,
                        ExpectedValue = expectedValue,
                        Message = message
                    });
                }
            }
        }

        // Begin generating the GivenRepository class.
        var sourceBuilder = new StringBuilder(@"
using System;
using Workspec.Architecture.SmartMocks;

namespace Workspec.Architecture.SmartMocks.Generated
{
    public partial class GivenRepository
    {
");

        // For each interface and each Given condition, create a property.
        foreach (var kvp in givenData)
        {
            var interfaceSymbol = kvp.Key;
            // Using FullyQualifiedFormat to ensure correct type resolution.
            var interfaceName = interfaceSymbol.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);

            foreach (var given in kvp.Value)
            {
                // Sanitize the message to create a valid property name.
                var sanitizedName = given.Message.Replace(" ", "_");
                sourceBuilder.Append($@"
        public GivenScenario {sanitizedName} => scenario.Given<{interfaceName}>(service => service.{given.PropertyName}, {given.ExpectedValue.ToString().ToLower()});
");
            }
        }

        sourceBuilder.Append(@"
    }
}
");

        context.AddSource("GivenRepository.g.cs", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
    }

    private class GivenInfo
    {
        public string PropertyName { get; set; }
        public bool ExpectedValue { get; set; }
        public string Message { get; set; }
    }
}
