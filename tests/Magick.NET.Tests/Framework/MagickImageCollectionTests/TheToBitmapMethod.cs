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

#if !NETCORE

using System.Drawing;
using System.Drawing.Imaging;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        [TestClass]
        public class TheToBitmapMethod
        {
            [TestMethod]
            public void ShouldReturnBitmap()
            {
                using (IMagickImageCollection images = new MagickImageCollection(Files.RoseSparkleGIF))
                {
                    Assert.AreEqual(3, images.Count);

                    using (Bitmap bitmap = images.ToBitmap())
                    {
                        Assert.IsNotNull(bitmap);
                        Assert.AreEqual(3, bitmap.GetFrameCount(FrameDimension.Page));
                    }
                }
            }

            [TestMethod]
            public void ShouldUseOptimizationForSingleImage()
            {
                using (IMagickImageCollection images = new MagickImageCollection(Files.RoseSparkleGIF))
                {
                    images.RemoveAt(0);
                    images.RemoveAt(0);

                    Assert.AreEqual(1, images.Count);

                    using (Bitmap bitmap = images.ToBitmap())
                    {
                        Assert.IsNotNull(bitmap);
                    }
                }
            }
        }
    }
}

#endif