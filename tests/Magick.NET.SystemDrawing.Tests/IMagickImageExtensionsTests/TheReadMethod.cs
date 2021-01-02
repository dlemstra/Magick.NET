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

using System;
using System.Drawing;
using System.Drawing.Imaging;
using ImageMagick;
using Xunit;

namespace Magick.NET.SystemDrawing.Tests
{
    public partial class IMagickImageExtensionsTests
    {
        public partial class TheReadMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenBitmapIsNull()
            {
                using (var image = new MagickImage())
                {
                    Assert.Throws<ArgumentNullException>("bitmap", () => image.Read((Bitmap)null));
                }
            }

            [Fact]
            public void ShouldUsePngFormatWhenBitmapIsPng()
            {
                using (Bitmap bitmap = new Bitmap(Files.SnakewarePNG))
                {
                    using (var image = new MagickImage())
                    {
                        image.Read(bitmap);

                        Assert.Equal(286, image.Width);
                        Assert.Equal(67, image.Height);
                        Assert.Equal(MagickFormat.Png, image.Format);
                    }
                }
            }

            [Fact]
            public void ShouldUseBmpFormatWhenBitmapIsMemoryBmp()
            {
                using (Bitmap bitmap = new Bitmap(100, 50, PixelFormat.Format24bppRgb))
                {
                    Assert.Equal(bitmap.RawFormat, ImageFormat.MemoryBmp);

                    using (var image = new MagickImage())
                    {
                        image.Read(bitmap);

                        Assert.Equal(100, image.Width);
                        Assert.Equal(50, image.Height);
                        Assert.Equal(MagickFormat.Bmp3, image.Format);
                    }
                }
            }
        }
    }
}