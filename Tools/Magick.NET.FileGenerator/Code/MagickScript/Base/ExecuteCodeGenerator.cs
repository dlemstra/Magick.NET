using System;
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
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Magick.NET.FileGenerator
{
	//==============================================================================================
	internal abstract class ExecuteCodeGenerator : CodeGenerator
	{
		//===========================================================================================
		private bool IsStatic(MethodBase[] methods)
		{
			if (methods.Length != 1)
				return false;

			return IsStatic(methods[0]);
		}
		//===========================================================================================
		private bool IsStatic(MethodBase method)
		{
			if (method == null)
				return false;

			return method.GetParameters().Length == 0;
		}
		//===========================================================================================
		private bool IsStatic(MemberInfo memberInfo)
		{
			return IsStatic(memberInfo as MethodBase);
		}
		//===========================================================================================
		private void WriteSwitch(IndentedTextWriter writer, IEnumerable<string> names, int level)
		{
			IEnumerable<char> chars = (from name in names
												where name.Length > level
												select name[level]).Distinct();


			if (chars.Count() == 1 && names.Count() > 1)
			{
				WriteSwitch(writer, names, ++level);
			}
			else
			{
				string shortName = (from name in names
										  where name.Length == level
										  select name).FirstOrDefault();
				if (shortName != null)
				{
					writer.Write("if (element->Name->Length == ");
					writer.Write(level);
					writer.WriteLine(")");
					WriteStartColon(writer);
					WriteExecute(writer, shortName);
					WriteEndColon(writer);
				}

				if (chars.Count() > 1)
				{
					writer.Write("switch(element->Name[");
					writer.Write(level);
					writer.WriteLine("])");
					WriteStartColon(writer);
				}

				foreach (char c in chars)
				{
					writer.Write("case '");
					writer.Write(c);
					writer.WriteLine("':");
					WriteStartColon(writer);

					IEnumerable<string> children = from name in names
															 where name.Length > level && name[level] == c
															 select name;

					if (children.Count() == 1)
						WriteExecute(writer, children.First());
					else
						WriteSwitch(writer, children, level + 1);

					WriteEndColon(writer);
				}

				if (chars.Count() > 1)
				{
					WriteEndColon(writer);
					if (level != 0)
						writer.WriteLine("break;");
				}
			}
		}
		//===========================================================================================
		private void WriteExecute(IndentedTextWriter writer, string name)
		{
			MemberInfo member = (from property in Properties
										where MagickNET.GetXsdName(property).Equals(name, StringComparison.OrdinalIgnoreCase)
										select property).FirstOrDefault();

			if (member == null)
				member = (from overloads in Methods
							 let method = overloads[overloads.Length - 1]
							 where MagickNET.GetXsdName(method).Equals(name, StringComparison.OrdinalIgnoreCase)
							 select method).FirstOrDefault();


			if (ReturnType != "void")
				writer.Write("return ");
			writer.Write("Execute");
			if (member == null)
			{
				writer.Write(char.ToUpper(name[0]));
				writer.Write(name.Substring(1));
			}
			else
			{
				writer.Write(MagickNET.GetName(member));
			}
			writer.Write("(");
			if (member == null || !IsStatic(member))
				writer.Write("element, ");
			writer.Write(ExecuteArgument.Split(' ').Last());
			writer.WriteLine(");");
			if (ReturnType == "void")
				writer.WriteLine("return;");
		}
		//===========================================================================================
		private void WriteExecute(IndentedTextWriter writer)
		{
			writer.Write(ReturnType);
			writer.Write(" MagickScript::Execute");
			writer.Write(ExecuteName);
			writer.Write("(XmlElement^ element, ");
			writer.Write(ExecuteArgument);
			writer.WriteLine(")");
			WriteStartColon(writer);

			IEnumerable<string> names = (from property in Properties
												  select MagickNET.GetXsdName(property)).Concat(
												  from method in Methods
												  select MagickNET.GetXsdName(method[0])).Concat(
												  CustomMethods);

			WriteSwitch(writer, names, 0);

			writer.WriteLine("throw gcnew NotImplementedException(element->Name);");
			WriteEndColon(writer);
		}
		//===========================================================================================
		private void WriteExecute(IndentedTextWriter writer, MethodBase[] methods)
		{
			writer.Write(ReturnType);
			writer.Write(" MagickScript::Execute");
			writer.Write(MagickNET.GetName(methods[0]));
			writer.Write("(");
			if (!IsStatic(methods))
				writer.Write("XmlElement^ element, ");
			writer.Write(ExecuteArgument);
			writer.WriteLine(")");
			WriteStartColon(writer);

			WriteMethod(writer, methods);

			WriteEndColon(writer);
		}
		//===========================================================================================
		private void WriteExecute(IndentedTextWriter writer, PropertyInfo property)
		{
			writer.Write("void MagickScript::Execute");
			writer.Write(property.Name);
			writer.Write("(XmlElement^ element, ");
			writer.Write(ExecuteArgument);
			writer.WriteLine(")");
			WriteStartColon(writer);

			WriteSet(writer, property);

			WriteEndColon(writer);
		}
		//===========================================================================================
		private void WriteExecuteHeader(IndentedTextWriter writer)
		{
			writer.Write(ReturnType);
			writer.Write(" Execute");
			writer.Write(ExecuteName);
			writer.Write("(XmlElement^ element, ");
			writer.Write(ExecuteArgument);
			writer.WriteLine(");");
		}
		//===========================================================================================
		private void WriteHeader(IndentedTextWriter writer, MethodBase[] methods)
		{
			bool isStatic = IsStatic(methods);
			if (isStatic)
				writer.Write("static ");

			writer.Write(ReturnType);
			writer.Write(" Execute");
			writer.Write(MagickNET.GetName(methods[0]));
			writer.Write("(");
			if (!isStatic)
				writer.Write("XmlElement^ element, ");
			writer.Write(ExecuteArgument);
			writer.WriteLine(");");
		}
		//===========================================================================================
		private void WriteHeader(IndentedTextWriter writer, PropertyInfo property)
		{
			writer.Write("void Execute");
			writer.Write(property.Name);
			writer.WriteLine("(XmlElement^ element, MagickImage^ image);");
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
		protected void WriteGetValue(IndentedTextWriter writer, PropertyInfo property)
		{
			string typeName = MagickNET.GetCppTypeName(property);
			string xsdTypeName = MagickNET.GetXsdAttributeType(property);

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
		public void WriteExecuteMethods(IndentedTextWriter writer)
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
		public void WriteHeader(IndentedTextWriter writer)
		{
			WriteExecuteHeader(writer);

			foreach (PropertyInfo property in Properties)
			{
				WriteHeader(writer, property);
			}

			foreach (MethodBase[] overloads in Methods)
			{
				WriteHeader(writer, overloads);
			}
		}
		//===========================================================================================
		public virtual void WriteIncludes(IndentedTextWriter writer)
		{
		}
		//===========================================================================================
	}
	//==============================================================================================
}
