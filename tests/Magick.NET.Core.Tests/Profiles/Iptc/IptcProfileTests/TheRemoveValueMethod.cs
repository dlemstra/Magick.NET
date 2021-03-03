// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using System.Text;
using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class IptcProfileTests
    {
        public class TheRemoveValueMethod
        {
            [Fact]
            public void ShouldRemoveAllValues()
            {
                var profile = new IptcProfile();
                profile.SetValue(IptcTag.Byline, "test");
                profile.SetValue(IptcTag.Byline, "test2");

                var result = profile.RemoveValue(IptcTag.Byline);

                Assert.True(result);
                Assert.Empty(profile.Values);
            }

            [Fact]
            public void ShouldOnlyRemoveTheValueWithTheSpecifiedValue()
            {
                var profile = new IptcProfile();
                profile.SetValue(IptcTag.Byline, "test");
                profile.SetValue(IptcTag.Byline, "test2");

                var result = profile.RemoveValue(IptcTag.Byline, "test2");

                Assert.True(result);
                Assert.Contains(new IptcValue(IptcTag.Byline, Encoding.UTF8.GetBytes("test")), profile.Values);
            }
        }
    }
}
