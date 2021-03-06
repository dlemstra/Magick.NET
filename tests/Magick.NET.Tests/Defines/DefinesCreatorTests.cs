// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using ImageMagick;
using ImageMagick.Defines;
using Xunit;

namespace Magick.NET.Tests
{
    public class DefinesCreatorTests
    {
        [Fact]
        public void Test_MagickGeometry()
        {
            using (var image = new MagickImage())
            {
                image.Settings.SetDefines(new TestDefine());

                var value = image.Settings.GetDefine(MagickFormat.A, "geo");

                Assert.Equal("3x4+1+2", value);
            }
        }

        private class TestDefine : DefinesCreator
        {
            public TestDefine()
              : base(MagickFormat.A)
            {
            }

            public override IEnumerable<IDefine> Defines
            {
                get
                {
                    yield return CreateDefine("geo", new MagickGeometry(1, 2, 3, 4));
                }
            }
        }
    }
}
