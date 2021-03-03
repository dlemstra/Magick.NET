// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Globalization;

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
    internal static class IComplexSettingsExtensions
    {
        public static void SetImageArtifacts(this IComplexSettings self, IMagickImage<QuantumType> image)
        {
            if (self.SignalToNoiseRatio != null)
                image.SetArtifact("complex:snr", self.SignalToNoiseRatio.Value.ToString(CultureInfo.InvariantCulture));
        }

        public static void RemoveImageArtifacts(this IComplexSettings self, IMagickImage<QuantumType> image)
        {
            if (self.SignalToNoiseRatio != null)
                image.RemoveArtifact("complex:snr");
        }
    }
}
