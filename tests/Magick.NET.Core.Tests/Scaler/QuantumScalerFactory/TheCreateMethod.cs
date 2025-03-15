// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class QuantumScalerFactoryTests
{
    public class TheCreateMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenTypeIsNotSupported()
        {
            Assert.Throws<NotSupportedException>(() =>
            {
                var factory = QuantumScalerFactory.Create<int>();
            });
        }

        [Fact]
        public void ShouldReturnByteQuantumScalerWhenTypeIsByte()
        {
            var factory = QuantumScalerFactory.Create<byte>();
            Assert.IsType<ByteQuantumScaler>(factory);
        }

        [Fact]
        public void ShouldReturnUnsignedShortQuantumScalerWhenTypeIsUShort()
        {
            var factory = QuantumScalerFactory.Create<ushort>();
            Assert.IsType<UnsignedShortQuantumScaler>(factory);
        }

        [Fact]
        public void ShouldReturnFloatQuantumScalerWhenTypeIsFloat()
        {
            var factory = QuantumScalerFactory.Create<float>();
            Assert.IsType<FloatQuantumScaler>(factory);
        }
    }
}
