// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;

namespace Magick.NET.Tests
{
    internal static class FileHelper
    {
        public static byte[] ReadAllBytes(string fileName)
        {
            using (var input = OpenRead(fileName))
            {
                var bytes = new byte[input.Length];
                input.Read(bytes, 0, bytes.Length);
                return bytes;
            }
        }

        public static void Copy(string sourceFileName, string destFileName)
        {
            using (var input = OpenRead(sourceFileName))
            {
                using (var output = File.Open(destFileName, FileMode.Create, FileAccess.ReadWrite))
                {
                    input.CopyTo(output);
                }
            }
        }

        public static FileStream OpenRead(string fileName)
            => File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
    }
}
