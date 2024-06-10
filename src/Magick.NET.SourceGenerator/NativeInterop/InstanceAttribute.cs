// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick.SourceGenerator;

[AttributeUsage(AttributeTargets.Method)]
internal sealed class InstanceAttribute : Attribute
{
    public bool SetInstance { get; set; } = true;

    public bool UsesInstance { get; set; } = true;
}
