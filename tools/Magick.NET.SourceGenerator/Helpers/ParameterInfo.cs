// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#nullable enable

using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ImageMagick.SourceGenerator;

internal sealed class ParameterInfo
{
    private readonly ParameterSyntax _parameter;

    public ParameterInfo(SemanticModel semanticModel, ParameterSyntax parameter)
    {
        _parameter = parameter;

        IsOut = _parameter.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.OutKeyword));
        Type = new TypeInfo(semanticModel, _parameter.Type!);
    }

    public string Name
        => _parameter.Identifier.Text;

    public bool IsOut { get; }

    public TypeInfo Type { get; }
}
