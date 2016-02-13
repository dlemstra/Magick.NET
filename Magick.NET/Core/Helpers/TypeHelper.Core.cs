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
using System.IO;
using System.Reflection;

namespace ImageMagick
{
  internal static class TypeHelper
  {
    public static T GetCustomAttribute<T>(Type type)
      where T : Attribute
    {
      return (T)type.GetTypeInfo().Assembly.GetCustomAttribute<T>();
    }

    public static Stream GetManifestResourceStream(Type type, string path, string resourceName)
    {
      Assembly assembly = type.GetTypeInfo().Assembly;
      string newResourceName = assembly.GetName().Name + "." + resourceName;
      return assembly.GetManifestResourceStream(newResourceName);
    }

    public static bool IsEnum(Type type)
    {
      return type.GetTypeInfo().IsEnum;
    }

    public static Type[] GetGenericArguments(Type type)
    {
      return type.GetGenericArguments();
    }

    public static bool IsGeneric(Type type)
    {
      return type.GetTypeInfo().IsGenericType;
    }

    public static bool IsNullable(Type type)
    {
      return type.GetTypeInfo().GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    public static bool IsValueType(Type type)
    {
      return type.GetTypeInfo().IsValueType;
    }
  }
}
