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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheColorThresholdMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenStartColorIsNull()
            {
                using (var image = new MagickImage())
                {
                    Assert.Throws<ArgumentNullException>("startColor", () => image.ColorThreshold(null, new MagickColor()));
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenStopColorIsNull()
            {
                using (var image = new MagickImage())
                {
                    Assert.Throws<ArgumentNullException>("stopColor", () => image.ColorThreshold(new MagickColor(), null));
                }
            }

            [Fact]
            public void ShouldChangeTheImageToBlackAndWhite()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
                {
                    var startColor = MagickColor.FromRgb(60, 110, 150);
                    var stopColor = MagickColor.FromRgb(70, 120, 170);

                    image.ColorThreshold(startColor, stopColor);

                    ColorAssert.Equal(MagickColors.White, image, 300, 160);
                    ColorAssert.Equal(MagickColors.Black, image, 300, 260);
                }
            }
        }
    }
}
