// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Extension methods for the <see cref="IMagickImage"/> interface.
    /// </summary>
    public static class IMagickImageExtensions
    {
        /// <summary>
        /// Returns the default density for this image in the specified <see cref="DensityUnit"/>.
        /// </summary>
        /// <param name="self">The image.</param>
        /// <param name="units">The units.</param>
        /// <returns>A <see cref="Density"/> instance.</returns>
        public static Density GetDefaultDensity(this IMagickImage self, DensityUnit units)
        {
            Throw.IfNull(nameof(self), self);

            if (units == DensityUnit.Undefined || (self.Density.Units == DensityUnit.Undefined && self.Density.X == 0 && self.Density.Y == 0))
                return new Density(96);

            return self.Density.ChangeUnits(units);
        }
    }
}
