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
			"Add", "AddRange", "Clear", "Dispose", "Draw", "Insert", "Ping", "Read", "RemoveAt", "Write"
		};
		//===========================================================================================
		private bool CanIgnoreResult(MethodInfo method)
		{
			if (method.IsSpecialName)
				return false;

			if (method.Name == "Quantize")
				return true;

			return false;
		}
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
		private string GetXsdAttributeType(Type type)
		{
			Type newType = type;

			if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
				newType = type.GetGenericArguments().First();

			string typeName = GetCppTypeName(newType);

			if (newType.IsEnum)
				return typeName;

			switch (typeName)
			{
				case "Encoding^":
				case "String^":
					return "xs:string";
				case "Percentage":
					return "double";
				case "bool":
				case "int":
				case "double":
					return typeName;
				case "Magick::Quantum":
					return "quantum";
				case "MagickColor^":
					return "color";
				case "MagickGeometry^":
					return "geometry";
				case "PointD":
					return "pointd";
				case "array<double>^":
				case "Coordinate":
				case "Drawable^":
				case "IEnumerable<Coordinate>^":
				case "IEnumerable<Drawable^>^":
				case "IEnumerable<PathArc^>^":
				case "IEnumerable<PathCurveto^>^":
				case "IEnumerable<PathQuadraticCurveto^>^":
				case "IEnumerable<PathBase^>^":
				case "IEnumerable<SparseColorArg^>^":
				case "ImageProfile^":
				case "MagickImage^":
				case "MontageSettings^":
				case "PathArc^":
				case "PathCurveto^":
				case "PathQuadraticCurveto^":
				case "PixelStorageSettings^":
				case "QuantizeSettings^":
					return null;
				default:
					throw new NotImplementedException(typeName);
			}
		}
		//===========================================================================================
		private string GetXsdElementType(Type type)
		{
			string typeName = GetCppTypeName(type);

			switch (typeName)
			{
				case "array<double>^":
					return "doubleArray";
				case "Coordinate":
					return "coordinate";
				case "Drawable^":
					return "drawable";
				case "IEnumerable<Coordinate>^":
					return "coordinates";
				case "IEnumerable<Drawable^>^":
					return "drawables";
				case "IEnumerable<PathBase^>^":
					return "paths";
				case "IEnumerable<PathArc^>^":
					return "pathArcs";
				case "IEnumerable<PathCurveto^>^":
					return "pathCurvetos";
				case "IEnumerable<PathQuadraticCurveto^>^":
					return "pathQuadraticCurvetos";
				case "IEnumerable<SparseColorArg^>^":
					return "sparseColorArgs";
				case "ImageProfile^":
					return "profile";
				case "MagickImage^":
					return "image";
				case "MontageSettings^":
					return "montageSettings";
				case "PathArc^":
					return "pathArc";
				case "PathCurveto^":
					return "pathCurveto";
				case "PathQuadraticCurveto^":
					return "pathQuadraticCurveto";
				case "PixelStorageSettings^":
					return "pixelStorageSettings";
				case "QuantizeSettings^":
					return "quantizeSettings";
				default:
					return null;
			}
		}
		//===========================================================================================
		private static string GetXsdName(string name)
		{
			string newName = name;
			if (name.ToUpperInvariant() == name)
				return name.ToLowerInvariant();

			return char.ToLowerInvariant(name[0]) + name.Substring(1);
		}
		//===========================================================================================
		private bool HasSupportedResult(MethodInfo method)
		{
			if (method.ReturnType.Name != "MagickImage")
				return false;

			return IsSupportedMethod(method);
		}
		//===========================================================================================
		private bool IsPublicType(Type type, bool withEnums)
		{
			if (!type.IsPublic)
				return false;

			if (type.IsEnum)
				return withEnums;

			if (!type.IsClass && !type.IsValueType)
				return false;

			if (type.Name.IndexOf("Wrapper<", StringComparison.Ordinal) != -1)
				return false;

			return true;
		}
		//===========================================================================================
		private bool IsSupported(ConstructorInfo constructor, ConstructorInfo[] constructors)
		{
			if (!constructor.IsPublic)
				return false;

			ParameterInfo[] parameters = constructor.GetParameters();
			if (parameters.Length == 0)
				return false;

			if (parameters.Any(parameter => GetCppTypeName(parameter) == "Unsupported"))
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
				return CanIgnoreResult(method);

			return IsSupportedMethod(method);
		}
		//===========================================================================================
		private bool IsSupported(PropertyInfo property)
		{
			if (property.SetMethod == null)
				return false;

			return GetCppTypeName(property) != "Unsupported";
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

			if (method.GetParameters().Any(parameter => GetCppTypeName(parameter) == "Unsupported"))
				return false;

			return true;
		}
		//===========================================================================================
		public MagickNET(QuantumDepth depth)
		{
			string assemblyFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\Magick.NET\bin\" + GetFolderName(depth) + @"\Win32\Magick.NET-x86.dll";

			if (!File.Exists(assemblyFile))
				throw new ArgumentException("Unable to find file: " + assemblyFile, "assemblyFile");

			_MagickNET = Assembly.LoadFile(assemblyFile);
			Depth = depth;
		}
		//===========================================================================================
		public QuantumDepth Depth
		{
			get;
			private set;
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
		private string CheckQuantum(string name)
		{
			switch (name)
			{
				case "Byte":
					return Depth == QuantumDepth.Q8 ? "Magick::Quantum" : "unsigned char";
				case "Single":
					return Depth == QuantumDepth.Q16HDRI ? "Magick::Quantum" : "float";
				case "UInt16":
					return Depth == QuantumDepth.Q16 ? "Magick::Quantum" : "unsigned short";
				default:
					throw new NotImplementedException(name);
			}
		}
		//===========================================================================================
		public string GetCppTypeName(ConstructorInfo constructor)
		{
			return GetCppTypeName(constructor.DeclaringType);
		}
		//===========================================================================================
		public string GetCppTypeName(ParameterInfo parameter)
		{
			return GetCppTypeName(parameter.ParameterType);
		}
		//===========================================================================================
		public string GetCppTypeName(PropertyInfo property)
		{
			return GetCppTypeName(property.PropertyType);
		}
		//===========================================================================================
		public string GetCppTypeName(Type type)
		{
			string name = GetTypeName(type);

			if (type.IsEnum)
				return name;

			switch (name)
			{
				case "Encoding":
				case "ColorProfile":
				case "ImageProfile":
				case "MagickColor":
				case "MagickGeometry":
				case "MagickImage":
				case "MagickReadSettings":
				case "MontageSettings":
				case "PathArc":
				case "PathCurveto":
				case "PathQuadraticCurveto":
				case "PixelStorageSettings":
				case "QuantizeSettings":
				case "SparseColorArg":
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
				case "IEnumerable<SparseColorArg>":
					return "IEnumerable<SparseColorArg^>^";
				case "Int32":
					return "int";
				case "Byte":
				case "Single":
				case "UInt16":
					return CheckQuantum(name);
				case "Boolean":
					return "bool";
				case "Double":
					return "double";
				case "Double[]":
					return "array<double>^";
				case "Coordinate":
				case "Nullable<Int32>":
				case "Nullable<ColorSpace>":
				case "Nullable<PointD>":
				case "Nullable<DitherMethod>":
				case "Nullable<MagickFormat>":
				case "Percentage":
				case "PointD":
					return name;
				case "Nullable<Boolean>":
					return "Nullable<bool>";
				case "Byte[]":
				case "Color":
				case "ColorMatrix":
				case "ConvolveMatrix":
				case "DrawableAffine":
				case "IEnumerable<MagickImage>":
				case "MagickImage[]":
				case "MagickImageCollection":
				case "Matrix":
				case "Rectangle":
				case "Stream":
					return "Unsupported";
				default:
					throw new NotImplementedException(name);
			}
		}
		//===========================================================================================
		public IEnumerable<ConstructorInfo[]> GetDrawables()
		{
			return GetSubclassConstructors("Drawable");
		}
		//===========================================================================================
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
		public IEnumerable<ConstructorInfo[]> GetPaths()
		{
			return GetSubclassConstructors("PathBase");
		}
		//===========================================================================================
		public IEnumerable<Type> GetPublicTypes(bool withEnums)
		{
			return from type in _MagickNET.GetTypes()
					 where IsPublicType(type, withEnums)
					 select type;
		}
		//===========================================================================================
		public static Type GetBaseType(Type type)
		{
			if (type.BaseType.Name.IndexOf("Wrapper") != -1)
				return type.BaseType.BaseType;

			return type.BaseType;
		}
		//===========================================================================================
		public static string GetTypeName(Type type)
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

					name += arguments[i].Name;
				}
				name += ">";
			}

			if (type == typeof(void))
				return "void";

			return name;
		}
		//===========================================================================================
		public IEnumerable<string> GetColorProfileNames()
		{
			foreach (string resourceName in from name in _MagickNET.GetManifestResourceNames()
													  orderby name
													  select name)
			{
				if (resourceName.EndsWith(".icc", StringComparison.OrdinalIgnoreCase) ||
					resourceName.EndsWith(".icm", StringComparison.OrdinalIgnoreCase))
				{
					yield return resourceName.Substring(0, resourceName.Length - 4);
				}
			}
		}
		//===========================================================================================
		public string GetXsdAttributeType(ParameterInfo parameter)
		{
			return GetXsdAttributeType(parameter.ParameterType);
		}
		//===========================================================================================
		public string GetXsdAttributeType(PropertyInfo property)
		{
			return GetXsdAttributeType(property.PropertyType);
		}
		//===========================================================================================
		public string GetXsdElementType(ParameterInfo parameter)
		{
			return GetXsdElementType(parameter.ParameterType);
		}
		//===========================================================================================
		public string GetXsdElementType(PropertyInfo property)
		{
			return GetXsdElementType(property.PropertyType);
		}
		//===========================================================================================
		public string GetXsdName(MemberInfo member)
		{
			return GetXsdName(MagickNET.GetName(member));
		}
		//===========================================================================================
		public string GetXsdName(ParameterInfo parameter)
		{
			return GetXsdName(parameter.Name);
		}
		//===========================================================================================
		public string GetXsdName(PropertyInfo property)
		{
			return GetXsdName(property.Name);
		}
		//===========================================================================================
		public static bool IsQuantumDependant(Type type)
		{
			switch (type.Name)
			{
				case "ColorCMYK":
				case "ColorGray":
				case "ColorHSL":
				case "ColorMono":
				case "ColorRGB":
				case "ColorYUV":
				case "MagickColor":
				case "MagickImage":
				case "Pixel":
				case "PixelBaseCollection":
				case "Quantum":
				case "WritablePixelCollection":
					return true;
				default:
					return false;
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}
