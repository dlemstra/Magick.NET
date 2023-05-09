// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheStripMethod
    {
        [Fact]
        public void ShouldRemovePropertiesFromImage()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                image.Comment = "FooBar";

                Assert.NotNull(image.Comment);
                Assert.NotNull(image.GetAttribute("date:create"));
                Assert.NotNull(image.GetAttribute("date:modify"));
                Assert.NotNull(image.GetAttribute("date:timestamp"));

                image.Strip();

                Assert.Null(image.Comment);
                Assert.Null(image.GetAttribute("date:create"));
                Assert.Null(image.GetAttribute("date:modify"));
                Assert.Null(image.GetAttribute("date:timestamp"));
            }
        }

        [Fact]
        public void ShouldRemoveProfilesFromImage()
        {
            using (var image = new MagickImage(Files.PictureJPG))
            {
                Assert.NotNull(image.GetColorProfile());

                image.Strip();

                Assert.Null(image.GetColorProfile());
            }
        }
    }
}
