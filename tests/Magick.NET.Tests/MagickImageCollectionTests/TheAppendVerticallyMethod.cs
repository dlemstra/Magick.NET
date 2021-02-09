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

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheAppendAppendVerticallyMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenCollectionIsEmpty()
            {
                using (var images = new MagickImageCollection())
                {
                    Assert.Throws<InvalidOperationException>(() =>
                    {
                        images.AppendVertically();
                    });
                }
            }

            [Fact]
            public void ShouldAppendTheImagesVertically()
            {
                int width = 70;
                int height = 46;

                using (var images = new MagickImageCollection())
                {
                    images.Read(Files.RoseSparkleGIF);

                    Assert.Equal(width, images[0].Width);
                    Assert.Equal(height, images[0].Height);

                    using (var image = images.AppendVertically())
                    {
                        Assert.Equal(width, image.Width);
                        Assert.Equal(height * 3, image.Height);
                    }
                }
            }
        }
    }
}
