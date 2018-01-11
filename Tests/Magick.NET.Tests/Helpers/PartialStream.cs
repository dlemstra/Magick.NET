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

using System.IO;

namespace Magick.NET.Tests
{
    [ExcludeFromCodeCoverage]
    internal sealed class PartialStream : TestStream
    {
        private bool _firstReadDone = false;

        public PartialStream(Stream innerStream, bool canSeek)
          : base(innerStream, canSeek)
        {
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (_firstReadDone)
                return InnerStream.Read(buffer, offset, count);

            _firstReadDone = true;
            return InnerStream.Read(buffer, offset, count / 2);
        }
    }
}
