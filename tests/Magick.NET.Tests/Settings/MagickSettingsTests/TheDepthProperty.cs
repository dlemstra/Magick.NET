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
    public partial class MagickSettingsTests
    {
        public class TheDepthProperty
        {
            [Fact]
            public void ShouldChangeTheDepthOfTheOutputImage()
            {
                using (var input = new MagickImage(Files.Builtin.Logo))
                {
                    input.Settings.Depth = 5;

                    var bytes = input.ToByteArray(MagickFormat.Tga);

                    using (var output = new MagickImage(bytes, MagickFormat.Tga))
                    {
                        Assert.Equal(5, output.Depth);
                    }
                }
            }
        }
    }
}
