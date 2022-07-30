// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ImageMagick
{
    internal static partial class FileHelper
    {
        public static string CheckForBaseDirectory(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return fileName;

            if (fileName.Length < 2 || fileName[0] != '~')
                return fileName;

            return AppDomain.CurrentDomain.BaseDirectory + fileName.Substring(1);
        }

        public static string GetFullPath(string path)
        {
            Throw.IfNullOrEmpty(nameof(path), path);

            path = CheckForBaseDirectory(path);
            path = Path.GetFullPath(path);
            Throw.IfFalse(nameof(path), Directory.Exists(path), $"Unable to find directory: {path}");
            return path;
        }

        public static async Task<byte[]> ReadAllBytesAsync(string fileName, CancellationToken cancellationToken)
        {
#if NETSTANDARD2_1
            return await File.ReadAllBytesAsync(fileName, cancellationToken).ConfigureAwait(false);
#else
            using (var fileStream = File.Open(fileName, FileMode.Open))
            {
                var bytes = new byte[fileStream.Length];
                await fileStream.ReadAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
                return bytes;
            }
#endif
        }
    }
}
