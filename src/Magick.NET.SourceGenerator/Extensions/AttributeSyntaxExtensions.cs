// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ImageMagick.SourceGenerator;

internal static class AttributeSyntaxExtensions
{
    public static string? GetArgumentValue(this AttributeSyntax attribute, string name)
    {
        var argument = attribute.ArgumentList!.Arguments
            .FirstOrDefault(argument => argument.NameEquals?.Name.Identifier.Text == name);

        if (argument == null)
            return null;

        return argument.Expression.ToString();
    }
}
