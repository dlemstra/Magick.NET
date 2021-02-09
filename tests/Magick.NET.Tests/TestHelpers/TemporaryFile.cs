// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

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
