// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#nullable enable

using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ImageMagick.SourceGenerator;

internal class NativeInteropInfo
{
    private readonly ClassDeclarationSyntax _class;

    public NativeInteropInfo(SemanticModel semanticModel, SyntaxNode syntaxNode)
    {
        _class = (ClassDeclarationSyntax)syntaxNode;
        Methods = _class.Members
            .OfType<MethodDeclarationSyntax>()
            .Where(method => method.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.PartialKeyword)))
            .Select(method => new MethodInfo(semanticModel, method))
            .ToList();

        var isNativeInstance = _class.BaseList?.Types.ToString() == "NativeInstance";
        var isConstNativeInstance = _class.BaseList?.Types.ToString() == "ConstNativeInstance";
        HasDispose = isNativeInstance;
        HasInstance = isNativeInstance || isConstNativeInstance;

        var parentClass = (ClassDeclarationSyntax)_class.Parent!;
        ParentClassName = parentClass.Identifier.Text;
        EntryPointClassName = ParentClassName == "MagickNET" ? "Magick" : ParentClassName;
        IsInternal = parentClass.Modifiers.Any(modifier => modifier.IsKind(SyntaxKind.InternalKeyword));
        Namespace = ((FileScopedNamespaceDeclarationSyntax)parentClass.Parent!).Name.ToString();

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

        HasStaticDispose = HasDispose && nativeInteropAttribute
            .Select(attribute => attribute.GetArgumentValue(nameof(NativeInteropAttribute.StaticDispose)))
            .FirstOrDefault() == "true";

        RaiseWarnings = nativeInteropAttribute
            .Select(attribute => attribute.GetArgumentValue(nameof(NativeInteropAttribute.RaiseWarnings)))
            .FirstOrDefault() == "true";
    }

    public string ClassName
        => _class.Identifier.Text;

    public string EntryPointClassName { get; }

    public bool HasDispose { get; }

    public bool HasInstance { get; }

    public bool HasStaticDispose { get; }

    public string? InterfaceName { get; }

    public bool IsInternal { get; }

    public bool ManagedToNative { get; }

    public List<MethodInfo> Methods { get; }

    public string Namespace { get; }

    public string ParentClassName { get; }

    public bool RaiseWarnings { get; }

    public bool UsesChannels
        => Methods.Any(method => method.UsesChannels);

    public bool UsesQuantumType
        => Methods.Any(method => method.UsesQuantumType);
}
