//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System;

namespace ImageMagick
{
    /// <summary>
    /// Result for a sub image search operation.
    /// </summary>
    public sealed class MagickSearchResult : IDisposable
    {
        internal MagickSearchResult(IMagickImage image, MagickGeometry bestMatch, double similarityMetric)
        {
            SimilarityImage = image;
            BestMatch = bestMatch;
            SimilarityMetric = similarityMetric;
        }

        /// <summary>
        /// Gets the offset for the best match.
        /// </summary>
        public MagickGeometry BestMatch
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the a similarity image such that an exact match location is completely white and if none of
        /// the pixels match, black, otherwise some gray level in-between.
        /// </summary>
        public IMagickImage SimilarityImage
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the similarity metric.
        /// </summary>
        public double SimilarityMetric
        {
            get;
            set;
        }

        /// <summary>
        /// Disposes the <see cref="MagickSearchResult"/> instance.
        /// </summary>
        public void Dispose()
        {
            if (SimilarityImage != null)
                SimilarityImage.Dispose();
            SimilarityImage = null;
        }
    }
}