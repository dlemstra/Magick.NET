// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ImageMagick.SourceGenerator;

internal static class TypeSyntaxExtensions
{
    public static string ToNativeString(this TypeSyntax? type)
    {
        if (type is null)
            return "unknown";

        return type.ToString();
    }
}
