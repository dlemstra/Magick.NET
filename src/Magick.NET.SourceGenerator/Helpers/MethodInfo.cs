// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ImageMagick.SourceGenerator;

internal sealed class MethodInfo
{
    private readonly MethodDeclarationSyntax _method;

    public MethodInfo(MethodDeclarationSyntax method)
    {
        _method = method;

        Parameters = _method.ParameterList.Parameters
            .Select(parameter => new ParameterInfo(parameter))
            .ToList();

        ReturnType = new TypeInfo(method.ReturnType);

        IsStatic = method.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.StaticKeyword));

        Throws = method.AttributeLists
            .SelectMany(list => list.Attributes)
            .Any(attribute => attribute.Name + "Attribute" == nameof(ThrowsAttribute));

        Cleanup = method.AttributeLists
            .SelectMany(list => list.Attributes)
            .Where(attribute => attribute.Name + "Attribute" == nameof(CleanupAttribute))
            .Select(attribute => new CleanupInfo(attribute.GetArgumentValue(nameof(CleanupAttribute.Name)), attribute.GetArgumentValue(nameof(CleanupAttribute.Arguments))))
            .FirstOrDefault();

        UsesInstance = !IsStatic;
        SetsInstance = !IsStatic && IsVoid && !Name.EndsWith("_Set");

        var instanceAttribute = method.AttributeLists
            .SelectMany(list => list.Attributes)
            .FirstOrDefault(attribute => attribute.Name + "Attribute" == nameof(InstanceAttribute));

        if (instanceAttribute is not null)
        {
            UsesInstance = instanceAttribute.GetArgumentValue(nameof(InstanceAttribute.UsesInstance)) != "false";
            SetsInstance = instanceAttribute.GetArgumentValue(nameof(InstanceAttribute.SetInstance)) != "false";
        }
    }

    public CleanupInfo? Cleanup { get; }

    public bool IsVoid
        => ReturnType.IsVoid;

    public bool IsStatic { get; }

    public bool OnlySupportedInNetstandard21
        => ReturnType.OnlySupportedInNetstandard21 ||
           Parameters.Any(parameter => parameter.Type.OnlySupportedInNetstandard21);

    public string Name
        => _method.Identifier.Text;

    public IReadOnlyList<ParameterInfo> Parameters { get; }

    public TypeInfo ReturnType { get; }

    public bool SetsInstance { get; }

    public bool Throws { get; }

    public bool UsesInstance { get; }

    public bool UsesQuantumType
        => ReturnType.Name == "QuantumType" ||
           Parameters.Any(parameter => parameter.Type.Name == "QuantumType");
}
