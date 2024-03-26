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
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Aai:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ai:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.APng:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Art:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Arw:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ashlar:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Avi:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Avif:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Avs:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.B:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Bayer:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Bayera:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Bgr:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Bgra:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Bgro:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Bmp:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Bmp2:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Bmp3:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Brf:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.C:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Cal:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Cals:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Canvas:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Caption:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Cin:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Cip:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Clip:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Clipboard:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Cmyk:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Cmyka:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Cr2:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Cr3:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Crw:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Cube:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Cur:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Cut:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Data:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Dcm:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Dcr:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Dcraw:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Dcx:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Dds:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Dfont:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Dib:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Dng:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Dpx:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Dxt1:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Dxt5:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Emf:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Epdf:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Epi:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Eps:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Eps2:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Eps3:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Epsf:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Epsi:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ept:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ept2:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ept3:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Erf:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Exr:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Farbfeld:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Fax:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ff:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.File:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Fits:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Fl32:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Flv:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Fractal:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ftp:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        if (Runtime.IsWindows)
                        {
                            Assert.True(formatInfo.SupportsReading);
                            Assert.True(formatInfo.CanReadMultithreaded);
                        }
                        else
                        {
                            Assert.False(formatInfo.SupportsReading);
                            Assert.True(formatInfo.CanReadMultithreaded);
                        }

                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Fts:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ftxt:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.G:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.G3:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.G4:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Gif:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Gif87:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Gradient:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Gray:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Graya:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Group4:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Hald:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Hdr:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Heic:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Heif:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Histogram:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Hrz:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Htm:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Html:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Http:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        if (Runtime.IsWindows)
                        {
                            Assert.True(formatInfo.SupportsReading);
                            Assert.True(formatInfo.CanReadMultithreaded);
                        }
                        else
                        {
                            Assert.False(formatInfo.SupportsReading);
                            Assert.True(formatInfo.CanReadMultithreaded);
                        }

                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Https:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Icb:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ico:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Icon:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Iiq:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Info:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Inline:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ipl:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Isobrl:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Isobrl6:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.J2c:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.J2k:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Jng:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Jnx:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Jp2:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Jpc:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Jpe:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Jpeg:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Jpg:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Jpm:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Jps:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Jpt:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Json:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Jxl:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.K:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.K25:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Kdc:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Label:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.M:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.M2v:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.M4v:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Mac:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Map:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Mask:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Mat:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Matte:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Mef:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Miff:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Mkv:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Mng:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Mono:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Mov:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Mp4:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Mpc:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Mpeg:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Mpg:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Mpo:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Mrw:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Msl:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Msvg:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.False(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Mtv:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Mvg:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Nef:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Nrw:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Null:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.O:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ora:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Orf:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Otb:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Otf:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pal:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Palm:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pam:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pango:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.False(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pattern:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pbm:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pcd:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pcds:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pcl:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.False(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pct:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pcx:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pdb:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pdf:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pdfa:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pef:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pes:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pfa:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pfb:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pfm:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pgm:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pgx:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Phm:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Picon:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pict:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pix:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pjpeg:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Plasma:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Png:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Png00:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Png24:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Png32:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Png48:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Png64:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Png8:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pnm:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pocketmod:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ppm:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ps:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ps2:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ps3:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Psb:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Psd:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ptif:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Pwp:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Qoi:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.R:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.RadialGradient:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Raf:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ras:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Raw:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Rgb:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Rgb565:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Rgba:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Rgbo:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Rgf:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Rla:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Rle:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Rmf:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Rsvg:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.False(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Rw2:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Scr:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Screenshot:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Sct:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Sfw:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Sgi:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Shtml:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Six:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Sixel:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.SparseColor:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Sr2:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Srf:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Stegano:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.StrImg:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Sun:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Svg:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.False(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Svgz:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.False(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.False(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Text:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Tga:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.ThreeFr:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.ThreeG2:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.ThreeGp:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Thumbnail:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Tif:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Tiff:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Tiff64:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Tile:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Tim:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Tm2:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ttc:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ttf:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Txt:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ubrl:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ubrl6:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Uil:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Uyvy:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Vda:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Vicar:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Vid:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Viff:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Vips:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Vst:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Wbmp:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.WebM:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.WebP:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Wmf:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Wmv:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Wpg:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.X3f:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Xbm:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Xc:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Xcf:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Xpm:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Xps:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.False(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Xv:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Y:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Yaml:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.False(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ycbcr:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Ycbcra:
                        Assert.True(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    case MagickFormat.Yuv:
                        Assert.False(formatInfo.SupportsMultipleFrames);
                        Assert.True(formatInfo.SupportsReading);
                        Assert.True(formatInfo.CanReadMultithreaded);
                        Assert.True(formatInfo.SupportsWriting);
                        Assert.True(formatInfo.CanWriteMultithreaded);
                        break;
                    default:
                        throw new NotImplementedException(formatInfo.ToString());
                }
            }
        }
    }
}
