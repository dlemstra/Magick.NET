// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;

namespace Magick.NET.Tests
{
    public class TemporaryFile : IDisposable
    {
        public TemporaryFile(byte[] data)
        {
            FileInfo = new FileInfo(Path.GetTempFileName());
            File.WriteAllBytes(FileInfo.FullName, data);
        }

        public TemporaryFile(string fileName)
        {
            if (File.Exists(fileName))
                CreateFromFile(fileName);
            else
                CreateEmptyFile(fileName);
        }

        public FileInfo FileInfo { get; private set; }

        public string FullName
            => FileInfo.FullName;

        public long Length
        {
            get
            {
                FileInfo.Refresh();
                return FileInfo.Length;
            }
        }

        public void Dispose()
            => Cleanup.DeleteFile(FileInfo);

        private void CreateEmptyFile(string fileName)
        {
            FileInfo = new FileInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + fileName));
            using (var fileStream = FileInfo.OpenWrite())
            {
            }
        }

        private void CreateFromFile(string fileName)
        {
            FileInfo = new FileInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + Path.GetFileName(fileName)));
            FileHelper.Copy(fileName, FileInfo.FullName);
            FileInfo.Refresh();
        }
    }
}
