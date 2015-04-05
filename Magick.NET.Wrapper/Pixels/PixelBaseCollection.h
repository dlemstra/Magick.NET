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
		private ref class PixelBaseCollection abstract
		{
			//========================================================================================
		private:
			//========================================================================================
			int _Channels;
			int _Height;
			Magick::Pixels* _View;
			int _Width;
			//========================================================================================
			!PixelBaseCollection();
			//========================================================================================
		protected private:
			//========================================================================================
			PixelBaseCollection(Magick::Image* image, int width, int height);
			//========================================================================================
			property const Magick::Quantum* Pixels
			{
				virtual const Magick::Quantum* get() abstract;
			}
			//========================================================================================
			property Magick::Pixels* View
			{
				Magick::Pixels* get();
			}
			//========================================================================================
			void CheckPixels();
			//========================================================================================
			int GetIndex(int x, int y);
			//========================================================================================
		public:
			//========================================================================================
			~PixelBaseCollection()
			{
				this->!PixelBaseCollection();
			}
			//========================================================================================
			property int Channels
			{
				int get();
			}
			//========================================================================================
			property int Height
			{
				int get();
			}
			//========================================================================================
			property int Width
			{
				int get();
			}
			//========================================================================================
			int GetIndex(PixelChannel channel);
			//========================================================================================
			QUANTUM_CLS_COMPLIANT array<Magick::Quantum>^ GetValue(int x, int y);
			//========================================================================================
			QUANTUM_CLS_COMPLIANT array<Magick::Quantum>^ GetValues();
			//========================================================================================
		};
		//===========================================================================================
	}
}