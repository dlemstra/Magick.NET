//=================================================================================================
// Copyright 2013-2014 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Magick.NET.FileGenerator
{
	//==============================================================================================
	internal sealed class ClassGenerator : FileGenerator
	{
		//===========================================================================================
		private Type[] _Types;
		//===========================================================================================
		private ClassGenerator()
			: base(@"Magick.NET.AnyCPU\Generated")
		{
			_Types = GetTypes();
		}
		//===========================================================================================
		private ClassGenerator(QuantumDepth depth)
			: base(@"Magick.NET.AnyCPU\Generated", depth)
		{
			_Types = GetTypes();
		}
		//===========================================================================================
		private bool CanSeal(Type type)
		{
			foreach (Type publicType in _Types)
			{
				if (publicType.BaseType == type)
					return false;
			}

			return true;
		}
		//===========================================================================================
		private static bool CanSkip(Type type, MethodInfo method)
		{
			if (!method.IsPublic)
				return true;

			if (method.IsSpecialName)
				return true;

			if (method.Name == "GetType")
				return true;

			if (method.DeclaringType != type && !method.DeclaringType.Name.Contains("Wrapper<"))
				return true;

			return false;
		}
		//===========================================================================================
		private static bool CanSkip(Type type, PropertyInfo property)
		{
			if (property.IsSpecialName)
				return true;

			if (property.DeclaringType != type && !property.DeclaringType.Name.Contains("Wrapper<"))
				return true;

			return false;
		}
		//===========================================================================================
		private void CopyDocumentation()
		{
			string folder = MagickNET.GetFolderName(MagickNET.Depth);
			string source = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\Magick.NET\bin\" + folder + @"\Win32\Magick.NET-x86.xml";
			string destination = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\Magick.NET.AnyCPU\bin\" + folder + @"\AnyCPU\Magick.NET-AnyCPU.xml";
			Directory.CreateDirectory(Path.GetDirectoryName(destination));
			File.Copy(source, destination, true);
		}
		//===========================================================================================
		private static bool HasMethod(Type type, MethodInfo method)
		{
			IEnumerable<MethodInfo> methods = type.GetMethods().Where(m => m.Name == method.Name);
			if (methods.Count() == 0)
				return false;

			ParameterInfo[] parameters = method.GetParameters();
			foreach (MethodInfo otherMethod in methods)
			{
				ParameterInfo[] otherParameters = otherMethod.GetParameters();
				if (parameters.Length == otherParameters.Length)
				{
					int count = 0;
					foreach (ParameterInfo parameter in parameters)
					{
						if (otherParameters.Any(p => p.Name.Equals(parameter.Name, StringComparison.Ordinal)))
							count++;
					}

					if (count == parameters.Length)
						return true;
				}
			}

			return false;
		}
		//===========================================================================================
		private bool IsArray(Type type)
		{
			return type.IsArray && _Types.Contains(type.GetElementType());
		}
		//===========================================================================================
		private void WriteArrayCast(IndentedTextWriter writer, MethodInfo method)
		{
			foreach (ParameterInfo parameter in method.GetParameters())
			{
				if (!IsArray(parameter.ParameterType))
					continue;

				writer.Write("object[] casted_");
				writer.Write(parameter.Name);
				writer.Write(" = ");
				WriteType(writer, parameter.ParameterType.GetElementType());
				writer.Write(".CastArray(");
				writer.Write(parameter.Name);
				writer.WriteLine(");");
			}
		}
		//===========================================================================================
		private static void WriteArrayMethods(IndentedTextWriter writer, Type type)
		{
			if (type.Name != "MagickImage")
				return;

			writer.Write("internal static object[] CastArray(");
			WriteType(writer, type);
			writer.WriteLine("[] array)");
			WriteStartColon(writer);
			WriteReferenceEquals(writer, "array", "null");
			writer.Write("Type arrayType = Types.");
			WriteType(writer, type);
			writer.WriteLine(".MakeArrayType();");
			writer.WriteLine("object[] result = (object[])arrayType.CreateInstance(array.Length);");
			writer.WriteLine("for (int i = 0; i < array.Length; i++)");
			WriteStartColon(writer);
			writer.WriteLine("if (!ReferenceEquals(array[i], null))");
			writer.Indent++;
			writer.Write("result[i] = ");
			WriteType(writer, type);
			writer.WriteLine(".GetInstance(array[i]);");
			writer.Indent--;
			WriteEndColon(writer);
			writer.WriteLine("return result;");
			WriteEndColon(writer);

			writer.Write("internal static void UpdateArray(");
			WriteType(writer, type);
			writer.WriteLine("[] array, object[] objectArray)");
			WriteStartColon(writer);
			WriteReferenceEquals(writer, "array", "");
			writer.WriteLine("for (int i = 0; i < array.Length; i++)");
			WriteStartColon(writer);
			writer.Write("array[i] = objectArray[i] == null ? null : new ");
			WriteType(writer, type);
			writer.WriteLine("(objectArray[i]);");
			WriteEndColon(writer);
			WriteEndColon(writer);
		}
		//===========================================================================================
		private void WriteCallMethod(IndentedTextWriter writer, MethodInfo method)
		{
			writer.Write(".CallMethod(\"");
			writer.Write(method.Name);
			writer.Write("\"");
			WriteParameterTypes(writer, method, ParameterMode.AddColon);
			WriteParameters(writer, method, ParameterMode.Name | ParameterMode.Instance | ParameterMode.AddColon);
			writer.WriteLine(");");
		}
		//===========================================================================================
		private void WriteCast(IndentedTextWriter writer, Type type, string name)
		{
			if (!_Types.Contains(type))
			{
				writer.Write("(");
				WriteType(writer, type);
				writer.Write(")");
				writer.Write(name);
			}
			else
			{
				writer.Write("(");
				writer.Write(name);
				writer.Write(" == null ? null : new ");
				WriteType(writer, type);
				writer.Write("(");
				writer.Write(name);
				writer.Write("))");
			}
		}
		//===========================================================================================
		private static void WriteCatch(IndentedTextWriter writer)
		{
			WriteEndColon(writer);
			writer.WriteLine("catch (Exception ex)");
			WriteStartColon(writer);
			writer.WriteLine("throw ExceptionHelper.Create(ex);");
			WriteEndColon(writer);
		}
		//===========================================================================================
		private void WriteClassStart(IndentedTextWriter writer, Type type)
		{
			if (type.IsAbstract && type.IsSealed)
				writer.Write("public static ");
			else if (type.IsAbstract)
				writer.Write("public abstract ");
			else if (CanSeal(type))
				writer.Write("public sealed ");
			else
				writer.Write("public ");

			writer.Write("class ");
			WriteType(writer, type);
			WriteInheritance(writer, type);
			writer.WriteLine();
			WriteStartColon(writer);

			if (type.IsAbstract && type.IsSealed)
			{
				writer.Write("static ");
				WriteType(writer, type);
				writer.WriteLine("()");
				WriteStartColon(writer);
				WriteEndColon(writer);
			}
			else if (!_Types.Contains(MagickNET.GetBaseType(type)))
			{
				writer.WriteLine("internal object _Instance;");
				writer.Write("internal ");
				WriteType(writer, type);
				writer.WriteLine("(object instance)");
				WriteStartColon(writer);
				writer.Write("_Instance = instance");
				if (type.IsValueType)
					writer.Write(".WrapIfValueType()");
				writer.WriteLine(";");
				WriteEndColon(writer);
				writer.Write("public static object GetInstance(");
				WriteType(writer, type);
				writer.WriteLine(" obj)");
				WriteStartColon(writer);
				WriteReferenceEquals(writer, "obj", "null");
				writer.Write("return obj._Instance");
				if (type.IsValueType)
					writer.Write(".UnwrapIfWrapped()");
				writer.WriteLine(";");
				WriteEndColon(writer);
				writer.WriteLine("public static object GetInstance(object obj)");
				WriteStartColon(writer);
				WriteReferenceEquals(writer, "obj", "null");
				WriteType(writer, type);
				writer.Write(" casted = obj as ");
				WriteType(writer, type);
				writer.WriteLine(";");
				WriteReferenceEquals(writer, "casted", "obj");
				writer.Write("return casted._Instance");
				if (type.IsValueType)
					writer.Write(".UnwrapIfWrapped()");
				writer.WriteLine(";");
				WriteEndColon(writer);
			}
			else
			{
				writer.Write("internal ");
				WriteType(writer, type);
				writer.WriteLine("(object instance)");
				writer.Indent++;
				writer.WriteLine(": base(instance)");
				writer.Indent--;
				WriteStartColon(writer);
				WriteEndColon(writer);
			}
		}
		//===========================================================================================
		private void WriteConstructor(IndentedTextWriter writer, Type type, ConstructorInfo constructor)
		{
			if (constructor != null && constructor.GetCustomAttribute<CLSCompliantAttribute>() != null)
				writer.WriteLine("[CLSCompliant(false)]");

			writer.Write("public ");
			WriteType(writer, type);
			writer.Write("(");
			if (constructor != null)
				WriteParameters(writer, constructor, ParameterMode.Name | ParameterMode.Type);
			writer.WriteLine(")");
			writer.Indent++;
			writer.Write(": ");
			if (_Types.Contains(MagickNET.GetBaseType(type)))
				writer.Write("base");
			else
				writer.Write("this");
			writer.Write("(AssemblyHelper.CreateInstance(");
			WriteTypeOf(writer, type);
			if (constructor != null)
			{
				WriteParameterTypes(writer, constructor, ParameterMode.AddColon);
				WriteParameters(writer, constructor, ParameterMode.Name | ParameterMode.Instance | ParameterMode.AddColon);
			}
			writer.WriteLine("))");
			writer.Indent--;
			WriteStartColon(writer);
			WriteEndColon(writer);
		}
		//===========================================================================================
		private void WriteConstructors(IndentedTextWriter writer, Type type)
		{
			if (type.IsValueType)
				WriteConstructor(writer, type, null);

			foreach (ConstructorInfo constructor in type.GetConstructors())
			{
				if (!constructor.IsPublic)
					continue;

				WriteConstructor(writer, type, constructor);
			}
		}
		//===========================================================================================
		private void WriteCompareTo(IndentedTextWriter writer, MethodInfo method, string suffix, string compare)
		{
			ParameterInfo[] parameters = method.GetParameters();

			WriteReferenceEquals(writer, suffix, parameters);
			writer.Write("return ");
			WriteParameters(writer, ParameterMode.Name, parameters[0]);
			writer.Write(".CompareTo(");
			WriteParameters(writer, ParameterMode.Name, parameters[1]);
			writer.Write(") ");
			writer.Write(compare);
			writer.WriteLine(";");
		}
		//===========================================================================================
		private static bool WriteCustomMethod(IndentedTextWriter writer, MethodInfo method)
		{
			ParameterInfo[] parameters = method.GetParameters();
			if (method.Name == "Draw" && parameters[0].GetCustomAttribute<ParamArrayAttribute>() != null)
			{
				writer.Write("Draw((IEnumerable<Drawable>)");
				writer.Write(parameters[0].Name);
				writer.WriteLine(");");
				return true;
			}

			return false;
		}
		//===========================================================================================
		private void WriteEquals(IndentedTextWriter writer, MethodInfo method, string suffix)
		{
			WriteReferenceEquals(writer, suffix, method.GetParameters());
			writer.Write("return ");
			writer.Write(suffix);
			writer.Write("Object.Equals(");
			WriteParameters(writer, method, ParameterMode.Name);
			writer.WriteLine(");");
		}
		//===========================================================================================
		private void WriteEvents(IndentedTextWriter writer, Type type)
		{
			foreach (EventInfo info in type.GetEvents().OrderBy(e => e.Name))
			{
				bool isStatic = info.AddMethod.IsStatic;
				Type argsType = info.AddMethod.GetParameters()[0].ParameterType;

				if (isStatic)
					writer.Write("private static Delegate _");
				else
					writer.Write("private Delegate _");
				writer.Write(info.Name);
				writer.WriteLine("Delegate;");
				if (isStatic)
					writer.Write("private static ");
				else
					writer.Write("private ");
				WriteType(writer, argsType);
				writer.Write(" _");
				writer.Write(info.Name);
				writer.WriteLine(";");

				if (info.AddMethod.IsStatic)
					writer.Write("private static object Handle");
				else
					writer.Write("private object Handle");
				writer.Write(info.Name);
				writer.WriteLine("Event(object[] args)");
				WriteStartColon(writer);
				writer.Write("if (_");
				writer.Write(info.Name);
				writer.WriteLine(" != null)");
				writer.Indent++;
				writer.Write("_");
				writer.Write(info.Name);
				if (info.AddMethod.IsStatic)
					writer.Write("(null");
				else
					writer.Write("(this");
				writer.Write(", new ");
				WriteType(writer, argsType.GetGenericArguments()[0]);
				writer.WriteLine("(args[1]));");
				writer.Indent--;
				writer.WriteLine("return null;");
				WriteEndColon(writer);

				if (isStatic)
					writer.Write("public static event ");
				else
					writer.Write("public event ");
				WriteType(writer, argsType);
				writer.Write(" ");
				writer.Write(info.Name);
				writer.WriteLine();
				WriteStartColon(writer);

				writer.WriteLine("add");
				WriteStartColon(writer);
				writer.Write("if (_");
				writer.Write(info.Name);
				writer.WriteLine(" == null)");
				WriteStartColon(writer);
				writer.Write("EventInfo eventInfo = Types.");
				WriteType(writer, type);
				writer.Write(".GetEvent(\"");
				writer.Write(info.Name);
				writer.Write("\", BindingFlags.Public | ");
				if (isStatic)
					writer.WriteLine("BindingFlags.Static);");
				else
					writer.WriteLine("BindingFlags.Instance);");
				writer.Write("if (_");
				writer.Write(info.Name);
				writer.WriteLine("Delegate == null)");
				writer.Indent++;
				writer.Write("_");
				writer.Write(info.Name);
				writer.Write("Delegate = eventInfo.EventHandlerType.BuildDynamicHandler(Handle");
				writer.Write(info.Name);
				writer.WriteLine("Event);");
				writer.Indent--;
				writer.Write("eventInfo.GetAddMethod(true).Invoke(");
				if (isStatic)
					WriteTypeOf(writer, type);
				else
					writer.Write("_Instance");
				writer.Write(", new object[] { _");
				writer.Write(info.Name);
				writer.WriteLine("Delegate });");
				WriteEndColon(writer);
				writer.Write("_");
				writer.Write(info.Name);
				writer.WriteLine(" += value;");
				WriteEndColon(writer);

				writer.WriteLine("remove");
				WriteStartColon(writer);
				writer.Write("_");
				writer.Write(info.Name);
				writer.WriteLine(" -= value;");
				writer.Write("if (_");
				writer.Write(info.Name);
				writer.WriteLine(" == null)");
				WriteStartColon(writer);
				writer.Write("EventInfo eventInfo = Types.");
				WriteType(writer, type);
				writer.Write(".GetEvent(\"");
				writer.Write(info.Name);
				writer.Write("\", BindingFlags.Public | ");
				if (isStatic)
					writer.WriteLine("BindingFlags.Static);");
				else
					writer.WriteLine("BindingFlags.Instance);");
				writer.Write("eventInfo.GetRemoveMethod(true).Invoke(");
				if (isStatic)
					WriteTypeOf(writer, type);
				else
					writer.Write("_Instance");
				writer.Write(", new object[] { _");
				writer.Write(info.Name);
				writer.WriteLine("Delegate });");
				WriteEndColon(writer);
				WriteEndColon(writer);

				WriteEndColon(writer);
			}
		}
		//===========================================================================================
		private void WriteHelperMethods(IndentedTextWriter writer, Type type)
		{
			WriteArrayMethods(writer, type);
			WriteIEnumerableMethods(writer, type);
		}
		//===========================================================================================
		private void WriteIEnumerableMethods(IndentedTextWriter writer, Type type)
		{
			if (!IsUsedAsIEnumerable(_Types, type))
				return;

			writer.Write("internal static object CastIEnumerable(IEnumerable<");
			WriteType(writer, type);
			writer.WriteLine("> list)");
			WriteStartColon(writer);
			WriteReferenceEquals(writer, "list", "null");
			writer.Write("Type listType = typeof(List<>).MakeGenericType(Types.");
			WriteType(writer, type);
			writer.WriteLine(");");
			writer.WriteLine("object result = listType.CreateInstance();");
			writer.Write("foreach (");
			WriteType(writer, type);
			writer.WriteLine(" item in list)");
			writer.Indent++;
			writer.Write("result.CallMethod(\"Add\", ");
			WriteType(writer, type);
			writer.WriteLine(".GetInstance(item));");
			writer.Indent--;
			writer.WriteLine("return result;");
			WriteEndColon(writer);
		}
		//===========================================================================================
		private void WriteInheritance(IndentedTextWriter writer, Type type)
		{
			bool writeColon = true;
			Type baseType = MagickNET.GetBaseType(type);

			if (_Types.Contains(baseType) || baseType == typeof(EventArgs) || baseType == typeof(Exception))
			{
				writer.Write(": ");
				WriteType(writer, baseType);
				writeColon = false;
			}

			foreach (Type interfaceType in type.GetInterfaces())
			{
				if (baseType.GetInterfaces().Contains(interfaceType))
					continue;

				if (writeColon)
				{
					writer.Write(": ");
					writeColon = false;
				}
				else
				{
					writer.Write(", ");
				}

				WriteType(writer, interfaceType);
			}
		}
		//===========================================================================================
		private void WriteMethods(IndentedTextWriter writer, Type type)
		{
			foreach (MethodInfo method in type.GetMethods().OrderBy(m => m.Name))
			{
				if (CanSkip(type, method))
					continue;

				if (method.Name == "GetEnumerator2")
				{
					writer.WriteLine("IEnumerator IEnumerable.GetEnumerator()");
					WriteStartColon(writer);
					writer.WriteLine("return GetEnumerator();");
					WriteEndColon(writer);
					continue;
				}

				if (method.GetCustomAttribute<CLSCompliantAttribute>() != null)
					writer.WriteLine("[CLSCompliant(false)]");

				if (method.IsStatic)
					writer.Write("public static ");
				else if (HasMethod(MagickNET.GetBaseType(type), method))
					writer.Write("public override ");
				else if (method.IsVirtual && type.IsAbstract)
					writer.Write("public virtual ");
				else
					writer.Write("public ");
				WriteType(writer, method.ReturnType);
				writer.Write(" ");
				writer.Write(method.Name);
				writer.Write("(");
				WriteParameters(writer, method, ParameterMode.Name | ParameterMode.Type);
				writer.WriteLine(")");
				WriteStartColon(writer);

				if (!WriteCustomMethod(writer, method))
				{
					WriteArrayCast(writer, method);
					if (method.ReturnType != typeof(void))
						writer.WriteLine("object result;");
					WriteTry(writer);
					if (method.ReturnType != typeof(void))
						writer.Write("result = ");
					if (method.IsStatic)
						WriteTypeOf(writer, type);
					else
						writer.Write("_Instance");
					WriteCallMethod(writer, method);
					WriteCatch(writer);
					WriteUpdateArray(writer, method);
					WriteReturn(writer, method.ReturnType);
				}

				WriteEndColon(writer);
			}
		}
		//===========================================================================================
		private void WriteOperators(IndentedTextWriter writer, Type type)
		{
			foreach (MethodInfo method in type.GetMethods().OrderBy(m => m.Name))
			{
				if (!method.IsSpecialName || !method.IsPublic || !method.Name.StartsWith("op_", StringComparison.Ordinal))
					continue;

				if (method.Name == "op_Implicit" || method.Name == "op_Explicit")
				{
					if (HasMethod(MagickNET.GetBaseType(type), method))
						continue;

					writer.Write("public static ");
					writer.Write(method.Name.Substring(3).ToLowerInvariant());
					writer.Write(" operator ");
					WriteType(writer, method.ReturnType);
					writer.Write("(");
					WriteParameters(writer, method, ParameterMode.Name | ParameterMode.Type);
					writer.WriteLine(")");
					WriteStartColon(writer);
					writer.Write("object result = ");
					WriteTypeOf(writer, type);
					WriteCallMethod(writer, method);
					WriteReturn(writer, method.ReturnType);
					WriteEndColon(writer);
				}
				else
				{
					writer.Write("public static bool operator ");
					switch (method.Name)
					{
						case "op_Equality":
							writer.Write("==");
							break;
						case "op_GreaterThan":
							writer.Write(">");
							break;
						case "op_GreaterThanOrEqual":
							writer.Write(">=");
							break;
						case "op_Inequality":
							writer.Write("!=");
							break;
						case "op_LessThan":
							writer.Write("<");
							break;
						case "op_LessThanOrEqual":
							writer.Write("<=");
							break;
						default:
							throw new NotImplementedException();
					}
					writer.Write("(");
					WriteParameters(writer, method, ParameterMode.Name | ParameterMode.Type);
					writer.WriteLine(")");
					WriteStartColon(writer);
					switch (method.Name)
					{
						case "op_Equality":
							WriteEquals(writer, method, "");
							break;
						case "op_GreaterThan":
							WriteCompareTo(writer, method, "", "== 1");
							break;
						case "op_GreaterThanOrEqual":
							WriteCompareTo(writer, method, "", ">= 0");
							break;
						case "op_Inequality":
							WriteEquals(writer, method, "!");
							break;
						case "op_LessThan":
							WriteCompareTo(writer, method, "!", "== -1");
							break;
						case "op_LessThanOrEqual":
							WriteCompareTo(writer, method, "!", "<= 0");
							break;
						default:
							throw new NotImplementedException();
					}
					WriteEndColon(writer);
				}
			}
		}
		//===========================================================================================
		private void WriteParameters(IndentedTextWriter writer, MethodBase method, ParameterMode mode)
		{
			WriteParameters(writer, mode, method.GetParameters());
		}
		//===========================================================================================
		private void WriteParameters(IndentedTextWriter writer, ParameterMode mode, params ParameterInfo[] parameters)
		{
			for (int i = 0; i < parameters.Length; i++)
			{
				if (i > 0 || mode.HasFlag(ParameterMode.AddColon))
					writer.Write(", ");

				if (mode.HasFlag(ParameterMode.Type))
				{
					if (parameters[i].GetCustomAttribute<ParamArrayAttribute>() != null)
						writer.Write("params ");

					WriteType(writer, parameters[i].ParameterType);
					writer.Write(" ");
				}

				if (mode.HasFlag(ParameterMode.Instance) && (_Types.Contains(parameters[i].ParameterType) || parameters[i].ParameterType == typeof(object)))
				{
					WriteType(writer, parameters[i]);
					writer.Write(".GetInstance(");
					writer.Write(parameters[i].Name);
					writer.Write(")");
				}
				else
				{
					Type type = GetIEnumerable(parameters[i]);
					if (type != null && mode.HasFlag(ParameterMode.Instance))
					{
						WriteType(writer, type);
						writer.Write(".CastIEnumerable(");
						writer.Write(parameters[i].Name);
						writer.Write(")");
					}
					else
					{
						if (!mode.HasFlag(ParameterMode.Type) && mode.HasFlag(ParameterMode.Name) && IsArray(parameters[i].ParameterType))
							writer.Write("casted_");

						writer.Write(parameters[i].Name);
					}
				}
			}
		}
		//===========================================================================================
		private void WriteParameterTypes(IndentedTextWriter writer, MethodBase method, ParameterMode mode)
		{
			ParameterInfo[] parameters = method.GetParameters();
			if (parameters.Length == 0)
				return;

			if (mode.HasFlag(ParameterMode.AddColon))
				writer.Write(", ");

			writer.Write("new Type[] {");
			for (int i = 0; i < parameters.Length; i++)
			{
				if (i > 0)
					writer.Write(", ");

				Type type = GetIEnumerable(parameters[i]);
				if (type != null)
				{
					writer.Write("Types.IEnumerable");
					WriteType(writer, type);
				}
				else
				{
					WriteTypeOf(writer, parameters[i].ParameterType);
				}
			}
			writer.Write("}");
		}
		//===========================================================================================
		private void WriteProperties(IndentedTextWriter writer, Type type)
		{
			foreach (PropertyInfo property in type.GetProperties().OrderBy(p => p.Name))
			{
				if (CanSkip(type, property))
					continue;

				MethodInfo method = property.GetMethod != null ? property.GetMethod : property.SetMethod;

				if (HasMethod(MagickNET.GetBaseType(type), method))
				{
					if (!method.IsVirtual)
						return;
				}

				if (property.GetCustomAttribute<CLSCompliantAttribute>() != null)
					writer.WriteLine("[CLSCompliant(false)]");

				writer.Write("public ");
				if (method.IsStatic)
					writer.Write("static ");
				WriteType(writer, property.PropertyType);
				writer.Write(" ");
				if (property.GetIndexParameters().Length > 0)
				{
					writer.Write("this[");
					WriteParameters(writer, method, ParameterMode.Name | ParameterMode.Type);
					writer.WriteLine("]");
				}
				else
				{
					writer.Write(property.Name);
					writer.WriteLine();
				}

				bool isIndexed = property.GetIndexParameters().Length > 0;
				WriteStartColon(writer);
				if (property.GetMethod != null)
				{
					writer.WriteLine("get");
					WriteStartColon(writer);
					if (method.ReturnType != typeof(void))
						writer.WriteLine("object result;");
					WriteTry(writer);

					if (isIndexed)
					{
						if (method.ReturnType != typeof(void))
							writer.Write("result = ");
						if (method.IsStatic)
							WriteTypeOf(writer, type);
						else
							writer.Write("_Instance.");
						writer.Write("GetIndexer(");
						WriteParameterTypes(writer, property.GetMethod, ParameterMode.None);
						WriteParameters(writer, method, ParameterMode.Name | ParameterMode.Instance | ParameterMode.AddColon);
						writer.WriteLine(");");
						WriteCatch(writer);
						WriteReturn(writer, method.ReturnType);
					}
					else
					{
						if (method.ReturnType != typeof(void))
							writer.Write("result = ");
						if (method.IsStatic)
							WriteTypeOf(writer, type);
						else
							writer.Write("_Instance");
						writer.Write(".GetPropertyValue(\"");
						writer.Write(property.Name);
						writer.WriteLine("\");");
						WriteCatch(writer);
						WriteReturn(writer, property.PropertyType);
					}
					WriteEndColon(writer);
				}
				if (property.SetMethod != null)
				{
					writer.WriteLine("set");
					WriteStartColon(writer);
					WriteTry(writer);
					if (isIndexed)
					{
						if (method.IsStatic)
							WriteTypeOf(writer, type);
						else
							writer.Write("_Instance");
						writer.Write(".SetIndexer(");
						WriteParameterTypes(writer, property.SetMethod, ParameterMode.None);
						WriteParameters(writer, property.SetMethod, ParameterMode.Name | ParameterMode.Instance | ParameterMode.AddColon);
						writer.WriteLine(");");
					}
					else
					{
						if (method.IsStatic)
							WriteTypeOf(writer, type);
						else
							writer.Write("_Instance");
						writer.Write(".SetPropertyValue(\"");
						writer.Write(property.Name);
						writer.Write("\", ");
						WriteValue(writer, property);
						writer.WriteLine(");");
					}
					WriteCatch(writer);
					WriteEndColon(writer);
				}
				WriteEndColon(writer);
			}
		}
		//===========================================================================================
		private static void WriteReferenceEquals(IndentedTextWriter writer, string suffix, ParameterInfo[] parameters)
		{
			writer.Write("if (ReferenceEquals(");
			writer.Write(parameters[0].Name);
			writer.WriteLine(", null))");
			writer.Indent++;
			writer.Write("return ");
			writer.Write(suffix);
			writer.Write("ReferenceEquals(");
			writer.Write(parameters[1].Name);
			writer.WriteLine(", null);");
			writer.Indent--;
		}
		//===========================================================================================
		private static void WriteReferenceEquals(IndentedTextWriter writer, string name, string result)
		{
			writer.Write("if (ReferenceEquals(");
			writer.Write(name);
			writer.WriteLine(", null))");
			writer.Indent++;
			writer.Write("return ");
			writer.Write(result);
			writer.WriteLine(";");
			writer.Indent--;
		}
		//===========================================================================================
		private void WriteReturn(IndentedTextWriter writer, Type type)
		{
			if (type == typeof(void))
				return;

			if (type.IsGenericType && type.Name.StartsWith("IEnumerable", StringComparison.Ordinal))
			{
				Type genericArgument = type.GetGenericArguments()[0];

				writer.WriteLine("IEnumerator enumerator = ((IEnumerable)result).GetEnumerator();");
				writer.WriteLine("while (enumerator.MoveNext())");
				writer.Indent++;
				if (_Types.Contains(genericArgument))
				{
					writer.Write("yield return new ");
					WriteType(writer, genericArgument);
					writer.WriteLine("(enumerator.Current);");
				}
				else
				{
					writer.Write("yield return (");
					WriteType(writer, genericArgument);
					writer.WriteLine(")enumerator.Current;");
				}
				writer.Indent--;
			}
			else if ((type.IsGenericType && type.Name.StartsWith("IEnumerator", StringComparison.Ordinal)))
			{
				Type genericArgument = type.GetGenericArguments()[0];
				writer.Write("return new EnumeratorWrapper<");
				WriteType(writer, genericArgument);
				writer.WriteLine(">(result).GetEnumerator();");
			}
			else if (type.IsGenericType && type.Name.StartsWith("Dictionary", StringComparison.Ordinal))
			{
				Type key = type.GetGenericArguments()[0];
				Type value = type.GetGenericArguments()[1];

				writer.Write("Dictionary<");
				WriteType(writer, key);
				writer.Write(",");
				WriteType(writer, value);
				writer.Write("> casted_result = new Dictionary<");
				WriteType(writer, key);
				writer.Write(",");
				WriteType(writer, value);
				writer.WriteLine(">();");
				writer.WriteLine("foreach (object key in (IEnumerable)result.GetPropertyValue(\"Keys\"))");
				WriteStartColon(writer);
				writer.Write("casted_result[");
				WriteCast(writer, key, "key");
				writer.Write("] = ");
				WriteCast(writer, value, "result.GetIndexer(key)");
				writer.WriteLine(";");
				WriteEndColon(writer);
				writer.WriteLine("return casted_result;");
			}
			else if (type.IsGenericType && type.Name.StartsWith("Nullable", StringComparison.Ordinal))
			{
				Type genericArgument = type.GetGenericArguments()[0];

				writer.WriteLine("if (result == null)");
				writer.Indent++;
				writer.Write("return new Nullable<");
				WriteType(writer, genericArgument);
				writer.WriteLine(">();");
				writer.Indent--;
				writer.WriteLine("else");
				writer.Indent++;
				writer.Write("return new Nullable<");
				WriteType(writer, genericArgument);
				writer.Write(">((");
				WriteType(writer, genericArgument);
				writer.WriteLine(")result);");
				writer.Indent--;
			}
			else if (type.IsSubclassOf(typeof(Exception)))
			{
				writer.Write("return (");
				WriteType(writer, type);
				writer.WriteLine(")ExceptionHelper.Create((Exception)result);");
			}
			else
			{
				writer.Write("return ");
				WriteCast(writer, type, "result");
				writer.WriteLine(";");
			}
		}
		//===========================================================================================
		private static void WriteTry(IndentedTextWriter writer)
		{
			writer.WriteLine("try");
			WriteStartColon(writer);
		}
		//===========================================================================================
		private static void WriteType(IndentedTextWriter writer, ParameterInfo parameter)
		{
			writer.Write("ImageMagick.");
			if (parameter.ParameterType == typeof(object))
				WriteType(writer, parameter.Member.DeclaringType);
			else
				WriteType(writer, parameter.ParameterType);
		}
		//===========================================================================================
		private void WriteTypeOf(IndentedTextWriter writer, Type type)
		{
			if (type.IsEnum)
			{
				writer.Write("Types.");
				writer.Write(type.Name);
			}
			else if (IsArray(type))
			{
				writer.Write("Types.");
				WriteType(writer, type.GetElementType());
				writer.Write(".MakeArrayType()");
			}
			else if (!_Types.Contains(type))
			{
				writer.Write("typeof(");
				writer.Write(type.Name);
				writer.Write(")");
			}
			else
			{
				writer.Write("Types.");
				WriteType(writer, type);
			}
		}
		//===========================================================================================
		private void WriteUpdateArray(IndentedTextWriter writer, MethodInfo method)
		{
			foreach (ParameterInfo parameter in method.GetParameters())
			{
				if (!IsArray(parameter.ParameterType))
					continue;

				WriteType(writer, parameter.ParameterType.GetElementType());
				writer.Write(".UpdateArray(");
				writer.Write(parameter.Name);
				writer.Write(", casted_");
				writer.Write(parameter.Name);
				writer.WriteLine(");");
			}
		}
		//===========================================================================================
		private static void WriteUsing(IndentedTextWriter writer)
		{
			writer.WriteLine("using System;");
			writer.WriteLine("using System.Collections;");
			writer.WriteLine("using System.Collections.Generic;");
			writer.WriteLine("using System.Drawing;");
			writer.WriteLine("using System.Drawing.Drawing2D;");
			writer.WriteLine("using System.Drawing.Imaging;");
			writer.WriteLine("using System.IO;");
			writer.WriteLine("using System.Reflection;");
			writer.WriteLine("using System.Text;");
			writer.WriteLine("using System.Windows.Media.Imaging;");
			writer.WriteLine("using System.Xml.Linq;");
			writer.WriteLine("using System.Xml.XPath;");
			writer.WriteLine("using Fasterflect;");
			writer.WriteLine();
		}
		//===========================================================================================
		private void WriteValue(IndentedTextWriter writer, PropertyInfo property)
		{
			ParameterInfo parameter = property.SetMethod.GetParameters()[0];
			if (parameter.ParameterType.IsGenericType)
			{
				Type genericArgument = parameter.ParameterType.GetGenericArguments()[0];

				if (parameter.ParameterType.Name.StartsWith("Nullable", StringComparison.Ordinal))
				{
					writer.Write("value == null ? Types.Nullable");
					WriteType(writer, genericArgument);
					writer.Write(".CreateInstance() : Types.Nullable");
					WriteType(writer, genericArgument);
					writer.Write(".CreateInstance(new Type[] { ");
					if (genericArgument == typeof(int))
					{
						writer.Write("typeof(Int32)");
					}
					else
					{
						writer.Write("Types.");
						WriteType(writer, genericArgument);
					}
					writer.Write(" }, value.Value)");
				}
				else
				{
					throw new NotImplementedException();
				}
			}
			else if (_Types.Contains(parameter.ParameterType))
			{
				WriteType(writer, parameter.ParameterType);
				writer.Write(".GetInstance(");
				writer.Write("value)");
			}
			else
			{
				writer.Write("value");
			}
		}
		//===========================================================================================
		private void Generate(Type type)
		{
			using (IndentedTextWriter writer = CreateWriter(type.Name + ".cs"))
			{
				WriteHeader(writer);
				WriteUsing(writer);
				WriteStartNamespace(writer);
				WriteClassStart(writer, type);
				WriteHelperMethods(writer, type);
				WriteConstructors(writer, type);
				WriteEvents(writer, type);
				WriteOperators(writer, type);
				WriteProperties(writer, type);
				WriteMethods(writer, type);
				WriteEndColon(writer);
				WriteEndColon(writer);
				Close(writer);
			}
		}
		//===========================================================================================
		private static void Generate(QuantumDepth depth)
		{
			ClassGenerator generator = new ClassGenerator(depth);
			foreach (Type type in generator._Types)
			{
				if (!MagickNET.IsQuantumDependant(type))
					continue;

				generator.Generate(type);
				generator.CopyDocumentation();
			}
		}
		//===========================================================================================
		public static void Generate()
		{
			ClassGenerator generator = new ClassGenerator();
			foreach (Type type in generator._Types)
			{
				if (!MagickNET.IsQuantumDependant(type))
					generator.Generate(type);
			}

			Generate(QuantumDepth.Q8);
			Generate(QuantumDepth.Q16);
			Generate(QuantumDepth.Q16HDRI);
		}
		//===========================================================================================
	}
	//==============================================================================================
}
