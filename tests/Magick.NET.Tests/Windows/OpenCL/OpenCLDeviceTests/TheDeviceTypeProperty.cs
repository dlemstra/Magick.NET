// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if WINDOWS_BUILD

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class TheDeviceTypeProperty
    {
        [Fact]
        public void ShouldNotBeUndefines()
        {
            foreach (var device in OpenCL.Devices)
            {
                Assert.NotEqual(OpenCLDeviceType.Undefined, device.DeviceType);
            }
        }
    }
}

#endif

