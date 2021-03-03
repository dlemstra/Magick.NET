// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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
