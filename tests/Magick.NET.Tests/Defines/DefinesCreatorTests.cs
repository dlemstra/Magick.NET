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
        public void Test_Null()
        {
            using (var image = new MagickImage())
            {
                image.Settings.SetDefines(new TestDefine());

                Assert.Null(image.Settings.GetDefine(MagickFormat.A, "null"));
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
                    yield return CreateDefine("null", (MagickGeometry)null);
                }
            }
        }
    }
}
