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

using System.IO;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class JpegWriteDefinesTests
    {
        public class TheExtentProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                var defines = new JpegWriteDefines
                {
                    Extent = 5,
                };

                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(defines);

                    Assert.Equal("5KB", image.Settings.GetDefine(MagickFormat.Jpeg, "extent"));
                }
            }

            [Fact]
            public void ShouldLimitTheSizeOfTheOutputFile()
            {
                var defines = new JpegWriteDefines
                {
                    Extent = 10,
                };

                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    using (MemoryStream memStream = new MemoryStream())
                    {
                        image.Settings.SetDefines(defines);

                        image.Format = MagickFormat.Jpeg;
                        image.Write(memStream);
                        Assert.True(memStream.Length < 10000);
                    }
                }
            }
        }
    }
}
