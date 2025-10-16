// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;
using Xunit.Sdk;

namespace Magick.NET.Tests;

public partial class PdfReadDefinesTests
{
    [Collection(nameof(IsolatedUnitTest))]
    public class ThePasswordProperty
    {
        [Fact]
        public void ShouldSetTheDefineWhenValueIsSet()
        {
            IsolatedUnitTest.Execute(static () =>
            {
                using var image = new MagickImage(MagickColors.Magenta, 1, 1);
                image.Settings.SetDefines(new PdfReadDefines
                {
                    Password = "test",
                });

                Assert.Equal("test", image.Settings.GetDefine("authenticate"));
            });
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenValueIsNotSet()
        {
            IsolatedUnitTest.Execute(static () =>
            {
                using var image = new MagickImage();
                image.Settings.SetDefines(new PdfReadDefines
                {
                    Password = null,
                });

                Assert.Null(image.Settings.GetDefine("authenticate"));
            });
        }

        [Fact]
        public void ShouldUseThePasswordToReadTheImage()
        {
            Assert.SkipUnless(Ghostscript.IsAvailable, "Ghostscript is not available");

            IsolatedUnitTest.Execute(static () =>
            {
                var settings = new MagickReadSettings
                {
                    Defines = new PdfReadDefines
                    {
                        Password = "test",
                    },
                };

                using var image = new MagickImage();
                image.Read(Files.Coders.PdfExamplePasswordOriginalPDF, settings);

                Assert.Equal(612U, image.Width);
                Assert.Equal(792U, image.Height);
            });
        }

        [Fact]
        public void ShouldNotBeAbleToOpenFileWithNullPassword()
        {
            Assert.SkipUnless(Ghostscript.IsAvailable, "Ghostscript is not available");

            IsolatedUnitTest.Execute(static () =>
            {
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
#if WINDOWS_BUILD
                    ExceptionAssert.Contains("This file requires a password for access.", exception);
#else
                    ExceptionAssert.Contains("Error: Couldn't initialise file.", exception);
#endif
                    return;
                }

                throw new XunitException("Exception should be thrown.");
            });
        }
    }
}
