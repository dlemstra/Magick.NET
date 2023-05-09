// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace FileGenerator.MagickColors;

internal sealed class IMagickColorsGenerator : ColorsGenerator
{
    public static void Generate()
    {
        var generator = new IMagickColorsGenerator();
        generator.CreateWriter(PathHelper.GetFullPath(@"src\Magick.NET.Core\Colors\IMagickColors{TQuantumType}.cs"));
        generator.WriteStart("ImageMagick");
        generator.WriteColors();
        generator.CloseWriter();
    }

    protected override void WriteUsing()
    {
        WriteLine("using System;");
        WriteLine();
    }

    private void WriteColor(MagickColor color)
    {
        WriteComment(color.Comment);
        WriteLine("IMagickColor<TQuantumType> " + color.Name + " { get; }");
        WriteLine();
    }

    private void WriteColors()
    {
        WriteComment("Interface that contains the same colors as System.Drawing.Colors.");
        WriteLine("[System.CodeDom.Compiler.GeneratedCode(\"Magick.NET.FileGenerator\", \"\")]");
        WriteLine("public interface IMagickColors<TQuantumType>");
        Indent++;
        WriteLine("where TQuantumType : struct, IConvertible");
        Indent--;
        WriteStartColon();

        foreach (var color in GetColors())
            WriteColor(color);

        WriteEndColon();
    }
}
