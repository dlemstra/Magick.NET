// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class ByteQuantumScalerTests
{
    public class TheScaleToByteMethod
    {
        [Fact]
        public void ShouldScaleByteToByte()
        {
            var scaler = new ByteQuantumScaler();
            var result = scaler.ScaleToByte(255);

            Assert.Equal(255, result);
        }
    }
}
