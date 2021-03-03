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
    internal static class IConnectedComponentsSettingsExtensions
    {
        public static void SetImageArtifacts(this IConnectedComponentsSettings self, IMagickImage<QuantumType> image)
        {
            if (self.AngleThreshold != null)
                image.SetArtifact("connected-components:angle-threshold", self.AngleThreshold.Value.ToString());

            if (self.AreaThreshold != null)
                image.SetArtifact("connected-components:area-threshold", self.AreaThreshold.Value.ToString());

            if (self.CircularityThreshold != null)
                image.SetArtifact("connected-components:circularity-threshold", self.CircularityThreshold.Value.ToString());

            if (self.DiameterThreshold != null)
                image.SetArtifact("connected-components:diameter-threshold", self.DiameterThreshold.Value.ToString());

            if (self.EccentricityThreshold != null)
                image.SetArtifact("connected-components:eccentricity-threshold", self.EccentricityThreshold.Value.ToString());

            if (self.MajorAxisThreshold != null)
                image.SetArtifact("connected-components:major-axis-threshold", self.MajorAxisThreshold.Value.ToString());

            if (self.MeanColor)
                image.SetArtifact("connected-components:mean-color", self.MeanColor);

            if (self.MinorAxisThreshold != null)
                image.SetArtifact("connected-components:minor-axis-threshold", self.MinorAxisThreshold.Value.ToString());

            if (self.PerimeterThreshold != null)
                image.SetArtifact("connected-components:perimeter-threshold", self.PerimeterThreshold.Value.ToString());
        }

        public static void RemoveImageArtifacts(this IConnectedComponentsSettings self, IMagickImage<QuantumType> image)
        {
            if (self.AngleThreshold != null)
                image.RemoveArtifact("connected-components:angle-threshold");

            if (self.AreaThreshold != null)
                image.RemoveArtifact("connected-components:area-threshold");

            if (self.CircularityThreshold != null)
                image.RemoveArtifact("connected-components:circularity-threshold");

            if (self.DiameterThreshold != null)
                image.RemoveArtifact("connected-components:diameter-threshold");

            if (self.EccentricityThreshold != null)
                image.RemoveArtifact("connected-components:eccentricity-threshold");

            if (self.MajorAxisThreshold != null)
                image.RemoveArtifact("connected-components:major-axis-threshold");

            if (self.MeanColor)
                image.RemoveArtifact("connected-components:mean-color");

            if (self.MinorAxisThreshold != null)
                image.RemoveArtifact("connected-components:minor-axis-threshold");

            if (self.PerimeterThreshold != null)
                image.RemoveArtifact("connected-components:perimeter-threshold");
        }
    }
}
