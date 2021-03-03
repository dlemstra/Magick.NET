// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick
{
    /// <summary>
    /// Encapsulation of the ImageMagick ImageChannelStatistics object.
    /// </summary>
    public interface IChannelStatistics : IEquatable<IChannelStatistics>
    {
        /// <summary>
        /// Gets the channel.
        /// </summary>
        PixelChannel Channel { get; }

        /// <summary>
        /// Gets the depth of the channel.
        /// </summary>
        int Depth { get; }

        /// <summary>
        /// Gets the entropy.
        /// </summary>
        double Entropy { get; }

        /// <summary>
        /// Gets the kurtosis.
        /// </summary>
        double Kurtosis { get; }

        /// <summary>
        /// Gets the maximum value observed.
        /// </summary>
        double Maximum { get; }

        /// <summary>
        /// Gets the average (mean) value observed.
        /// </summary>
        double Mean { get; }

        /// <summary>
        /// Gets the minimum value observed.
        /// </summary>
        double Minimum { get; }

        /// <summary>
        /// Gets the skewness.
        /// </summary>
        double Skewness { get; }

        /// <summary>
        /// Gets the standard deviation, sqrt(variance).
        /// </summary>
        double StandardDeviation { get; }

        /// <summary>
        /// Gets the sum.
        /// </summary>
        double Sum { get; }

        /// <summary>
        /// Gets the sum cubed.
        /// </summary>
        double SumCubed { get; }

        /// <summary>
        /// Gets the sum fourth power.
        /// </summary>
        double SumFourthPower { get; }

        /// <summary>
        /// Gets the sum squared.
        /// </summary>
        double SumSquared { get; }

        /// <summary>
        /// Gets the variance.
        /// </summary>
        double Variance { get; }
    }
}
