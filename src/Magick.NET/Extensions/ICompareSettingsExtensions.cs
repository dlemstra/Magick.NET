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
    internal static class ICompareSettingsExtensions
    {
        public static void SetImageArtifacts(this ICompareSettings<QuantumType> self, IMagickImage<QuantumType> image)
        {
            if (self.HighlightColor != null)
                image.SetArtifact("compare:highlight-color", self.HighlightColor.ToString());

            if (self.LowlightColor != null)
                image.SetArtifact("compare:lowlight-color", self.LowlightColor.ToString());

            if (self.MasklightColor != null)
                image.SetArtifact("compare:masklight-color", self.MasklightColor.ToString());
        }

        public static void RemoveImageArtifacts(this ICompareSettings<QuantumType> self, IMagickImage<QuantumType> image)
        {
            if (self.HighlightColor != null)
                image.RemoveArtifact("compare:highlight-color");

            if (self.LowlightColor != null)
                image.RemoveArtifact("compare:lowlight-color");

            if (self.MasklightColor != null)
                image.RemoveArtifact("compare:masklight-color");
        }
    }
}
