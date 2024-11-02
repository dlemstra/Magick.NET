// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <content />
public partial class MagickImage
{
    private sealed class Mutater : CloneMutator
    {
        public Mutater(NativeMagickImage nativeMagickImage)
            : base(nativeMagickImage)
        {
        }

        protected override void SetResult(IntPtr result)
            => NativeMagickImage.Instance = result;
    }
}
