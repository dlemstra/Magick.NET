// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// The normalized moments of one image channels.
    /// </summary>
    public interface IChannelMoments
    {
        /// <summary>
        /// Gets the centroid.
        /// </summary>
        PointD Centroid { get; }

        /// <summary>
        /// Gets the channel of this moment.
        /// </summary>
        PixelChannel Channel { get; }

        /// <summary>
        /// Gets the ellipse axis.
        /// </summary>
        PointD EllipseAxis { get; }

        /// <summary>
        /// Gets the ellipse angle.
        /// </summary>
        double EllipseAngle { get; }

        /// <summary>
        /// Gets the ellipse eccentricity.
        /// </summary>
        double EllipseEccentricity { get; }

        /// <summary>
        /// Gets the ellipse intensity.
        /// </summary>
        double EllipseIntensity { get; }

        /// <summary>
        /// Returns the Hu invariants.
        /// </summary>
        /// <param name="index">The index to use.</param>
        /// <returns>The Hu invariants.</returns>
        double HuInvariants(int index);
    }
}