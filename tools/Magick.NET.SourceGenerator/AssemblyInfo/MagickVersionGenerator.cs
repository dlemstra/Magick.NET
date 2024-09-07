// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#nullable enable

using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace ImageMagick.SourceGenerator;

[Generator]
internal class MagickVersionGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(context => context.AddAttributeSource<MagickVersionAttribute>());
        context.RegisterAttributeCodeGenerator<MagickVersionAttribute, string>(GetVersion, GenerateCode);
    }

    private string GetVersion(GeneratorAttributeSyntaxContext context)
    {
        var assemblyAttributes = context.SemanticModel.Compilation.Assembly.GetAttributes();

        var title = assemblyAttributes
            .Single(attribute => attribute.AttributeClass?.ToDisplayString() == typeof(AssemblyTitleAttribute).FullName)
            .ConstructorArguments.FirstOrDefault().Value?.ToString();

        var version = assemblyAttributes
            .Single(attribute => attribute.AttributeClass?.ToDisplayString() == typeof(AssemblyVersionAttribute).FullName)
            .ConstructorArguments.FirstOrDefault().Value?.ToString();

        if (title is null || version is null)
            return "\"";

        return $"{title} {version}";
    }

    private void GenerateCode(SourceProductionContext context, string version)
    {
        var codeBuilder = new CodeBuilder();
        codeBuilder.AppendLine("#nullable enable");
        codeBuilder.AppendLine();
        codeBuilder.AppendLine("namespace ImageMagick;");
        codeBuilder.AppendLine();

        codeBuilder.AppendLine("internal static class MagickVersion");
        codeBuilder.AppendOpenBrace();
        codeBuilder.Append(@"public const string Version = """);
        codeBuilder.Append(version);
        codeBuilder.AppendLine(@""";");
        codeBuilder.AppendCloseBrace();

        context.AddSource("MagickVersion.g.cs", SourceText.From(codeBuilder.ToString(), Encoding.UTF8));
    }
}
