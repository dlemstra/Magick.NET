//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System;
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
  internal sealed class HexColor
  {
    private static IEnumerable<QuantumType> ParseQ16(string value)
    {
      if (value.Length == 13 || value.Length == 17)
      {
#if Q8
        yield return ParseHexQ8(value, 1);
        yield return ParseHexQ8(value, 5);
        yield return ParseHexQ8(value, 9);

        if (value.Length == 17)
          yield return ParseHexQ8(value, 13);
#else
        yield return ParseHex(value, 1, 4);
        yield return ParseHex(value, 5, 4);
        yield return ParseHex(value, 9, 4);

        if (value.Length == 17)
          yield return ParseHex(value, 13, 4);
#endif
      }
      else
        throw new ArgumentException("Invalid hex value.", nameof(value));
    }

#if Q8
    private static QuantumType ParseHexQ8(string color, int offset)
    {
      ushort result = 0;
      ushort k = 1;

      int i = 3;
      while (i >= 0)
      {
        char c = color[offset + i];

        if (c >= '0' && c <= '9')
          result += (ushort)(k * (c - '0'));
        else if (c >= 'a' && c <= 'f')
          result += (ushort)(k * (c - 'a' + '\n'));
        else if (c >= 'A' && c <= 'F')
          result += (ushort)(k * (c - 'A' + '\n'));
        else
          throw new ArgumentException("Invalid character: " + c + ".", nameof(color));

        i--;
        k = (ushort)(k * 16);
      }

      return Quantum.Convert(result);
    }
#endif

    private static IEnumerable<QuantumType> ParseQ8(string value)
    {
      byte red;
      byte green;
      byte blue;
      byte alpha;

      if (value.Length == 3)
      {
        yield return Quantum.Convert((byte)ParseHex(value, 1, 2));
      }
      else if (value.Length == 4 || value.Length == 5)
      {
        red = (byte)ParseHex(value, 1, 1);
        red += (byte)(red * 16);
        yield return Quantum.Convert(red);

        green = (byte)ParseHex(value, 2, 1);
        green += (byte)(green * 16);
        yield return Quantum.Convert(green);

        blue = (byte)ParseHex(value, 3, 1);
        blue += (byte)(blue * 16);
        yield return Quantum.Convert(blue);

        if (value.Length == 5)
        {
          alpha = (byte)ParseHex(value, 4, 1);
          alpha += (byte)(alpha * 16);
          yield return Quantum.Convert(alpha);
        }
      }
      else if (value.Length == 7 || value.Length == 9)
      {
        red = (byte)ParseHex(value, 1, 2);
        yield return Quantum.Convert(red);

        green = (byte)ParseHex(value, 3, 2);
        yield return Quantum.Convert(green);

        blue = (byte)ParseHex(value, 5, 2);
        yield return Quantum.Convert(blue);

        if (value.Length == 9)
        {
          alpha = (byte)ParseHex(value, 7, 2);
          yield return Quantum.Convert(alpha);
        }
      }
      else
        throw new ArgumentException("Invalid hex value.", nameof(value));
    }

    private static QuantumType ParseHex(string value, int offset, int length)
    {
      QuantumType result = 0;
      QuantumType k = 1;

      int i = length - 1;
      while (i >= 0)
      {
        char c = value[offset + i];

        if (c >= '0' && c <= '9')
          result += (QuantumType)(k * (c - '0'));
        else if (c >= 'a' && c <= 'f')
          result += (QuantumType)(k * (c - 'a' + '\n'));
        else if (c >= 'A' && c <= 'F')
          result += (QuantumType)(k * (c - 'A' + '\n'));
        else
          throw new ArgumentException("Invalid character: " + c + ".", nameof(value));

        i--;
        k = (QuantumType)(k * 16);
      }

      return result;
    }

    public static List<QuantumType> Parse(string value)
    {
      if (value.Length < 13)
        return new List<QuantumType>(ParseQ8(value));

      return new List<QuantumType>(ParseQ16(value));
    }
  }
}
