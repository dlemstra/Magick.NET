// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick
{
    internal static class INativeInstanceExtensions
    {
        internal static IntPtr GetInstance(this INativeInstance self)
        {
            if (self == null)
                return IntPtr.Zero;

            return self.Instance;
        }
    }
}
