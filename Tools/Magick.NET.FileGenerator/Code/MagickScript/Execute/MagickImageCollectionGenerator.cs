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
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Magick.NET.FileGenerator
{
	//==============================================================================================
	internal sealed class MagickImageCollectionGenerator : ExecuteCodeGenerator
	{
		//===========================================================================================
		private bool ReturnsImage(MethodBase method)
		{
			return ((MethodInfo)method).ReturnType.Name == "MagickImage";
		}
		//===========================================================================================
		protected override string ExecuteArgument
		{
			get
			{
				return "MagickImageCollection^ collection";
			}
		}
		//===========================================================================================
		protected override string ExecuteName
		{
			get
			{
				return "Collection";
			}
		}
		//===========================================================================================
		protected override IEnumerable<MethodBase[]> Methods
		{
			get
			{
				return MagickNET.GetGroupedMagickImageCollectionMethods().
							Concat(MagickNET.GetGroupedMagickImageCollectionResultMethods());
			}
		}
		//===========================================================================================
		protected override string ReturnType
		{
			get
			{
				return "MagickImage^";
			}
		}
		//===========================================================================================
		protected override void WriteCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters)
		{
			if (ReturnsImage(method))
				writer.Write("return ");

			writer.Write("collection->");
			writer.Write(method.Name);
			writer.Write("(");
			WriteParameters(writer, parameters);
			writer.WriteLine(");");

			if (!ReturnsImage(method))
				writer.WriteLine("return nullptr;");
		}
		//===========================================================================================
		protected override void WriteHashtableCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters)
		{
			bool returnsImage = ReturnsImage(method);

			if (returnsImage)
				writer.Write("return ");
			else
				WriteStartColon(writer);

			writer.Write("collection->");
			writer.Write(method.Name);
			writer.Write("(");
			WriteHashtableParameters(writer, parameters);
			writer.WriteLine(");");

			if (!ReturnsImage(method))
			{
				writer.WriteLine("return nullptr;");
				WriteEndColon(writer);
			}
		}
		//===========================================================================================
		protected override void WriteSet(IndentedTextWriter writer, PropertyInfo property)
		{
			throw new NotImplementedException();
		}
	}
	//==============================================================================================
}
