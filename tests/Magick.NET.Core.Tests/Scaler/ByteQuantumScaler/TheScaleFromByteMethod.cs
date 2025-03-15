// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class ByteQuantumScalerTests
{
    public class TheScaleFromByteMethod
    {
        [Fact]
        public void ShouldScaleByteToByte()
        {
            var scaler = new ByteQuantumScaler();
            var result = scaler.ScaleFromByte(255);

            Assert.Equal(255, result);
        }
    }
}
