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
using System.Collections;
using System.Collections.Generic;

namespace ImageMagick
{
	//==============================================================================================
	internal sealed class PixelCollectionEnumerator : IEnumerator<Pixel>
	{
		//===========================================================================================
		private Wrapper.PixelBaseCollection _Collection;
		private Wrapper.WritablePixelCollection _WritableCollection;
		private int _X;
		private int _Y;
		//===========================================================================================
		internal PixelCollectionEnumerator(Wrapper.PixelBaseCollection collection)
		{
			_Collection = collection;
			_WritableCollection = collection as Wrapper.WritablePixelCollection;
			Reset();
		}
		//===========================================================================================
		object IEnumerator.Current
		{
			get
			{
				return Current;
			}
		}
		//===========================================================================================
		public Pixel Current
		{
			get
			{
				if (_X == -1)
					return null;

				return Pixel.Create(_WritableCollection, _X, _Y, _Collection.GetValue(_X, _Y));
			}
		}
		//===========================================================================================
		public void Dispose()
		{
		}
		//===========================================================================================
		public bool MoveNext()
		{
			if (++_X == _Collection.Width)
			{
				_X = 0;
				_Y++;
			}

			if (_Y < _Collection.Height)
				return true;

			_X = _Collection.Width - 1;
			_Y = _Collection.Height - 1;
			return false;
		}
		//===========================================================================================
		public void Reset()
		{
			_X = -1;
			_Y = 0;
		}
		//===========================================================================================
	}
	//==============================================================================================
}