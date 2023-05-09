// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Globalization;

namespace ImageMagick;

internal sealed class TemporaryDefines : IDisposable
{
    private readonly MagickImage _image;
    private readonly List<string> _names = new();

    public TemporaryDefines(MagickImage image)
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

    public void SetArtifact(string name, bool value)
    {
        _names.Add(name);
        _image.SetArtifact(name, value);
    }

    public void SetArtifact(string name, double? value)
    {
        if (value is null)
            return;

        _names.Add(name);
        _image.SetArtifact(name, value.Value.ToString(CultureInfo.InvariantCulture));
    }

    public void SetArtifact<TValue>(string name, TValue? value)
    {
        if (value is null)
            return;

        _names.Add(name);
        _image.SetArtifact(name, value.ToString());
    }
}
