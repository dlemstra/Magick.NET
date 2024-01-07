// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace ImageMagick.SourceGenerator;

[Generator]
internal sealed class MagickColorsGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(context => context.AddAttributeSource<MagickColorsGeneratorAttribute>());

        var fullName = typeof(MagickColorsGeneratorAttribute).FullName ?? throw new InvalidOperationException();

        var valuesProvider = context.SyntaxProvider.ForAttributeWithMetadataName(fullName, (_, _) => true, CheckForInterface);
        context.RegisterSourceOutput(valuesProvider, GenerateCode);
    }

    private static bool CheckForInterface(GeneratorAttributeSyntaxContext context, CancellationToken token)
        => context.TargetNode.IsKind(SyntaxKind.InterfaceDeclaration);

    private static IReadOnlyCollection<SystemDrawingColor> GetColors()
    {
        var type = typeof(Color);

        var properties = type
            .GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Where(property => property.PropertyType == type);

        var colors = properties
            .Select(property => new SystemDrawingColor(property))
            .ToList();

        colors.Insert(0, new SystemDrawingColor("None", properties.First(p => p.Name == "Transparent")));

        return colors;
    }

    private void GenerateCode(SourceProductionContext context, bool generateInterface)
    {
        var codeBuilder = new CodeBuilder();
        codeBuilder.AppendLine("#nullable enable");
        if (!generateInterface)
        {
            codeBuilder.AppendLine();
            codeBuilder.AppendQuantumType();
        }

        codeBuilder.AppendLine();
        codeBuilder.AppendLine("namespace ImageMagick;");
        codeBuilder.AppendLine();
        codeBuilder.Append("public partial ");
        codeBuilder.AppendLine(generateInterface ? "interface IMagickColors<TQuantumType>" : "class MagickColors");
        codeBuilder.AppendLine("{");
        codeBuilder.Indent++;
        AppendSystemDrawingColors(codeBuilder, generateInterface);
        codeBuilder.Indent--;
        codeBuilder.AppendLine("}");

        var hintName = generateInterface ? "IMagickColors{TQuantumType}.g.cs" : "MagickColors.g.cs";
        context.AddSource(hintName, SourceText.From(codeBuilder.ToString(), Encoding.UTF8));
    }

    private void AppendSystemDrawingColors(CodeBuilder codeBuilder, bool generateInterface)
    {
        foreach (var color in GetColors())
        {
            codeBuilder.AppendComment(color.Comment);
            if (generateInterface)
            {
                codeBuilder.Append("IMagickColor<TQuantumType> ");
                codeBuilder.Append(color.Name);
                codeBuilder.AppendLine(" { get; }");
                codeBuilder.AppendLine();
            }
            else
            {
                codeBuilder.Append("public static MagickColor ");
                codeBuilder.Append(color.Name);
                codeBuilder.AppendLine();
                codeBuilder.Indent++;
                codeBuilder.Append("=> MagickColor.FromRgba(");
                codeBuilder.Append(color.R);
                codeBuilder.Append(", ");
                codeBuilder.Append(color.G);
                codeBuilder.Append(", ");
                codeBuilder.Append(color.B);
                codeBuilder.Append(", ");
                codeBuilder.Append(color.A);
                codeBuilder.AppendLine(");");
                codeBuilder.Indent--;
                codeBuilder.AppendLine();

                codeBuilder.AppendComment(color.Comment);
                codeBuilder.Append("IMagickColor<QuantumType> IMagickColors<QuantumType>.");
                codeBuilder.Append(color.Name);
                codeBuilder.AppendLine();
                codeBuilder.Indent++;
                codeBuilder.Append("=> ");
                codeBuilder.Append(color.Name);
                codeBuilder.AppendLine(";");
                codeBuilder.Indent--;
                codeBuilder.AppendLine();
            }
        }
    }
}
