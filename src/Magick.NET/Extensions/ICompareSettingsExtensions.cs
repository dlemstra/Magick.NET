// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
