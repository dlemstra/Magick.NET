// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Drawing;
using System.Reflection;

namespace FileGenerator.MagickColors
{
    internal class MagickColor : CodeGenerator
    {
        public MagickColor(PropertyInfo property)
            : this(property.Name, property)
        {
        }

        public MagickColor(string name, PropertyInfo property)
        {
            Name = name;
            Color = (Color)property.GetValue(null, null)!;

            if (Color.A == 0)
                Color = Color.FromArgb(0, 0, 0, 0);

            Comment = $"Gets a system-defined color that has an RGBA value of #{Color.R:X2}{Color.G:X2}{Color.B:X2}{Color.A:X2}.";
        }

        public string Name { get; }

        public Color Color { get; }

        public string Comment { get; }
    }
}
