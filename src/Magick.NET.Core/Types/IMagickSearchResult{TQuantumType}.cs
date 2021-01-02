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

using System;

namespace ImageMagick
{
    /// <summary>
    /// Result for a sub image search operation.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    public interface IMagickSearchResult<TQuantumType> : IDisposable
        where TQuantumType : struct
    {
        /// <summary>
        /// Gets the offset for the best match.
        /// </summary>
        IMagickGeometry BestMatch { get; }

        /// <summary>
        /// Gets the a similarity image such that an exact match location is completely white and if none of
        /// the pixels match, black, otherwise some gray level in-between.
        /// </summary>
        IMagickImage<TQuantumType> SimilarityImage { get; }

        /// <summary>
        /// Gets the similarity metric.
        /// </summary>
        double SimilarityMetric { get; }
    }
}