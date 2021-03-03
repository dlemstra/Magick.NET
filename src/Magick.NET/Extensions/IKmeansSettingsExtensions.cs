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
    internal static class IKmeansSettingsExtensions
    {
        public static void SetImageArtifacts(this IKmeansSettings self, IMagickImage<QuantumType> image)
        {
            if (self.SeedColors != null && self.SeedColors.Length > 0)
                image.SetArtifact("kmeans:seed-colors", self.SeedColors);
        }

        public static void RemoveImageArtifacts(this IKmeansSettings self, IMagickImage<QuantumType> image)
        {
            if (!string.IsNullOrEmpty(self.SeedColors))
                image.RemoveArtifact("kmeans:seed-colors");
        }
    }
}
