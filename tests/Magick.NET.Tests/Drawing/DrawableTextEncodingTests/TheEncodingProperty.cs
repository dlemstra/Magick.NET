// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Text;
using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class DrawableTextEncodingTests
{
    public class TheEncodingProperty
    {
        public void ShouldSetTheProperties()
        {
            var encoding = new DrawableTextEncoding(Encoding.UTF8);
            Assert.Equal(Encoding.UTF8, encoding.Encoding);
        }
    }
}
