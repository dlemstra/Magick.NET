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
using System.IO;
using System.Linq;
using System.Reflection;

namespace FileGenerator.MagickScript
{
  public sealed class MagickScriptTypes : MagickTypes
  {
    private static readonly string[] _UnsupportedMethods = new string[]
    {
      "Add", "AddRange", "Clear", "Dispose", "Draw", "Insert", "Ping", "Read", "RemoveAt", "Write"
    };

    private static bool CanIgnoreResult(MethodInfo method)
    {
      if (method.IsSpecialName)
        return false;

      if (method.Name == "Quantize")
        return true;

      return false;
    }

    private string CheckForQuantum(string name)
    {
      if (Depth == QuantumDepth.Q16HDRI)
        return name == "Single" ? "QuantumType" : name;
      else
        throw new NotImplementedException();
    }

    private IEnumerable<ConstructorInfo[]> GetInterfaceConstructors(string interfaceName)
    {
      return from type in GetInterfaceTypes(interfaceName)
             let constructors = GetConstructors(type.Name).ToArray()
             where constructors.Length > 0
             orderby type.Name
             select constructors;
    }

    private string GetQuantumName(QuantumDepth depth)
    {
      switch (depth)
      {
        case QuantumDepth.Q8:
        case QuantumDepth.Q16:
          return depth.ToString();
        case QuantumDepth.Q16HDRI:
          return "Q16-HDRI";
        default:
          throw new NotImplementedException();
      }
    }

    private IEnumerable<MethodInfo[]> GetGroupedMethods(string name)
    {
      return from type in MagickNET.GetTypes()
             where type.Name == name
             from method in type.GetMethods()
             where IsSupported(method)
             group method by method.Name into g
             orderby g.Key
             select g.OrderBy(m => m.GetParameters().Count()).ToArray();
    }

    private static string GetXsdAttributeType(Type type)
    {
      if (type.IsEnum)
        return type.Name;

      string typeName = type.Name;

      if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
      {
        Type genericType = type.GetGenericArguments().First();
        if (genericType.IsEnum)
          return genericType.Name;

        typeName = genericType.Name;
      }
      else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        typeName = "IEnumerable<" + type.GetGenericArguments().First().Name + ">";

      switch (typeName)
      {
        case "Boolean":
          return "bool";
        case "Byte":
          return "byte";
        case "Density":
          return "density";
        case "Double":
          return "double";
        case "Encoding":
        case "String":
          return "xs:string";
        case "Percentage":
          return "double";
        case "Int32":
          return "int";
        case "MagickColor":
          return "color";
        case "MagickGeometry":
          return "geometry";
        case "PointD":
          return "pointd";
        case "Single":
          return "float";
        case "UInt16":
          return "short";
        case "ColorProfile":
        case "Double[]":
        case "Drawable":
        case "IDefines":
        case "IEnumerable<Double>":
        case "IEnumerable<Drawable>":
        case "IEnumerable<IPath>":
        case "IEnumerable<MagickGeometry>":
        case "IEnumerable<PathArc>":
        case "IEnumerable<PointD>":
        case "IEnumerable<SparseColorArg>":
        case "IEnumerable<String>":
        case "ImageProfile":
        case "IReadDefines":
        case "MagickImage":
        case "MagickSettings":
        case "MontageSettings":
        case "MorphologySettings":
        case "PathArc":
        case "PrimaryInfo":
        case "PixelStorageSettings":
        case "QuantizeSettings":
          return null;
        default:
          throw new NotImplementedException("GetXsdAttributeType: " + typeName);
      }
    }

    private static string GetXsdName(string name)
    {
      if (name.ToUpperInvariant() == name)
        return name.ToLowerInvariant();

      return char.ToLowerInvariant(name[0]) + name.Substring(1);
    }

    private static string GetXsdElementType(Type type)
    {
      string typeName = type.Name;

      if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        typeName = "IEnumerable<" + type.GetGenericArguments().First().Name + ">";

      switch (typeName)
      {
        case "Double[]":
        case "IEnumerable<Double>":
          return "doubleArray";
        case "ColorProfile":
        case "Drawable":
        case "IDefines":
        case "IReadDefines":
        case "MagickSettings":
        case "MontageSettings":
        case "PathArc":
        case "PrimaryInfo":
        case "PixelStorageSettings":
        case "QuantizeSettings":
          return char.ToLowerInvariant(typeName[0]) + typeName.Substring(1);
        case "IEnumerable<Drawable>":
          return "drawables";
        case "IEnumerable<MagickGeometry>":
          return "geometries";
        case "IEnumerable<IPath>":
          return "paths";
        case "IEnumerable<PathArc>":
          return "pathArcs";
        case "IEnumerable<PointD>":
          return "points";
        case "IEnumerable<SparseColorArg>":
          return "sparseColorArgs";
        case "ImageProfile":
          return "profile";
        case "MagickImage":
          return "image";
        default:
          return null;
      }
    }

