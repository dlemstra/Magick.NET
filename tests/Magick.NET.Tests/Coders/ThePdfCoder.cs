﻿// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

[Collection(nameof(IsolatedUnitTest))]
public partial class ThePdfCoder
{
    [Fact]
    public async Task ShouldReadFileMultithreadedCorrectly()
    {
        Assert.SkipUnless(Ghostscript.IsAvailable, "Ghostscript is not available");

        await IsolatedUnitTest.Execute(static async () =>
        {
            var results = new Task[3];

            for (var i = 0; i < results.Length; ++i)
            {
                results[i] = Task.Run(
                    () =>
                    {
                        using var image = new MagickImage();
                        image.Read(Files.Coders.CartoonNetworkStudiosLogoAI);

                        Assert.Equal(765U, image.Width);
                        Assert.Equal(361U, image.Height);
                        Assert.Equal(MagickFormat.Ai, image.Format);
                    },
                    TestContext.Current.CancellationToken);
            }

            for (var i = 0; i < results.Length; ++i)
            {
                await results[i];
            }
        });
    }

    [Fact]
    public void ShouldReturnTheCorrectFormatForAiFile()
    {
        Assert.SkipUnless(Ghostscript.IsAvailable, "Ghostscript is not available");

        IsolatedUnitTest.Execute(static () =>
        {
            using var image = new MagickImage(Files.Coders.CartoonNetworkStudiosLogoAI);

            Assert.Equal(765U, image.Width);
            Assert.Equal(361U, image.Height);
            Assert.Equal(MagickFormat.Ai, image.Format);
        });
    }
}
