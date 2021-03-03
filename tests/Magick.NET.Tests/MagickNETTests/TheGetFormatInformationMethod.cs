// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using ImageMagick;
using Xunit;
using Xunit.Sdk;
using OperatingSystem = ImageMagick.OperatingSystem;

namespace Magick.NET.Tests
{
    public partial class MagickNETTests
    {
        public class TheGetFormatInformationMethod
        {
            [Fact]
            public void ShouldReturnFormatInfoForAllFormats()
            {
                var missingFormats = new List<string>();

                foreach (MagickFormat format in Enum.GetValues(typeof(MagickFormat)))
                {
                    if (format == MagickFormat.Unknown)
                        continue;

                    var formatInfo = MagickNET.GetFormatInformation(format);
                    if (formatInfo == null)
                    {
                        if (ShouldReport(format))
                            missingFormats.Add(format.ToString());
                    }
                }

                if (missingFormats.Count > 0)
                    throw new XunitException("Cannot find MagickFormatInfo for: " + string.Join(", ", missingFormats.ToArray()));
            }

            private static bool ShouldReport(MagickFormat format)
            {
                if (!OperatingSystem.IsWindows)
                {
                    if (format == MagickFormat.Clipboard || format == MagickFormat.Emf || format == MagickFormat.Wmf)
                        return false;

                    if (format == MagickFormat.Flif)
                        return false;
                }

                if (OperatingSystem.IsMacOS)
                {
                    if (format == MagickFormat.Jxl)
                        return false;
                }

                return true;
            }
        }
    }
}
