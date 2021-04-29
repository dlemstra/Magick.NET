// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if WINDOWS_BUILD

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class TheIsEnabledProperty
    {
        [Fact]
        public void ShouldReturnTheCorrectValue()
        {
            foreach (var device in OpenCL.Devices)
            {
                bool isEnabled = device.IsEnabled;

                device.IsEnabled = !isEnabled;
                Assert.NotEqual(isEnabled, device.IsEnabled);

                device.IsEnabled = isEnabled;
                Assert.Equal(isEnabled, device.IsEnabled);
            }
        }
    }
}

#endif

