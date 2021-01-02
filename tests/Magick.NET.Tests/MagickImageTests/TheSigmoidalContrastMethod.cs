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

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheSigmoidalContrastMethod : MagickImageTests
        {
            [Fact]
            public void ShouldSharpenByDefault()
            {
                using (var image = new MagickImage(Files.NoisePNG))
                {
                    using (var other = image.Clone())
                    {
                        image.SigmoidalContrast(8.0);
                        other.SigmoidalContrast(true, 8.0);

                        var difference = other.Compare(image, ErrorMetric.RootMeanSquared);
                        Assert.Equal(0.0, difference);
                    }
                }
            }

            [Fact]
            public void ShouldUseHalfOfQuantumForMidpointByDefault()
            {
                using (var image = new MagickImage(Files.NoisePNG))
                {
                    using (var other = image.Clone())
                    {
                        image.SigmoidalContrast(true, 4.0);
                        other.SigmoidalContrast(true, 4.0, new Percentage(50));

                        var difference = other.Compare(image, ErrorMetric.RootMeanSquared);
                        Assert.Equal(0.0, difference);
                    }
                }
            }

            [Fact]
            public void ShouldAdjustTheImageContrast()
            {
                using (var image = new MagickImage(Files.NoisePNG))
                {
                    using (var other = image.Clone())
                    {
                        other.SigmoidalContrast(true, 4.0, new Percentage(25));

                        var difference = other.Compare(image, ErrorMetric.RootMeanSquared);
                        Assert.InRange(difference, 0.051, 0.052);
                    }
                }
            }
        }
    }
}
