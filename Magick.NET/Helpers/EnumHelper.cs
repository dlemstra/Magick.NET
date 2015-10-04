//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.Collections.ObjectModel;
using System.Globalization;

namespace ImageMagick
{
  internal static class EnumHelper
  {
    public static string ConvertFlags<TEnum>(TEnum value)
      where TEnum : struct, IConvertible
    {
      List<string> flags = new List<string>();

      foreach (TEnum enumValue in Enum.GetValues(typeof(TEnum)))
      {
        if (HasFlag(value, enumValue))
          flags.Add(Enum.GetName(typeof(TEnum), enumValue));
      }

      return string.Join(",", flags.ToArray());
    }

    public static bool HasFlag<TEnum>(TEnum value, TEnum flag)
      where TEnum : struct, IConvertible
    {
      return (value.ToUInt32(CultureInfo.InvariantCulture) & flag.ToUInt32(CultureInfo.InvariantCulture)) != 0;
    }

    public static TEnum Parse<TEnum>(int value, TEnum defaultValue)
      where TEnum : struct, IConvertible
    {
      foreach (TEnum enumValue in Enum.GetValues(typeof(TEnum)))
      {
        if (value == enumValue.ToInt32(CultureInfo.InvariantCulture))
          return enumValue;
      }

      return defaultValue;
    }

    public static object Parse(Type enumType, string value)
    {
      foreach (string name in Enum.GetNames(enumType))
      {
        if (name.Equals(value, StringComparison.OrdinalIgnoreCase))
          return Enum.Parse(enumType, name);
      }

      return null;
    }
  }
}