// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#nullable enable

using System;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace ImageMagick.SourceGenerator;

internal static class IncrementalGeneratorPostInitializationContextExtensions
{
    public static void AddAttributeSource<TAttribute>(this IncrementalGeneratorPostInitializationContext context)
    {
        var type = typeof(TAttribute);

        using var stream = type.Assembly.GetManifestResourceStream(type.Name) ?? throw new InvalidOperationException();

        using var streamReader = new StreamReader(stream, Encoding.UTF8);
        var sourceCode = streamReader.ReadToEnd();

        context.AddSource(
             $"{type.Name}.g.cs",
             SourceText.From(sourceCode, Encoding.UTF8));
    }
}
