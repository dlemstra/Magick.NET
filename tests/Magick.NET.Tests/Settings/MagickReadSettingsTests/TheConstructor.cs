// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickReadSettingsTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldThrowExceptionWhenDefinesIsNull()
        {
            Assert.Throws<ArgumentNullException>("defines", () => new MagickReadSettings((IReadDefines)null));
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenDefineIsNull()
        {
            var defines = new TestReadDefines
            {
                Defines = new IDefine[] { null },
            };

            new MagickReadSettings(defines);
        }

        private class TestReadDefines : IReadDefines
        {
            public MagickFormat Format { get; set; }

            public IEnumerable<IDefine> Defines { get; set; }
        }
    }
}
