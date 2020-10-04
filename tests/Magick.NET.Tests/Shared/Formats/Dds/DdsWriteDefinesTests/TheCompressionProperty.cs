// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System.IO;
using ImageMagick;
using ImageMagick.Formats.Dds;
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
