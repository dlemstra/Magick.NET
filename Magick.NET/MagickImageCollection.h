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
	///=============================================================================================
	///<summary>
	/// Represents the collection of images.
	///</summary>
	public ref class MagickImageCollection sealed : IList<MagickImage^>
	{
		//===========================================================================================
	private:
		//===========================================================================================
		std::list<Magick::Image>* _Images;
		String^ _ReadWarning;
		//===========================================================================================
		MagickImageCollection();
		//===========================================================================================
		!MagickImageCollection() 
		{ 
			if (_Images == NULL)
				return;

			delete _Images;
			_Images = NULL;
		}
		//===========================================================================================
		ref class MagickImageCollectionEnumerator sealed : IEnumerator<MagickImage^>
		{
			//========================================================================================
		private:
			MagickImageCollection^ _Collection;
			int _Index;
			//========================================================================================
		public:
			MagickImageCollectionEnumerator(MagickImageCollection^ collection)
			{
				_Collection = collection;
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

					std::list<Magick::Image>::iterator iter = _Collection->_Images->begin();
					std::advance(iter, _Index);

					return gcnew MagickImage(*iter);
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
			virtual bool MoveNext();
			//========================================================================================
			virtual void Reset();
			//========================================================================================
		};
		//===========================================================================================
	public:
		//===========================================================================================
		~MagickImageCollection()
		{
			this->!MagickImageCollection();
		}
		///==========================================================================================
		///<summary>
		/// Gets or sets the image at the specified index.
		///</summary>
		property MagickImage^ default[int]
		{
			virtual MagickImage^ get(int index) sealed
			{
				if (index < 0 || index > (int)_Images->size() - 1)
					return nullptr;

				std::list<Magick::Image>::const_iterator iter = _Images->begin();
				std::advance(iter, index);

				return gcnew MagickImage(*iter);
			}
			virtual void set(int index, MagickImage^ value) sealed
			{
				if (index < 0 || index >= (int)_Images->size() || value == nullptr)
					return;

				std::list<Magick::Image>::iterator iter = _Images->begin();
				std::advance(iter, index);

				_Images->erase(iter);

				Insert(index, value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Returns the number of images in the collection.
		///</summary>
		property int Count 
		{
			virtual int get() sealed
			{
				return (int)_Images->size();
			}
		}
		///==========================================================================================
		///<summary>
		/// Returns the number of images in the collection.
		///</summary>
		property bool IsReadOnly
		{
			virtual bool get() sealed
			{
				return false;
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
		/// Adds an image to the collection.
		///</summary>
		///<param name="item">The image to add.</param>
		virtual void Add(MagickImage^ item);
		///==========================================================================================
		///<summary>
		/// Removes all images from the collection.
		///</summary>
		virtual void Clear();
		///==========================================================================================
		///<summary>
		/// Determines whether the collection contains the specified image.
		///</summary>
		///<param name="item">The image to check.</param>
		virtual bool Contains(MagickImage^ item);
		///==========================================================================================
		///<summary>
		/// Copies the images to an Array, starting at a particular Array index.
		///</summary>
		///<param name="destination">The one-dimensional Array that is the destination.</param>
		///<param name="arrayIndex">The zero-based index in 'destination' at which copying begins.</param>
		virtual void CopyTo(array<MagickImage^>^ destination, int arrayIndex);
		///==========================================================================================
		///<summary>
		/// Returns an enumerator that can iterate through the collection.
		///</summary>
		virtual IEnumerator<MagickImage^>^ GetEnumerator();
		///==========================================================================================
		///<summary>
		/// Returns an enumerator that can iterate through the collection.
		///</summary>
		virtual System::Collections::IEnumerator^ GetEnumerator2() = System::Collections::IEnumerable::GetEnumerator;
		///==========================================================================================
		///<summary>
		/// Determines the index of the specified image.
		///</summary>
		///<param name="item">The image to check.</param>
		virtual int IndexOf(MagickImage^ item);
		///==========================================================================================
		///<summary>
		/// Inserts an image into the collection.
		///</summary>
		///<param name="index">The index to add insert the image.</param>
		///<param name="item">The image to insert.</param>
		virtual void Insert(int index, MagickImage^ item);
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
		///==========================================================================================
		///<summary>
		/// Removes the first occurrence of the specified image from the collection.
		///</summary>
		///<param name="item">The image to remove.</param>
		virtual bool Remove(MagickImage^ item);
		///==========================================================================================
		///<summary>
		/// Removes the image at the specified index from the collection.
		///</summary>
		///<param name="index">The index of the image to remove.</param>
		virtual void RemoveAt(int index);
		//===========================================================================================
	};
	//==============================================================================================
}