// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheAttributeNamesMethod
    {
        [Fact]
        public void ShouldReturnTheAttributeNames()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            image.SetAttribute("foo", "bar");
            image.SetArtifact("bar", "foo");

            var names = image.AttributeNames;
            var allNames = string.Join(",", names);

            Assert.Equal(7, names.Count());
            Assert.Equal("date:create,date:modify,date:timestamp,foo,jpeg:colorspace,jpeg:sampling-factor,mime:type", allNames);
        }
    }
}
