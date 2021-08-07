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

namespace ImageMagick
{
    internal sealed class TemporaryMagickFormat : IDisposable
    {
        private readonly List<MagickFormatData> _images = new();

        public TemporaryMagickFormat(MagickImage image, MagickFormat format)
        {
            AddImage(image, format);
        }

        public TemporaryMagickFormat(MagickImageCollection images, MagickFormat format)
        {
            foreach (var image in images)
            {
                AddImage(image, format);
            }
        }

        public void Dispose()
        {
            foreach (var image in _images)
            {
                image.RestoreOriginalFormat();
            }
        }

        private void AddImage(IMagickImage<QuantumType> image, MagickFormat format)
        {
            _images.Add(new MagickFormatData(image));
            image.Format = format;
        }

        private sealed class MagickFormatData
        {
            private readonly IMagickImage<QuantumType> _image;
            private readonly MagickFormat _originalImageFormat;
            private readonly MagickFormat _originalSettingsFormat;

            public MagickFormatData(IMagickImage<QuantumType> image)
            {
                _image = image;
                _originalImageFormat = image.Format;
                _originalSettingsFormat = image.Settings.Format;
            }

            public void RestoreOriginalFormat()
            {
                _image.Format = _originalImageFormat;
                _image.Settings.Format = _originalSettingsFormat;
            }
        }
    }
}
