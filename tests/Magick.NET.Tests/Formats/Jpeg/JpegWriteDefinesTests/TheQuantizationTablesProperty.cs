// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class JpegWriteDefinesTests
    {
        public class TheQuantizationTablesProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                var defines = new JpegWriteDefines
                {
                    QuantizationTables = @"C:\path\to\file.xml",
                };

                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(defines);

                    Assert.Equal(@"C:\path\to\file.xml", image.Settings.GetDefine(MagickFormat.Jpeg, "q-table"));
                }
            }
        }
    }
}
