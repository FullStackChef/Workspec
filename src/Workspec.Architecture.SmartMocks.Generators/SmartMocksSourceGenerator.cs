using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Workspec.Architecture.SmartMocks.Generators;

[Generator]
public class SmartMocksSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var syntaxProvider = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: (s, _) => s is MethodDeclarationSyntax method && method.AttributeLists.Count > 0,
                transform: (ctx, _) => ctx.Node as MethodDeclarationSyntax)
            .Where(method => method != null);

        var compilationAndMethods = context.CompilationProvider.Combine(syntaxProvider.Collect());

        context.RegisterSourceOutput(compilationAndMethods, (spc, source) =>
        {
            var (compilation, methods) = source;
            var methodNames = new HashSet<string>();
            var givenMethods = new StringBuilder();
            var whenMethods = new StringBuilder();

            foreach (var method in methods)
            {
                var methodName = method.Identifier.Text;
                var parentClass = method.Parent as ClassDeclarationSyntax;
                var className = parentClass?.Identifier.Text ?? "UnknownClass";

                var attributeLists = method.AttributeLists;

                foreach (var attributeList in attributeLists)
                {
                    foreach (var attribute in attributeList.Attributes)
                    {
                        var attributeName = attribute.Name.ToString();
                        var attributeArgument = attribute.ArgumentList?.Arguments.FirstOrDefault()?.ToString().Trim('"');

                        if (string.IsNullOrEmpty(attributeArgument))
                            continue;

                        if (!methodNames.Add(attributeArgument))
                        {
                            var diagnostic = Diagnostic.Create(
                                new DiagnosticDescriptor(
                                    id: "SM001",
                                    title: "Naming Collision Detected",
                                    messageFormat: $"The name '{attributeArgument}' from attributes is used multiple times, which causes a collision.",
                                    category: "SmartMocks.SourceGenerator",
                                    DiagnosticSeverity.Error,
                                    isEnabledByDefault: true),
                                method.GetLocation());

                            spc.ReportDiagnostic(diagnostic);
                            continue;
                        }

                        if (attributeName.Contains("Given"))
                        {
                            givenMethods.Append($"""
                            public GivenScenario {attributeArgument.Replace(" ", "_")} => _scenario.Given<{className}>(s => s.{methodName}, true);
                        """);
                        }
                        else if (attributeName.Contains("When"))
                        {
                            whenMethods.Append($"""
                            public WhenScenario {attributeArgument.Replace(" ", "_")} => _scenario.When<{className}>(s => s.{methodName}, true);
                        """);
                        }
                    }
                }
            }

            var generatedCode = new StringBuilder();

            generatedCode.Append($$"""
                                namespace Workspec.Architecture.SmartMocks.Generated
                                {
                                    public class GivenConditions
                                    {
                                        private readonly Scenario _scenario;
                                        public GivenConditions(Scenario scenario) => _scenario = scenario;
                                """);

            generatedCode.Append(givenMethods);

            generatedCode.Append($$"""
                                    }

                                    public class WhenConditions
                                    {
                                        private readonly Scenario _scenario;
                                        public WhenConditions(Scenario scenario) => _scenario = scenario;
                                """);

            generatedCode.Append(whenMethods);

            generatedCode.Append($$"""
                                    }

                                    public partial class Scenario
                                    {
                                        public Scenario Given(Func<GivenConditions, Scenario> condition) => condition(new GivenConditions(this));
                                        public Scenario When(Func<WhenConditions, Scenario> condition) => condition(new WhenConditions(this));
                                    }
                                }
                                """);

            spc.AddSource("SmartMocksGenerated", SourceText.From(generatedCode.ToString(), Encoding.UTF8));
        });
    }
}
