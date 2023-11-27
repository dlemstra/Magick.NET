// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick.ImageOptimizers;
using Xunit;

namespace Magick.NET.Tests;

public abstract class ImageOptimizerTestHelper<TOptimizer> : ImageOptimizerTestHelper
    where TOptimizer : IImageOptimizer, new()
{
    protected IImageOptimizer Optimizer
        => new TOptimizer();

    protected long AssertCompressSmaller(string fileName)
    {
        var lengthA = AssertCompressSmaller(fileName, (FileInfo file) => Optimizer.Compress(file));
        var lengthB = AssertCompressSmaller(fileName, (string file) => Optimizer.Compress(file));
        var lengthC = AssertCompressSmaller(fileName, (Stream stream) => Optimizer.Compress(stream));

        Assert.InRange(lengthA, lengthB - 1, lengthB + 1);
        Assert.InRange(lengthB, lengthC - 1, lengthC + 1);
        return lengthA;
    }

    protected void AssertCompressNotSmaller(string fileName)
    {
        var lengthA = AssertCompressNotSmaller(fileName, (FileInfo file) => Optimizer.Compress(file));
        var lengthB = AssertCompressNotSmaller(fileName, (string file) => Optimizer.Compress(file));
        var lengthC = AssertCompressNotSmaller(fileName, (Stream stream) => Optimizer.Compress(stream));

        Assert.InRange(lengthA, lengthB - 1, lengthB + 1);
        Assert.InRange(lengthB, lengthC - 1, lengthC + 1);
    }

    protected void AssertCompressTwice(string fileName)
    {
        using var tempFile = new TemporaryFile(fileName);

        var compressed1 = Optimizer.Compress(tempFile.File);

        var after1 = tempFile.Length;

        var compressed2 = Optimizer.Compress(tempFile.File);

        var after2 = tempFile.Length;

        Assert.InRange(after1, after2 - 1, after2 + 1);
        Assert.True(compressed1);
    }

    protected void AssertCompressInvalidFileFormat(string fileName)
    {
        AssertInvalidFileFormat(fileName, (FileInfo file) => Optimizer.Compress(file));
        AssertInvalidFileFormat(fileName, (string file) => Optimizer.Compress(file));
        AssertInvalidFileFormat(fileName, (Stream stream) => Optimizer.Compress(stream));
    }

    protected long AssertLosslessCompressSmaller(string fileName)
    {
        var lengthA = AssertCompressSmaller(fileName, (FileInfo file) => Optimizer.LosslessCompress(file));
        var lengthB = AssertCompressSmaller(fileName, (string file) => Optimizer.LosslessCompress(file));
        var lengthC = AssertCompressSmaller(fileName, (Stream stream) => Optimizer.LosslessCompress(stream));

        Assert.InRange(lengthA, lengthB - 1, lengthB + 1);
        Assert.InRange(lengthB, lengthC - 1, lengthC + 1);
        return lengthA;
    }

    protected void AssertLosslessCompressNotSmaller(string fileName)
    {
        var lengthA = AssertCompressNotSmaller(fileName, (FileInfo file) => Optimizer.LosslessCompress(file));
        var lengthB = AssertCompressNotSmaller(fileName, (string file) => Optimizer.LosslessCompress(file));
        var lengthC = AssertCompressNotSmaller(fileName, (Stream stream) => Optimizer.LosslessCompress(stream));

        Assert.Equal(lengthA, lengthB);
        Assert.Equal(lengthB, lengthC);
    }

    protected void AssertLosslessCompressTwice(string fileName)
    {
        using var tempFile = new TemporaryFile(fileName);

        var compressed1 = Optimizer.LosslessCompress(tempFile.File);

        var after1 = tempFile.Length;

        Optimizer.LosslessCompress(tempFile.File);

        var after2 = tempFile.Length;

        Assert.InRange(after1, after2 - 1, after2 + 1);
        Assert.True(compressed1);
    }

    protected void AssertLosslessCompressInvalidFileFormat(string fileName)
    {
        AssertInvalidFileFormat(fileName, (FileInfo file) => Optimizer.LosslessCompress(file));
        AssertInvalidFileFormat(fileName, (string file) => Optimizer.LosslessCompress(file));
        AssertInvalidFileFormat(fileName, (Stream stream) => Optimizer.LosslessCompress(stream));
    }
}
