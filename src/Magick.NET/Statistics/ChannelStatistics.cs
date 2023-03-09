// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick
{
    /// <summary>
    /// Encapsulation of the ImageMagick ImageChannelStatistics object.
    /// </summary>
    public sealed partial class ChannelStatistics : IChannelStatistics
    {
        private ChannelStatistics(PixelChannel channel, IntPtr instance)
        {
            Channel = channel;

            var nativeInstance = new NativeChannelStatistics(instance);
            Depth = nativeInstance.Depth;
            Entropy = nativeInstance.Entropy;
            Kurtosis = nativeInstance.Kurtosis;
            Maximum = nativeInstance.Maximum;
            Mean = nativeInstance.Mean;
            Minimum = nativeInstance.Minimum;
            Skewness = nativeInstance.Skewness;
            StandardDeviation = nativeInstance.StandardDeviation;
        }

        /// <summary>
        /// Gets the channel.
        /// </summary>
        public PixelChannel Channel { get; }

        /// <summary>
        /// Gets the depth of the channel.
        /// </summary>
        public int Depth { get; }

        /// <summary>
        /// Gets the entropy.
        /// </summary>
        public double Entropy { get; }

        /// <summary>
        /// Gets the kurtosis.
        /// </summary>
        public double Kurtosis { get; }

        /// <summary>
        /// Gets the maximum value observed.
        /// </summary>
        public double Maximum { get; }

        /// <summary>
        /// Gets the average (mean) value observed.
        /// </summary>
        public double Mean { get; }

        /// <summary>
        /// Gets the minimum value observed.
        /// </summary>
        public double Minimum { get; }

        /// <summary>
        /// Gets the skewness.
        /// </summary>
        public double Skewness { get; }

        /// <summary>
        /// Gets the standard deviation, sqrt(variance).
        /// </summary>
        public double StandardDeviation { get; }

        internal static ChannelStatistics? Create(PixelChannel channel, IntPtr instance)
        {
            if (instance == IntPtr.Zero)
                return null;

            return new ChannelStatistics(channel, instance);
        }
    }
}
