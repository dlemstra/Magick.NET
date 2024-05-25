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

internal sealed class TemporaryMagickFormat : IDisposable
{
    private readonly List<MagickFormatInfo> _formatInfos = new();

    public TemporaryMagickFormat(MagickImage image, MagickFormat format)
        => AddImage(image, format);

    public TemporaryMagickFormat(MagickImageCollection images, MagickFormat format)
    {
        foreach (var image in images)
        {
            AddImage(image, format);
        }
    }

    public void Dispose()
    {
        foreach (var formatInfo in _formatInfos)
        {
            formatInfo.RestoreOriginalFormat();
        }
    }

    private void AddImage(IMagickImage<QuantumType> image, MagickFormat format)
    {
        _formatInfos.Add(new MagickFormatInfo(image));
        image.Format = format;
    }

    private sealed class MagickFormatInfo
    {
        private readonly IMagickImage<QuantumType> _image;
        private readonly MagickFormat _originalImageFormat;
        private readonly MagickFormat _originalSettingsFormat;

        public MagickFormatInfo(IMagickImage<QuantumType> image)
        {
            _image = image;
            _originalImageFormat = image.Format;
            _originalSettingsFormat = image.Settings.Format;
        }

        public void RestoreOriginalFormat()
        {
            _image.Format = _originalImageFormat;
            /*
             * We need to set the format of the settings because it is possible that this was different
             * from the format of the image. And the Format property of the image also changes the settings.
            */
            _image.Settings.Format = _originalSettingsFormat;
        }
    }
}
