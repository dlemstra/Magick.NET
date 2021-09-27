// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class DefinesCreatorTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldSetTheFormat()
            {
                var testDefine = new TestDefine();

                Assert.Equal(MagickFormat.A, testDefine.PublicFormat);
            }
        }
    }
}
