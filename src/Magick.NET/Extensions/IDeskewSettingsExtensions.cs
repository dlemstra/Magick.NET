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
    internal static class IDeskewSettingsExtensions
    {
        public static void SetImageArtifacts(this IDeskewSettings self, IMagickImage<QuantumType> image)
        {
            if (self.AutoCrop)
                image.SetArtifact("deskew:auto-crop", self.AutoCrop);
        }

        public static void RemoveImageArtifacts(this IDeskewSettings self, IMagickImage<QuantumType> image)
        {
            if (self.AutoCrop)
                image.RemoveArtifact("deskew:auto-crop");
        }
    }
}
