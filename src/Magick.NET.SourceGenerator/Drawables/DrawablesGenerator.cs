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

    private static void AppendMethods(CodeBuilder codeBuilder, INamedTypeSymbol type, string name, ImmutableArray<PropertyInfo> properties, bool generateInterface)
    {
        var isEnableProperty = properties.SingleOrDefault(property => property.ParameterName == "isEnabled");
        if (isEnableProperty is not null)
        {
            AppendEnableDisabledMethods(codeBuilder, name, isEnableProperty, generateInterface);
            return;
        }

        var readOnlyListProperty = properties.SingleOrDefault(property =>
            property.Type.StartsWith("System.Collections.Generic.IReadOnlyCollection<", StringComparison.Ordinal) ||
            property.Type.StartsWith("System.Collections.Generic.IReadOnlyList<", StringComparison.Ordinal));
        if (readOnlyListProperty is not null)
        {
            AppendReadOnlyListMethod(codeBuilder, type, name, readOnlyListProperty, generateInterface);
            return;
        }

        AppendDefaultMethod(codeBuilder, type, name, properties, generateInterface);
    }

    private static void AppendEnableDisabledMethods(CodeBuilder codeBuilder, string name, PropertyInfo property, bool generateInterface)
    {
        var comment = property.Documentation
            .Replace("Gets a value indicating whether ", string.Empty)
            .Replace(" is enabled or disabled", string.Empty);

        AppendEnableDisabledMethod(codeBuilder, name, "Disable", comment, generateInterface);
        codeBuilder.AppendLine();
        AppendEnableDisabledMethod(codeBuilder, name, "Enable", comment, generateInterface);
    }

    private static void AppendEnableDisabledMethod(CodeBuilder codeBuilder, string name, string type, string comment, bool generateInterface)
    {
        codeBuilder.AppendComment($"{type}s {comment}");
        codeBuilder.AppendReturnsComment($"The <see cref=\"IDrawables{{{(generateInterface ? "T" : null)}QuantumType}}\" /> instance.");
        AppendIDrawablesGenericType(codeBuilder, generateInterface);
        codeBuilder.Append(type, name, "()");

        if (generateInterface)
        {
            codeBuilder.AppendLine(";");
        }
        else
        {
            codeBuilder.AppendLine();
            codeBuilder.AppendLine("{");
            codeBuilder.Indent++;
            codeBuilder.AppendLine("_drawables.Add(Drawable", name, ".", type, "d);");
            codeBuilder.AppendLine("return this;");
            codeBuilder.Indent--;
            codeBuilder.AppendLine("}");
        }
    }

    private static void AppendReadOnlyListMethod(CodeBuilder codeBuilder, INamedTypeSymbol type, string name, PropertyInfo property, bool generateInterface)
    {
        var typeName = property.TypeArguments[0];

        codeBuilder.AppendComment(type.GetDocumentation());
        codeBuilder.AppendReturnsComment("The <see cref=\"IDrawables{TQuantumType}\" /> instance.");
        AppendIDrawablesGenericType(codeBuilder, generateInterface);
        codeBuilder.Append(name, "(params ", typeName, "[] ", property.ParameterName, ")");
        if (generateInterface)
            codeBuilder.AppendLine(";");
        else
            AppendReadOnlyListImplementation(codeBuilder, name, property);

        codeBuilder.AppendLine();

        codeBuilder.AppendComment(type.GetDocumentation());
        AppendIDrawablesGenericType(codeBuilder, generateInterface);
        codeBuilder.Append(name, "(System.Collections.Generic.IEnumerable<", typeName, "> ", property.ParameterName, ")");
        if (generateInterface)
            codeBuilder.AppendLine(";");
        else
            AppendReadOnlyListImplementation(codeBuilder, name, property);
    }

    private static void AppendReadOnlyListImplementation(CodeBuilder codeBuilder, string name, PropertyInfo property)
    {
        codeBuilder.AppendLine();
        codeBuilder.AppendLine("{");
        codeBuilder.Indent++;
        codeBuilder.AppendLine("_drawables.Add(new Drawable", name, "(", property.ParameterName, "));");
        codeBuilder.AppendLine("return this;");
        codeBuilder.Indent--;
        codeBuilder.AppendLine("}");
    }

    private static void AppendDefaultMethod(CodeBuilder codeBuilder, INamedTypeSymbol type, string name, ImmutableArray<PropertyInfo> properties, bool generateInterface)
    {
        codeBuilder.AppendComment(type.GetDocumentation());
        codeBuilder.AppendReturnsComment("The <see cref=\"IDrawables{TQuantumType}\" /> instance.");

        foreach (var property in properties)
        {
            var parameterName = property.ParameterName;
            var comment = property.Documentation.Replace("Gets or sets t", "T");
            codeBuilder.AppendParameterComment(parameterName, comment);
        }

        AppendIDrawablesGenericType(codeBuilder, generateInterface);
        codeBuilder.Append(name);
        codeBuilder.Append("(");

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
            codeBuilder.Append("_drawables.Add(new Drawable", name, "(");

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

    private static void AppendIDrawablesGenericType(CodeBuilder codeBuilder, bool generateInterface)
    {
        if (!generateInterface)
            codeBuilder.Append("public ");
        codeBuilder.Append("IDrawables<");
        if (generateInterface)
            codeBuilder.Append("T");
        codeBuilder.Append("QuantumType> ");
    }

    private void GenerateCode(SourceProductionContext context, (bool GenerateInterface, ImmutableArray<INamedTypeSymbol> Interfaces) info)
    {
        var allProperties = new Dictionary<INamedTypeSymbol, ImmutableArray<PropertyInfo>>(SymbolEqualityComparer.Default);

        foreach (var type in info.Interfaces)
        {
            allProperties[type] = type.GetMembers()
                .OfType<IPropertySymbol>()
                .Where(property => !property.GetAttributes().Any(attribute => attribute.AttributeClass?.Name == nameof(ObsoleteAttribute)))
                .Select(property => new PropertyInfo(property))
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
        codeBuilder.AppendLine(info.GenerateInterface ? "interface IDrawables<TQuantumType>" : "class Drawables");
        codeBuilder.AppendLine("{");
        codeBuilder.Indent++;

        for (var i = 0; i < info.Interfaces.Length; i++)
        {
            if (i > 0)
                codeBuilder.AppendLine();

            var type = info.Interfaces[i];
            var name = type.Name.Substring(info.GenerateInterface ? 9 : 8);
            var properties = allProperties[type];
            AppendMethods(codeBuilder, type, name, properties, info.GenerateInterface);
        }

        codeBuilder.Indent--;
        codeBuilder.AppendLine("}");

        var hintName = info.GenerateInterface ? "IDrawables{TQuantumType}" : "Drawables";
        context.AddSource($"{hintName}.g.cs", SourceText.From(codeBuilder.ToString(), Encoding.UTF8));
    }
}
