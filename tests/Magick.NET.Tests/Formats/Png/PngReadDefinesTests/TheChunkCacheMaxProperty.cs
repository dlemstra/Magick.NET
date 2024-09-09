// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PngReadDefinesTests
{
    public class TheChunkCacheMaxProperty
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PngReadDefines
            {
                ChunkCacheMax = 10,
            });

            Assert.Equal("10", image.Settings.GetDefine(MagickFormat.Png, "chunk-cache-max"));
        }

        [Fact]
        public void ShouldLimitTheNumberOfChunks()
        {
            var warning = string.Empty;

            using var image = new MagickImage();
            image.Warning += (object? sender, WarningEventArgs e) =>
            {
                warning = e.Message;
            };

            var settings = new MagickReadSettings
            {
                Defines = new PngReadDefines
                {
                    ChunkCacheMax = 2,
                },
            };
            image.Read(Files.SnakewarePNG, settings);

            Assert.Contains("tEXt: no space in chunk cache", warning);
        }
    }
}
