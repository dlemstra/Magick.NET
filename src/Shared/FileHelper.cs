// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ImageMagick;

internal static partial class FileHelper
{
    public static string CheckForBaseDirectory(string? fileName)
    {
        Throw.IfNullOrEmpty(fileName);
        return PrependWithBaseDirectory(fileName);
    }

    public static string GetFullPath(string? path)
    {
        Throw.IfNullOrEmpty(path);
        path = PrependWithBaseDirectory(path);
        path = Path.GetFullPath(path);
        Throw.IfFalse(Directory.Exists(path), nameof(path), "Unable to find directory: {0}", path);
        return path;
    }

    private static string PrependWithBaseDirectory(string fileName)
    {
        if (fileName.Length < 2 || fileName[0] != '~')
            return fileName;

        return AppDomain.CurrentDomain.BaseDirectory + fileName.Substring(1);
    }
}
