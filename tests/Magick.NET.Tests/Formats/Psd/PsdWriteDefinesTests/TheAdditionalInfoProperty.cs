// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests
{
    public partial class PsdWriteDefinesTests
    {
        public class TheAdditionalInfoProperty
        {
            [Fact]
            public void ShouldSetTheDefine()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.SetDefines(new PsdWriteDefines
                    {
                        AdditionalInfo = PsdAdditionalInfoPart.Selective,
                    });

                    Assert.Equal("Selective", image.Settings.GetDefine(MagickFormat.Psd, "additional-info"));
                }
            }

            [Fact]
            public void ShouldMakeSetWhichAdditionalInfoShouldBeWritten()
            {
                using (var images = new MagickImageCollection())
                {
                    images.Read(Files.Coders.LayerStylesSamplePSD);

                    CheckProfile(images[1], 264);

                    var defines = new PsdWriteDefines
                    {
                        AdditionalInfo = PsdAdditionalInfoPart.All,
                    };
                    WriteAndCheckProfile(images, defines, 264);

                    defines.AdditionalInfo = PsdAdditionalInfoPart.Selective;
                    WriteAndCheckProfile(images, defines, 152);

                    defines.AdditionalInfo = PsdAdditionalInfoPart.None;
                    WriteAndCheckProfile(images, defines, 0);
                }
            }

            private static void CheckProfile(IMagickImage<QuantumType> image, int expectedLength)
            {
                var profile = image.GetProfile("psd:additional-info");
                int actualLength = profile?.ToByteArray().Length ?? 0;
                Assert.Equal(expectedLength, actualLength);
            }

            private static void WriteAndCheckProfile(IMagickImageCollection<QuantumType> images, PsdWriteDefines defines, int expectedLength)
            {
                using (var memStream = new MemoryStream())
                {
                    images.Write(memStream, defines);

                    memStream.Position = 0;
                    images.Read(memStream);
                    CheckProfile(images[1], expectedLength);
                }
            }
        }
    }
}
