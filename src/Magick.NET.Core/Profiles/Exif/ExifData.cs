// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.ObjectModel;

namespace ImageMagick;

internal sealed class ExifData
{
    public uint ThumbnailLength { get; set; }

    public uint ThumbnailOffset { get; set; }

    public Collection<ExifTag> InvalidTags { get; } = new Collection<ExifTag>();

    public Collection<IExifValue> Values { get; } = new Collection<IExifValue>();
}
