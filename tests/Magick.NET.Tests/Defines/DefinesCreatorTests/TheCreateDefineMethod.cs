// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class DefinesCreatorTests
    {
        public class TheCreateDefineMethod
        {
            public class WithBoolean
            {
                [Fact]
                public void ShouldReturnTheCorrectDefine()
                {
                    var testDefine = new TestDefine();
                    var value = true;

                    var define = testDefine.PublicCreateDefine("test", value);

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
                    var testDefine = new TestDefine();
                    var value = 42.42;

                    var define = testDefine.PublicCreateDefine("test", value);

                    Assert.Equal(MagickFormat.A, define.Format);
                    Assert.Equal("test", define.Name);
                    Assert.Equal("42.42", define.Value);
                }
            }

            public class WithInteger
            {
                [Fact]
                public void ShouldReturnTheCorrectDefine()
                {
                    var testDefine = new TestDefine();
                    var value = 42;

                    var define = testDefine.PublicCreateDefine("test", value);

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
                    var testDefine = new TestDefine();
                    var value = 42L;

                    var define = testDefine.PublicCreateDefine("test", value);

                    Assert.Equal(MagickFormat.A, define.Format);
                    Assert.Equal("test", define.Name);
                    Assert.Equal("42", define.Value);
                }
            }

            public class WithGeometry
            {
                [Fact]
                public void ShouldReturnTheCorrectDefine()
                {
                    var testDefine = new TestDefine();
                    var geometry = new MagickGeometry(1, 2, 3, 4);

                    var define = testDefine.PublicCreateDefine("test", geometry);

                    Assert.Equal(MagickFormat.A, define.Format);
                    Assert.Equal("test", define.Name);
                    Assert.Equal("3x4+1+2", define.Value);
                }
            }

            public class WithString
            {
                [Fact]
                public void ShouldReturnTheCorrectDefine()
                {
                    var testDefine = new TestDefine();
                    var value = "42";

                    var define = testDefine.PublicCreateDefine("test", value);

                    Assert.Equal(MagickFormat.A, define.Format);
                    Assert.Equal("test", define.Name);
                    Assert.Equal("42", define.Value);
                }

                [Fact]
                public void ShouldThrowExceptionWhenValueIsNull()
                {
                    var testDefine = new TestDefine();
                    IMagickGeometry value = null;

                    Assert.Throws<ArgumentNullException>("value", () => testDefine.PublicCreateDefine("test", value));
                }
            }

            public class WithEnum
            {
                [Fact]
                public void ShouldReturnTheCorrectDefine()
                {
                    var testDefine = new TestDefine();
                    var value = Channels.Red;

                    var define = testDefine.PublicCreateDefine("test", value);

                    Assert.Equal(MagickFormat.A, define.Format);
                    Assert.Equal("test", define.Name);
                    Assert.Equal("Red", define.Value);
                }
            }

            public class WithEnumerable
            {
                [Fact]
                public void ShouldReturnTheCorrectDefine()
                {
                    var testDefine = new TestDefine();
                    var value = new[] { Channels.Red, Channels.Green };

                    var define = testDefine.PublicCreateDefine("test", value);

                    Assert.Equal(MagickFormat.A, define.Format);
                    Assert.Equal("test", define.Name);
                    Assert.Equal("Cyan,Green", define.Value);
                }

                [Fact]
                public void ShouldReturnNullWhenValueIsNull()
                {
                    var testDefine = new TestDefine();

                    var define = testDefine.PublicCreateDefine("test", (IEnumerable<string>)null);

                    Assert.Null(define);
                }

                [Fact]
                public void ShouldSkipNullvalue()
                {
                    var testDefine = new TestDefine();
                    var value = new[] { "A", null, "B" };

                    var define = testDefine.PublicCreateDefine("test", value);

                    Assert.Equal(MagickFormat.A, define.Format);
                    Assert.Equal("test", define.Name);
                    Assert.Equal("A,B", define.Value);
                }

                [Fact]
                public void ShouldReturnNullForEmptyCollection()
                {
                    var testDefine = new TestDefine();
                    var value = Enumerable.Empty<string>();

                    var define = testDefine.PublicCreateDefine("test", value);

                    Assert.Null(define);
                }
            }
        }
    }
}
