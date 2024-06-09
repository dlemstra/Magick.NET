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

        IsStatic = method.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.StaticKeyword));
        Throws = method.AttributeLists
            .SelectMany(list => list.Attributes)
            .Any(attribute => attribute.Name + "Attribute" == nameof(ThrowsAttribute));
    }

    public bool IsVoid
        => ReturnType == "void";

    public bool IsStatic { get; }

    public string Name
        => _method.Identifier.Text;

    public string NativeReturnType
        => _method.ReturnType.ToNativeString();

    public IReadOnlyList<ParameterInfo> Parameters { get; }

    public string ReturnType
        => _method.ReturnType.ToString();

    public bool Throws { get; }

    public bool UsesQuantumType
        => ReturnType == "QuantumType" ||
           Parameters.Any(parameter => parameter.Type == "QuantumType");
}
