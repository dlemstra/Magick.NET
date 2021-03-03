// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheArtifactNamesMethod
        {
            [Fact]
            public void ShouldReturnTheArtifactNames()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    image.SetArtifact("foo", "bar");
                    image.SetAttribute("bar", "foo");

                    var names = image.ArtifactNames;
                    Assert.Single(names);
                    Assert.Equal("foo", string.Join(",", (from name in names
                                                              orderby name
                                                              select name).ToArray()));
                }
            }
        }
    }
}
