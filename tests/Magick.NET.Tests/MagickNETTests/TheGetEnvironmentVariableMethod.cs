// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickNETTests
{
    public class TheGetEnvironmentVariableMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenNameIsNull()
        {
            Assert.Throws<ArgumentNullException>("name", () => { MagickNET.GetEnvironmentVariable(null!); });
        }

        [Fact]
        public void ShouldThrowExceptionWhenNameIsEmpty()
        {
            Assert.Throws<ArgumentException>("name", () => { MagickNET.GetEnvironmentVariable(string.Empty); });
        }
    }
}
