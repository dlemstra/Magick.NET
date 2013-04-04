//=================================================================================================
// Copyright 2013 Dirk Lemstra <http://magick.codeplex.com/>
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

#include "MagickImage.h"
#include "Helpers\MagickReader.h"

using namespace System::Collections::Generic;

namespace ImageMagick
{
	//==============================================================================================
	public ref class MagickImageCollection sealed : IEnumerable<MagickImage^>
	{
		//===========================================================================================
	private:
		//===========================================================================================
		std::list<Magick::Image>* _Images;
		String^ _ReadWarning;
		//===========================================================================================
		MagickImageCollection();
		//===========================================================================================
		ref class MagickImageCollectionEnumerator sealed : IEnumerator<MagickImage^>
		{
			//========================================================================================
		private:
			int _Index;
			MagickImageCollection^ _List;
			//========================================================================================
		public:
			MagickImageCollectionEnumerator(MagickImageCollection^ list)
			{
				_List = list;
				_Index = -1;
			}
			//========================================================================================
			~MagickImageCollectionEnumerator()
			{
			}
			//========================================================================================
			property MagickImage^ Current
			{
				virtual MagickImage^ get() = IEnumerator<MagickImage^>::Current::get
				{
					if (_Index == -1)
						return nullptr;

					std::list<Magick::Image>::iterator iter = _List->_Images->begin();
					std::advance(iter, _Index);

					return gcnew MagickImage(&*(iter));
				}
			}
			//========================================================================================
			property Object^ Current2
			{
				virtual Object^ get() = System::Collections::IEnumerator::Current::get
				{
					return Current;
				}
			}
			//========================================================================================
			virtual bool MoveNext()
			{
				if (_Index + 1 == (int)_List->_Images->size())
					return false;

				_Index++;
				return true;
			}
			//========================================================================================
			virtual void Reset()
			{
				_Index = -1;
			}
			//========================================================================================
		};
		//===========================================================================================
	protected:
		//===========================================================================================
		~MagickImageCollection()
		{
			this->!MagickImageCollection();
		}
		//===========================================================================================
		!MagickImageCollection() 
		{ 
			if (_Images == NULL)
				return;

			delete _Images;
			_Images = NULL;
		}
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Returns the image at the specified index.
		///</summary>
		property MagickImage^ default[int]
		{
			MagickImage^ get(int index)
			{
				if (index < 0 || index > (int)_Images->size() - 1)
					return nullptr;

				std::list<Magick::Image>::iterator iter = _Images->begin();
				std::advance(iter, index);

				return gcnew MagickImage(&*(iter));
			}
		}
		///==========================================================================================
		///<summary>
		/// Returns the number of images in the collection.
		///</summary>
		property int Count
		{
			int get()
			{
				return (int)_Images->size();
			}
		}
		///==========================================================================================
		///<summary>
		/// Returns the warning that occurred during the read operation.
		///</summary>
		property String^ ReadWarning
		{
			String^ get()
			{
				return _ReadWarning;
			}
		}
		///==========================================================================================
		///<summary>
		/// Returns an enumerator that can iterate through the collection.
		///</summary>
		virtual IEnumerator<MagickImage^>^ GetEnumerator()
		{
			return gcnew MagickImageCollectionEnumerator(this);
		}
		///==========================================================================================
		///<summary>
		/// Returns an enumerator that can iterate through the collection.
		///</summary>
		virtual System::Collections::IEnumerator^ GetEnumerator2() = System::Collections::IEnumerable::GetEnumerator
		{
			return gcnew MagickImageCollectionEnumerator(this);
		}
		///==========================================================================================
		///<summary>
		/// Read all image frames.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<exception cref="MagickException"/>
		static MagickImageCollection^ Read(String^ fileName);
		///==========================================================================================
		///<summary>
		/// Read all image frames.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<param name="colorSpace">The colorspace to convert the image to.</param>
		///<exception cref="MagickException"/>
		static MagickImageCollection^ Read(String^ fileName, ColorSpace colorSpace);
		//===========================================================================================
	};
	//==============================================================================================
}