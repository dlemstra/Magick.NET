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
        public class TheLocalContrastMethod
        {
            [Fact]
            public void ShouldOnlyChangeSpecifiedChannels()
            {
                using (var image = new MagickImage("plasma:purple", 100, 100))
                {
                    using (var allChannels = image.Clone())
                    {
                        allChannels.LocalContrast(2, new Percentage(50));
                        image.LocalContrast(2, new Percentage(50), Channels.Red);

                        var difference = image.Compare(allChannels, ErrorMetric.RootMeanSquared);
                        Assert.NotEqual(0, difference);
                    }
                }
            }
        }
    }
}
