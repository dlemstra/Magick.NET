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
using System.IO;
using System.Linq;
using System.Reflection;

namespace Magick.NET.FileGenerator
{
	//==============================================================================================
	internal abstract class FileGenerator
	{
		//===========================================================================================
		private MagickNET _MagickNET;
		//===========================================================================================
		private string SetOutputFolder(string outputFolder)
		{
			string result = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\..\";
			result += outputFolder;
			if (result[result.Length - 1] != '\\')
				result += "\\";

			return result;
		}
		//===========================================================================================
		protected FileGenerator(string outputFolder)
		{
			OutputFolder = SetOutputFolder(outputFolder);
			_MagickNET = new MagickNET(QuantumDepth.Q16);
		}
		//===========================================================================================
		protected FileGenerator(string outputFolder, QuantumDepth depth)
		{
			OutputFolder = SetOutputFolder(outputFolder + "\\" + MagickNET.GetFolderName(depth));
			_MagickNET = new MagickNET(depth);
		}
		//===========================================================================================
		protected MagickNET MagickNET
		{
			get
			{
				return _MagickNET;
			}
		}
		//===========================================================================================
		protected string OutputFolder
		{
			get;
			private set;
		}
		//===========================================================================================
		protected static void Close(IndentedTextWriter writer)
		{
			writer.InnerWriter.Dispose();
		}
		//===========================================================================================
		protected IndentedTextWriter CreateWriter(string fileName)
		{
			string outputFile = Path.GetFullPath(OutputFolder + @"\" + fileName);
			Console.WriteLine("Creating: " + outputFile);

			FileStream output = File.Create(outputFile);
			StreamWriter streamWriter = new StreamWriter(output);
			IndentedTextWriter writer = new IndentedTextWriter(streamWriter, "\t");
			return writer;
		}
		//===========================================================================================
		protected static Type GetIEnumerable(ParameterInfo paramater)
		{
			if (!paramater.ParameterType.IsGenericType)
				return null;

			if (!paramater.ParameterType.Name.StartsWith("IEnumerable", StringComparison.Ordinal))
				return null;

			return paramater.ParameterType.GetGenericArguments()[0];
		}
		//===========================================================================================
		protected static Type GetNullable(ParameterInfo paramater)
		{
			if (!paramater.ParameterType.IsGenericType)
				return null;

			if (!paramater.ParameterType.Name.StartsWith("Nullable", StringComparison.Ordinal))
				return null;

			return paramater.ParameterType.GetGenericArguments()[0];
		}
		//===========================================================================================
		protected Type[] GetTypes()
		{
			return GetTypes(false);
		}
		//===========================================================================================
		protected Type[] GetTypes(bool withEnums)
		{
			return (from type in MagickNET.GetPublicTypes(withEnums)
					  where !type.IsSubclassOf(typeof(Exception))
					  orderby type.Name
					  select type).ToArray();
		}
		//===========================================================================================
		protected bool IsInternalInterface(Type type)
		{
			if (!type.IsInterface)
				return false;

			return MagickNET.Interfaces.Contains(type);
		}
		//===========================================================================================
		protected static bool IsUsedAsIEnumerable(Type[] types, Type type)
		{
			if ((from t in types
				  from m in t.GetMethods()
				  from p in m.GetParameters()
				  where GetIEnumerable(p) == type
				  select t).Count() > 0)
				return true;

			if ((from t in types
				  from c in t.GetConstructors()
				  from p in c.GetParameters()
				  where GetIEnumerable(p) == type
				  select t).Count() > 0)
				return true;

			return false;
		}
		//===========================================================================================
		protected static bool IsUsedAsNullable(Type[] types, Type type)
		{
			if ((from t in types
				  from m in t.GetMethods()
				  from p in m.GetParameters()
				  where GetNullable(p) == type
				  select t).Count() > 0)
				return true;

			if ((from t in types
				  from c in t.GetConstructors()
				  from p in c.GetParameters()
				  where GetNullable(p) == type
				  select t).Count() > 0)
				return true;

			return false;
		}
		//===========================================================================================
		protected static void WriteEndColon(IndentedTextWriter writer)
		{
			writer.Indent--;
			writer.WriteLine("}");
		}
		//===========================================================================================
		protected static void WriteHeader(IndentedTextWriter writer)
		{
			writer.WriteLine("//=================================================================================================");
			writer.WriteLine("// Copyright 2013-" + DateTime.Now.Year + " Dirk Lemstra <https://magick.codeplex.com/>");
			writer.WriteLine("//");
			writer.WriteLine("// Licensed under the ImageMagick License (the \"License\"); you may not use this file except in");
			writer.WriteLine("// compliance with the License. You may obtain a copy of the License at");
			writer.WriteLine("//");
			writer.WriteLine("//   http://www.imagemagick.org/script/license.php");
			writer.WriteLine("//");
			writer.WriteLine("// Unless required by applicable law or agreed to in writing, software distributed under the");
			writer.WriteLine("// License is distributed on an \"AS IS\" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either");
			writer.WriteLine("// express or implied. See the License for the specific language governing permissions and");
			writer.WriteLine("// limitations under the License.");
			writer.WriteLine("//=================================================================================================");
		}
		//===========================================================================================
		protected static void WriteStartColon(IndentedTextWriter writer)
		{
			writer.WriteLine("{");
			writer.Indent++;
		}
		//===========================================================================================
		protected static void WriteStartNamespace(IndentedTextWriter writer)
		{
			WriteStartNamespace(writer, null);
		}
		//===========================================================================================
		protected static void WriteStartNamespace(IndentedTextWriter writer, Type type)
		{
			if (type == null)
				writer.WriteLine("namespace ImageMagick");
			else
				writer.WriteLine("namespace " + type.Namespace);
			WriteStartColon(writer);
		}
		//===========================================================================================
		protected static void WriteType(IndentedTextWriter writer, Type type)
		{
			writer.Write(MagickNET.GetTypeName(type));
		}
		//===========================================================================================
	}
	//==============================================================================================
}
