// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

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
        internal static void SetImageArtifacts(this IConnectedComponentsSettings self, IMagickImage<QuantumType> image)
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
                image.SetArtifact("connected-components:mean-color", "true");

            if (self.MinorAxisThreshold != null)
                image.SetArtifact("connected-components:minor-axis-threshold", self.MinorAxisThreshold.Value.ToString());

            if (self.PerimeterThreshold != null)
                image.SetArtifact("connected-components:perimeter-threshold", self.PerimeterThreshold.Value.ToString());
        }
    }
}
