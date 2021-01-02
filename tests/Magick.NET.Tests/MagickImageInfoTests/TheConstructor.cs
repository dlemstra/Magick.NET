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
    public partial class MagickImageInfoTests
    {
        public class TheConstructor
        {
            public class WithByteArray
            {
                [Fact]
                public void ShouldThrowExceptionWhenDataIsNull()
                {
                    Assert.Throws<ArgumentNullException>("data", () => new MagickImageInfo((byte[])null));
                }

                [Fact]
                public void ShouldThrowExceptionWhenDataIsEmpty()
                {
                    Assert.Throws<ArgumentException>("data", () => new MagickImageInfo(new byte[0]));
                }
            }

            public class WithByteArrayAndOffset
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    Assert.Throws<ArgumentNullException>("data", () => new MagickImageInfo(null, 0, 0));
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    Assert.Throws<ArgumentException>("data", () => new MagickImageInfo(new byte[] { }, 0, 0));
                }

                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    Assert.Throws<ArgumentException>("offset", () => new MagickImageInfo(new byte[] { 215 }, -1, 0));
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    Assert.Throws<ArgumentException>("count", () => new MagickImageInfo(new byte[] { 215 }, 0, 0));
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    Assert.Throws<ArgumentException>("count", () => new MagickImageInfo(new byte[] { 215 }, 0, -1));
                }
            }

            public class WithFileInfo
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    Assert.Throws<ArgumentNullException>("file", () => new MagickImageInfo((FileInfo)null));
                }
            }

            public class WithFileName
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    Assert.Throws<ArgumentNullException>("fileName", () => new MagickImageInfo((string)null));
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    Assert.Throws<ArgumentException>("fileName", () => new MagickImageInfo(string.Empty));
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsInvalid()
                {
                    var exception = Assert.Throws<MagickBlobErrorException>(() =>
                    {
                        new MagickImageInfo(Files.Missing);
                    });

                    Assert.Contains("error/blob.c/OpenBlob", exception.Message);
                }
            }

            public class WithStream
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    Assert.Throws<ArgumentNullException>("stream", () => new MagickImageInfo((Stream)null));
                }
            }
        }
    }
}
