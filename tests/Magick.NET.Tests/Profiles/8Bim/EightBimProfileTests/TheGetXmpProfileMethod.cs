// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class EightBimProfileTests
{
    public class TheGetXmpProfileMethod
    {
        [Fact]
        public void ShouldReturnNullWhenProfileHasNoIptcProfile()
        {
            using var image = new MagickImage(Files.EightBimTIF);

            var profile = image.Get8BimProfile()!;

            Assert.Null(profile.GetXmpProfile());
        }
    }
}
