// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick;

internal sealed class TemporaryImageAttacher : IDisposable
{
    private readonly List<IMagickImage<byte>> _images;

    public TemporaryImageAttacher(List<IMagickImage<QuantumType>> images)
    {
        if (images.Count == 0)
            throw new InvalidOperationException("Operation requires at least one image.");

        _images = images;
        AttachImages();
    }

    public void Dispose()
        => DetachImages();

    private void AttachImages()
    {
        for (var i = 0; i < _images.Count - 1; i++)
        {
            if (_images[i] is MagickImage image)
                image.SetNext(_images[i + 1]);
        }
    }

    private void DetachImages()
    {
        for (var i = 0; i < _images.Count - 1; i++)
        {
            if (_images[i] is MagickImage image)
                image.SetNext(null);
        }
    }
}
