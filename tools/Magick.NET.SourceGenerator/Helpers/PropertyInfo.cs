﻿// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#nullable enable

using System.Collections.Immutable;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace ImageMagick.SourceGenerator;

internal sealed class PropertyInfo
{
    private readonly IPropertySymbol _symbol;

    public PropertyInfo(IPropertySymbol symbol)
    {
        _symbol = symbol;
        ParameterName = symbol.Name.Substring(0, 1).ToLowerInvariant() + symbol.Name.Substring(1);
        Documentation = symbol.GetDocumentation().Single();
    }

    public string Documentation { get; }

    public string ParameterName { get; }

    public string Type
        => _symbol.Type.ToDisplayString();

    public ImmutableArray<string> TypeArguments
        => ((INamedTypeSymbol)_symbol.Type).TypeArguments
                .Select(argument => argument.ToDisplayString())
                .ToImmutableArray();
}
