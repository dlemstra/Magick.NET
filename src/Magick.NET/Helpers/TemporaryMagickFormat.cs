// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick
{
    internal sealed class TemporaryMagickFormat : IDisposable
    {
        private readonly MagickImage _image;
        private readonly MagickFormat _originalImageFormat;
        private readonly MagickFormat _originalSettingsFormat;

        public TemporaryMagickFormat(MagickImage image, MagickFormat format)
        {
            _image = image;
            _originalImageFormat = image.Format;
            _originalSettingsFormat = image.Settings.Format;

            _image.Format = format;
        }

        public void Dispose()
        {
            _image.Format = _originalImageFormat;
            _image.Settings.Format = _originalSettingsFormat;
        }
    }
}
