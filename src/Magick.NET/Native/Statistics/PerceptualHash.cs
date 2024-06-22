// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

/// <content />
public partial class PerceptualHash
{
    [NativeInterop]
    private partial class NativePerceptualHash
    {
        public static partial void DisposeList(IntPtr list);

        public static partial IntPtr GetInstance(IMagickImage image, IntPtr list, PixelChannel channel);
    }
}
