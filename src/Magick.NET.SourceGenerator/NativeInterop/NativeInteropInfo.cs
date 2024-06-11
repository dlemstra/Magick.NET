// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
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

        IsNativeInstance = _class.BaseList?.Types.ToString() == "NativeInstance";

        var parentClass = (ClassDeclarationSyntax)_class.Parent!;
        ParentClassName = parentClass.Identifier.Text;
        IsInternal = parentClass.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.InternalKeyword));

        var nativeInteropAttribute = _class.AttributeLists
            .SelectMany(list => list.Attributes)
            .Where(attribute => attribute.Name + "Attribute" == nameof(NativeInteropAttribute));

        var interfaceSuffix = nativeInteropAttribute
            .Select(attribute => attribute.GetArgumentValue(nameof(NativeInteropAttribute.QuantumType)))
            .FirstOrDefault() == "true" ? "<QuantumType>" : string.Empty;

        InterfaceName = $"I{ParentClassName}{interfaceSuffix}";

        ManagedToNative = nativeInteropAttribute
            .Select(attribute => attribute.GetArgumentValue(nameof(NativeInteropAttribute.ManagedToNative)))
            .FirstOrDefault() == "true";

        NativeToManaged = nativeInteropAttribute
            .Select(attribute => attribute.GetArgumentValue(nameof(NativeInteropAttribute.NativeToManaged)))
            .FirstOrDefault() == "true";
    }

    public string ClassName
        => _class.Identifier.Text;

    public bool IsNativeInstance { get; }

    public string? InterfaceName { get; }

    public bool IsInternal { get; }

    public bool ManagedToNative { get; }

    public List<MethodInfo> Methods { get; }

    public bool NativeToManaged { get; }

    public string ParentClassName { get; }

    public bool UsesQuantumType
        => Methods.Any(method => method.UsesQuantumType);
}
