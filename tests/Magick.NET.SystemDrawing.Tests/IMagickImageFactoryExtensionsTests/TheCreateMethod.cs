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
using ImageMagick;
using Xunit;

namespace Magick.NET.SystemDrawing.Tests
{
    public partial class MagickImageFactoryTests
    {
        public partial class TheCreateMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenBitmapIsNull()
            {
                var factory = new MagickImageFactory();
                Assert.Throws<ArgumentNullException>("bitmap", () => factory.Create((Bitmap)null));
            }

            [Fact]
            public void ShouldCreateImageFromBitmap()
            {
                using (var bitmap = new Bitmap(Files.SnakewarePNG))
                {
                    var factory = new MagickImageFactory();
                    using (var image = factory.Create(bitmap))
                    {
                        Assert.Equal(286, image.Width);
                        Assert.Equal(67, image.Height);
                        Assert.Equal(MagickFormat.Png, image.Format);
                    }
                }
            }
        }
    }
}