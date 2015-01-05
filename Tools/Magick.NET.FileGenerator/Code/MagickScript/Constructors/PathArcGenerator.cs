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
using System.Reflection;

namespace Magick.NET.FileGenerator
{
	//==============================================================================================
	internal sealed class PathArcGenerator : ConstructorCodeGenerator
	{
		//===========================================================================================
		protected override string ClassName
		{
			get
			{
				return "PathArc";
			}
		}
		//===========================================================================================
		protected override void WriteCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters)
		{
			writer.Write("return gcnew ");
			writer.Write(method.DeclaringType.Name);
			writer.Write("(");
			WriteParameters(writer, parameters);
			writer.WriteLine(");");
		}
		//===========================================================================================
		protected override void WriteHashtableCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters)
		{
			writer.Write("return gcnew ");
			writer.Write(method.DeclaringType.Name);
			writer.Write("(");
			WriteHashtableParameters(writer, parameters);
			writer.WriteLine(");");
		}
		//===========================================================================================
	}
	//==============================================================================================
}
