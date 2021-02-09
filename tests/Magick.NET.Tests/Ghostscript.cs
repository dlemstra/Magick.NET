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

using System.IO;
using ImageMagick;

namespace Magick.NET.Tests
{
    public static class Ghostscript
    {
        public static bool IsAvailable
        {
            get
            {
                if (OperatingSystem.IsWindows)
                    return true;

                return File.Exists("/usr/bin/gs");
            }
        }

        public static void Initialize()
        {
            if (OperatingSystem.IsWindows)
                MagickNET.SetGhostscriptDirectory(@"C:\Program Files (x86)\gs\gs9.53.1\bin");
        }
    }
}
