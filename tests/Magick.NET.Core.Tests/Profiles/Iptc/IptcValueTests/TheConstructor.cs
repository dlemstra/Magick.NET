// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class IptcValueTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldThrowExceptionWhenBytesIsNull()
        {
            Assert.Throws<ArgumentNullException>("value", () => new IptcValue(IptcTag.Caption, (byte[]?)null!));
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenStringIsNull()
        {
            var value = new IptcValue(IptcTag.Caption, (string?)null!);
        }
    }
}
