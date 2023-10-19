// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;

namespace ImageMagick;

internal sealed class TemporaryFile : IDisposable
{
    private readonly FileInfo _tempFile;

    public TemporaryFile()
        => _tempFile = new FileInfo(Path.Combine(MagickNET.TemporaryDirectory, Guid.NewGuid().ToString()));

    public long Length
    {
        get
        {
            _tempFile.Refresh();
            return _tempFile.Length;
        }
    }

    public string FullName
        => _tempFile.FullName;

    public void CopyTo(TemporaryFile temporaryFile)
        => CopyTo(temporaryFile._tempFile);

    public void CopyTo(FileInfo file)
    {
        _tempFile.CopyTo(file.FullName, true);
        file.Refresh();
    }

    public void Dispose()
    {
        if (_tempFile.Exists)
            _tempFile.Delete();
    }
}
