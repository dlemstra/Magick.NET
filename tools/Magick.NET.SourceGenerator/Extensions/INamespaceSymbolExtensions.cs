// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace ImageMagick.SourceGenerator;

internal static class INamespaceSymbolExtensions
{
    public static IEnumerable<INamedTypeSymbol> GetTypeMembersRecursive(this INamespaceSymbol self)
    {
        foreach (var type in self.GetTypeMembers())
        {
            yield return type;
        }

        foreach (var namespaceSymbol in self.GetNamespaceMembers())
        {
            foreach (var type in namespaceSymbol.GetTypeMembersRecursive())
            {
                yield return type;
            }
        }
    }
}
