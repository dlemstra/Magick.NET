// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace FileGenerator.MagickColors;

internal abstract class ColorsGenerator : CodeGenerator
{
    private readonly Type _type = typeof(Color);

    protected IReadOnlyCollection<MagickColor> GetColors()
    {
        var properties = _type
            .GetProperties(BindingFlags.Public | BindingFlags.Static)
            .Where(property => property.PropertyType == _type);

        var colors = properties
            .Select(property => new MagickColor(property))
            .ToList();

        colors.Insert(0, new MagickColor("None", properties.First(p => p.Name == "Transparent")));

        return colors;
    }

    protected void WriteComment(string comment)
    {
        WriteLine("/// <summary>");
        WriteLine("/// " + comment);
        WriteLine("/// </summary>");
    }
}
