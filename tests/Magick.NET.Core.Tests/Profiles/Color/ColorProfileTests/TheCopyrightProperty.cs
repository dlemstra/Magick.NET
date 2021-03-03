// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class ColorProfileTests
    {
        public class TheCopyrightProperty
        {
            [Fact]
            public void ShouldReturnTheCorrectValue()
            {
                Assert.Equal("Copyright 2000 Adobe Systems Incorporated", ColorProfile.AdobeRGB1998.Copyright);
                Assert.Equal("Copyright 2000 Adobe Systems Incorporated", ColorProfile.AppleRGB.Copyright);
                Assert.Equal("Copyright 2007 Adobe Systems, Inc.", ColorProfile.CoatedFOGRA39.Copyright);
                Assert.Equal("Copyright 2000 Adobe Systems Incorporated", ColorProfile.ColorMatchRGB.Copyright);
                Assert.Equal("Copyright (c) 1998 Hewlett-Packard Company", ColorProfile.SRGB.Copyright);
                Assert.Equal("Copyright 2000 Adobe Systems, Inc.", ColorProfile.USWebCoatedSWOP.Copyright);
            }

            [Fact]
            public void ShouldIgnoreIncorrectTagValueType()
            {
                var data = new byte[148];
                Array.Clear(data, 0, data.Length);

                // Colorspace
                data[16] = (byte)'R';
                data[17] = (byte)'G';
                data[18] = (byte)'B';

                // Tag table count
                data[131] = 1;

                // Copyright tag
                data[132] = 99;
                data[133] = 112;
                data[134] = 114;
                data[135] = 116;

                // Offset
                data[139] = 144;

                // Length
                data[143] = 1;

                // Tag value type
                data[144] = (byte)'m';
                data[145] = (byte)'l';
                data[146] = (byte)'u';
                data[147] = (byte)'c';

                var colorProfile = new ColorProfile(data);
                Assert.Null(colorProfile.Copyright);
            }
        }
    }
}
