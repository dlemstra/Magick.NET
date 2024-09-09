// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PngReadDefinesTests
{
    public class TheChunkMallocMaxProperty
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PngReadDefines
            {
                ChunkMallocMax = 20,
            });

            Assert.Equal("20", image.Settings.GetDefine(MagickFormat.Png, "chunk-malloc-max"));
        }

        [Fact]
        public void ShouldLimitTheChunkSize()
        {
            using var image = new MagickImage();
            var message = string.Empty;

            image.Warning += (object? sender, WarningEventArgs e) =>
            {
                message = e.Message;
            };

            var settings = new MagickReadSettings
            {
                Defines = new PngReadDefines
                {
                    ChunkMallocMax = 2,
                },
            };
            image.Read(Files.SnakewarePNG, settings);

            Assert.Contains("IHDR: chunk data is too large", message);
        }
    }
}
