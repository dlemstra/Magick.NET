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

namespace Magick.NET.FileGenerator
{
	//==============================================================================================
	internal class Program
	{
		//===========================================================================================
		internal static void Main(string[] args)
		{
			bool generateMagickScript = (args.Length == 0);
			bool generateAnyCPU = (args.Length == 0) || (args[0] == "--AnyCPU");

			if (generateMagickScript)
			{
				XsdGenerator.Generate();
				MagickScriptGenerator.Generate();
			}

			if (generateAnyCPU)
				AnyCPUGenerator.Generate();
		}
	}
	//==============================================================================================
}
