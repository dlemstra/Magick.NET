// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class DrawableDensityTests
    {
        [Fact]
        public void Test_ImageSize()
        {
            using (var image = CreateImage(null))
            {
                Assert.Equal(107, image.Width);
                Assert.Equal(19, image.Height);
            }

            using (var image = CreateImage(97))
            {
                Assert.Equal(142, image.Width);
                Assert.Equal(24, image.Height);
            }
        }

        [Fact]
        public void Test_Constructor()
        {
            var density = new DrawableDensity(new PointD(4, 2));
            Assert.Equal(4, density.Density.X);
            Assert.Equal(2, density.Density.Y);
        }

        private MagickImage CreateImage(int? density)
        {
            var image = new MagickImage(MagickColors.Purple, 500, 500);
            var pointSize = new DrawableFontPointSize(20);
            var text = new DrawableText(250, 250, "Magick.NET");

            if (!density.HasValue)
                image.Draw(pointSize, text);
            else
                image.Draw(pointSize, new DrawableDensity(density.Value), text);

            image.Trim();

            return image;
        }
    }
}
