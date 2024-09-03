// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class IptcValueTests
{
    public class TheToStringMethod
    {
        [Fact]
        public void ShouldReturnTheValue()
        {
            var value = new IptcValue(IptcTag.Caption, "Test");

            Assert.Equal("Test", value.ToString());
        }

        [Fact]
        public void ShouldReturnEmptyStringWhenValueIsNull()
        {
            var value = new IptcValue(IptcTag.Caption, (string?)null!);

            Assert.Empty(value.ToString());
        }

        [Fact]
        public void ShouldReturnEmptyStringWhenValueIsEmpty()
        {
            var value = new IptcValue(IptcTag.Caption, []);

            Assert.Empty(value.ToString());
        }
    }
}
