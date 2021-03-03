// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class IptcProfileTests
    {
        public class TheGetAllValuesMethod
        {
            [Fact]
            public void ShouldReturnAllValues()
            {
                var profile = new IptcProfile();
                profile.SetValue(IptcTag.Byline, "test");
                profile.SetValue(IptcTag.Byline, "test2");
                profile.SetValue(IptcTag.Caption, "test");

                var result = profile.GetAllValues(IptcTag.Byline);

                Assert.NotNull(result);
                Assert.Equal(2, result.Count());
            }
        }
    }
}
