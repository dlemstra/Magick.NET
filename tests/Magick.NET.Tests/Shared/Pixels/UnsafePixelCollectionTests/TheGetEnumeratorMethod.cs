// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class UnsafePixelCollectionTests
    {
        [TestClass]
        public class TheGetEnumeratorMethod
        {
            [TestMethod]
            public void ShouldReturnEnumerator()
            {
                using (IMagickImage image = new MagickImage(Files.CirclePNG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        IEnumerator<Pixel> enumerator = pixels.GetEnumerator();
                        Assert.IsNotNull(enumerator);
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnEnumeratorForInterfaceImplementation()
            {
                using (IMagickImage image = new MagickImage(Files.CirclePNG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        IEnumerable enumerable = pixels;
                        Assert.IsNotNull(enumerable.GetEnumerator());
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnEnumeratorForFirst()
            {
                using (IMagickImage image = new MagickImage(Files.ConnectedComponentsPNG, 10, 10))
                {
                    Pixel pixel = image.GetPixelsUnsafe().First(p => p.ToColor() == MagickColors.Black);
                    Assert.IsNotNull(pixel);

                    Assert.AreEqual(350, pixel.X);
                    Assert.AreEqual(196, pixel.Y);
                    Assert.AreEqual(2, pixel.Channels);
                }
            }

            [TestMethod]
            public void ShouldReturnEnumeratorForCount()
            {
                using (IMagickImage image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        Assert.AreEqual(50, pixels.Count());
                    }
                }
            }
        }
    }
}
