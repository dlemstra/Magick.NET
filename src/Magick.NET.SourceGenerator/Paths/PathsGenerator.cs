// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace ImageMagick.SourceGenerator;

[Generator]
internal class PathsGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(context => context.AddAttributeSource<PathsAttribute>());
        context.RegisterAttributeCodeGenerator<PathsAttribute, (bool, ImmutableArray<INamedTypeSymbol>)>(CheckForInterface, GenerateCode);
    }

    private static (bool GenerateInterface, ImmutableArray<INamedTypeSymbol> Interfaces) CheckForInterface(GeneratorAttributeSyntaxContext context)
    {
        var generateInterface = context.TargetNode.IsKind(SyntaxKind.InterfaceDeclaration);
        var assembly = context.TargetSymbol.ContainingAssembly;
        var interfaces = assembly.GlobalNamespace.GetNamespaceMembers()
            .SelectMany(ns => ns.GetTypeMembers())
            .Where(type => type.AllInterfaces.Any(i => i.Name == "IPath"))
            .ToImmutableArray();
        return (generateInterface, interfaces);
    }

    private static void AppendMethods(CodeBuilder codeBuilder, INamedTypeSymbol type, string name, ImmutableArray<PropertySymbolInfo> properties, bool generateInterface)
    {
        var readOnlyListProperty = properties.SingleOrDefault(property => property.Type.StartsWith("System.Collections.Generic.IReadOnlyList<", StringComparison.Ordinal));
        if (readOnlyListProperty is not null)
        {
            AppendReadOnlyListMethod(codeBuilder, type, name, readOnlyListProperty, generateInterface);
            return;
        }

        AppendDefaultMethod(codeBuilder, type, name, properties, generateInterface);
    }

    private static void AppendReadOnlyListMethod(CodeBuilder codeBuilder, INamedTypeSymbol type, string name, PropertySymbolInfo property, bool generateInterface)
    {
        if (generateInterface && name != "Close")
        {
            AppendReadOnlyListMethod(codeBuilder, type, name, "Abs", property, generateInterface);
            AppendReadOnlyListMethod(codeBuilder, type, name, "Rel", property, generateInterface);
        }
        else
        {
            AppendReadOnlyListMethod(codeBuilder, type, name, null, property, generateInterface);
        }
    }

    private static void AppendReadOnlyListMethod(CodeBuilder codeBuilder, INamedTypeSymbol type, string name, string? suffix, PropertySymbolInfo property, bool generateInterface)
    {
        var typeName = property.TypeArguments[0];

        codeBuilder.AppendComment(type.GetDocumentation());
        codeBuilder.AppendReturnsComment("The <see cref=\"IPaths{TQuantumType}\" /> instance.");
        AppendIPathsGenericType(codeBuilder, generateInterface);
        codeBuilder.Append(name, suffix, "(params ", typeName, "[] ", property.ParameterName, ")");
        if (generateInterface)
            codeBuilder.AppendLine(";");
        else
            AppendReadOnlyListImplementation(codeBuilder, name, property);

        codeBuilder.AppendLine();

        codeBuilder.AppendComment(type.GetDocumentation());
        AppendIPathsGenericType(codeBuilder, generateInterface);
        codeBuilder.Append(name, suffix, "(System.Collections.Generic.IEnumerable<", typeName, "> ", property.ParameterName, ")");
        if (generateInterface)
            codeBuilder.AppendLine(";");
        else
            AppendReadOnlyListImplementation(codeBuilder, name, property);
    }

    private static void AppendDefaultMethod(CodeBuilder codeBuilder, INamedTypeSymbol type, string name, ImmutableArray<PropertySymbolInfo> properties, bool generateInterface)
    {
        if (generateInterface && name != "Close")
        {
            AppendDefaultMethod(codeBuilder, type, name, "Abs", properties, generateInterface);
            AppendDefaultMethod(codeBuilder, type, name, "Rel", properties, generateInterface);
        }
        else
        {
            AppendDefaultMethod(codeBuilder, type, name, null, properties, generateInterface);
        }
    }

    private static void AppendDefaultMethod(CodeBuilder codeBuilder, INamedTypeSymbol type, string name, string? suffix, ImmutableArray<PropertySymbolInfo> properties, bool generateInterface)
    {
        codeBuilder.AppendComment(type.GetDocumentation());
        codeBuilder.AppendReturnsComment("The <see cref=\"IPaths{TQuantumType}\" /> instance.");

        foreach (var property in properties)
        {
            var parameterName = property.ParameterName;
            var comment = property.Documentation.Replace("Gets or sets t", "T");
            codeBuilder.AppendParameterComment(parameterName, comment);
        }

        AppendIPathsGenericType(codeBuilder, generateInterface);
        codeBuilder.Append(name, suffix, "(");

        for (var i = 0; i < properties.Length; i++)
        {
            if (i > 0)
                codeBuilder.Append(", ");
            codeBuilder.Append(properties[i].Type, " ", properties[i].ParameterName);
        }

        codeBuilder.Append(")");
        if (generateInterface)
        {
            codeBuilder.AppendLine(";");
        }
        else
        {
            codeBuilder.AppendLine();
            codeBuilder.AppendLine("{");
            codeBuilder.Indent++;
            codeBuilder.Append("_paths.Add(new Path", name, "(");

            for (var i = 0; i < properties.Length; i++)
            {
                if (i > 0)
                    codeBuilder.Append(", ");
                codeBuilder.Append(properties[i].ParameterName);
            }

            codeBuilder.AppendLine("));");
            codeBuilder.AppendLine("return this;");
            codeBuilder.Indent--;
            codeBuilder.AppendLine("}");
        }
    }

    private static void AppendReadOnlyListImplementation(CodeBuilder codeBuilder, string name, PropertySymbolInfo property)
    {
        codeBuilder.AppendLine();
        codeBuilder.AppendLine("{");
        codeBuilder.Indent++;
        codeBuilder.AppendLine("_paths.Add(new Path", name, "(", property.ParameterName, "));");
        codeBuilder.AppendLine("return this;");
        codeBuilder.Indent--;
        codeBuilder.AppendLine("}");
    }

    private static void AppendIPathsGenericType(CodeBuilder codeBuilder, bool generateInterface)
    {
        if (!generateInterface)
            codeBuilder.Append("public ");
        codeBuilder.Append("IPaths<");
        if (generateInterface)
            codeBuilder.Append("T");
        codeBuilder.Append("QuantumType> ");
    }

    private void GenerateCode(SourceProductionContext context, (bool GenerateInterface, ImmutableArray<INamedTypeSymbol> Interfaces) info)
    {
        var allProperties = new Dictionary<INamedTypeSymbol, ImmutableArray<PropertySymbolInfo>>(SymbolEqualityComparer.Default);

        foreach (var type in info.Interfaces)
        {
            allProperties[type] = type.GetMembers()
                .OfType<IPropertySymbol>()
                .Where(property => !property.GetAttributes().Any(attribute => attribute.AttributeClass?.Name == nameof(ObsoleteAttribute)))
                .Select(property => new PropertySymbolInfo(property))
                .ToImmutableArray();
        }

        var codeBuilder = new CodeBuilder();
        codeBuilder.AppendLine("#nullable enable");

        if (!info.GenerateInterface)
        {
            codeBuilder.AppendLine();
            codeBuilder.AppendQuantumType();
        }

        codeBuilder.AppendLine();
        codeBuilder.AppendLine("namespace ImageMagick;");
        codeBuilder.AppendLine();

        codeBuilder.Append("public partial ");
        codeBuilder.AppendLine(info.GenerateInterface ? "interface IPaths<TQuantumType>" : "class Paths");
        codeBuilder.AppendLine("{");
        codeBuilder.Indent++;

        for (var i = 0; i < info.Interfaces.Length; i++)
        {
            if (i > 0)
                codeBuilder.AppendLine();

            var type = info.Interfaces[i];
            var name = type.Name.Substring(info.GenerateInterface ? 5 : 4);
            var properties = allProperties[type];
            AppendMethods(codeBuilder, type, name, properties, info.GenerateInterface);
        }

        codeBuilder.Indent--;
        codeBuilder.AppendLine("}");

        var hintName = info.GenerateInterface ? "IPaths{TQuantumType}" : "Paths";
        context.AddSource($"{hintName}.g.cs", SourceText.From(codeBuilder.ToString(), Encoding.UTF8));
    }
}
