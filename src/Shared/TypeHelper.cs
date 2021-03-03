// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;

namespace ImageMagick
{
    internal static class TypeHelper
    {
        public static T GetCustomAttribute<T>(Type type)
            where T : Attribute
        {
            return (T)type.Assembly.GetCustomAttributes(typeof(T), false)[0];
        }

        public static T[]? GetCustomAttributes<T>(Enum value)
            where T : Attribute
        {
            var field = value.GetType().GetField(value.ToString());
            if (field == null)
                return null;

            return (T[])field.GetCustomAttributes(typeof(T), false);
        }

        public static Type[] GetGenericArguments(Type type)
            => type.GetGenericArguments();

        public static Stream GetManifestResourceStream(Type type, string resourcePath, string resourceName)
            => type.Assembly.GetManifestResourceStream(resourcePath + "." + resourceName);

        public static bool IsEnum(Type type)
            => type.IsEnum;

        public static bool IsGeneric(Type type)
            => type.IsGenericType;

        public static bool IsNullable(Type type)
            => type.GetGenericTypeDefinition() == typeof(Nullable<>);

        public static bool IsValueType(Type type)
            => type.IsValueType;
    }
}
