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
using System.Reflection;

namespace Magick.NET.FileGenerator
{
	//==============================================================================================
	internal sealed class ApplicationProxy : MarshalByRefObject
	{
		//===========================================================================================
		private Assembly ResolveAssembly(object sender, ResolveEventArgs args)
		{
			return Assembly.ReflectionOnlyLoad(args.Name);
		}
		//===========================================================================================
		public void GenerateXsd(QuantumDepth depth)
		{
			AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += ResolveAssembly;

			XsdGenerator generator = new XsdGenerator(depth);
			generator.Generate();
		}
		//===========================================================================================
		public void GenerateCode()
		{
			AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += ResolveAssembly;

			MagickScriptGenerator.Generate();
		}
	}
	//==============================================================================================
}
