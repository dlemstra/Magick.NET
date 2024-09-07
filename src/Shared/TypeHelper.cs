// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;

namespace ImageMagick;

internal static class TypeHelper
{
    public static Stream GetManifestResourceStream(Type type, string resourcePath, string resourceName)
        => type.Assembly.GetManifestResourceStream(resourcePath + "." + resourceName)!;
}
