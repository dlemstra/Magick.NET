// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Text;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class DrawableTextEncodingTests
    {
        [Fact]
        public void Test_Encoding()
        {
            DrawableTextEncoding encoding = new DrawableTextEncoding(Encoding.UTF8);
            encoding.Encoding = null;

            using (var image = new MagickImage(MagickColors.Firebrick, 10, 10))
            {
                image.Draw(encoding);
            }
        }
    }
}
