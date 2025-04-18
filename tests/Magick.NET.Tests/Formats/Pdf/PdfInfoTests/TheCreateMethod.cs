// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;
using Xunit.Sdk;

namespace Magick.NET.Tests;

public partial class PdfInfoTests
{
    public class TheCreateMethod
    {
        public class WithFileInfo
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileIsNull()
            {
                Assert.Throws<ArgumentNullException>("file", () => PdfInfo.Create((FileInfo)null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenPasswordIsNull()
            {
                using var tempFile = new TemporaryFile("foo.pdf");

                Assert.Throws<ArgumentNullException>("password", () => PdfInfo.Create(tempFile.File, null!));
            }
        }

        public class WithFileName
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                Assert.Throws<ArgumentNullException>("fileName", () => PdfInfo.Create((string)null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                Assert.Throws<ArgumentException>("fileName", () => PdfInfo.Create(string.Empty));
            }

            [Fact]
            public void ShouldThrowExceptionWhenPasswordIsNull()
            {
                using var tempFile = new TemporaryFile("foo.pdf");

                Assert.Throws<ArgumentNullException>("password", () => PdfInfo.Create(tempFile.File.FullName, null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileIsPng()
            {
                Assert.SkipUnless(Ghostscript.IsAvailable, "Ghostscript is not available");

                MagickErrorException? exception = null;

                try
                {
                    PdfInfo.Create(Files.CirclePNG);
                }
                catch (MagickErrorException ex)
                {
                    exception = ex;
                }

                Assert.NotNull(exception);

                if (exception is MagickDelegateErrorException delegateErrorException)
                {
                    Assert.Single(delegateErrorException.RelatedExceptions);
                    ExceptionAssert.Contains("Error: /syntaxerror in pdfopen", delegateErrorException);
                }
                else
                {
                    ExceptionAssert.Contains("Unable to determine the page count.", exception);
                }
            }
        }
    }
}
