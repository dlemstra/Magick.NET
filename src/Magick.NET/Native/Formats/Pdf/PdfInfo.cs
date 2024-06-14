// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.SourceGenerator;

namespace ImageMagick.Formats;

/// <content />
public partial class PdfInfo
{
    [NativeInterop]
    private partial class NativePdfInfo
    {
        [Throws]
        public static partial int PageCount(string fileName, string password);
    }
}
