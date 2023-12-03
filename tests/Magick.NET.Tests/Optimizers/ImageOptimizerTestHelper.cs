// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public abstract class ImageOptimizerTestHelper
{
    protected long AssertCompressSmaller(string fileName, Func<FileInfo, bool> action)
        => AssertCompress(fileName, action, resultIsSmaller: true);

    protected long AssertCompressNotSmaller(string fileName, Func<FileInfo, bool> action)
        => AssertCompress(fileName, action, resultIsSmaller: false);

    protected long AssertCompressSmaller(string fileName, Func<Stream, bool> action)
        => AssertCompress(fileName, action, resultIsSmaller: true);

    protected long AssertCompressNotSmaller(string fileName, Func<Stream, bool> action)
        => AssertCompress(fileName, action, resultIsSmaller: false);

    protected long AssertCompressSmaller(string fileName, Func<string, bool> action)
        => AssertCompress(fileName, action, resultIsSmaller: true);

    protected long AssertCompressNotSmaller(string fileName, Func<string, bool> action)
        => AssertCompress(fileName, action, resultIsSmaller: false);

    protected void AssertInvalidFileFormat(string fileName, Action<FileInfo> action)
    {
        using var tempFile = new TemporaryFile(fileName);

        Assert.Throws<MagickCorruptImageErrorException>(() => action(tempFile.File));
    }

    protected void AssertInvalidFileFormat(string fileName, Action<Stream> action)
    {
        using var tempFile = new TemporaryFile(fileName);
        using var fileStream = FileHelper.OpenRead(fileName);
        using var memoryStream = new MemoryStream();
        fileStream.CopyTo(memoryStream);
        memoryStream.Position = 0;

        Assert.Throws<MagickCorruptImageErrorException>(() => action(memoryStream));
    }

    protected void AssertInvalidFileFormat(string fileName, Action<string> action)
    {
        using var tempFile = new TemporaryFile(fileName);

        Assert.Throws<MagickCorruptImageErrorException>(() => action(tempFile.File.FullName));
    }

    private long AssertCompress(string fileName, Func<FileInfo, bool> action, bool resultIsSmaller)
    {
        using var tempFile = new TemporaryFile(fileName);

        var before = tempFile.Length;

        var result = action(tempFile.File);

        var after = tempFile.Length;

        Assert.Equal(resultIsSmaller, result);

        if (resultIsSmaller)
            Assert.True(after < before, $"Expected {after} to be smaller than {before}.");
        else
            Assert.Equal(before, after);

        return after;
    }

    private long AssertCompress(string fileName, Func<Stream, bool> action, bool resultIsSmaller)
    {
        using var fileStream = FileHelper.OpenRead(fileName);
        using var memoryStream = new MemoryStream();

        memoryStream.Position = 42;
        fileStream.CopyTo(memoryStream);
        memoryStream.Position = 42;

        var before = memoryStream.Length;

        var result = action(memoryStream);
        memoryStream.Flush();

        var after = memoryStream.Length;

        Assert.Equal(42, memoryStream.Position);
        Assert.Equal(resultIsSmaller, result);

        if (resultIsSmaller)
            Assert.True(after < before, $"Expected {after} to be smaller than {before}.");
        else
            Assert.Equal(before, after);

        return after - 42;
    }

    private long AssertCompress(string fileName, Func<string, bool> action, bool resultIsSmaller)
    {
        using var tempFile = new TemporaryFile(fileName);

        var before = tempFile.Length;

        var result = action(tempFile.File.FullName);

        var after = tempFile.Length;

        Assert.Equal(resultIsSmaller, result);

        if (resultIsSmaller)
            Assert.True(after < before, $"Expected {after} to be smaller than {before}.");
        else
            Assert.Equal(before, after);

        return after;
    }
}
