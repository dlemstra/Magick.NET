// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;

namespace Magick.NET.Tests
{
    public class TemporaryFile : IDisposable
    {
        private FileInfo _tempFile;

        public TemporaryFile(byte[] data)
        {
            _tempFile = new FileInfo(Path.GetTempFileName());
            File.WriteAllBytes(_tempFile.FullName, data);
        }

        public TemporaryFile(string fileName)
        {
            if (File.Exists(fileName))
                CreateFromFile(fileName);
            else
                CreateEmptyFile(fileName);
        }

        public string FullName
            => _tempFile.FullName;

        public long Length
            => _tempFile.Length;

        public static implicit operator FileInfo(TemporaryFile file)
            => file._tempFile;

        public void Dispose()
            => Cleanup.DeleteFile(_tempFile);

        public FileStream OpenWrite()
            => _tempFile.OpenWrite();

        public void Refresh()
            => _tempFile.Refresh();

        private void CreateEmptyFile(string fileName)
        {
            _tempFile = new FileInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + fileName));
            using (var fileStream = _tempFile.OpenWrite())
            {
            }
        }

        private void CreateFromFile(string fileName)
        {
            _tempFile = new FileInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + Path.GetFileName(fileName)));
            FileHelper.Copy(fileName, _tempFile.FullName);
            _tempFile.Refresh();
        }
    }
}
