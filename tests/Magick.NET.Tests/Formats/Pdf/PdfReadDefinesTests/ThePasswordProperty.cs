// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;
using Xunit.Sdk;

namespace Magick.NET.Tests;

public partial class PdfReadDefinesTests
{
    public class ThePasswordProperty
    {
        [Fact]
        public void ShouldSetTheDefineWhenValueIsSet()
        {
            using var image = new MagickImage(MagickColors.Magenta, 1, 1);
            image.Settings.SetDefines(new PdfReadDefines
            {
                Password = "test",
            });

            Assert.Equal("test", image.Settings.GetDefine("authenticate"));
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenValueIsNotSet()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PdfReadDefines
            {
                Password = null,
            });

            Assert.Null(image.Settings.GetDefine("authenticate"));
        }

        [Fact]
        public void ShouldUseThePasswordToReadTheImage()
        {
            if (!Ghostscript.IsAvailable)
                return;

            var settings = new MagickReadSettings
            {
                Defines = new PdfReadDefines
                {
                    Password = "test",
                },
            };

            using var image = new MagickImage();
            image.Read(Files.Coders.PdfExamplePasswordOriginalPDF, settings);
        }

        [Fact]
        public void ShouldNotBeAbleToOpenFileWithNullPassword()
        {
            if (!Ghostscript.IsAvailable)
                return;

            var settings = new MagickReadSettings
            {
                Defines = new PdfReadDefines
                {
                    Password = null,
                },
            };

            using var image = new MagickImage();
            try
            {
                image.Read(Files.Coders.PdfExamplePasswordOriginalPDF, settings);
            }
            catch (MagickDelegateErrorException exception)
            {
                ExceptionAssert.Contains("This file requires a password for access.", exception);
                return;
            }

            throw new XunitException("Exception should be thrown.");
        }
    }
}