    private static bool HasSupportedResult(MethodInfo method)
    {
      if (method.ReturnType.Name != "MagickImage")
        return false;

      return IsSupportedMethod(method);
    }

    private static bool IsSupported(ConstructorInfo constructor, ConstructorInfo[] constructors)
    {
      if (!constructor.IsPublic)
        return false;

      ParameterInfo[] parameters = constructor.GetParameters();
      if (parameters.Length == 0)
        return false;

      if (parameters.Any(parameter => !IsSupported(parameter)))
        return false;

      if (parameters.Length == 1)
      {
        ParameterInfo parameter = parameters[0];
        if (!parameter.ParameterType.IsGenericType)
        {
          if ((from c in constructors
               where c != constructor
               from p in c.GetParameters()
               where (p.ParameterType.IsGenericType && p.ParameterType.GenericTypeArguments[0] == parameter.ParameterType)
               select c).Any())
            return false;
        }
      }

      return true;
    }

    private static bool IsSupported(MethodInfo method)
    {
      if (method.ReturnType != typeof(void))
        return CanIgnoreResult(method);

      return IsSupportedMethod(method);
    }

    private static bool IsSupported(ParameterInfo parameter)
    {
      return IsSupported(parameter.ParameterType);
    }

    private static bool IsSupported(PropertyInfo property)
    {
      if (property.SetMethod == null)
        return false;

      return IsSupported(property.PropertyType);
    }

    private static bool IsSupported(Type type)
    {
      Type newType = type;

      if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
        newType = type.GetGenericArguments().First();
      else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>))
        newType = type.GetGenericArguments().First();

      if (newType.IsEnum)
        return true;

