// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

#if !NETCORE

using System;
using System.IO;
using ImageMagick;
using ImageMagick.Web;

namespace Magick.NET.Tests
{
    [ExcludeFromCodeCoverage]
    public sealed class TestStreamUrlResolver : IStreamUrlResolver
    {
        public TestStreamUrlResolver()
        {
            ImageId = "foo.jpg";
        }

        public TestStreamUrlResolver(string fileName)
        {
            ImageId = fileName;
        }

        public static bool Result { get; set; } = false;

        public MagickFormat Format => MagickFormatInfo.Create(ImageId).Format;

        public string ImageId { get; }

        public DateTime ModifiedTimeUtc => File.GetLastWriteTimeUtc(ImageId);

        public Stream OpenStream() => File.OpenRead(ImageId);

        public bool Resolve(Uri url)
        {
            return Result;
        }
    }
}

#endif