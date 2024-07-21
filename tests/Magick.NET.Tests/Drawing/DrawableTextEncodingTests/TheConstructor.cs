// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class DrawableTextEncodingTests
{
    public class TheConstructor
    {
        public void ShouldThrowExceptionWhenEncodingIsNull()
        {
            Assert.Throws<ArgumentNullException>("encoding", () =>
            {
                new DrawableTextEncoding(null);
            });
        }
    }
}
