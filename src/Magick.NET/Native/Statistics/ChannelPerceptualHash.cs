// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.SourceGenerator;

namespace ImageMagick;

/// <content />
public partial class ChannelPerceptualHash
{
    [NativeInterop]
    private partial class NativeChannelPerceptualHash : ConstNativeInstance
    {
        public partial double GetHuPhash(nuint colorSpaceIndex, nuint index);
    }
}
