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
using ImageMagick.Formats.Jp2;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class Jp2WriteDefinesTests
    {
        public class TheQualityProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new Jp2WriteDefines
                    {
                        Quality = new float[] { 4, 2 },
                    });

                    Assert.Equal("4,2", image.Settings.GetDefine(MagickFormat.Jp2, "quality"));
                }
            }

            [Fact]
            public void ShouldNotSetTheDefineWhenTheCollectionIsEmpty()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new Jp2WriteDefines
                    {
                        Quality = new float[] { },
                    });

                    Assert.Null(image.Settings.GetDefine(MagickFormat.Jp2, "quality"));
                }
            }
        }
    }
}
