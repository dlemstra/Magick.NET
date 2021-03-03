// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick
{
    internal sealed class ExifData
    {
        public uint ThumbnailLength { get; set; }

        public uint ThumbnailOffset { get; set; }

        public List<ExifTag> InvalidTags { get; } = new List<ExifTag>();

        public List<IExifValue> Values { get; } = new List<IExifValue>();
    }
}