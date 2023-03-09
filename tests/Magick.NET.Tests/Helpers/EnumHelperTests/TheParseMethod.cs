// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class EnumHelperTests
    {
        public class TheParseMethod
        {
            public class WithIntegerAndDefault
            {
                [Fact]
                public void ShouldReturnTheCorrectValue()
                {
                    var result = EnumHelper.Parse(4, ExifDataType.Undefined);
                    Assert.Equal(ExifDataType.Long, result);
                }

                [Fact]
                public void ShouldReturnTheDefaultValueWhenValueIsNotInEnum()
                {
                    var result = EnumHelper.Parse(42, ExifDataType.Rational);
                    Assert.Equal(ExifDataType.Rational, result);
                }
            }

            public class WithShortAndDefault
            {
                [Fact]
                public void ShouldReturnTheCorrectValue()
                {
                    var result = EnumHelper.Parse((ushort)8, IptcTag.Unknown);
                    Assert.Equal(IptcTag.EditorialUpdate, result);
                }

                [Fact]
                public void ShouldReturnTheDefaultValueWhenValueIsNotInEnum()
                {
                    var result = EnumHelper.Parse((ushort)256, IptcTag.Unknown);
                    Assert.Equal(IptcTag.Unknown, result);
                }
            }

            public class WithStringAndDefault
            {
                [Fact]
                public void ShouldReturnTheCorrectValue()
                {
                    var result = EnumHelper.Parse("Green", Channels.Undefined);
                    Assert.Equal(Channels.Green, result);
                }

                [Fact]
                public void ShouldReturnTheDefaultValueWhenValueIsNull()
                {
                    var result = EnumHelper.Parse(null, Channels.Yellow);
                    Assert.Equal(Channels.Yellow, result);
                }

                [Fact]
                public void ShouldReturnTheDefaultValueWhenValueIsEmpty()
                {
                    var result = EnumHelper.Parse(string.Empty, Channels.Black);
                    Assert.Equal(Channels.Black, result);
                }

                [Fact]
                public void ShouldReturnTheDefaultValueWhenValueIsNotInEnum()
                {
                    var result = EnumHelper.Parse("Purple", Channels.Undefined);
                    Assert.Equal(Channels.Undefined, result);
                }
            }
        }
    }
}
