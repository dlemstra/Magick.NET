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
    /// Class that contains setting for the connected components operation.
    /// </summary>
    public interface IConnectedComponentsSettings
    {
        /// <summary>
        /// Gets or sets the threshold that merges any object not within the min and max angle threshold.
        /// </summary>
        Threshold? AngleThreshold { get; set; }

        /// <summary>
        /// Gets or sets the threshold that eliminate small objects by merging them with their larger neighbors.
        /// </summary>
        Threshold? AreaThreshold { get; set; }

        /// <summary>
        /// Gets or sets the threshold that merges any object not within the min and max circularity threshold.
        /// </summary>
        Threshold? CircularityThreshold { get; set; }

        /// <summary>
        /// Gets or sets how many neighbors to visit, choose from 4 or 8.
        /// </summary>
        int Connectivity { get; set; }

        /// <summary>
        /// Gets or sets the threshold that merges any object not within the min and max diameter threshold.
        /// </summary>
        Threshold? DiameterThreshold { get; set; }

        /// <summary>
        /// Gets or sets the threshold that merges any object not within the min and max eccentricity threshold.
        /// </summary>
        Threshold? EccentricityThreshold { get; set; }

        /// <summary>
        /// Gets or sets the threshold that merges any object not within the min and max ellipse major threshold.
        /// </summary>
        Threshold? MajorAxisThreshold { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the object color in the labeled image will be replaced with the mean-color from the source image.
        /// </summary>
        bool MeanColor { get; set; }

        /// <summary>
        /// Gets or sets the threshold that merges any object not within the min and max ellipse minor threshold.
        /// </summary>
        Threshold? MinorAxisThreshold { get; set; }

        /// <summary>
        /// Gets or sets the threshold that merges any object not within the min and max perimeter threshold.
        /// </summary>
        Threshold? PerimeterThreshold { get; set; }
    }
}
