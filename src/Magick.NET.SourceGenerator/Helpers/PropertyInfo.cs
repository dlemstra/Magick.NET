// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace ImageMagick.SourceGenerator;

internal sealed class PropertySymbolInfo
{
    private readonly IPropertySymbol _symbol;

    public PropertySymbolInfo(IPropertySymbol symbol)
    {
        _symbol = symbol;
        ParameterName = symbol.Name.Substring(0, 1).ToLowerInvariant() + symbol.Name.Substring(1);
        Documentation = symbol.GetDocumentation().Single();
    }

    public INamedTypeSymbol ContainingType
        => _symbol.ContainingType;

    public string Documentation { get; }

    public string ParameterName { get; }

    public string Type
        => _symbol.Type.ToDisplayString();

    public ImmutableArray<string> TypeArguments
        => ((INamedTypeSymbol)_symbol.Type).TypeArguments
                .Select(argument => argument.ToDisplayString())
                .ToImmutableArray();
}
