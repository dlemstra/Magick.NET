// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    internal enum GeometryFlags
    {
        NoValue = 0,
        PercentValue = 0x1000,      /* '%'  percentage of something */
        IgnoreAspectRatio = 0x2000, /* '!'  resize no-aspect - special use flag */
        Less = 0x4000,              /* '<'  resize smaller - special use flag */
        Greater = 0x8000,           /* '>'  resize larger - spacial use flag */
        FillArea = 0x10000,         /* '^'  special handling needed */
        LimitPixels = 0x20000,      /* '@'  resize to area - special use flag */
        AspectRatio = 0x100000,     /* '~'  special handling needed  */
    }
}