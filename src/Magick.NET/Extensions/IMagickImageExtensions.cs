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

        internal static IMagickErrorInfo? CreateErrorInfo(this IMagickImage? self)
        {
            if (self == null)
                return null;

            if (self is MagickImage image)
                return MagickImage.CreateErrorInfo(image);

            throw new NotSupportedException();
        }

        internal static void SetNext(this IMagickImage self, IMagickImage? next)
        {
            if (self is MagickImage image)
                image.SetNext(next);
            else
                throw new NotSupportedException();
        }
    }
}
