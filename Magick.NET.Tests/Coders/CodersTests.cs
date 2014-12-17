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

using System.Drawing;
using ImageMagick;

namespace Magick.NET.Tests.Coders
{
	//==============================================================================================
	public abstract class CodersTests
	{
		//===========================================================================================
#if NET20
		private static string _Root = @"..\..\..\..\Magick.NET.Tests\Images\Coders\";
#else
		private static string _Root = @"..\..\..\Magick.NET.Tests\Images\Coders\";
#endif
		//===========================================================================================
		protected MagickImage LoadImage(string fileName)
		{
			return new MagickImage(_Root + fileName);
		}
		//===========================================================================================
	}
	//==============================================================================================
}