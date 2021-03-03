// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class IptcProfileTests
    {
        public class TheValuesProperty
        {
            [Fact]
            public void ShouldReturnTheValues()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    var profile = image.GetIptcProfile();
                    Assert.NotNull(profile);

                    Assert.Equal(18, profile.Values.Count());

                    foreach (IptcValue value in profile.Values)
                    {
                        Assert.NotNull(value.Value);
                    }
                }
            }
        }
    }
}
