// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

namespace ImageMagick
{
    internal abstract class DrawableCoordinates<TCoordinateType>
    {
        protected DrawableCoordinates(IEnumerable<TCoordinateType> coordinates, int minCount)
        {
            Throw.IfNull(nameof(coordinates), coordinates);

            Coordinates = DrawableCoordinates<TCoordinateType>.CheckCoordinates(new List<TCoordinateType>(coordinates), minCount);
        }

        protected List<TCoordinateType> Coordinates { get; }

        public List<TCoordinateType> ToList()
            => Coordinates;

        private static List<TCoordinateType> CheckCoordinates(List<TCoordinateType> coordinates, int minCount)
        {
            if (coordinates.Count == 0)
                throw new ArgumentException("Value cannot be empty", nameof(coordinates));

            foreach (var coordinate in coordinates)
            {
                if (coordinate is null)
                    throw new ArgumentNullException(nameof(coordinates), "Value should not contain null values");
            }

            if (coordinates.Count < minCount)
                throw new ArgumentException("Value should contain at least " + minCount + " coordinates.", nameof(coordinates));

            return coordinates;
        }
    }
}
