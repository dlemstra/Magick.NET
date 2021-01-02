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
using Xunit;

namespace Magick.NET.Tests
{
    public class TheAvifCoder
    {
        [Fact]
        public void ShouldEncodeAndDecodeAlphaChannel()
        {
            using (var input = new MagickImage(Files.TestPNG))
            {
                input.Resize(new Percentage(15));

                using (var stream = new MemoryStream())
                {
                    input.Write(stream, MagickFormat.Avif);

                    stream.Position = 0;

                    using (var output = new MagickImage(stream))
                    {
                        Assert.True(output.HasAlpha);
                        Assert.Equal(MagickFormat.Avif, output.Format);
                        Assert.Equal(input.Width, output.Width);
                        Assert.Equal(input.Height, output.Height);
                    }
                }
            }
        }
    }
}
