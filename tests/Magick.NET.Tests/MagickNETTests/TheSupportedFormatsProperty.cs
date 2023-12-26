// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;
using Xunit.Sdk;

namespace Magick.NET.Tests;

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
            var formatsCount = MagickNET.SupportedFormats.Count;

            if (Runtime.IsWindows)
                Assert.Equal(268, formatsCount);
            else
                Assert.Equal(265, formatsCount);
        }

        [Fact]
        public void ShouldContainTheCorrectNumberOfReadableFormats()
        {
            var formatsCount = MagickNET.SupportedFormats
                .Where(format => format.SupportsReading)
                .Count();

            if (Runtime.IsWindows)
                Assert.Equal(246, formatsCount);
            else
                Assert.Equal(241, formatsCount);
        }

        [Fact]
        public void ShouldContainTheCorrectNumberOfWritableFormats()
        {
            var formatsCount = MagickNET.SupportedFormats
                .Where(format => format.SupportsWriting)
                .Count();

            if (Runtime.IsWindows)
                Assert.Equal(191, formatsCount);
            else
                Assert.Equal(190, formatsCount);
        }
    }
}
