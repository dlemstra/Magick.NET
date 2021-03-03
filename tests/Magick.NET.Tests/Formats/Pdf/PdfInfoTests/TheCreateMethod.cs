// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Linq;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class PdfInfoTests
    {
        public class TheCreateMethod
        {
            public class WithFile
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    Assert.Throws<ArgumentNullException>("file", () => PdfInfo.Create((FileInfo)null));
                }

                [Fact]
                public void ShouldThrowExceptionWhenPasswordIsNull()
                {
                    using (TemporaryFile file = new TemporaryFile("foo.pdf"))
                    {
                        Assert.Throws<ArgumentNullException>("password", () => PdfInfo.Create(file, null));
                    }
                }
            }

            public class WithFileName
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    Assert.Throws<ArgumentNullException>("fileName", () => PdfInfo.Create((string)null));
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    Assert.Throws<ArgumentException>("fileName", () => PdfInfo.Create(string.Empty));
                }

                [Fact]
                public void ShouldThrowExceptionWhenPasswordIsNull()
                {
                    using (TemporaryFile file = new TemporaryFile("foo.pdf"))
                    {
                        Assert.Throws<ArgumentNullException>("password", () => PdfInfo.Create(file.FullName, null));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileIsPng()
                {
                    if (!Ghostscript.IsAvailable)
                        return;

                    var exception = Assert.Throws<MagickDelegateErrorException>(() => PdfInfo.Create(Files.CirclePNG));

                    Assert.Single(exception.RelatedExceptions);
                    Assert.Contains("Error: /syntaxerror in pdfopen", exception.RelatedExceptions.First().Message);
                }
            }
        }
    }
}
