// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
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

        [Fact]
        public void ShouldReturnTheFormatsWithTheCorrectSettings()
        {
            foreach (var formatInfo in MagickNET.SupportedFormats)
            {
                switch (formatInfo.Format)
                {
                    case MagickFormat.A:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Aai:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ai:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.APng:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Art:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Arw:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ashlar:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Avi:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Avif:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Avs:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.B:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Bayer:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Bayera:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Bgr:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Bgra:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Bgro:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Bmp:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Bmp2:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Bmp3:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Brf:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.C:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Cal:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Cals:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Canvas:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Caption:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Cin:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Cip:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Clip:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Clipboard:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Cmyk:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Cmyka:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Cr2:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Cr3:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Crw:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Cube:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Cur:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Cut:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Data:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Dcm:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Dcr:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Dcraw:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Dcx:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Dds:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Dfont:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Dib:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Dng:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Dpx:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Dxt1:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Dxt5:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Emf:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Epdf:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Epi:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Eps:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Eps2:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Eps3:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Epsf:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Epsi:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ept:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ept2:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ept3:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Erf:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Exr:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Farbfeld:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Fax:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ff:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.File:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Fits:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Fl32:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Flv:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Fractal:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ftp:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Fts:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ftxt:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.G:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.G3:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.G4:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Gif:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Gif87:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Gradient:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Gray:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Graya:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Group4:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Hald:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Hdr:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Heic:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Heif:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Histogram:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Hrz:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Htm:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Html:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Http:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Https:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Icb:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ico:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Icon:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Iiq:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Info:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Inline:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ipl:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Isobrl:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Isobrl6:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.J2c:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.J2k:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Jng:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Jnx:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Jp2:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Jpc:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Jpe:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Jpeg:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Jpg:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Jpm:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Jps:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Jpt:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Json:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Jxl:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.K:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.K25:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Kdc:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Label:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.M:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.M2v:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.M4v:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Mac:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Map:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Mask:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Mat:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Matte:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Mef:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Miff:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Mkv:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Mng:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Mono:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Mov:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Mp4:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Mpc:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Mpeg:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Mpg:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Mpo:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Mrw:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Msl:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Msvg:
                        Assert.False(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Mtv:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Mvg:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Nef:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Nrw:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Null:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.O:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ora:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Orf:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Otb:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Otf:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pal:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Palm:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pam:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pango:
                        Assert.False(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pattern:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pbm:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pcd:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pcds:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pcl:
                        Assert.False(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pct:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pcx:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pdb:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pdf:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pdfa:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pef:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pes:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pfa:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pfb:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pfm:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pgm:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pgx:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Phm:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Picon:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pict:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pix:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pjpeg:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Plasma:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Png:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Png00:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Png24:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Png32:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Png48:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Png64:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Png8:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pnm:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pocketmod:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ppm:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ps:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ps2:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ps3:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Psb:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Psd:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ptif:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Pwp:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Qoi:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.R:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.RadialGradient:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Raf:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ras:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Raw:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Rgb:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Rgb565:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Rgba:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Rgbo:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Rgf:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Rla:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Rle:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Rmf:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Rsvg:
                        Assert.False(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Rw2:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Scr:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Screenshot:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Sct:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Sfw:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Sgi:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Shtml:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Six:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Sixel:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.SparseColor:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Sr2:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Srf:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Stegano:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.StrImg:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Sun:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Svg:
                        Assert.False(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Svgz:
                        Assert.False(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Text:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Tga:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.ThreeFr:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.ThreeG2:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.ThreeGp:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Thumbnail:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Tif:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Tiff:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Tiff64:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Tile:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Tim:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Tm2:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ttc:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ttf:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Txt:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ubrl:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ubrl6:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Uil:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Uyvy:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Vda:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Vicar:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Vid:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Viff:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Vips:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Vst:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Wbmp:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.WebM:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.WebP:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Wmf:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Wmv:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Wpg:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.X3f:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Xbm:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Xc:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Xcf:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Xpm:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Xps:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Xv:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Y:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Yaml:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ycbcr:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Ycbcra:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    case MagickFormat.Yuv:
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.SupportsWriting);
                        break;
                    default:
                        throw new NotImplementedException(formatInfo.ToString());
                }
            }
        }
    }
}
