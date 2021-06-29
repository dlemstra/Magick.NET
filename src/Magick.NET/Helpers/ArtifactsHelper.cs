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
    internal class ArtifactsHelper
    {
        public static void SetImageArtifacts(IMagickImage<QuantumType> image, ICompareSettings<QuantumType> settings)
        {
            if (settings.HighlightColor != null)
                image.SetArtifact("compare:highlight-color", settings.HighlightColor.ToString());

            if (settings.LowlightColor != null)
                image.SetArtifact("compare:lowlight-color", settings.LowlightColor.ToString());

            if (settings.MasklightColor != null)
                image.SetArtifact("compare:masklight-color", settings.MasklightColor.ToString());
        }

        public static void RemoveImageArtifacts(IMagickImage<QuantumType> image, ICompareSettings<QuantumType> settings)
        {
            if (settings.HighlightColor != null)
                image.RemoveArtifact("compare:highlight-color");

            if (settings.LowlightColor != null)
                image.RemoveArtifact("compare:lowlight-color");

            if (settings.MasklightColor != null)
                image.RemoveArtifact("compare:masklight-color");
        }

        public static void SetImageArtifacts(IMagickImage<QuantumType> image, IComplexSettings settings)
        {
            if (settings.SignalToNoiseRatio != null)
                image.SetArtifact("complex:snr", settings.SignalToNoiseRatio.Value.ToString(CultureInfo.InvariantCulture));
        }

        public static void RemoveImageArtifacts(IMagickImage<QuantumType> image, IComplexSettings self)
        {
            if (self.SignalToNoiseRatio != null)
                image.RemoveArtifact("complex:snr");
        }
    }
}
