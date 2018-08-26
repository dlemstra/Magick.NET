// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Shared
{
    public partial class MagickImageCollectionTests
    {
        [TestClass]
        public class TheCoalesceMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenCollectionIsEmpty()
            {
                using (IMagickImageCollection images = new MagickImageCollection())
                {
                    ExceptionAssert.Throws<InvalidOperationException>(() =>
                    {
                        images.Coalesce();
                    });
                }
            }

            [TestMethod]
            public void ShouldMergeTheImages()
            {
                using (IMagickImageCollection collection = new MagickImageCollection())
                {
                    collection.Read(Files.RoseSparkleGIF);

                    using (IPixelCollection pixels = collection[1].GetPixels())
                    {
                        MagickColor color = pixels.GetPixel(53, 3).ToColor();
                        Assert.AreEqual(0, color.A);
                    }

                    collection.Coalesce();

                    using (IPixelCollection pixels = collection[1].GetPixels())
                    {
                        MagickColor color = pixels.GetPixel(53, 3).ToColor();
                        Assert.AreEqual(Quantum.Max, color.A);
                    }
                }
            }
        }
    }
}
