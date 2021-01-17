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
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class DdsReadDefinesTests
    {
        public class TheSkipMipmapsProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                var settings = new MagickReadSettings
                {
                    Defines = new DdsReadDefines
                    {
                        SkipMipmaps = false,
                    },
                };

                using (var images = new MagickImageCollection())
                {
                    images.Read(Files.Coders.TestDDS, settings);

                    Assert.Equal(5, images.Count);
                    Assert.Equal("false", images[0].Settings.GetDefine(MagickFormat.Dds, "skip-mipmaps"));
                }
            }

            [Fact]
            public void ShouldSkipTheMipmaps()
            {
                var settings = new MagickReadSettings
                {
                    Defines = new DdsReadDefines
                    {
                        SkipMipmaps = true,
                    },
                };

                using (var images = new MagickImageCollection())
                {
                    images.Read(Files.Coders.TestDDS, settings);

                    Assert.Single(images);
                }
            }
        }
    }
}
