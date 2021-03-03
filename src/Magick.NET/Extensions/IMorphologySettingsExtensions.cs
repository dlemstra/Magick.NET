// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
    internal static class IMorphologySettingsExtensions
    {
        public static void SetImageArtifacts(this IMorphologySettings self, IMagickImage<QuantumType> image)
        {
            if (self.ConvolveBias != null)
                image.SetArtifact("convolve:bias", self.ConvolveBias.ToString());

            if (self.ConvolveScale != null)
                image.SetArtifact("convolve:scale", self.ConvolveScale.ToString());
        }

        public static void RemoveImageArtifacts(this IMorphologySettings self, IMagickImage<QuantumType> image)
        {
            if (self.ConvolveBias != null)
                image.RemoveArtifact("convolve:bias");

            if (self.ConvolveScale != null)
                image.RemoveArtifact("convolve:scale");
        }
    }
}
