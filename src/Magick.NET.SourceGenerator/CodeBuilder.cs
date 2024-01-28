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

    public void Append(params string?[] values)
    {
        AppendIdentation();

        foreach (var value in values)
            _builder.Append(value);
    }

    public void AppendComment(params string[] comment)
    {
        AppendLine("/// <summary>");
        foreach (var line in comment)
        {
            Append("/// ");
            AppendLine(line);
        }

        AppendLine("/// </summary>");
    }

    public void AppendParameterComment(string name, string comment)
    {
        Append("/// <param name=\"");
        Append(name);
        Append("\">");
        Append(comment);
        AppendLine("</param>");
    }

    public void AppendReturnsComment(string comment)
    {
        Append("/// <returns>");
        Append(comment);
        AppendLine("</returns>");
    }

    public void AppendLine(params string?[] values)
    {
        if (values.Length > 0)
            AppendIdentation();

        foreach (var value in values)
            _builder.Append(value);

        _builder.AppendLine();
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
