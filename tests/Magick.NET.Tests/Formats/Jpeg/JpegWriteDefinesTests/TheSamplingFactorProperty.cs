// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class JpegWriteDefinesTests
    {
        public class TheSamplingFactorProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                AssertSetDefine("4x2,1x1,1x1", JpegSamplingFactor.Ratio410);
                AssertSetDefine("4x1,1x1,1x1", JpegSamplingFactor.Ratio411);
                AssertSetDefine("2x2,1x1,1x1", JpegSamplingFactor.Ratio420);
                AssertSetDefine("2x1,1x1,1x1", JpegSamplingFactor.Ratio422);
                AssertSetDefine("1x2,1x1,1x1", JpegSamplingFactor.Ratio440);
                AssertSetDefine("1x1,1x1,1x1", JpegSamplingFactor.Ratio444);
            }

            [Fact]
            public void ShouldWriteJpegWithTheCorrectSamplingFactor()
            {
                var defines = new JpegWriteDefines
                {
                    SamplingFactor = JpegSamplingFactor.Ratio420,
                };

                using (var input = new MagickImage(Files.Builtin.Logo))
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        input.Write(memoryStream, defines);

                        memoryStream.Position = 0;
                        using (var output = new MagickImage(memoryStream))
                        {
                            output.Read(memoryStream);

                            Assert.Equal("2x2,1x1,1x1", output.GetAttribute("jpeg:sampling-factor"));
                        }
                    }
                }
            }

            private static void AssertSetDefine(string expected, JpegSamplingFactor samplingFactor)
            {
                var defines = new JpegWriteDefines
                {
                    SamplingFactor = samplingFactor,
                };

                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(defines);

                    Assert.Equal(expected, image.Settings.GetDefine(MagickFormat.Jpeg, "sampling-factor"));
                }
            }
        }
    }
}
