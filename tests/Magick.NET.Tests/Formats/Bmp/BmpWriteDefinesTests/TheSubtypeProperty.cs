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
    public partial class BmpWriteDefinesTests
    {
        public class TheSubtypeProperty
        {
            [Fact]
            public void ShouldBeUsed()
            {
                var defines = new BmpWriteDefines
                {
                    Subtype = BmpSubtype.RGB555,
                };

                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.Format = MagickFormat.Bmp;
                    image.ColorType = ColorType.TrueColor;

                    long length;

                    using (var memStream = new MemoryStream())
                    {
                        image.Write(memStream);
                        length = memStream.Length;
                    }

                    using (var memStream = new MemoryStream())
                    {
                        image.Write(memStream);
                        Assert.Equal(length, memStream.Length);
                    }

                    image.Settings.SetDefines(defines);

                    using (var memStream = new MemoryStream())
                    {
                        image.Write(memStream);
                        Assert.True(memStream.Length < length);
                    }
                }
            }
        }
    }
}
