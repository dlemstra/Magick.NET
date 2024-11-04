// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <content />
public partial class MagickImage
{
    private sealed class Mutator : CloneMutator
    {
        public Mutator(NativeMagickImage nativeMagickImage)
            : base(nativeMagickImage)
        {
        }

        protected override void SetResult(IntPtr result)
            => NativeMagickImage.Instance = result;
    }
}
