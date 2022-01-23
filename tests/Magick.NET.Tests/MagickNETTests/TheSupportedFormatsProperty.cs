// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;
using Xunit.Sdk;

namespace Magick.NET.Tests
{
    public partial class MagickNETTests
    {
        public class TheSupportedFormatsProperty
        {
            [Fact]
            public void ShouldContainNoFormatInformationWithMagickFormatSetToUnknown()
            {
                foreach (var formatInfo in MagickNET.SupportedFormats)
                {
                    if (formatInfo.Format == MagickFormat.Unknown)
                        throw new XunitException("Unknown format: " + formatInfo.Description + " (" + formatInfo.ModuleFormat + ")");
                }
            }

            [Fact]
            public void ShouldContainTheCorrectNumberOfFormats()
            {
                var formatsCount = MagickNET.SupportedFormats.Count();

                if (OperatingSystem.IsWindows)
#if PLATFORM_arm64
                    Assert.Equal(263, formatsCount);
#else
                    Assert.Equal(264, formatsCount);
#endif
                else if (OperatingSystem.IsLinux)
                    Assert.Equal(261, formatsCount);
                else
                    Assert.Equal(260, formatsCount);
            }
        }
    }
}
