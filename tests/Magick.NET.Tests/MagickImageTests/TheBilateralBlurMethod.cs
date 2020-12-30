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

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheBilateralBlurMethod
        {
            [Fact]
            public void ShouldApplyTheFilter()
            {
                using (var image = new MagickImage(Files.NoisePNG))
                {
                    using (var blurredImage = image.Clone())
                    {
                        blurredImage.BilateralBlur(2, 2);
#if Q8
                        Assert.InRange(image.Compare(blurredImage, ErrorMetric.RootMeanSquared), 0.0008, 0.00081);
#else
                        Assert.InRange(image.Compare(blurredImage, ErrorMetric.RootMeanSquared), 0.00069, 0.0007);
#endif
                    }
                }
            }
        }
    }
}
