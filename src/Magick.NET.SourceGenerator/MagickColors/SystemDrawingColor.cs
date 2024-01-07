// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Drawing;
using System.Reflection;

namespace ImageMagick.SourceGenerator;

internal sealed class SystemDrawingColor
{
    private readonly Color _color;

    public SystemDrawingColor(PropertyInfo property)
        : this(property.Name, property)
    {
    }

    public SystemDrawingColor(string name, PropertyInfo property)
    {
        Name = name;
        _color = (Color)property.GetValue(null, null)!;

        if (_color.A == 0)
            _color = Color.FromArgb(0, 0, 0, 0);

        Comment = $"Gets a system-defined color that has an RGBA value of #{_color.R:X2}{_color.G:X2}{_color.B:X2}{_color.A:X2}.";
    }

    public string Comment { get; }

    public string Name { get; }

    public byte R
        => _color.R;

    public byte G
        => _color.G;

    public byte B
        => _color.B;

    public byte A
        => _color.A;
}
