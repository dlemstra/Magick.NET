// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System.Collections.Generic;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
    internal static class HexColor
    {
        public static bool TryParse(string value, out List<QuantumType> channels)
        {
            channels = new List<QuantumType>();

            if (value.Length < 13)
                return TryParseQ8(value, channels);

            return TryParseQ16(value, channels);
        }

        private static bool TryParseQ8(string value, List<QuantumType> channels)
        {
            int size = 0;
            if (value.Length == 4 || value.Length == 5)
                size = 1;
            else if (value.Length == 3 || value.Length == 7 || value.Length == 9)
                size = 2;
            else
                return false;

            for (int i = 1; i < value.Length; i += size)
            {
                if (!TryParseHex(value, i, size, out ushort channel))
                    return false;

                channels.Add(Quantum.Convert((byte)channel));
            }

            return true;
        }

        private static bool TryParseQ16(string value, List<QuantumType> channels)
        {
            if (value.Length != 13 && value.Length != 17)
                return false;

            for (int i = 1; i < value.Length; i += 4)
            {
                if (!TryParseHex(value, i, 4, out ushort channel))
                    return false;

                channels.Add(Quantum.Convert(channel));
            }

            return true;
        }

        private static bool TryParseHex(string color, int offset, int length, out ushort channel)
        {
            channel = 0;
            ushort k = 1;

            int i = length - 1;
            while (i >= 0)
            {
                char c = color[offset + i];

                if (c >= '0' && c <= '9')
                    channel += (ushort)(k * (c - '0'));
                else if (c >= 'a' && c <= 'f')
                    channel += (ushort)(k * (c - 'a' + '\n'));
                else if (c >= 'A' && c <= 'F')
                    channel += (ushort)(k * (c - 'A' + '\n'));
                else
                    return false;

                i--;
                k = (ushort)(k * 16);
            }

            if (length == 1)
                channel += (byte)(channel * 16);

            return true;
        }
    }
}
