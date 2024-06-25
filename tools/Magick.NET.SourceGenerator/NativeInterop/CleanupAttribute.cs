// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#nullable enable

using System;

namespace ImageMagick.SourceGenerator;

[AttributeUsage(AttributeTargets.Method)]
internal sealed class CleanupAttribute : Attribute
{
    public string? Name { get; set; }

    public string? Arguments { get; set; }
}
