// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Drawing;
using System.Linq;
using System.Reflection;

namespace FileGenerator.MagickColors
{
    internal sealed class MagickColorsGenerator : CodeGenerator
    {
        Type _Type = typeof(Color);

        private string GetArguments(Color color)
            => $"{color.R}, {color.G}, {color.B}, {color.A}";

        private string GetComment(Color color)
            => $"Gets a system-defined color that has an RGBA value of #{color.R:X2}{color.G:X2}{color.B:X2}{color.A:X2}.";

        private void WriteColor(PropertyInfo property)
        {
            if (property.PropertyType != _Type)
                return;

            WriteColor(property.Name, property);
        }

        private void WriteColor(string name, PropertyInfo property)
        {
            var color = (Color)property.GetValue(null, null);

            if (color.A == 0)
                color = Color.FromArgb(0, 0, 0, 0);

            WriteComment(GetComment(color));
            WriteLine("public static MagickColor " + name);
            Indent++;
            WriteLine("=> MagickColor.FromRgba(" + GetArguments(color) + ");");
            Indent--;
            WriteLine();
        }

        private void WriteColors()
        {
            WriteComment("Class that contains the same colors as System.Drawing.Colors.");
            WriteLine("[System.CodeDom.Compiler.GeneratedCode(\"Magick.NET.FileGenerator\", \"\")]");
            WriteLine("public static class MagickColors");
            WriteStartColon();

            var properties = _Type.GetProperties(BindingFlags.Public | BindingFlags.Static);

            WriteColor("None", properties.First(p => p.Name == "Transparent"));
            foreach (var property in properties)
                WriteColor(property);

            WriteEndColon();
        }

        private void WriteComment(string comment)
        {
            WriteLine("/// <summary>");
            WriteLine("/// " + comment);
            WriteLine("/// </summary>");
        }

        public static void Generate()
        {
            var generator = new MagickColorsGenerator();
            generator.CreateWriter(PathHelper.GetFullPath(@"src\Magick.NET\Colors\MagickColors.cs"));
            generator.WriteStart("ImageMagick");
            generator.WriteColors();
            generator.WriteEnd();
            generator.CloseWriter();
        }
    }
}
