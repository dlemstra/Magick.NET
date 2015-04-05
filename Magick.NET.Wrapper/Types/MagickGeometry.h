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

using namespace System::Drawing;

namespace ImageMagick
{
	namespace Wrapper
	{
		//===========================================================================================
		private ref class MagickGeometry sealed
		{
			//========================================================================================
		private:
			//========================================================================================
			void Initialize(Magick::Geometry geometry);
			//========================================================================================
		internal:
			//========================================================================================
			MagickGeometry(Magick::Geometry geometry);
			//========================================================================================
			const Magick::Geometry* CreateGeometry();
			//========================================================================================
		public:
			//========================================================================================
			MagickGeometry(int x, int y, int width, int height, bool isPercentage);
			//========================================================================================
			MagickGeometry(String^ value);
			//========================================================================================
			property bool FillArea;
			//========================================================================================
			property bool Greater;
			//========================================================================================
			property int Height;
			//========================================================================================
			property bool IgnoreAspectRatio;
			//========================================================================================
			property bool IsPercentage;
			//========================================================================================
			property bool Less;
			//========================================================================================
			property bool LimitPixels;
			//========================================================================================
			property int Width;
			//========================================================================================
			property int X;
			//========================================================================================
			property int Y;
			//========================================================================================
			virtual String^ ToString() override;
			//========================================================================================
		};
	}
}