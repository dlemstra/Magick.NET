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
	internal sealed class PathsGenerator : ExecuteCodeGenerator
	{
		//===========================================================================================
		protected override string ExecuteArgument
		{
			get
			{
				return "System::Collections::ObjectModel::Collection<PathBase^>^ paths";
			}
		}
		//===========================================================================================
		protected override string ExecuteName
		{
			get
			{
				return "Path";
			}
		}
		//===========================================================================================
		protected override IEnumerable<MethodBase[]> Methods
		{
			get
			{
				return MagickNET.GetPaths();
			}
		}
		//===========================================================================================
		protected override void WriteCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters)
		{
			writer.Write("paths->Add(gcnew ");
			writer.Write(method.DeclaringType.Name);
			writer.Write("(");
			WriteParameters(writer, parameters);

			writer.WriteLine("));");
		}
		//===========================================================================================
		protected override void WriteHashtableCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters)
		{
			writer.Write("paths->Add(gcnew ");
			writer.Write(method.DeclaringType.Name);
			writer.Write("(");
			WriteHashtableParameters(writer, parameters);
			writer.WriteLine("));");
		}
		//===========================================================================================
		public override void WriteIncludes(IndentedTextWriter writer)
		{
			base.WriteIncludes(writer);

			foreach (string pathBase in from constructor in Methods
												 select constructor.First().DeclaringType.Name)
			{
				writer.Write(@"#include ""..\..\Drawables\Paths\");
				writer.Write(pathBase);
				writer.WriteLine(@".h""");
			}
		}
		//===========================================================================================
		protected override void WriteSet(IndentedTextWriter writer, PropertyInfo property)
		{
			throw new NotImplementedException();
		}
		//===========================================================================================
	}
	//==============================================================================================
}
