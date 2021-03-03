// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickExceptionTests
    {
        public class TheRelatedExceptionsProperty
        {
            [Fact]
            public void ShouldBeSetWhenReadingImageRaisedRelatedExceptions()
            {
                using (var image = new MagickImage())
                {
                    var exception = Assert.Throws<MagickCoderErrorException>(() =>
                    {
                        image.Read(Files.Coders.IgnoreTagTIF);
                    });

                    var relatedExceptions = exception.RelatedExceptions.ToArray();
                    Assert.Single(relatedExceptions);

                    var warning = relatedExceptions[0] as MagickCoderWarningException;
                    Assert.NotNull(warning);
                    Assert.Empty(warning.RelatedExceptions);
                }
            }
        }
    }
}
