﻿// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

#if NET20
using System.IO;

namespace ImageMagick
{
    internal static class StreamExtensions
    {
        public static void CopyTo(this Stream self, Stream output)
        {
            byte[] buffer = new byte[81920];
            int len;

            while ((len = self.Read(buffer, 0, buffer.Length)) > 0)
                output.Write(buffer, 0, len);
        }
    }
}
#endif
