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

using System;
using System.Globalization;

namespace ImageMagick
{
    internal static class MagickConverter
    {
        public static T Convert<T>(object value)
        {
            if (value == null)
                return default(T);

            Type type = typeof(T);
            Type objectType = value.GetType();

            if (objectType == type)
                return (T)value;

            if (objectType == typeof(string))
                return Convert<T>((string)value);

            if (type == typeof(Percentage))
            {
                if (objectType == typeof(int))
                    return (T)(object)new Percentage((int)value);

                if (objectType == typeof(double))
                    return (T)(object)new Percentage((double)value);
            }

            return (T)System.Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }

        public static T Convert<T>(string value)
        {
            Type type = typeof(T);

            if (type == typeof(string))
                return (T)(object)value;

            if (string.IsNullOrEmpty(value))
                return default(T);

            if (TypeHelper.IsGeneric(type) && TypeHelper.IsNullable(type))
                type = TypeHelper.GetGenericArguments(type)[0];

            if (TypeHelper.IsEnum(type))
                return (T)(object)EnumHelper.Parse(type, value);

            if (type == typeof(bool))
                return (T)(object)(value == "1" || value == "true");

            if (type == typeof(Density))
                return (T)(object)new Density(value);

            if (type == typeof(MagickColor))
                return (T)(object)new MagickColor(value);

            if (type == typeof(MagickGeometry))
                return (T)(object)new MagickGeometry(value);

            if (type == typeof(Percentage))
                return (T)(object)new Percentage((double)System.Convert.ChangeType(value, typeof(double), CultureInfo.InvariantCulture));

            if (type == typeof(PointD))
                return (T)(object)new PointD(value);

            return (T)System.Convert.ChangeType(value, type, CultureInfo.InvariantCulture);
        }
    }
}
