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

namespace ImageMagick
{
    internal static class ByteConverter
    {
        public static int ToUInt(byte[] data, ref int offset)
        {
            if (offset + 4 > data.Length)
                return 0;

            int value = data[offset++] << 24;
            value |= data[offset++] << 16;
            value |= data[offset++] << 8;
            value |= data[offset++];

            int result = (int)(value & 0xffffffff);
            return result < 0 ? 0 : result;
        }

        public static short ToShort(byte[] data, ref int offset)
        {
            if (offset + 2 > data.Length)
                return 0;

            short result = (short)(data[offset++] << 8);
            result = (short)(result | (short)data[offset++]);
            return (short)(result & 0xffff);
        }
    }
}
