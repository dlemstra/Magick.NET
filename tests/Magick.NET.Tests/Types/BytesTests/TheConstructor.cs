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

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class BytesTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                Assert.Throws<ArgumentNullException>("stream", () =>
                {
                    new Bytes(null);
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamPositionIsNotZero()
            {
                using (var memStream = new MemoryStream())
                {
                    memStream.Position = 10;

                    Assert.Throws<ArgumentException>("stream", () =>
                    {
                        new Bytes(memStream);
                    });
                }
            }

            [Fact]
            public void ShouldSetPropertiesWhenStreamIsEmpty()
            {
                using (var memStream = new MemoryStream())
                {
                    var bytes = new Bytes(memStream);

                    Assert.Equal(0, bytes.Length);
                    Assert.NotNull(bytes.GetData());
                    Assert.Empty(bytes.GetData());
                }
            }

            [Fact]
            public void ShouldSetPropertiesWhenStreamIsFileStream()
            {
                using (var fileStream = File.OpenRead(Files.ImageMagickJPG))
                {
                    var bytes = new Bytes(fileStream);

                    Assert.Equal(18749, bytes.Length);
                    Assert.NotNull(bytes.GetData());
                    Assert.Equal(18749, bytes.GetData().Length);
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamCannotRead()
            {
                using (var stream = new TestStream(false, true, true))
                {
                    Assert.Throws<ArgumentException>("stream", () =>
                    {
                        new Bytes(stream);
                    });
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsTooLong()
            {
                using (var stream = new TestStream(true, true, true))
                {
                    stream.SetLength(long.MaxValue);

                    Assert.Throws<ArgumentException>("length", () =>
                    {
                        new Bytes(stream);
                    });
                }
            }
        }
    }
}
