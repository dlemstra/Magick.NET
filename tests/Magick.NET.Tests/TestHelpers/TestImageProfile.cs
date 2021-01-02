// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using ImageMagick;

namespace Magick.NET
{
    public sealed class TestImageProfile : IImageProfile
    {
        private readonly byte[] _bytes;

        public TestImageProfile(string name, byte[] bytes)
        {
            Name = name;
            _bytes = bytes;
        }

        public string Name { get; }

        public bool Equals(IImageProfile other) => false;

        public byte[] GetData() => _bytes;

        public byte[] ToByteArray() => _bytes;
    }
}
