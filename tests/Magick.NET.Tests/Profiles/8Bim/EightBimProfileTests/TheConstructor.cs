// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class EightBimProfileTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldAllowInvalidValues()
        {
            var bytes = ToBytes('8', 'B', 'I', 'M', (short)42, (byte)0, 1);

            var profile = new EightBimProfile(bytes);
            Assert.Empty(profile.Values);

            bytes = ToBytes('8', 'B', 'I', 'M', (short)42, (short)0, -1);

            profile = new EightBimProfile(bytes);
            Assert.Empty(profile.Values);
        }

        [Fact]
        public void ShouldReadProfileValues()
        {
            var bytes = ToBytes('8', 'B', 'I', 'M', (short)2000, (short)0, 1, (byte)0);

            var profile = new EightBimProfile(bytes);
            Assert.Single(profile.Values);
            Assert.Empty(profile.ClipPaths);
        }

        private static byte[] ToBytes(params object[] objects)
        {
            var bytes = new List<byte>();
            foreach (var obj in objects)
            {
                if (obj is byte byteValue)
                    bytes.Add(byteValue);
                else if (obj is char charValue)
                    bytes.Add((byte)charValue);
                else if (obj is short shortValue)
                    bytes.AddRange(Enumerable.Reverse(BitConverter.GetBytes(shortValue)));
                else if (obj is int intValue)
                    bytes.AddRange(Enumerable.Reverse(BitConverter.GetBytes(intValue)));
            }

            return bytes.ToArray();
        }
    }
}
