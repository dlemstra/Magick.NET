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
		private static readonly string[] _UnsupportedMethods = new string[] { "Dispose", "Draw", "Write" };
		//===========================================================================================
		private bool IsSupported(MethodInfo method)
		{
			if (method.IsSpecialName)
				return false;

			if (!method.IsPublic)
				return false;

			if (method.ReturnType != typeof(void))
				return false;

			if (_UnsupportedMethods.Contains(method.Name))
				return false;

			return !method.GetParameters().Any(parameter => GetTypeName(parameter) == "Unsupported");
		}
		//===========================================================================================
		private bool IsSupported(PropertyInfo property)
		{
			if (property.SetMethod == null)
				return false;

			return GetTypeName(property) != "Unsupported";
		}
		//===========================================================================================
		public MagickNET(QuantumDepth depth)
		{
			string assemblyFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\Magick.NET\bin\Release" + depth + @"\v4.0\Win32\Magick.NET.dll";

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
		public IEnumerable<MethodInfo[]> GetGroupedMethods(string typeName)
		{
			return from type in _MagickNET.GetTypes()
					 where type.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase)
					 from method in type.GetMethods()
					 where IsSupported(method)
					 group method by method.Name into g
					 orderby g.Key
					 select g.OrderBy(m => m.GetParameters().Count()).ToArray();
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

			switch (name)
			{
				case "Encoding":
				case "MagickColor":
				case "MagickGeometry":
				case "MagickImage":
				case "String":
					return name + "^";
				case "Int32":
					return "int";
				case "Byte":
				case "UInt16":
					return "Magick::Quantum";
				case "Boolean":
					return "bool";
				case "Double":
					return "double";
				case "Byte[]":
				case "ColorProfile":
				case "Double[]":
				case "DrawableAffine":
				case "MatrixColor":
				case "MatrixConvolve":
				case "Stream":
					return "Unsupported";
				default:
					return name;
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}
