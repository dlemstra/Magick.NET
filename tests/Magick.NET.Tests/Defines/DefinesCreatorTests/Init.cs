// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using ImageMagick;
using ImageMagick.Defines;

namespace Magick.NET.Tests
{
    public partial class DefinesCreatorTests
    {
        private class TestDefine : DefinesCreator
        {
            public TestDefine()
              : base(MagickFormat.A)
            {
            }

            public MagickFormat PublicFormat
                => Format;

            public override IEnumerable<IDefine> Defines
                => Enumerable.Empty<IDefine>();

            public MagickDefine PublicCreateDefine(string name, bool value)
                => CreateDefine(name, value);

            public MagickDefine PublicCreateDefine(string name, double value)
                => CreateDefine(name, value);

            public MagickDefine PublicCreateDefine(string name, int value)
                => CreateDefine(name, value);

            public MagickDefine PublicCreateDefine(string name, long value)
                => CreateDefine(name, value);

            public MagickDefine PublicCreateDefine(string name, IMagickGeometry value)
                => CreateDefine(name, value);

            public MagickDefine PublicCreateDefine(string name, string value)
                => CreateDefine(name, value);

            public MagickDefine PublicCreateDefine<TEnum>(string name, TEnum value)
              where TEnum : Enum
                => CreateDefine(name, value);

            public MagickDefine PublicCreateDefine<T>(string name, IEnumerable<T> value)
                => CreateDefine(name, value);
        }
    }
}
