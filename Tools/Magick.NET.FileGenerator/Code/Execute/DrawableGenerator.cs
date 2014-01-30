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
	internal sealed class DrawableGenerator : ExecuteCodeGenerator
	{
		//===========================================================================================
		protected override string ExecuteArgument
		{
			get
			{
				return "System::Collections::ObjectModel::Collection<Drawable^>^ drawables";
			}
		}
		//===========================================================================================
		protected override string ExecuteName
		{
			get
			{
				return "Drawable";
			}
		}
		//===========================================================================================
		protected override IEnumerable<MethodBase[]> Methods
		{
			get
			{
				return MagickNET.GetDrawables();
			}
		}
		//===========================================================================================
		protected override void WriteCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters)
		{
			writer.Write("drawables->Add(gcnew ");
			writer.Write(method.DeclaringType.Name);
			writer.Write("(");
			WriteParameters(writer, parameters);
			writer.WriteLine("));");
		}
		//===========================================================================================
		protected override void WriteHashtableCall(IndentedTextWriter writer, MethodBase method, ParameterInfo[] parameters)
		{
			writer.Write("drawables->Add(gcnew ");
			writer.Write(method.DeclaringType.Name);
			writer.Write("(");
			WriteHashtableParameters(writer, parameters);
			writer.WriteLine("));");
		}
		//===========================================================================================
		public override void WriteIncludes(IndentedTextWriter writer)
		{
			base.WriteIncludes(writer);

			foreach (string drawable in from constructor in Methods
												 select constructor.First().DeclaringType.Name)
			{
				writer.Write(@"#include ""..\..\Drawables\");
				writer.Write(drawable);
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
