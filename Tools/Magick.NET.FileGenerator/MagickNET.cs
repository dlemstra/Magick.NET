//=================================================================================================
// Copyright 2013 Dirk Lemstra <http://magick.codeplex.com/>
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

namespace Magick.NET.FileGenerator
{
	//==============================================================================================
	internal class MagickNET
	{
		//===========================================================================================
		private Assembly _MagickNET;
		private static readonly string[] _UnsupportedMethods = new string[]
		{
			"Add", "Clear", "Dispose", "Draw", "Insert", "RemoveAt", "Write"
		};
		//===========================================================================================
		private IEnumerable<ConstructorInfo[]> GetSubclassConstructors(string baseClass)
		{
			return from type in _MagickNET.GetTypes()
					 where type.IsSubclassOf(baseClass)
					 let constructors = GetConstructors(type.Name).ToArray()
					 where constructors.Length > 0
					 orderby type.Name
					 select constructors;
		}
		//===========================================================================================
		private bool HasSupportedResult(MethodInfo method)
		{
			if (method.ReturnType.Name != "MagickImage")
				return false;

			return IsSupportedMethod(method);
		}
		//===========================================================================================
		private bool IsSupported(ConstructorInfo constructor, ConstructorInfo[] constructors)
		{
			if (!constructor.IsPublic)
				return false;

			ParameterInfo[] parameters = constructor.GetParameters();
			if (parameters.Length == 0)
				return false;

			if (parameters.Any(parameter => GetTypeName(parameter) == "Unsupported"))
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
		//===========================================================================================
		private bool IsSupported(MethodInfo method)
		{
			if (method.ReturnType != typeof(void))
				return false;

			return IsSupportedMethod(method);
		}
		//===========================================================================================
		private bool IsSupported(PropertyInfo property)
		{
			if (property.SetMethod == null)
				return false;

			return GetTypeName(property) != "Unsupported";
		}
		//===========================================================================================
		private bool IsSupportedMethod(MethodInfo method)
		{
			if (method.IsSpecialName)
				return false;

			if (!method.IsPublic)
				return false;

			if (_UnsupportedMethods.Contains(method.Name))
				return false;

			if (method.GetParameters().Any(parameter => GetTypeName(parameter) == "Unsupported"))
				return false;

			return true;
		}
		//===========================================================================================
		public MagickNET(QuantumDepth depth)
		{
			string assemblyFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\Magick.NET\bin\Release" + depth + @"\v4.0\Win32\Magick.NET-x86.dll";

			if (!File.Exists(assemblyFile))
				throw new ArgumentException("Unable to find file: " + assemblyFile, "assemblyFile");

			_MagickNET = Assembly.LoadFile(assemblyFile);
		}
		//===========================================================================================
		public IEnumerable<Type> Enums
		{
			get
			{
				return from type in _MagickNET.GetTypes()
						 where type.IsEnum && type.IsPublic
						 orderby type.Name
						 select type;
			}
		}
		//===========================================================================================
		public IEnumerable<ConstructorInfo> GetConstructors(string typeName)
		{
			return from type in _MagickNET.GetTypes()
					 where type.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase)
					 let constructors = type.GetConstructors()
					 from constructor in constructors
					 where IsSupported(constructor, constructors)
					 orderby constructor.GetParameters().Count()
					 select constructor;
		}
		//===========================================================================================
		public IEnumerable<ConstructorInfo[]> GetDrawables()
		{
			return GetSubclassConstructors("Drawable");
		}
		//===========================================================================================
		public IEnumerable<MethodInfo[]> GetGroupedMagickImageMethods()
		{
			return from type in _MagickNET.GetTypes()
					 where type.Name == "MagickImage"
					 from method in type.GetMethods()
					 where IsSupported(method)
					 group method by method.Name into g
					 orderby g.Key
					 select g.OrderBy(m => m.GetParameters().Count()).ToArray();
		}
		//===========================================================================================
		public IEnumerable<MethodInfo[]> GetGroupedMagickImageCollectionMethods()
		{
			return from type in _MagickNET.GetTypes()
					 where type.Name == "MagickImageCollection"
					 from method in type.GetMethods()
					 where IsSupported(method)
					 group method by method.Name into g
					 orderby g.Key
					 select g.OrderBy(m => m.GetParameters().Count()).ToArray();
		}
		//===========================================================================================
		public IEnumerable<MethodInfo[]> GetGroupedMagickImageCollectionResultMethods()
		{
			return from type in _MagickNET.GetTypes()
					 where type.Name == "MagickImageCollection"
					 from method in type.GetMethods()
					 where HasSupportedResult(method)
					 group method by method.Name into g
					 orderby g.Key
					 select g.OrderBy(m => m.GetParameters().Count()).ToArray();
		}
		//===========================================================================================
		internal IEnumerable<PropertyInfo> GetMagickImageProperties()
		{
			return GetProperties("MagickImage");
		}
		//===========================================================================================
		public IEnumerable<MethodInfo> GetMethods(string typeName)
		{
			return from type in _MagickNET.GetTypes()
					 where type.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase)
					 from method in type.GetMethods()
					 where IsSupported(method)
					 select method;
		}
		//===========================================================================================
		public IEnumerable<PropertyInfo> GetProperties(string typeName)
		{
			return from type in _MagickNET.GetTypes()
					 where type.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase)
					 from property in type.GetProperties()
					 where IsSupported(property)
					 orderby property.Name
					 select property;
		}
		//===========================================================================================
		public static string GetName(MemberInfo member)
		{
			ConstructorInfo constructor = member as ConstructorInfo;
			if (constructor != null)
			{
				string name = constructor.DeclaringType.Name;
				if (name.StartsWith("Drawable", StringComparison.OrdinalIgnoreCase))
					name = name.Substring(8);
				else if (name.StartsWith("Path", StringComparison.OrdinalIgnoreCase))
					name = name.Substring(4);

				return name;
			}

			return member.Name;
		}
		//===========================================================================================
		public IEnumerable<ConstructorInfo[]> GetPaths()
		{
			return GetSubclassConstructors("PathBase");
		}
		//===========================================================================================
		public static string GetTypeName(ConstructorInfo constructor)
		{
			return GetTypeName(constructor.DeclaringType);
		}
		//===========================================================================================
		public static string GetTypeName(ParameterInfo parameter)
		{
			return GetTypeName(parameter.ParameterType);
		}
		//===========================================================================================
		public static string GetTypeName(PropertyInfo property)
		{
			return GetTypeName(property.PropertyType);
		}
		//===========================================================================================
		public static string GetTypeName(Type type)
		{
			string name = type.Name;

			if (type.IsGenericType)
				name = name.Replace("`1", "") + "<" + type.GetGenericArguments()[0].Name + ">";

			if (type.IsEnum)
				return name;

			switch (name)
			{
				case "Encoding":
				case "ImageProfile":
				case "MagickColor":
				case "MagickGeometry":
				case "MagickImage":
				case "MagickReadSettings":
				case "PathArc":
				case "PathCurveto":
				case "PathQuadraticCurveto":
				case "PixelStorageSettings":
				case "String":
					return name + "^";
				case "IEnumerable<Coordinate>":
					return "IEnumerable<Coordinate>^";
				case "IEnumerable<PathArc>":
					return "IEnumerable<PathArc^>^";
				case "IEnumerable<PathCurveto>":
					return "IEnumerable<PathCurveto^>^";
				case "IEnumerable<PathQuadraticCurveto>":
					return "IEnumerable<PathQuadraticCurveto^>^";
				case "IEnumerable<PathBase>":
					return "IEnumerable<PathBase^>^";
				case "Int32":
					return "int";
				case "Single":
					return "Magick::Quantum";
				case "Boolean":
					return "bool";
				case "Double":
					return "double";
				case "Coordinate":
				case "Nullable<Int32>":
				case "Nullable<ColorSpace>":
				case "Nullable<MagickFormat>":
				case "Percentage":
					return name;
				case "Byte[]":
				case "Color":
				case "ColorMatrix":
				case "ColorProfile":
				case "ConvolveMatrix":
				case "Double[]":
				case "DrawableAffine":
				case "MagickImage[]":
				case "Matrix":
				case "Rectangle":
				case "Stream":
					return "Unsupported";
				default:
					throw new NotImplementedException(name);
			}
		}
	}
	//==============================================================================================
}
