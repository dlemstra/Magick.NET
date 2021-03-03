// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace FileGenerator.Native
{
    [Flags]
    public enum DynamicMode
    {
        None = 0,
        NativeToManaged = 1,
        ManagedToNative = 2,
        Both = 3
    }
}