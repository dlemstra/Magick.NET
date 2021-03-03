// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
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
                foreach (object obj in objects)
                {
                    if (obj is byte)
                        bytes.Add((byte)obj);
                    else if (obj is char)
                        bytes.Add((byte)(char)obj);
                    else if (obj is short)
                        bytes.AddRange(BitConverter.GetBytes((short)obj).Reverse());
                    else if (obj is int)
                        bytes.AddRange(BitConverter.GetBytes((int)obj).Reverse());
                }

                return bytes.ToArray();
            }
        }
    }
}
