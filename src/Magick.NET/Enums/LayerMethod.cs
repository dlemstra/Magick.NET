// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    internal enum LayerMethod
    {
        Undefined,
        Coalesce,
        CompareAny,
        CompareClear,
        CompareOverlay,
        Dispose,
        Optimize,
        OptimizeImage,
        OptimizePlus,
        OptimizeTrans,
        RemoveDups,
        RemoveZero,
        Composite,
        Merge,
        Flatten,
        Mosaic,
        Trimbounds,
    }
}