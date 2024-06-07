// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickDefineTests
{
    public class TheConstructor
    {
        public class WithBoolean
        {
            [Fact]
            public void ShouldReturnTheCorrectDefine()
            {
                var value = true;
                var define = new MagickDefine(MagickFormat.A, "test", value);

                Assert.Equal(MagickFormat.A, define.Format);
                Assert.Equal("test", define.Name);
                Assert.Equal("true", define.Value);
            }
        }

        public class WithDouble
        {
            [Fact]
            public void ShouldReturnTheCorrectDefine()
            {
                var value = 42.42;
                var define = new MagickDefine(MagickFormat.A, "test", value);

                Assert.Equal(MagickFormat.A, define.Format);
                Assert.Equal("test", define.Name);
                Assert.Equal("42.42", define.Value);
            }
        }

        public class WithEnum
        {
            [Fact]
            public void ShouldReturnTheCorrectDefine()
            {
                var value = Channels.Red;
                var define = new MagickDefine(MagickFormat.A, "test", value);

                Assert.Equal(MagickFormat.A, define.Format);
                Assert.Equal("test", define.Name);
                Assert.Equal("cyan", define.Value);
            }
        }

        public class WithGeometry
        {
            [Fact]
            public void ShouldReturnTheCorrectDefine()
            {
                var value = new MagickGeometry(1, 2, 3, 4);
                var define = new MagickDefine(MagickFormat.A, "test", value);

                Assert.Equal(MagickFormat.A, define.Format);
                Assert.Equal("test", define.Name);
                Assert.Equal("3x4+1+2", define.Value);
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                IMagickGeometry value = null;

                Assert.Throws<ArgumentNullException>("value", () =>
                {
                    new MagickDefine(MagickFormat.A, "test", value);
                });
            }
        }

        public class WithInteger
        {
            [Fact]
            public void ShouldReturnTheCorrectDefine()
            {
                var value = 42;
                var define = new MagickDefine(MagickFormat.A, "test", value);

                Assert.Equal(MagickFormat.A, define.Format);
                Assert.Equal("test", define.Name);
                Assert.Equal("42", define.Value);
            }
        }

        public class WithLong
        {
            [Fact]
            public void ShouldReturnTheCorrectDefine()
            {
                var value = 42L;
                var define = new MagickDefine(MagickFormat.A, "test", value);

                Assert.Equal(MagickFormat.A, define.Format);
                Assert.Equal("test", define.Name);
                Assert.Equal("42", define.Value);
            }
        }

        public class WithString
        {
            [Fact]
            public void ShouldReturnTheCorrectDefine()
            {
                var value = "42";
                var define = new MagickDefine(MagickFormat.A, "test", value);

                Assert.Equal(MagickFormat.A, define.Format);
                Assert.Equal("test", define.Name);
                Assert.Equal("42", define.Value);
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                string value = null;

                Assert.Throws<ArgumentNullException>("value", () => new MagickDefine(MagickFormat.A, "test", value));
            }
        }
    }
}
