// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Globalization;

namespace ImageMagick;

internal sealed class TemporaryDefines : IDisposable
{
    private readonly INativeMagickImage _image;
    private readonly List<string> _names = new();

    public TemporaryDefines(IMagickImage image)
    {
        _image = MagickImage.GetNativeImage(image);
    }

    public TemporaryDefines(INativeMagickImage image)
    {
        _image = image;
    }

    public void Dispose()
    {
        foreach (var name in _names)
        {
            _image.RemoveArtifact(name);
        }
    }

    public void SetArtifact(string name, string? value)
    {
        if (value is null || value.Length < 1)
            return;

        _names.Add(name);
        _image.SetArtifact(name, value);
    }

    public void SetArtifact(string name, bool flag)
    {
        _names.Add(name);
        _image.SetArtifact(name, flag ? "true" : "false");
    }

    public void SetArtifact<TValue>(string name, TValue? value)
    {
        if (value is null)
            return;

        _names.Add(name);

        string? stringValue;
        if (value is IConvertible convertible)
            stringValue = convertible.ToString(CultureInfo.InvariantCulture);
        else
            stringValue = value.ToString();

        if (stringValue is not null)
            _image.SetArtifact(name, stringValue);
    }
}
