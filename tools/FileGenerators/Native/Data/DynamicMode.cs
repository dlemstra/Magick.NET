// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace FileGenerator.Native;

[Flags]
public enum DynamicMode
{
    None = 0b0,

    NativeToManaged = 0b01,

    ManagedToNative = 0b10,

    Both = 0b11,

    Custom = 0b100,
}
