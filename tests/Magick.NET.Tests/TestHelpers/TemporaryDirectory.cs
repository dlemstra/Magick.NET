// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.IO;

namespace Magick.NET.Tests
{
    public class TemporaryDirectory : IDisposable
    {
        private DirectoryInfo _tempDirectory;

        public TemporaryDirectory()
            : this(string.Empty)
        {
        }

        public TemporaryDirectory(string directoryName)
        {
            _tempDirectory = new DirectoryInfo(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + directoryName));
            _tempDirectory.Create();
        }

        public string FullName
            => _tempDirectory.FullName;

        public void Dispose()
            => Cleanup.DeleteDirectory(_tempDirectory);
    }
}
