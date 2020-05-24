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
    /// Class that contains setting for the kmeans operation.
    /// </summary>
    public interface IKmeansSettings
    {
        /// <summary>
        /// Gets or sets the seed clusters from color list (e.g. red;green;blue).
        /// </summary>
        string SeedColors { get; set; }

        /// <summary>
        /// Gets or sets the number of colors to use as seeds.
        /// </summary>
        int NumberColors { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of iterations while converging.
        /// </summary>
        int MaxIterations { get; set; }

        /// <summary>
        /// Gets or sets the maximum tolerance.
        /// </summary>
        double Tolerance { get; set; }
    }
}
