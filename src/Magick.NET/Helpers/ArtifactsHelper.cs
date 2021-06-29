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

        public static void SetImageArtifacts(IMagickImage<QuantumType> image, IConnectedComponentsSettings settings)
        {
            if (settings.AngleThreshold != null)
                image.SetArtifact("connected-components:angle-threshold", settings.AngleThreshold.Value.ToString());

            if (settings.AreaThreshold != null)
                image.SetArtifact("connected-components:area-threshold", settings.AreaThreshold.Value.ToString());

            if (settings.CircularityThreshold != null)
                image.SetArtifact("connected-components:circularity-threshold", settings.CircularityThreshold.Value.ToString());

            if (settings.DiameterThreshold != null)
                image.SetArtifact("connected-components:diameter-threshold", settings.DiameterThreshold.Value.ToString());

            if (settings.EccentricityThreshold != null)
                image.SetArtifact("connected-components:eccentricity-threshold", settings.EccentricityThreshold.Value.ToString());

            if (settings.MajorAxisThreshold != null)
                image.SetArtifact("connected-components:major-axis-threshold", settings.MajorAxisThreshold.Value.ToString());

            if (settings.MeanColor)
                image.SetArtifact("connected-components:mean-color", settings.MeanColor);

            if (settings.MinorAxisThreshold != null)
                image.SetArtifact("connected-components:minor-axis-threshold", settings.MinorAxisThreshold.Value.ToString());

            if (settings.PerimeterThreshold != null)
                image.SetArtifact("connected-components:perimeter-threshold", settings.PerimeterThreshold.Value.ToString());
        }

        public static void RemoveImageArtifacts(IMagickImage<QuantumType> image, IConnectedComponentsSettings settings)
        {
            if (settings.AngleThreshold != null)
                image.RemoveArtifact("connected-components:angle-threshold");

            if (settings.AreaThreshold != null)
                image.RemoveArtifact("connected-components:area-threshold");

            if (settings.CircularityThreshold != null)
                image.RemoveArtifact("connected-components:circularity-threshold");

            if (settings.DiameterThreshold != null)
                image.RemoveArtifact("connected-components:diameter-threshold");

            if (settings.EccentricityThreshold != null)
                image.RemoveArtifact("connected-components:eccentricity-threshold");

            if (settings.MajorAxisThreshold != null)
                image.RemoveArtifact("connected-components:major-axis-threshold");

            if (settings.MeanColor)
                image.RemoveArtifact("connected-components:mean-color");

            if (settings.MinorAxisThreshold != null)
                image.RemoveArtifact("connected-components:minor-axis-threshold");

            if (settings.PerimeterThreshold != null)
                image.RemoveArtifact("connected-components:perimeter-threshold");
        }

        public static void SetImageArtifacts(IMagickImage<QuantumType> image, IDeskewSettings settings)
        {
            if (settings.AutoCrop)
                image.SetArtifact("deskew:auto-crop", settings.AutoCrop);
        }

        public static void RemoveImageArtifacts(IMagickImage<QuantumType> image, IDeskewSettings settings)
        {
            if (settings.AutoCrop)
                image.RemoveArtifact("deskew:auto-crop");
        }
    }
}
