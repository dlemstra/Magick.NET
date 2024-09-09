// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickNETTests
{
    public class TheSetEnvironmentVariableMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenNameIsNull()
        {
            Assert.Throws<ArgumentNullException>("name", () =>
            {
                MagickNET.SetEnvironmentVariable(null!, "test");
            });
        }

        [Fact]
        public void ShouldThrowExceptionWhenNameIsEmpty()
        {
            Assert.Throws<ArgumentException>("name", () =>
            {
                MagickNET.SetEnvironmentVariable(string.Empty, "test");
            });
        }

        [Fact]
        public void ShouldSetTheSpecifiedEnvironmentVariable()
        {
            var value = "價值";

            MagickNET.SetEnvironmentVariable("FOO", value);

            var result = MagickNET.GetEnvironmentVariable("FOO");
            Assert.Equal(value, result);
        }
    }
}
