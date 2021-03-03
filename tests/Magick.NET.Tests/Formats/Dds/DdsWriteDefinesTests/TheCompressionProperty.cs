// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class DdsWriteDefinesTests
    {
        public class TheCompressionProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage())
                {
                    var defines = new DdsWriteDefines
                    {
                        Compression = DdsCompression.Dxt1,
                    };

                    image.Settings.SetDefines(defines);

                    Assert.Equal("Dxt1", image.Settings.GetDefine(MagickFormat.Dds, "compression"));
                }
            }

            [Fact]
            public void ShouldUseNoCompressionWhenSetToNone()
            {
                using (var input = new MagickImage(Files.Builtin.Logo))
                {
                    input.Settings.SetDefines(new DdsWriteDefines
                    {
                        Compression = DdsCompression.None,
                    });

                    using (var output = WriteDds(input))
                    {
                        Assert.Equal(CompressionMethod.NoCompression, output.Compression);
                    }
                }
            }

            [Fact]
            public void ShouldUseDxt1CompressionWhenSetToDxt1()
            {
                using (var input = new MagickImage(Files.Builtin.Logo))
                {
                    input.Settings.SetDefines(new DdsWriteDefines
                    {
                        Compression = DdsCompression.Dxt1,
                    });

                    using (var output = WriteDds(input))
                    {
                        Assert.Equal(CompressionMethod.DXT1, output.Compression);
                    }
                }
            }

            [Fact]
            public void ShouldUseDxt1CompressionWhenSetToDxt1AndImageHasAlphaChannel()
            {
                using (var input = new MagickImage(Files.Builtin.Logo))
                {
                    input.Alpha(AlphaOption.Set);

                    input.Settings.SetDefines(new DdsWriteDefines
                    {
                        Compression = DdsCompression.Dxt1,
                    });

                    using (var output = WriteDds(input))
                    {
                        Assert.Equal(CompressionMethod.DXT1, output.Compression);
                    }
                }
            }

            private static MagickImage WriteDds(MagickImage input)
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    input.Format = MagickFormat.Dds;
                    input.Write(memStream);
                    memStream.Position = 0;

                    return new MagickImage(memStream);
                }
            }
        }
    }
}
