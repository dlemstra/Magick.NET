// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;

namespace Magick.NET.Tests;

public sealed class TemporaryFile : IDisposable
{
    private readonly FileInfo _file;

    public TemporaryFile(byte[] data)
    {
        _file = new FileInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()));
        System.IO.File.WriteAllBytes(_file.FullName, data);
    }

    public TemporaryFile(string fileName)
    {
        if (System.IO.File.Exists(fileName))
            _file = CreateFromFile(fileName);
        else
            _file = CreateEmptyFile(fileName);
    }

    public long Length
    {
        get
        {
            _file.Refresh();
            return _file.Length;
        }
    }

    public FileInfo File
        => _file;

    public void Dispose()
        => Cleanup.DeleteFile(_file);

    private static FileInfo CreateEmptyFile(string fileName)
    {
        var file = new FileInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + fileName));
        file.Create().Dispose();
        file.Refresh();

        return file;
    }

    private static FileInfo CreateFromFile(string fileName)
    {
        var file = new FileInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + Path.GetFileName(fileName)));
        FileHelper.Copy(fileName, file.FullName);
        file.Refresh();

        return file;
    }
}
