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

#if !NETSTANDARD1_3

using System;
using System.IO;
using System.Reflection;

namespace ImageMagick
{
    internal static class TypeHelper
    {
        public static T GetCustomAttribute<T>(Type type)
          where T : Attribute
        {
            return (T)type.Assembly.GetCustomAttributes(typeof(T), false)[0];
        }

        public static T[] GetCustomAttributes<T>(Enum value)
          where T : Attribute
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            if (field == null)
                return null;

            return (T[])field.GetCustomAttributes(typeof(T), false);
        }

        public static Type[] GetGenericArguments(Type type)
        {
            return type.GetGenericArguments();
        }

        public static Stream GetManifestResourceStream(Type type, string resourcePath, string resourceName)
        {
            return type.Assembly.GetManifestResourceStream(resourcePath + "." + resourceName);
        }

        public static bool IsEnum(Type type)
        {
            return type.IsEnum;
        }

        public static bool IsGeneric(Type type)
        {
            return type.IsGenericType;
        }

        public static bool IsNullable(Type type)
        {
            return type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static bool IsValueType(Type type)
        {
            return type.IsValueType;
        }
    }
}

#endif