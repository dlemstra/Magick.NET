// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#nullable enable

using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ImageMagick.SourceGenerator;

internal static class AttributeSyntaxExtensions
{
    public static string? GetArgumentValue(this AttributeSyntax attribute, string name)
    {
        if (attribute.ArgumentList is null)
            return null;

        var argument = attribute.ArgumentList.Arguments
            .FirstOrDefault(argument => argument.NameEquals?.Name.Identifier.Text == name);

        if (argument == null)
            return null;

        var value = argument.Expression.ToString()
            .Replace("nameof(", string.Empty)
            .Replace(")", string.Empty)
            .Replace("\"", string.Empty);

        return value;
    }
}
