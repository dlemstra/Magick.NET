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
