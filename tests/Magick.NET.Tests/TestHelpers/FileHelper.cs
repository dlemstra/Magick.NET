// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;

namespace Magick.NET.Tests;

internal static class FileHelper
{
    public static void Copy(string sourceFileName, string destFileName)
    {
        var bytes = File.ReadAllBytes(sourceFileName);

        using var output = File.Open(destFileName, FileMode.Create, FileAccess.Write);
        output.Write(bytes, 0, bytes.Length);
    }

    public static MemoryStream OpenRead(string fileName)
    {
        var bytes = File.ReadAllBytes(fileName);

        return new MemoryStream(bytes);
    }
}
