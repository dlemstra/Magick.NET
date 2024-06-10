// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.SourceGenerator;

namespace ImageMagick;

/// <content />
public partial class MagickFormatInfo
{
    [NativeInterop]
    private unsafe sealed partial class NativeMagickFormatInfo : ConstNativeInstance
    {
        protected override string TypeName
            => nameof(MagickFormatInfo);

        [Throws]
        [Cleanup(Name = nameof(DisposeList), Arguments = "length")]
        public static partial IntPtr CreateList(out int length);

        public static partial void DisposeList(IntPtr instance, int length);

        public static partial bool Unregister(string name);

        public partial string Description_Get();

        public partial bool CanReadMultithreaded_Get();

        public partial bool CanWriteMultithreaded_Get();

        public partial string Format_Get();

        public partial bool SupportsMultipleFrames_Get();

        public partial bool SupportsReading_Get();

        public partial bool SupportsWriting_Get();

        public partial string? MimeType_Get();

        public partial string Module_Get();

        [Throws]
        [Instance(UsesInstance = false)]
        public partial void GetInfo(IntPtr list, int index);

        [Throws]
        [Instance(UsesInstance = false)]
        public partial void GetInfoByName(string name);

        [Throws]
        [Instance(UsesInstance = false)]
        public partial void GetInfoWithBlob(byte[] data, int length);

#if NETSTANDARD2_1
        [Throws]
        [Instance(UsesInstance = false)]
        public partial void GetInfoWithBlob(ReadOnlySpan<byte> data, int length);
#endif
    }
}
