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
using System.Globalization;
using System.IO;

namespace ImageMagick
{
  internal static class Throw
  {
    private static string FormatMessage(string message, params object[] args)
    {
      if (args.Length == 0)
        return message;

      return string.Format(CultureInfo.InvariantCulture, message, args);
    }

    public static void IfFalse(string paramName, bool condition, string message, params object[] args)
    {
      if (!condition)
        throw new ArgumentException(FormatMessage(message, args), paramName);
    }

    public static void IfInvalidFileName(string fileName)
    {
      IfNullOrEmpty("fileName", fileName);

      if (fileName.Length > 248)
        return;

      string path = fileName;

      int colonIndex = fileName.IndexOf(':');
      if (colonIndex != -1)
      {
        if (colonIndex + 1 == fileName.Length)
          return;

        if (!fileName.Contains("\\"))
          return;

        if (fileName[colonIndex + 1] != '/' && fileName[colonIndex + 1] != '\\')
          path = path.Substring(colonIndex + 1);

        if (path.Length > 1 && path[0] == '@')
          path = path.Substring(1);
      }

      path = Path.GetFullPath(path);
      if (path.EndsWith("]", StringComparison.OrdinalIgnoreCase))
      {
        int endIndex = path.IndexOf("[", StringComparison.OrdinalIgnoreCase);
        if (endIndex != -1)
          path = path.Substring(0, endIndex);
      }

      IfFalse("fileName", File.Exists(path), "Unable to find file: {0}.", path);
    }

    public static void IfNull(string paramName, object value)
    {
      if (ReferenceEquals(value, null))
        throw new ArgumentNullException(paramName);
    }

    public static void IfNull(string paramName, object value, string message, params object[] args)
    {
      if (value == null)
        throw new ArgumentNullException(paramName, FormatMessage(message, args));
    }

    public static void IfNullOrEmpty(string paramName, [ValidatedNotNull] string value)
    {
      IfNull(paramName, value);

      if (value.Length == 0)
        throw new ArgumentException("Value cannot be empty.", paramName);
    }

    public static void IfNullOrEmpty(string paramName, string value, string message, params object[] args)
    {
      IfNull(paramName, value, message, args);

      if (value.Length == 0)
        throw new ArgumentException(FormatMessage(message, args), paramName);
    }

    public static void IfNullOrEmpty(string paramName, [ValidatedNotNull] Array value)
    {
      IfNull(paramName, value);

      if (value.Length == 0)
        throw new ArgumentException("Value cannot be empty.", paramName);
    }

    public static void IfNegative(string paramName, Percentage value)
    {
      if ((double)value < 0.0)
        throw new ArgumentException("Value should be greater then zero.", paramName);
    }

    public static void IfOutOfRange(string paramName, int index, int length)
    {
      if (index < 0 || index >= length)
        throw new ArgumentOutOfRangeException(paramName);
    }

    public static void IfOutOfRange(string paramName, int min, int max, int value, string message, params object[] args)
    {
      if (value < min || value > max)
        throw new ArgumentOutOfRangeException(FormatMessage(message, args), paramName);
    }

    public static void IfTrue(string paramName, bool condition, string message, params object[] args)
    {
      if (condition)
        throw new ArgumentException(FormatMessage(message, args), paramName);
    }
  }
}