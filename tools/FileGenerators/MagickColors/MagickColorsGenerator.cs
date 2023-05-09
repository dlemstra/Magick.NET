// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Drawing;

namespace FileGenerator.MagickColors;

internal sealed class MagickColorsGenerator : ColorsGenerator
{
    public static void Generate()
    {
        var generator = new MagickColorsGenerator();
        generator.CreateWriter(PathHelper.GetFullPath(@"src\Magick.NET\Colors\MagickColors.cs"));
        generator.WriteStart("ImageMagick");
        generator.WriteColors();
        generator.CloseWriter();
    }

    protected override void WriteUsing()
        => WriteQuantumType();

    private static string GetArguments(Color color)
        => $"{color.R}, {color.G}, {color.B}, {color.A}";

    private void WriteColor(MagickColor color)
    {
        WriteComment(color.Comment);
        WriteLine("public static MagickColor " + color.Name);
        Indent++;
        WriteLine("=> MagickColor.FromRgba(" + GetArguments(color.Color) + ");");
        Indent--;
        WriteLine();

        WriteComment(color.Comment);
        WriteLine("IMagickColor<QuantumType> IMagickColors<QuantumType>." + color.Name);
        Indent++;
        WriteLine("=> " + color.Name + ";");
        Indent--;
        WriteLine();
    }

    private void WriteColors()
    {
        WriteComment("Class that contains the same colors as System.Drawing.Colors.");
        WriteLine("[System.CodeDom.Compiler.GeneratedCode(\"Magick.NET.FileGenerator\", \"\")]");
        WriteLine("public class MagickColors : IMagickColors<QuantumType>");
        WriteStartColon();

        foreach (var color in GetColors())
            WriteColor(color);

        WriteEndColon();
    }
}
