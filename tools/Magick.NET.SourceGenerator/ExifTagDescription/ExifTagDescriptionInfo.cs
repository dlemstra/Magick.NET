// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace ImageMagick.SourceGenerator;

internal sealed class ExifTagDescriptionInfo
{
    private readonly IFieldSymbol _value;

    public ExifTagDescriptionInfo(IFieldSymbol value)
    {
        _value = value;
    }

    public string ValueName
        => _value.Name;

    public ImmutableArray<AttributeData> Attributes
        => _value.GetAttributes();
}
