// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if WINDOWS_BUILD

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class OpenCLTests
    {
        [Fact]
        public void Test_IsEnabled()
        {
            OpenCL.IsEnabled = false;
            Assert.False(OpenCL.IsEnabled);

            OpenCL.IsEnabled = true;
            Assert.True(OpenCL.IsEnabled);
        }
    }
}

#endif