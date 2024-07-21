// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick;

internal class PathArcCoordinates : DrawableCoordinates<PathArc>
{
    public PathArcCoordinates(IEnumerable<PathArc> coordinates)
      : base(coordinates, 0)
    {
    }
}
