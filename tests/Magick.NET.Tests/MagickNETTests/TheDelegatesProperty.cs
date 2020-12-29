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
    public partial class MagickNETTests
    {
        public class TheDelegatesProperty
        {
            [Fact]
            public void ShouldReturnAllDelegates()
            {
                var delegates = MagickNET.Delegates;

                if (OperatingSystem.IsWindows)
                    Assert.Equal("cairo flif freetype gslib heic jng jp2 jpeg jxl lcms lqr openexr pangocairo png ps raw rsvg tiff webp xml zlib", delegates);
                else if (OperatingSystem.IsLinux)
                    Assert.Equal("cairo fontconfig freetype heic jng jp2 jpeg jxl lcms lqr openexr pangocairo png raw rsvg tiff webp xml zlib", delegates);
                else
                    Assert.Equal("cairo fontconfig freetype heic jng jp2 jpeg lcms lqr openexr pangocairo png raw rsvg tiff webp xml zlib", delegates);
            }
        }
    }
}