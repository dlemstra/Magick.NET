// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

/// <content />
public partial class ChannelPerceptualHash
{
    [NativeInterop]
    private partial class NativeChannelPerceptualHash : ConstNativeInstance
    {
        public NativeChannelPerceptualHash(IntPtr instance)
            => Instance = instance;

        public partial double GetHuPhash(uint colorSpaceIndex, uint index);
    }
}
