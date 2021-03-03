// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class ThePangoCoder
    {
        [Fact]
        public void IsThreadSafe()
        {
            string LoadImage()
            {
                using (var image = new MagickImage("pango:1"))
                {
                    return image.Signature;
                }
            }

            string signature = LoadImage();
            Parallel.For(1, 10, (int i) =>
            {
                Assert.Equal(signature, LoadImage());
            });
        }

        [Fact]
        public void CanReadFromLargePangoFile()
        {
            string fileName = "pango:<span font=\"Arial\">" + new string('*', 4500) + "</span>";
            using (var image = new MagickImage(fileName))
            {
            }
        }
    }
}
