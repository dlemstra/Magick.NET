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
		private string _HashtableName;
		//===========================================================================================
		private bool HasOnlyStatic
		{
			get
			{
				return !Properties.Any(p => !IsStatic(p)) && !Methods.Any(m => !IsStatic(m));
			}
		}
		//===========================================================================================
		private static bool IsStatic(PropertyInfo property)
		{
			return MagickNET.GetTypeName(property) != "MagickImage^";
		}
		//===========================================================================================
		private void WriteInitializeExecuteMethods(IndentedTextWriter writer, bool isStatic)
		{
			foreach (MethodBase[] method in from m in Methods
													  where IsStatic(m) == isStatic
													  select m)
			{
				WriteInitializeExecute(writer, XsdGenerator.GetName(method[0]), MagickNET.GetName(method[0]), isStatic);
			}
		}
		//===========================================================================================
		private void WriteInitializeExecuteProperties(IndentedTextWriter writer, bool isStatic)
		{
			foreach (PropertyInfo property in from p in Properties
														 where IsStatic(p) == isStatic
														 select p)
			{
				WriteInitializeExecute(writer, XsdGenerator.GetName(property), property.Name, isStatic);
			}
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
			writer.WriteLine("{");
			writer.Indent++;
			writer.Write("ExecuteElement");
			writer.Write(ExecuteName);
			writer.Write("^ method = dynamic_cast<ExecuteElement");
			writer.Write(ExecuteName);
			writer.Write("^>(_StaticExecute");
			writer.Write(ExecuteName);
			writer.WriteLine("[element->Name]);");

			if (!HasOnlyStatic)
			{
				writer.WriteLine("if (method == nullptr)");
				writer.Indent++;
				writer.Write("method = dynamic_cast<ExecuteElement");
				writer.Write(ExecuteName);
				writer.Write("^>(_Execute");
				writer.Write(ExecuteName);
				writer.WriteLine("[element->Name]);");
				writer.Indent--;
			}

			writer.WriteLine("if (method == nullptr)");
			writer.Indent++;
			writer.WriteLine("throw gcnew NotImplementedException(element->Name);");
			writer.Indent--;

			if (ReturnType != "void")
				writer.Write("return ");
			writer.Write("method(element,");
			writer.Write(ExecuteArgument.Split(' ').Last());
			writer.WriteLine(");");

			writer.Indent--;
			writer.WriteLine("}");
		}
		//===========================================================================================
		private void WriteExecute(IndentedTextWriter writer, MethodBase[] methods)
		{
			writer.Write(ReturnType);
			writer.Write(" MagickScript::Execute");
			writer.Write(MagickNET.GetName(methods[0]));
			writer.Write("(XmlElement^ element, ");
			writer.Write(ExecuteArgument);
			writer.WriteLine(")");
			writer.WriteLine("{");
			writer.Indent++;

			WriteMethod(writer, methods);

			writer.Indent--;
			writer.WriteLine("}");
		}
		//===========================================================================================
		private void WriteExecute(IndentedTextWriter writer, PropertyInfo property)
		{
			writer.Write("void MagickScript::Execute");
			writer.Write(property.Name);
			writer.Write("(XmlElement^ element, ");
			writer.Write(ExecuteArgument);
			writer.WriteLine(")");
			writer.WriteLine("{");
			writer.Indent++;

			WriteSet(writer, property);

			writer.Indent--;
			writer.WriteLine("}");
		}
		//===========================================================================================
		private void WriteExecuteHeader(IndentedTextWriter writer)
		{
			if (HasOnlyStatic)
				writer.Write("static ");

			writer.Write(ReturnType);
			writer.Write(" Execute");
			writer.Write(ExecuteName);
			writer.Write("(XmlElement^ element, ");
			writer.Write(ExecuteArgument);
			writer.WriteLine(");");
		}
		//===========================================================================================
		private void WriteHashtableHeader(IndentedTextWriter writer)
		{
			writer.Write("delegate ");
			writer.Write(ReturnType);
			writer.Write(" ExecuteElement");
			writer.Write(ExecuteName);
			writer.Write("(XmlElement^ element, ");
			writer.Write(ExecuteArgument);
			writer.WriteLine(");");
			writer.Write("static initonly System::Collections::Hashtable^ _StaticExecute");
			writer.Write(ExecuteName);
			writer.Write(" = InitializeStaticExecute");
			writer.Write(ExecuteName);
			writer.WriteLine("();");
			writer.Write("static System::Collections::Hashtable^ InitializeStaticExecute");
			writer.Write(ExecuteName);
			writer.WriteLine("();");

			if (HasOnlyStatic)
				return;

			writer.Write("System::Collections::Hashtable^ _Execute");
			writer.Write(ExecuteName);
			writer.WriteLine(";");
			writer.Write("void InitializeExecute");
			writer.Write(ExecuteName);
			writer.WriteLine("();");
		}
		//===========================================================================================
		private void WriteHeader(IndentedTextWriter writer, MethodBase[] methods)
		{
			if (IsStatic(methods))
				writer.Write("static ");

			writer.Write(ReturnType);
			writer.Write(" Execute");
			writer.Write(MagickNET.GetName(methods[0]));
			writer.Write("(XmlElement^ element, ");
			writer.Write(ExecuteArgument);
			writer.WriteLine(");");
		}
		//===========================================================================================
		private void WriteHeader(IndentedTextWriter writer, PropertyInfo property)
		{
			if (IsStatic(property))
				writer.Write("static ");

			writer.Write("void Execute");
			writer.Write(property.Name);
			writer.WriteLine("(XmlElement^ element, MagickImage^ image);");
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
		protected virtual void WriteInitializeExecute(IndentedTextWriter writer, bool isStatic)
		{
		}
		//===========================================================================================
		protected void WriteInitializeExecute(IndentedTextWriter writer, string xsdName, string name, bool isStatic)
		{
			writer.Write(_HashtableName);
			writer.Write("[\"");
			writer.Write(xsdName);
			writer.Write("\"] = gcnew ExecuteElement");
			writer.Write(ExecuteName);
			writer.Write("(");
			if (!isStatic)
				writer.Write("this, &");
			writer.Write("MagickScript::Execute");
			writer.Write(name);
			writer.WriteLine(");");
		}
		//===========================================================================================
		protected abstract void WriteSet(IndentedTextWriter writer, PropertyInfo property);
		//===========================================================================================
		protected static void WriteGetValue(IndentedTextWriter writer, PropertyInfo property)
		{
			string typeName = MagickNET.GetTypeName(property);
			string xsdTypeName = XsdGenerator.GetAttributeType(property);

			if (xsdTypeName != null)
			{
				WriteGetAttributeValue(writer, typeName, "value");
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
		public void WriteCallInitializeExecute(IndentedTextWriter writer)
		{
			if (HasOnlyStatic)
				return;

			writer.Write("InitializeExecute");
			writer.Write(ExecuteName);
			writer.WriteLine("();");
		}
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
			WriteHashtableHeader(writer);
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
		public void WriteInitializeExecute(IndentedTextWriter writer)
		{
			writer.Write("System::Collections::Hashtable^ MagickScript::InitializeStaticExecute");
			writer.Write(ExecuteName);
			writer.WriteLine("()");
			writer.WriteLine("{");
			writer.Indent++;
			writer.WriteLine("System::Collections::Hashtable^ result = gcnew System::Collections::Hashtable();");

			_HashtableName = "result";
			WriteInitializeExecuteProperties(writer, true);
			WriteInitializeExecuteMethods(writer, true);
			WriteInitializeExecute(writer, true);

			writer.WriteLine("return result;");
			writer.Indent--;
			writer.WriteLine("}");

			if (HasOnlyStatic)
				return;

			writer.Write("void MagickScript::InitializeExecute");
			writer.Write(ExecuteName);
			writer.WriteLine("()");
			writer.WriteLine("{");
			writer.Indent++;
			writer.Write("_Execute");
			writer.Write(ExecuteName);
			writer.WriteLine(" = gcnew System::Collections::Hashtable();");

			_HashtableName = "_Execute" + ExecuteName;
			WriteInitializeExecuteProperties(writer, false);
			WriteInitializeExecuteMethods(writer, false);
			WriteInitializeExecute(writer, false);

			writer.Indent--;
			writer.WriteLine("}");
		}
		//===========================================================================================
		public virtual void WriteIncludes(IndentedTextWriter writer)
		{
		}
		//===========================================================================================
	}
	//==============================================================================================
}
