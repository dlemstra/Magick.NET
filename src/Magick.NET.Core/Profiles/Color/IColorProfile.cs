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

namespace ImageMagick
{
    /// <summary>
    /// Interface that describes an ICM/ICC color profile.
    /// </summary>
    public interface IColorProfile : IImageProfile
    {
        /// <summary>
        /// Gets the color space of the profile.
        /// </summary>
        ColorSpace ColorSpace { get; }

        /// <summary>
        /// Gets the copyright of the profile.
        /// </summary>
        string Copyright { get; }

        /// <summary>
        /// Gets the description of the profile.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the manufacturer of the profile.
        /// </summary>
        string Manufacturer { get; }

        /// <summary>
        /// Gets the model of the profile.
        /// </summary>
        string Model { get; }
    }
}