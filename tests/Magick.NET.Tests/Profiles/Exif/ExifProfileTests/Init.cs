// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ExifProfileTests
{
    private static void AssertValue(IExifValue? value, string expected)
    {
        Assert.NotNull(value);
        Assert.Equal(expected, value.GetValue());
    }
}
