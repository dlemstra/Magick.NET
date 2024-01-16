// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Text;

namespace ImageMagick;

internal sealed class CodeBuilder
{
    private readonly StringBuilder _builder = new();
    private bool _indentationWritten = false;

    public int Indent { get; set; }

    public void Append(int value)
    {
        AppendIdentation();
        _builder.Append(value);
    }

    public void Append(string? value)
    {
        AppendIdentation();
        _builder.Append(value);
    }

    public void AppendComment(string value)
    {
        AppendLine("/// <summary>");
        Append("/// ");
        AppendLine(value);
        AppendLine("/// </summary>");
    }

    public void AppendLine(string? value = null)
    {
        AppendIdentation();

        _builder.AppendLine(value);
        _indentationWritten = false;
    }

    public void AppendQuantumType()
    {
        AppendLine("#if Q8");
        AppendLine("using QuantumType = System.Byte;");
        AppendLine("#elif Q16");
        AppendLine("using QuantumType = System.UInt16;");
        AppendLine("#elif Q16HDRI");
        AppendLine("using QuantumType = System.Single;");
        AppendLine("#else");
        AppendLine("#error Not implemented!");
        AppendLine("#endif");
    }

    public override string ToString()
        => _builder.ToString();

    private void AppendIdentation()
    {
        if (_indentationWritten)
            return;

        for (var i = 0; i < Indent; i++)
            _builder.Append("    ");

        _indentationWritten = true;
    }
}
