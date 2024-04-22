// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ImageMagick.SourceGenerator;

internal sealed class ParameterInfo
{
    private readonly ParameterSyntax _parameter;

    public ParameterInfo(ParameterSyntax parameter)
    {
        _parameter = parameter;
    }

    public string Name
        => _parameter.Identifier.Text;

    public string NativeType
        => _parameter.Type.ToNativeString();

    public string Type
        => _parameter.Type!.ToString();
}
