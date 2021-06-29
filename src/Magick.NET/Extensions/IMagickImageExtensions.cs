// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
    internal static class IMagickImageExtensions
    {
        internal static IntPtr GetInstance(this IMagickImage? self)
        {
            if (self == null)
                return IntPtr.Zero;

            if (self is INativeInstance nativeInstance)
                return nativeInstance.Instance;

            throw new NotSupportedException();
        }

        internal static MagickSettings GetSettings(this IMagickImage<QuantumType>? self)
        {
            if (self?.Settings is MagickSettings settings)
                return settings;

            throw new NotSupportedException();
        }
    }
}
