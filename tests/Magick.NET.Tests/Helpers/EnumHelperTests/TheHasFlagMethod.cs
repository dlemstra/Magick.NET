// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class EnumHelperTests
{
    public class TheHasFlagMethod
    {
        [Fact]
        public void ShouldReturnTrueWhenValueHasFlag()
        {
            var redBlue = Channels.Red | Channels.Blue;

            var result = EnumHelper.HasFlag(redBlue, Channels.Blue);

            Assert.True(result);
        }

        [Fact]
        public void ShouldReturnFalseWhenValueDoesNotHaveFlag()
        {
            var redBlue = Channels.Red | Channels.Blue;

            var result = EnumHelper.HasFlag(redBlue, Channels.Green);

            Assert.False(result);
        }
    }
}
