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

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheSeparateMethod
        {
            [Fact]
            public void ShouldReturnTheCorrectNumberOfChannels()
            {
                using (var rose = new MagickImage(Files.Builtin.Rose))
                {
                    var i = 0;
                    foreach (MagickImage image in rose.Separate())
                    {
                        i++;
                        image.Dispose();
                    }

                    Assert.Equal(3, i);
                }
            }

            [Fact]
            public void ShouldReturnTheSpecifiedChannels()
            {
                using (var rose = new MagickImage(Files.Builtin.Rose))
                {
                    var i = 0;
                    foreach (MagickImage image in rose.Separate(Channels.Red | Channels.Green))
                    {
                        i++;
                        image.Dispose();
                    }

                    Assert.Equal(2, i);
                }
            }

            [Fact]
            public void ShouldReturnImageWithGrayColorspace()
            {
                using (var logo = new MagickImage(Files.Builtin.Logo))
                {
                    using (var blue = logo.Separate(Channels.Blue).First())
                    {
                        Assert.Equal(ColorSpace.Gray, blue.ColorSpace);
                    }
                }
            }
        }
    }
}
