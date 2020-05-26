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

namespace ImageMagick
{
    /// <summary>
    /// Extension methods for the <see cref="IMagickImage"/> interface.
    /// </summary>
    public static class IMagickImageExtensions
    {
        /// <summary>
        /// Returns the default density for this image in the specified <see cref="DensityUnit"/>.
        /// </summary>
        /// <param name="self">The image.</param>
        /// <param name="units">The units.</param>
        /// <returns>A <see cref="Density"/> instance.</returns>
        public static Density GetDefaultDensity(this IMagickImage self, DensityUnit units)
        {
            Throw.IfNull(nameof(self), self);

            if (units == DensityUnit.Undefined || (self.Density.Units == DensityUnit.Undefined && self.Density.X == 0 && self.Density.Y == 0))
                return new Density(96);

            return self.Density.ChangeUnits(units);
        }
    }
}
