// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ImageMagick.SourceGenerator;

internal sealed class TypeInfo
{
    private readonly TypeSyntax _type;

    public TypeInfo(SemanticModel semanticModel, TypeSyntax type)
    {
        _type = type;

        var typeSymbol = semanticModel.GetSymbolInfo(type).Symbol as INamedTypeSymbol;
        IsEnum = typeSymbol?.TypeKind == TypeKind.Enum;

        ClassName = Name.TrimStart('I')
            .Replace("<QuantumType>", string.Empty)
            .Replace("?", string.Empty);
    }

    public string ClassName { get; }

    public bool HasCreateInstance
        => ClassName switch
        {
            "DrawingSettings" => true,
            "MagickColor" => true,
            "string" => true,
            _ => false,
        };

    public bool HasGetInstance
        => ClassName switch
        {
            "PointInfoCollection" => true,
            "MagickImage" => true,
            _ => false,
        };

    public bool IsEnum { get; } = false;

    public bool IsFixed
        => Name switch
        {
            "byte[]" => true,
            "double[]" => true,
            "QuantumType[]" => true,
            "ReadOnlySpan<byte>" => true,
            "ReadOnlySpan<QuantumType>" => true,
            _ => false,
        };

    public bool OnlySupportedInNetstandard21
        => Name switch
        {
            "ReadOnlySpan<byte>" => true,
            "ReadOnlySpan<QuantumType>" => true,
            _ => false,
        };

    public bool IsVoid
        => Name == "void";

    public string Name
        => _type!.ToString();

    public string NativeName
        => _type.ToString() switch
        {
            "bool" => "bool",
            "byte" => "byte",
            "byte[]" => "byte*",
            "double" => "double",
            "double[]" => "double*",
            "int" => "int",
            "LogDelegate?" => "LogDelegate?",
            "QuantumType" => "QuantumType",
            "QuantumType[]" => "QuantumType*",
            "ReadOnlySpan<byte>" => "byte*",
            "ReadOnlySpan<QuantumType>" => "QuantumType*",
            "ReadWriteStreamDelegate" => "ReadWriteStreamDelegate",
            "uint" => "uint",
            "ulong" => "ulong",
            _ => IsEnum ? "UIntPtr" : "IntPtr",
        };
}
