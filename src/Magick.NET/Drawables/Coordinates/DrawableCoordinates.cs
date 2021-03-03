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

            Coordinates = CheckCoordinates(new List<TCoordinateType>(coordinates), minCount);
        }

        protected List<TCoordinateType> Coordinates { get; }

        public IList<TCoordinateType> ToList()
            => Coordinates;

        private List<TCoordinateType> CheckCoordinates(List<TCoordinateType> coordinates, int minCount)
        {
            if (coordinates.Count == 0)
                throw new ArgumentException("Value cannot be empty", nameof(coordinates));

            foreach (TCoordinateType coordinate in coordinates)
            {
                if (coordinate == null)
                    throw new ArgumentNullException(nameof(coordinates), "Value should not contain null values");
            }

            if (coordinates.Count < minCount)
                throw new ArgumentException("Value should contain at least " + minCount + " coordinates.", nameof(coordinates));

            return coordinates;
        }
    }
}