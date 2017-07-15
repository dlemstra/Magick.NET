//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System.IO;

namespace Magick.NET.Tests
{
    [ExcludeFromCodeCoverage]
    public static class Cleanup
    {
        public static void DeleteDirectory(string path)
        {
            if (path != null && Directory.Exists(path))
                Directory.Delete(path, true);
        }

        public static void DeleteFile(string path)
        {
            if (path != null && File.Exists(path))
                File.Delete(path);
        }

        public static void DeleteFile(FileInfo file)
        {
            DeleteFile(file.FullName);
        }
    }
}
