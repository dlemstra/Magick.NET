// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections;
using ImageMagick;
using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class PathsTests
{
    [Fact]
    public void Test_Draw_Drawables()
    {
        using var image = new MagickImage(MagickColors.Green, 100, 10);
        image.Draw(new Drawables()
          .StrokeColor(MagickColors.Red)
          .StrokeWidth(5)
          .Paths()
          .LineToRel(10, 2)
          .LineToRel(80, 4)
          .Drawables());

        ColorAssert.Equal(MagickColors.Green, image, 9, 5);
        ColorAssert.Equal(MagickColors.Red, image, 55, 5);
        ColorAssert.Equal(MagickColors.Green, image, 90, 2);
        ColorAssert.Equal(MagickColors.Green, image, 90, 9);
    }

    [Fact]
    public void Test_Draw_Paths()
    {
        using var image = new MagickImage(MagickColors.Fuchsia, 100, 3);
        image.Draw(new Paths()
          .LineToAbs(10, 1)
          .LineToAbs(90, 1)
          .Drawables());

        ColorAssert.Equal(MagickColors.Fuchsia, image, 9, 1);

        ColorAssert.Equal(MagickColors.Fuchsia, image, 10, 0);
        ColorAssert.Equal(MagickColors.Black, image, 10, 1);
        ColorAssert.Equal(MagickColors.Fuchsia, image, 10, 2);

        ColorAssert.Equal(MagickColors.Fuchsia, image, 90, 0);
        ColorAssert.Equal(MagickColors.Black, image, 90, 1);
        ColorAssert.Equal(MagickColors.Fuchsia, image, 90, 2);
    }

    [Fact]
    public void Test_Paths()
    {
        var paths = new Paths();
        var enumerator = ((IEnumerable)paths).GetEnumerator();
        Assert.False(enumerator.MoveNext());
    }
}
