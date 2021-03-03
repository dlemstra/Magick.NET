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
    internal static class IDistortSettingsExtensions
    {
        public static void SetImageArtifacts(this IDistortSettings self, IMagickImage<QuantumType> image)
        {
            if (self.Scale != null)
                image.SetArtifact("distort:scale", self.Scale.Value.ToString(CultureInfo.InvariantCulture));

            if (self.Viewport != null)
                image.SetArtifact("distort:viewport", self.Viewport.ToString());
        }

        public static void RemoveImageArtifacts(this IDistortSettings self, IMagickImage<QuantumType> image)
        {
            if (self.Scale != null)
                image.RemoveArtifact("distort:scale");

            if (self.Viewport != null)
                image.RemoveArtifact("distort:viewport");
        }
    }
}
