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
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;

namespace Magick.NET.FileGenerator
{
	//==============================================================================================
	internal sealed class MagickImageGenerator : ExecuteCodeGenerator
	{
		//===========================================================================================
		protected override string ExecuteArgument
		{
			get
			{
				return "MagickImage^ image";
			}
		}
		//===========================================================================================
		protected override string ExecuteName
		{
			get
			{
				return "Image";
			}
		}
		//===========================================================================================
		protected override IEnumerable<PropertyInfo> Properties
		{
			get
			{
				return MagickNET.GetMagickImageProperties();
			}
		}
		//===========================================================================================
		protected override IEnumerable<MethodBase[]> Methods
		{
			get
			{
				return MagickNET.GetGroupedMagickImageMethods();
			}
		}
		//===========================================================================================
		protected override void WriteCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters)
		{
			writer.Write("image->");
			writer.Write(method.Name);
			writer.Write("(");
			WriteParameters(writer, parameters);
			writer.WriteLine(");");
		}
		//===========================================================================================
		protected override void WriteHashtableCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters)
		{
			writer.Write("image->");
			writer.Write(method.Name);
			writer.Write("(");
			WriteHashtableParameters(writer, parameters);
			writer.WriteLine(");");
		}
		//===========================================================================================
		protected override void WriteInitializeExecute(IndentedTextWriter writer, bool isStatic)
		{
			base.WriteInitializeExecute(writer, isStatic);

			if (!isStatic)
			{
				WriteInitializeExecute(writer, "clone", "Clone", isStatic);
				WriteInitializeExecute(writer, "draw", "Draw", isStatic);
				WriteInitializeExecute(writer, "write", "Write", isStatic);
			}
		}
		//===========================================================================================
		protected override void WriteSet(IndentedTextWriter writer, PropertyInfo property)
		{
			writer.Write("image->");
			writer.Write(property.Name);
			writer.Write(" = ");
			WriteGetValue(writer, property);
		}
		//===========================================================================================
	}
	//==============================================================================================
}
