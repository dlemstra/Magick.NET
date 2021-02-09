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
    public partial class MagickImageTests
    {
        public class TheClaheMethod
        {
            [Fact]
            public void ShouldChangeTheImage()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
                {
                    using (var result = image.Clone())
                    {
                        result.Clahe(10, 20, 30, 1.5);

                        Assert.InRange(image.Compare(result, ErrorMetric.RootMeanSquared), 0.08, 0.09);
                    }
                }
            }

            [Fact]
            public void ShouldUsePercentageOfTheWidthAndHeight()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
                {
                    using (var result = image.Clone())
                    {
                        result.Clahe(new Percentage(1.6666), new Percentage(5), 30, 1.5);

                        Assert.InRange(image.Compare(result, ErrorMetric.RootMeanSquared), 0.07, 0.08);
                    }
                }
            }
        }
    }
}
