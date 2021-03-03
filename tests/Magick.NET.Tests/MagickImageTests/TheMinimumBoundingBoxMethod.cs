// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheMinimumBoundingBoxMethod
        {
            [Fact]
            public void ShouldReturnTheMinimumBoundingBox()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    var coordinates = image.MinimumBoundingBox().ToList();
                    Assert.Equal(4, coordinates.Count);

                    Assert.InRange(coordinates[0].X, 550, 551);
                    Assert.InRange(coordinates[0].Y, 469, 470);
                    Assert.InRange(coordinates[1].X, 109, 110);
                    Assert.InRange(coordinates[1].Y, 489, 490);
                    Assert.InRange(coordinates[2].X, 86, 87);
                    Assert.InRange(coordinates[2].Y, 7, 8);
                    Assert.InRange(coordinates[3].X, 527, 528);
                    Assert.InRange(coordinates[3].Y, -14, -13);
                }
            }
        }
    }
}
