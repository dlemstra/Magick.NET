// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#nullable enable

using System.Drawing;

namespace ImageMagick.SourceGenerator;

internal sealed class SystemDrawingColor
{
    private readonly Color _color;

    public SystemDrawingColor(System.Reflection.PropertyInfo property)
        : this(property.Name, property)
    {
    }

    public SystemDrawingColor(string name, System.Reflection.PropertyInfo property)
    {
        Name = name;
        _color = (Color)property.GetValue(null, null)!;

        if (_color.A == 0)
            _color = Color.FromArgb(0, 0, 0, 0);

        Comment = $"Gets a system-defined color that has an RGBA value of #{_color.R:X2}{_color.G:X2}{_color.B:X2}{_color.A:X2}.";
    }

    public string Comment { get; }

    public string Name { get; }

    public string R
        => _color.R.ToString();

    public string G
        => _color.G.ToString();

    public string B
        => _color.B.ToString();

    public string A
        => _color.A.ToString();
}
