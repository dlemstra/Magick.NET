// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ImageMagick.SourceGenerator;

internal sealed class TypeInfo
{
    private readonly TypeSyntax _type;

    public TypeInfo(TypeSyntax type)
    {
        _type = type;
    }

    public bool IsFixed
        => Name switch
        {
            "byte[]" => true,
            "ReadOnlySpan<byte>" => true,
            _ => false,
        };

    public bool IsInstance
        => Name switch
        {
            "bool" => false,
            "byte" => false,
            "byte[]" => false,
            "double" => false,
            "int" => false,
            "IntPtr" => false,
            "QuantumType" => false,
            "ReadOnlySpan<byte>" => false,
            "ulong" => false,
            _ => true,
        };

    public bool OnlySupportedInNetstandard21
        => Name switch
        {
            "ReadOnlySpan<byte>" => true,
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
            "int" => "int",
            "QuantumType" => "QuantumType",
            "ReadOnlySpan<byte>" => "byte*",
            "ulong" => "ulong",
            _ => "IntPtr",
        };
}