      switch (newType.Name)
      {
        case "Byte":
        case "Boolean":
        case "Encoding":
        case "ColorProfile":
        case "Density":
        case "Double":
        case "Double[]":
        case "IDefines":
        case "ImageProfile":
        case "Int32":
        case "IPath":
        case "IReadDefines":
        case "MagickColor":
        case "MagickGeometry":
        case "MagickImage":
        case "MagickReadSettings":
        case "MagickSettings":
        case "MontageSettings":
        case "MorphologySettings":
        case "PathArc":
        case "Percentage":
        case "PixelStorageSettings":
        case "PointD":
        case "PrimaryInfo":
        case "QuantizeSettings":
        case "Single":
        case "SparseColorArg":
        case "String":
        case "UInt16":
          return true;
        case "Byte[]":
        case "Color":
        case "MagickColorMatrix":
        case "ConvolveMatrix":
        case "DrawableAffine":
        case "IEnumerable<MagickImage>":
        case "IPath[]":
        case "MagickImage[]":
        case "MagickImageCollection":
        case "PathArc[]":
        case "PointD[]":
        case "Matrix":
        case "Rectangle":
        case "Stream":
          return false;
        default:
          throw new NotSupportedException("IsSupported: " + newType.Name);
      }
    }

    private static bool IsSupportedMethod(MethodInfo method)
    {
      if (method.IsSpecialName)
        return false;

      if (!method.IsPublic)
        return false;

      if (_UnsupportedMethods.Contains(method.Name))
        return false;

      if (method.GetParameters().Any(parameter => !IsSupported(parameter)))
        return false;

      return true;
    }

    private static Assembly LoadAssembly(string assemblyFile)
    {
      if (!File.Exists(assemblyFile))
        throw new ArgumentException("Unable to find file: " + assemblyFile, "fileName");

      return Assembly.ReflectionOnlyLoad(File.ReadAllBytes(assemblyFile));
    }

    public MagickScriptTypes(QuantumDepth depth)
      : base(depth)
    {
    }

    public IEnumerable<ConstructorInfo> GetConstructors(string typeName)
    {
      return from type in GetTypes()
             where type.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase)
             let constructors = type.GetConstructors()
             from constructor in constructors
             where IsSupported(constructor, constructors)
             orderby constructor.GetParameters().Count()
             select constructor;
    }

    public static string GetFolderName(QuantumDepth depth)
    {
      switch (depth)
      {
        case QuantumDepth.Q8:
          return "ReleaseQ8";
        case QuantumDepth.Q16:
          return "ReleaseQ16";
        case QuantumDepth.Q16HDRI:
          return "ReleaseQ16-HDRI";
        default:
          throw new NotImplementedException();
      }
    }

    public IEnumerable<string> GetColorProfileNames()
    {
      return from name in MagickNET.GetManifestResourceNames()
             where
              name.EndsWith(".icc", StringComparison.OrdinalIgnoreCase) ||
              name.EndsWith(".icm", StringComparison.OrdinalIgnoreCase)
             let tmpName = name.Substring(0, name.Length - 4)
             let resourceName = tmpName.Substring(tmpName.LastIndexOf('.') + 1)
             orderby resourceName
             select resourceName;
    }

    public IEnumerable<ConstructorInfo[]> GetDrawables()
    {
      return GetInterfaceConstructors("IDrawable");
    }

    public IEnumerable<Type> GetEnums()
    {
      return from type in GetTypes()
             where type.IsEnum && type.IsPublic
             orderby type.Name
             select type;
    }

    public IEnumerable<MethodInfo[]> GetGroupedMagickImageCollectionMethods()
    {
      return GetGroupedMethods("MagickImageCollection");
    }

    public IEnumerable<MethodInfo[]> GetGroupedMagickImageCollectionResultMethods()
    {
      return from type in GetTypes()
             where type.Name == "MagickImageCollection"
             from method in type.GetMethods()
             where HasSupportedResult(method)
             group method by method.Name into g
             orderby g.Key
             select g.OrderBy(m => m.GetParameters().Count()).ToArray();
    }

    public IEnumerable<MethodInfo[]> GetGroupedMagickImageMethods()
    {
      return GetGroupedMethods("MagickImage");
    }

    public IEnumerable<MethodInfo[]> GetGroupedMagickSettingsMethods()
    {
      return GetGroupedMethods("MagickSettings");
    }

    public IEnumerable<MethodInfo[]> GetGroupedMagickReadSettingsMethods()
    {
      return GetGroupedMethods("MagickReadSettings");
    }

    public IEnumerable<PropertyInfo> GetMagickImageProperties()
    {
      return GetProperties(GetType("MagickImage"));
    }

    public IEnumerable<PropertyInfo> GetMagickSettingsProperties()
    {
      return GetProperties(GetType("MagickSettings"));
    }

    public IEnumerable<PropertyInfo> GetMagickReadSettingsProperties()
    {
      return GetProperties(GetType("MagickReadSettings"));
    }

    public IEnumerable<MethodInfo> GetMethods(string typeName)
    {
      return GetMethods(GetType(typeName));
    }

    public static IEnumerable<MethodInfo> GetMethods(Type type)
    {
      return from method in type.GetMethods()
             where IsSupported(method)
             select method;
    }

    public string GetName(Type type)
    {
      string name = type.Name;

      if (type.IsGenericType)
      {
        Type[] arguments = type.GetGenericArguments();
        for (int i = 0; i < arguments.Length; i++)
        {
          name = name.Replace("`" + (i + 1), "");
        }
        name += "<";
        for (int i = 0; i < arguments.Length; i++)
        {
          if (i > 0)
            name += ",";

          if (arguments[i].Namespace.StartsWith("ImageMagick.", StringComparison.Ordinal))
            name += arguments[i].Namespace + ".";
          else
            name = CheckForQuantum(name);

          name += arguments[i].Name;
        }
        name += ">";

        return name;
      }

      switch (name)
      {
        case "Double":
          return "double";
        default:
          if (type.Namespace.StartsWith("ImageMagick.", StringComparison.Ordinal))
            return type.Namespace + "." + name;
          else
            return CheckForQuantum(name);
      }
    }

    public static string GetName(MemberInfo member)
    {
      ConstructorInfo constructor = member as ConstructorInfo;
      if (constructor != null)
        return constructor.DeclaringType.Name;

      return member.Name;
    }

    public IEnumerable<ConstructorInfo[]> GetPaths()
    {
      return GetInterfaceConstructors("IPath");
    }

    public IEnumerable<PropertyInfo> GetProperties(string typeName)
    {
      return GetProperties(GetType(typeName));
    }

    public static IEnumerable<PropertyInfo> GetProperties(Type type)
    {
      return from property in type.GetProperties()
             where IsSupported(property)
             orderby property.Name
             select property;
    }

    public Type GetType(string typeName)
    {
      return (from type in GetTypes()
              where type.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase)
              select type).FirstOrDefault();
    }

    public static string GetXsdAttributeType(ParameterInfo parameter)
    {
      return GetXsdAttributeType(parameter.ParameterType);
    }

    public static string GetXsdAttributeType(PropertyInfo property)
    {
      return GetXsdAttributeType(property.PropertyType);
    }

    public static string GetXsdElementType(ParameterInfo parameter)
    {
      return GetXsdElementType(parameter.ParameterType);
    }

    public static string GetXsdElementType(PropertyInfo property)
    {
      return GetXsdElementType(property.PropertyType);
    }

    public static string GetXsdName(MemberInfo member)
    {
      string name = GetName(member);
      if (name.StartsWith("Drawable", StringComparison.OrdinalIgnoreCase))
        name = name.Substring(8);
      else if (name.StartsWith("Path", StringComparison.OrdinalIgnoreCase))
        name = name.Substring(4);

      return GetXsdName(name);
    }

    public static string GetXsdName(ParameterInfo parameter)
    {
      return GetXsdName(parameter.Name);
    }

    public static string GetXsdName(PropertyInfo property)
    {
      return GetXsdName(property.Name);
    }
  }
}
