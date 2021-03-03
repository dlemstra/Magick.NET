// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;

namespace ImageMagick
{
    internal sealed class TemporaryFile : IDisposable
    {
        private FileInfo _tempFile;

        public TemporaryFile()
        {
            _tempFile = new FileInfo(Path.GetTempFileName());
        }

        public long Length => _tempFile.Length;

        public static implicit operator FileInfo(TemporaryFile file)
        {
            return file._tempFile;
        }

        public void CopyTo(FileInfo file)
        {
            _tempFile.CopyTo(file.FullName, true);
            file.Refresh();
        }

        public void Dispose()
        {
            if (_tempFile.Exists)
                _tempFile.Delete();
        }
    }
}
