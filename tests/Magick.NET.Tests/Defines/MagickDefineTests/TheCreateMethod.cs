// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickDefineTests
{
    public class TheCreateMethod
    {
        [Fact]
        public void ShouldReturnTheCorrectDefine()
        {
            var value = new[] { Channels.Red, Channels.Green };

            var define = MagickDefine.Create(MagickFormat.A, "test", value);

            Assert.Equal(MagickFormat.A, define.Format);
            Assert.Equal("test", define.Name);
            Assert.Equal(2, define.Value.Split(',').Length);
            Assert.True(define.Value.Contains("Red") || define.Value.Contains("Cyan"));
            Assert.True(define.Value.Contains("Green") || define.Value.Contains("Magenta"));
        }

        [Fact]
        public void ShouldReturnNullWhenValueIsNull()
        {
            var define = MagickDefine.Create(MagickFormat.A, "test", (IEnumerable<string>)null);

            Assert.Null(define);
        }

        [Fact]
        public void ShouldSkipNullvalue()
        {
            var value = new[] { "A", null, "B" };

            var define = MagickDefine.Create(MagickFormat.A, "test", value);

            Assert.Equal(MagickFormat.A, define.Format);
            Assert.Equal("test", define.Name);
            Assert.Equal("A,B", define.Value);
        }

        [Fact]
        public void ShouldReturnNullForEmptyCollection()
        {
            var value = Array.Empty<string>();

            var define = MagickDefine.Create(MagickFormat.A, "test", value);

            Assert.Null(define);
        }
    }
}
