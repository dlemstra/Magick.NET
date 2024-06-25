// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick.SourceGenerator;

[AttributeUsage(AttributeTargets.Class)]
internal sealed class NativeInteropAttribute : Attribute
{
    public bool CustomInstance { get; set; } = false;

    public bool ManagedToNative { get; set; } = false;

    public bool QuantumType { get; set; } = false;

    public bool StaticDispose { get; set; } = false;
}
