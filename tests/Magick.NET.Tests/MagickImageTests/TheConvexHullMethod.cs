// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheConvexHullMethod
        {
            [Fact]
            public void ShouldReturnTheConvexHull()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    var coordinates = image.ConvexHull();
                    Assert.Equal(29, coordinates.Count());

                    var coordinate = coordinates.Skip(10).First();
                    Assert.Equal(537, coordinate.X);
                    Assert.Equal(465, coordinate.Y);
                }
            }
        }
    }
}
