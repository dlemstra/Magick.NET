// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ImageMagick.SourceGenerator;

internal class NativeInteropInfo
{
    private readonly ClassDeclarationSyntax _class;

    public NativeInteropInfo(SyntaxNode syntaxNode)
    {
        _class = (ClassDeclarationSyntax)syntaxNode;
        Methods = _class.Members
            .OfType<MethodDeclarationSyntax>()
            .Select(method => new MethodInfo(method))
            .ToList();
    }

    public string ClassName
        => _class.Identifier.Text;

    public List<MethodInfo> Methods { get; }

    public string ParentClassName
        => ((ClassDeclarationSyntax)_class.Parent!).Identifier.Text;

    public bool UsesQuantumType
        => Methods.Any(method => method.UsesQuantumType);
}
