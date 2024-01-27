// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace ImageMagick.SourceGenerator;

[Generator]
internal class ExifTagDescriptionGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(context => context.AddAttributeSource<ExifTagDescriptionAttribute>());
        context.RegisterAttributeCodeGenerator<ExifTagDescriptionAttribute, IEnumerable<ExifTagDescriptionInfo>>(CreateInfo, GenerateCode);
    }

    private void GenerateCode(SourceProductionContext context, IEnumerable<ExifTagDescriptionInfo> descriptionInfos)
    {
        var codeBuilder = new CodeBuilder();
        codeBuilder.AppendLine("#nullable enable");
        codeBuilder.AppendLine();
        codeBuilder.AppendLine("using System.Collections.Generic;");
        codeBuilder.AppendLine();
        codeBuilder.AppendLine("namespace ImageMagick;");
        codeBuilder.AppendLine();
        codeBuilder.AppendLine("internal static class ExifTagDescriptions");
        codeBuilder.AppendLine("{");
        codeBuilder.Indent++;
        codeBuilder.AppendLine("public static Dictionary<ExifTagValue, Dictionary<object, string>> ForExifTagValue { get; } = new()");
        codeBuilder.AppendLine("{");
        codeBuilder.Indent++;
        foreach (var descriptionInfo in descriptionInfos)
        {
            codeBuilder.AppendLine("{");
            codeBuilder.Indent++;
            codeBuilder.Append("ExifTagValue.");
            codeBuilder.Append(descriptionInfo.ValueName);
            codeBuilder.AppendLine(", new()");
            codeBuilder.AppendLine("{");
            codeBuilder.Indent++;

            foreach (var attribute in descriptionInfo.Attributes)
            {
                var value = attribute.ConstructorArguments[0].Value;
                if (value is null)
                    continue;

                codeBuilder.Append("{ ");
                switch (value)
                {
                    case string:
                        codeBuilder.Append("\"");
                        codeBuilder.Append(value.ToString());
                        codeBuilder.Append("\"");
                        break;
                    default:
                        codeBuilder.Append("(");
                        codeBuilder.Append(value.GetType().ToString());
                        codeBuilder.Append(") ");
                        codeBuilder.Append(value.ToString());
                        break;
                }

                codeBuilder.Append(", \"");
                codeBuilder.Append(attribute.ConstructorArguments[1].Value?.ToString());
                codeBuilder.AppendLine("\" },");
            }

            codeBuilder.Indent--;
            codeBuilder.AppendLine("}");
            codeBuilder.Indent--;
            codeBuilder.AppendLine("},");
        }

        codeBuilder.Indent--;
        codeBuilder.AppendLine("};");
        codeBuilder.Indent--;
        codeBuilder.AppendLine("}");

        context.AddSource("ExifTagDescriptions.g.cs", SourceText.From(codeBuilder.ToString(), Encoding.UTF8));
    }

    private IEnumerable<ExifTagDescriptionInfo> CreateInfo(GeneratorAttributeSyntaxContext context)
        => ((INamedTypeSymbol)context.TargetSymbol)
            .GetMembers().OfType<IFieldSymbol>()
            .Where(value => value.GetAttributes().Any())
            .Select(value => new ExifTagDescriptionInfo(value));
}
