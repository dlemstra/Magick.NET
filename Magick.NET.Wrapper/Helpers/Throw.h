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
#pragma once

namespace ImageMagick
{
	namespace Wrapper
	{
		//===========================================================================================
		private ref class Throw abstract sealed
		{
		private:
			//========================================================================================
			static String^ FormatMessage(String^ message, ... array<Object^>^ args);
			//========================================================================================
		public:
			//========================================================================================
			static void IfFalse(String^ paramName, bool condition, String^ message, ... array<Object^>^ args);
			//========================================================================================
			static void IfInvalidFileName(String^ fileName);
			//========================================================================================
			static void IfNull(String^ paramName, Object^ value);
			//========================================================================================
			static void IfNull(String^ paramName, Object^ value, String^ message, ... array<Object^>^ args);
			//========================================================================================
			static void IfNullOrEmpty(String^ paramName, Array^ value);
			//========================================================================================
			static void IfNullOrEmpty(String^ paramName, String^ value);
			//========================================================================================
			static void IfNullOrEmpty(String^ paramName, String^ value, String^ message, ... array<Object^>^ args);
			//========================================================================================
			static void IfOutOfRange(String^ paramName, int index, int length);
			//========================================================================================
			static void IfTrue(String^ paramName, bool condition, String^ message, ... array<Object^>^ args);
			//========================================================================================
		};
		//===========================================================================================
	}
}