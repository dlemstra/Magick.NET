// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class IptcValueTests
{
    public class TheValueProperty
    {
        [Fact]
        public void ShouldChangeTheValue()
        {
            var value = new IptcValue(IptcTag.Caption, "Test");
            value.Value = string.Empty;

            Assert.Equal(string.Empty, value.ToString());
        }
    }
}
