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
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Magick.NET.FileGenerator
{
	//==============================================================================================
	internal abstract class ExecuteCodeGenerator : SwitchCodeGenerator
	{
		//===========================================================================================
		private static bool HasParameters(MethodBase[] methods)
		{
			if (methods.Length != 1)
				return false;

			return HasParameters(methods[0]);
		}
		//===========================================================================================
		private static bool HasParameters(MethodBase method)
		{
			if (method == null)
				return false;

			return method.GetParameters().Length == 0;
		}
		//===========================================================================================
		private static bool HasParameters(MemberInfo memberInfo)
		{
			return HasParameters(memberInfo as MethodBase);
		}
		//===========================================================================================
		private bool IsStatic(MethodBase[] methods)
		{
			if (methods == null)
				return false;

			if (methods.Length != 1)
				return false;

			ParameterInfo[] parameters = methods[0].GetParameters();

			if (parameters.Length == 0)
				return true;

			foreach (ParameterInfo parameter in parameters)
			{
				string xsdTypeName = MagickTypes.GetXsdAttributeType(parameter);
				if (xsdTypeName != null)
					return false;

				string typeName = GetName(parameter);
				if (!HasStaticCreateMethod(typeName))
					return false;
			}

			return true;
		}
		//===========================================================================================
		private void WriteExecute(IndentedTextWriter writer)
		{
			writer.WriteLine("[SuppressMessage(\"Microsoft.Maintainability\", \"CA1502:AvoidExcessiveComplexity\")]");
			writer.WriteLine("[SuppressMessage(\"Microsoft.Maintainability\", \"CA1505:AvoidUnmaintainableCode\")]");
			writer.Write("private ");
			writer.Write(ReturnType);
			writer.Write(" Execute");
			writer.Write(ExecuteName);
			writer.Write("(XmlElement element, ");
			writer.Write(ExecuteArgument);
			writer.WriteLine(")");
			WriteStartColon(writer);

			IEnumerable<string> names = (from property in Properties
												  select MagickTypes.GetXsdName(property)).Concat(
												  from method in Methods
												  select MagickTypes.GetXsdName(method[0])).Concat(
												  CustomMethods);

			WriteSwitch(writer, names);
			WriteEndColon(writer);
		}
		//===========================================================================================
		private void WriteExecute(IndentedTextWriter writer, MethodBase[] methods)
		{
			WriteSeparator(writer);
			writer.Write("private ");
			if (IsStatic(methods))
				writer.Write("static ");
			writer.Write(ReturnType);
			writer.Write(" Execute");
			writer.Write(GetName(methods[0]));
			writer.Write("(");
			if (!HasParameters(methods))
				writer.Write("XmlElement element, ");
			writer.Write(ExecuteArgument);
			writer.WriteLine(")");
			WriteStartColon(writer);

			WriteMethod(writer, methods);

			WriteEndColon(writer);
		}
		//===========================================================================================
		private void WriteExecute(IndentedTextWriter writer, PropertyInfo property)
		{
			WriteSeparator(writer);
			writer.Write("private ");
			writer.Write("void Execute");
			writer.Write(property.Name);
			writer.Write("(XmlElement element, ");
			writer.Write(ExecuteArgument);
			writer.WriteLine(")");
			WriteStartColon(writer);

			WriteSet(writer, property);

			WriteEndColon(writer);
		}
		//===========================================================================================
		protected virtual string[] CustomMethods
		{
			get
			{
				return new string[] { };
			}
		}
		//===========================================================================================
		protected abstract string ExecuteArgument
		{
			get;
		}
		//===========================================================================================
		protected abstract string ExecuteName
		{
			get;
		}
		//===========================================================================================
		protected virtual IEnumerable<MethodBase[]> Methods
		{
			get
			{
				return Enumerable.Empty<MethodBase[]>();
			}
		}
		//===========================================================================================
		protected virtual IEnumerable<PropertyInfo> Properties
		{
			get
			{
				return Enumerable.Empty<PropertyInfo>();
			}
		}
		//===========================================================================================
		protected virtual string ReturnType
		{
			get
			{
				return "void";
			}
		}
		//===========================================================================================
		protected sealed override void WriteCase(IndentedTextWriter writer, string name)
		{
			MemberInfo member = (from property in Properties
										where MagickTypes.GetXsdName(property).Equals(name, StringComparison.OrdinalIgnoreCase)
										select property).FirstOrDefault();

			if (member == null)
				member = (from overloads in Methods
							 let method = overloads[overloads.Length - 1]
							 where MagickTypes.GetXsdName(method).Equals(name, StringComparison.OrdinalIgnoreCase)
							 select method).FirstOrDefault();


			if (ReturnType != "void")
				writer.Write("return ");
			writer.Write("Execute");
			if (member == null)
			{
				writer.Write(char.ToUpper(name[0], CultureInfo.InvariantCulture));
				writer.Write(name.Substring(1));
			}
			else
			{
				writer.Write(GetName(member));
			}
			writer.Write("(");
			if (member == null || !HasParameters(member))
				writer.Write("element, ");
			writer.Write(ExecuteArgument.Split(' ').Last());
			writer.WriteLine(");");
			if (ReturnType == "void")
				writer.WriteLine("return;");
		}
		//===========================================================================================
		protected override void WriteCode(IndentedTextWriter writer)
		{
			WriteExecute(writer);

			foreach (PropertyInfo property in Properties)
			{
				WriteExecute(writer, property);
			}

			foreach (MethodBase[] methods in Methods)
			{
				WriteExecute(writer, methods);
			}
		}
		//===========================================================================================
		protected void WriteGetValue(IndentedTextWriter writer, PropertyInfo property)
		{
			string typeName = GetName(property);
			string xsdTypeName = MagickTypes.GetXsdAttributeType(property);

			if (xsdTypeName != null)
			{
				WriteGetElementValue(writer, typeName, "value");
			}
			else
			{
				WriteCreateMethod(writer, typeName);
				writer.Write("(");
				WriteSelectElement(writer, typeName, null);
				writer.WriteLine(");");
			}
		}
		//===========================================================================================
		protected abstract void WriteSet(IndentedTextWriter writer, PropertyInfo property);
		//===========================================================================================
		public override string Name
		{
			get
			{
				return ExecuteName;
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}
