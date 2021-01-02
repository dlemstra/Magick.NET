// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System.Drawing;
using System.Drawing.Imaging;
using ImageMagick;
using Xunit;

namespace Magick.NET.SystemDrawing.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheToBitmapMethod
        {
            [Fact]
            public void ShouldReturnBitmap()
            {
                using (var images = new MagickImageCollection(Files.RoseSparkleGIF))
                {
                    Assert.Equal(3, images.Count);

                    using (Bitmap bitmap = images.ToBitmap())
                    {
                        Assert.NotNull(bitmap);
                        Assert.Equal(3, bitmap.GetFrameCount(FrameDimension.Page));
                    }
                }
            }

            [Fact]
            public void ShouldUseOptimizationForSingleImage()
            {
                using (var images = new MagickImageCollection(Files.RoseSparkleGIF))
                {
                    images.RemoveAt(0);
                    images.RemoveAt(0);

                    Assert.Single(images);

                    using (Bitmap bitmap = images.ToBitmap())
                    {
                        Assert.NotNull(bitmap);
                    }
                }
            }
        }
    }
}