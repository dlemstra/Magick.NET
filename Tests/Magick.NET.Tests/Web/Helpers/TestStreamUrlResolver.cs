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

#if !NETCOREAPP1_1

using System;
using System.IO;
using ImageMagick;
using ImageMagick.Web;

namespace Magick.NET.Tests
{
    [ExcludeFromCodeCoverage]
    public sealed class TestStreamUrlResolver : IStreamUrlResolver
    {
        private string _fileName;

        public TestStreamUrlResolver()
        {
            _fileName = "foo.jpg";
        }

        public TestStreamUrlResolver(string fileName)
        {
            _fileName = fileName;
        }

        public static bool Result { get; set; } = false;

        public MagickFormat Format => MagickFormatInfo.Create(_fileName).Format;

        public string ImageId => _fileName;

        public DateTime ModifiedTimeUtc => File.GetLastWriteTimeUtc(_fileName);

        public Stream OpenStream() => File.OpenRead(_fileName);

        public bool Resolve(Uri url)
        {
            return Result;
        }
    }
}

#endif