// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class ByteQuantumScalerTests
{
    public class TheScaleFromUnsignedShortMethod
    {
        [Theory]
        [InlineData(ushort.MinValue, byte.MinValue)]
        [InlineData(ushort.MaxValue, byte.MaxValue)]
        public void ShouldScaleUnsignedShortToByte(ushort input, byte output)
        {
            var scaler = new ByteQuantumScaler();
            var result = scaler.ScaleFromUnsignedShort(input);

            Assert.Equal(output, result);
        }
    }
}
