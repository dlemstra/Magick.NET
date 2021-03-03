// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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