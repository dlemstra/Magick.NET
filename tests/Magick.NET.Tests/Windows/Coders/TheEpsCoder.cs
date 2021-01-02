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

#if WINDOWS_BUILD

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class TheEpsCoder
    {
        [Fact]
        public void ShouldReadTwoImages()
        {
            using (var images = new MagickImageCollection(Files.Coders.SwedenHeartEPS))
            {
                Assert.Equal(2, images.Count);

                Assert.Equal(447, images[0].Width);
                Assert.Equal(420, images[0].Height);
                Assert.Equal(MagickFormat.Ept, images[0].Format);

                Assert.Equal(447, images[1].Width);
                Assert.Equal(420, images[1].Height);
                Assert.Equal(MagickFormat.Tiff, images[1].Format);
            }
        }

        [Fact]
        public void ShouldReadClipPathsInTiffPreview()
        {
            using (var images = new MagickImageCollection(Files.Coders.SwedenHeartEPS))
            {
                var profile = images[1].Get8BimProfile();

                Assert.Single(profile.ClipPaths);
            }
        }
    }
}

#endif