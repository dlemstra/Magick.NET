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

using System.Collections.Generic;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheAutoThresholdMethod
        {
            [Fact]
            public void ShouldThresholdImageWithKapurMethod()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
                {
                    image.AutoThreshold(AutoThresholdMethod.Kapur);

                    var colors = image.Histogram();

                    Assert.Equal(ColorType.Bilevel, image.DetermineColorType());
                    Assert.Equal(236359, colors[MagickColors.Black]);
                    Assert.Equal(3641, colors[MagickColors.White]);
                }
            }

            [Fact]
            public void ShouldThresholdImageWithOTSUMethod()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
                {
                    image.AutoThreshold(AutoThresholdMethod.OTSU);

                    var colors = image.Histogram();

                    Assert.Equal(ColorType.Bilevel, image.DetermineColorType());
                    Assert.Equal(67844, colors[MagickColors.Black]);
                    Assert.Equal(172156, colors[MagickColors.White]);
                }
            }

            [Fact]
            public void ShouldThresholdImageWithTriangleMethod()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
                {
                    image.AutoThreshold(AutoThresholdMethod.Triangle);

                    var colors = image.Histogram();

                    Assert.Equal(ColorType.Bilevel, image.DetermineColorType());
                    Assert.Equal(210553, colors[MagickColors.Black]);
                    Assert.Equal(29447, colors[MagickColors.White]);
                }
            }
        }
    }
}
