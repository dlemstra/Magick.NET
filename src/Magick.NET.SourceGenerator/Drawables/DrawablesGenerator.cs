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
internal class DrawablesGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(context => context.AddAttributeSource<DrawablesAttribute>());
        context.RegisterAttributeCodeGenerator<DrawablesAttribute, (bool, ImmutableArray<INamedTypeSymbol>)>(CheckForInterface, GenerateCode);
    }

    private static (bool GenerateInterface, ImmutableArray<INamedTypeSymbol> Interfaces) CheckForInterface(GeneratorAttributeSyntaxContext context)
    {
        var generateInterface = context.TargetNode.IsKind(SyntaxKind.InterfaceDeclaration);
        var assembly = context.TargetSymbol.ContainingAssembly;
        var interfaces = assembly.GlobalNamespace.GetNamespaceMembers()
            .SelectMany(ns => ns.GetTypeMembers())
            .Where(type => type.AllInterfaces.Any(i => i.Name == "IDrawable"))
            .ToImmutableArray();
        return (generateInterface, interfaces);
    }

    private static void AppendProperties(CodeBuilder codeBuilder, INamedTypeSymbol type, string name, ImmutableArray<PropertySymbolInfo> properties)
    {
        var isEnableProperty = properties.SingleOrDefault(x => x.ParameterName == "isEnabled");
        if (isEnableProperty is not null)
        {
            AppendEnableDisabledMethods(codeBuilder, name, isEnableProperty);
            return;
        }

        var readOnlyListProperty = properties.SingleOrDefault(x =>
            x.Type.StartsWith("System.Collections.Generic.IReadOnlyCollection<", StringComparison.Ordinal) ||
            x.Type.StartsWith("System.Collections.Generic.IReadOnlyList<", StringComparison.Ordinal));
        if (readOnlyListProperty is not null)
        {
            AppendReadOnlyListMethod(codeBuilder, type, name, readOnlyListProperty);
            return;
        }

        AppendDefaultMethod(codeBuilder, type, name, properties);
    }

    private static void AppendEnableDisabledMethods(CodeBuilder codeBuilder, string name, PropertySymbolInfo property)
    {
        var comment = property.Documentation
            .Replace("Gets a value indicating whether ", string.Empty)
            .Replace(" is enabled or disabled", string.Empty);

        codeBuilder.AppendComment($"Disables {comment}");
        codeBuilder.AppendReturnsComment("The <see cref=\"IDrawables{TQuantumType}\" /> instance.");
        codeBuilder.Append("IDrawables<TQuantumType> Disable");
        codeBuilder.Append(name);
        codeBuilder.AppendLine("();");
        codeBuilder.AppendLine();

        codeBuilder.AppendComment($"Enables {comment}");
        codeBuilder.Append("IDrawables<TQuantumType> Enable");
        codeBuilder.Append(name);
        codeBuilder.AppendLine("();");
    }

    private static void AppendReadOnlyListMethod(CodeBuilder codeBuilder, INamedTypeSymbol type, string name, PropertySymbolInfo property)
    {
        var typeName = property.TypeArguments[0];

        codeBuilder.AppendComment(type.GetDocumentation());
        codeBuilder.AppendReturnsComment("The <see cref=\"IDrawables{TQuantumType}\" /> instance.");
        codeBuilder.Append("IDrawables<TQuantumType> ");
        codeBuilder.Append(name);
        codeBuilder.Append("(params ");
        codeBuilder.Append(typeName);
        codeBuilder.Append("[] ");
        codeBuilder.Append(property.ParameterName);
        codeBuilder.AppendLine(");");
        codeBuilder.AppendLine();

        codeBuilder.AppendComment(type.GetDocumentation());
        codeBuilder.Append("IDrawables<TQuantumType> ");
        codeBuilder.Append(name);
        codeBuilder.Append("(System.Collections.Generic.IEnumerable<");
        codeBuilder.Append(typeName);
        codeBuilder.Append("> ");
        codeBuilder.Append(property.ParameterName);
        codeBuilder.AppendLine(");");
        codeBuilder.AppendLine();
    }

    private static void AppendDefaultMethod(CodeBuilder codeBuilder, INamedTypeSymbol type, string name, ImmutableArray<PropertySymbolInfo> properties)
    {
        codeBuilder.AppendComment(type.GetDocumentation());
        codeBuilder.AppendReturnsComment("The <see cref=\"IDrawables{TQuantumType}\" /> instance.");

        foreach (var property in properties)
        {
            var parameterName = property.ParameterName;
            var comment = property.Documentation.Replace("Gets or sets t", "T");
            codeBuilder.AppendParameterComment(parameterName, comment);
        }

        codeBuilder.Append("IDrawables<TQuantumType> ");
        codeBuilder.Append(name);
        codeBuilder.Append("(");

        for (var i = 0; i < properties.Length; i++)
        {
            if (i > 0)
                codeBuilder.Append(", ");
            codeBuilder.Append(properties[i].Type);
            codeBuilder.Append(" ");
            codeBuilder.Append(properties[i].ParameterName);
        }

        codeBuilder.AppendLine(");");
    }

    private void GenerateCode(SourceProductionContext context, (bool GenerateInterface, ImmutableArray<INamedTypeSymbol> Interfaces) info)
    {
        var allProperties = new Dictionary<INamedTypeSymbol, ImmutableArray<PropertySymbolInfo>>(SymbolEqualityComparer.Default);

        foreach (var type in info.Interfaces)
        {
            allProperties[type] = type.GetMembers().OfType<IPropertySymbol>().Select(property => new PropertySymbolInfo(property)).ToImmutableArray();
        }

        var codeBuilder = new CodeBuilder();
        codeBuilder.AppendLine("#nullable enable");
        codeBuilder.AppendLine();
        codeBuilder.AppendLine("namespace ImageMagick;");
        codeBuilder.AppendLine();

        codeBuilder.Append("public partial ");
        codeBuilder.AppendLine(info.GenerateInterface ? "interface IDrawables<TQuantumType>" : "class Drawables");
        codeBuilder.AppendLine("{");
        codeBuilder.Indent++;

        for (var i = 0; i < info.Interfaces.Length; i++)
        {
            if (i > 0)
                codeBuilder.AppendLine();

            var type = info.Interfaces[i];
            var name = type.Name.Substring(9);
            var properties = allProperties[type];
            AppendProperties(codeBuilder, type, name, properties);
        }

        codeBuilder.Indent--;
        codeBuilder.AppendLine("}");

        var hintName = info.GenerateInterface ? "IDrawables{TQuantumType}" : "Drawables";
        context.AddSource($"{hintName}.g.cs", SourceText.From(codeBuilder.ToString(), Encoding.UTF8));
    }
}
