// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections;
using System.Collections.Generic;

namespace ImageMagick.Drawing;

internal abstract class DrawableCoordinates<TCoordinateType> : IReadOnlyList<TCoordinateType>
{
    private readonly List<TCoordinateType> _coordinates;

    protected DrawableCoordinates(IEnumerable<TCoordinateType> coordinates, int minCount)
    {
        Throw.IfNull(nameof(coordinates), coordinates);

        _coordinates = DrawableCoordinates<TCoordinateType>.CheckCoordinates(new List<TCoordinateType>(coordinates), minCount);
    }

    public int Count
        => _coordinates.Count;

    public TCoordinateType this[int index]
        => _coordinates[index];

    public IEnumerator<TCoordinateType> GetEnumerator()
        => _coordinates.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => _coordinates.GetEnumerator();

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
