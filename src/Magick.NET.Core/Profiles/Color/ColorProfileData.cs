// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    internal sealed class ColorProfileData
    {
        public ColorSpace ColorSpace { get; set; } = ColorSpace.Undefined;

        public string? Copyright { get; set; }

        public string? Description { get; set; }

        public string? Manufacturer { get; set; }

        public string? Model { get; set; }
    }
}
