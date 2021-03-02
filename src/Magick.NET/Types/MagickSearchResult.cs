// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
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
    /// <summary>
    /// Result for a sub image search operation.
    /// </summary>
    public sealed class MagickSearchResult : IMagickSearchResult<QuantumType>
    {
        internal MagickSearchResult(IMagickImage<QuantumType> image, IMagickGeometry bestMatch, double similarityMetric)
        {
            SimilarityImage = image;
            BestMatch = bestMatch;
            SimilarityMetric = similarityMetric;
        }

        /// <summary>
        /// Gets the offset for the best match.
        /// </summary>
        public IMagickGeometry BestMatch { get; }

        /// <summary>
        /// Gets the a similarity image such that an exact match location is completely white and if none of
        /// the pixels match, black, otherwise some gray level in-between.
        /// </summary>
        public IMagickImage<QuantumType> SimilarityImage { get; }

        /// <summary>
        /// Gets the similarity metric.
        /// </summary>
        public double SimilarityMetric { get; }

        /// <summary>
        /// Disposes the <see cref="MagickSearchResult"/> instance.
        /// </summary>
        public void Dispose()
            => SimilarityImage.Dispose();
    }
}