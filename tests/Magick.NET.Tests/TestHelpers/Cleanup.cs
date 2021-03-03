// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;

namespace Magick.NET.Tests
{
    public static class Cleanup
    {
        public static void DeleteDirectory(string path)
        {
            if (path != null && Directory.Exists(path))
                Directory.Delete(path, true);
        }

        public static void DeleteDirectory(DirectoryInfo directory)
            => DeleteDirectory(directory.FullName);

        public static void DeleteFile(string path)
        {
            if (path != null && File.Exists(path))
                File.Delete(path);
        }

        public static void DeleteFile(FileInfo file)
            => DeleteFile(file.FullName);
    }
}
