// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheInsertMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenCollectionAlreadyContainsItem()
            {
                using (var images = new MagickImageCollection())
                {
                    var image = new MagickImage();
                    images.Add(image);

                    Assert.Throws<InvalidOperationException>(() =>
                    {
                        images.Insert(0, image);
                    });
                }
            }
        }
    }
}
