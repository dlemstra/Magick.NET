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

#if NETCOREAPP
using System;
using System.IO;
using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheWriteAsyncMethod
        {
            public class WithStream
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("stream", () => image.WriteAsync(null));
                    }
                }
            }

            public class WithStreamAndMagickFormat
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("stream", () => image.WriteAsync(null, MagickFormat.Bmp));
                    }
                }

                [Fact]
                public async Task ShouldUseTheSpecifiedFormat()
                {
                    using (var input = new MagickImage(Files.CirclePNG))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var stream = new NonSeekableStream(memoryStream))
                            {
                                await input.WriteAsync(stream, MagickFormat.Tiff);
                                Assert.Equal(MagickFormat.Png, input.Format);

                                memoryStream.Position = 0;
                                using (var output = new MagickImage(stream))
                                {
                                    Assert.Equal(MagickFormat.Tiff, output.Format);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
#endif
