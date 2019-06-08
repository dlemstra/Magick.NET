// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class TheBgrCoder
    {
#if Q8
        private readonly byte[] _bytes = new byte[] { 1, 2, 3, 4 };
#elif Q16 || Q16HDRI
        private readonly byte[] _bytes = new byte[] { 1, 0, 2, 0, 3, 0, 4, 0 };
#else
#error Not implemented!
#endif

        private readonly MagickReadSettings _settings = new MagickReadSettings()
        {
            Width = 1,
            Height = 1,
        };

        [TestMethod]
        public void ShouldSetTheCorrectValueForTheAlphaChannel()
        {
            _settings.Format = MagickFormat.Bgra;
            using (IMagickImage image = new MagickImage(_bytes, _settings))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    Pixel pixel = pixels.GetPixel(0, 0);
                    Assert.AreEqual(4, pixel.Channels);
                    Assert.AreEqual(3, pixel.GetChannel(0));
                    Assert.AreEqual(2, pixel.GetChannel(1));
                    Assert.AreEqual(1, pixel.GetChannel(2));
                    Assert.AreEqual(4, pixel.GetChannel(3));
                }
            }
        }

        [TestMethod]
        public void ShouldSetTheCorrectValueForTheOpacityChannel()
        {
            _settings.Format = MagickFormat.Bgro;
            using (IMagickImage image = new MagickImage(_bytes, _settings))
            {
                using (IPixelCollection pixels = image.GetPixels())
                {
                    Pixel pixel = pixels.GetPixel(0, 0);
                    Assert.AreEqual(4, pixel.Channels);
                    Assert.AreEqual(3, pixel.GetChannel(0));
                    Assert.AreEqual(2, pixel.GetChannel(1));
                    Assert.AreEqual(1, pixel.GetChannel(2));
                    Assert.AreEqual(Quantum.Max - 4, pixel.GetChannel(3));
                }
            }
        }
    }
}
