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
#include "Enums\LayerMethod.h"
#include "Helpers\MagickReader.h"
#include "Helpers\MagickReadSettings.h"

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
		MagickWarningException^ _ReadWarning;
		//===========================================================================================
		!MagickImageCollection() 
		{ 
			if (_Images == NULL)
				return;

			delete _Images;
			_Images = NULL;
		}
		//===========================================================================================
		property std::list<Magick::Image>* Images
		{
			std::list<Magick::Image>* get()
			{
				if (_Images == NULL)
					throw gcnew ObjectDisposedException(GetType()->ToString());

				return _Images;
			}
		}
		//===========================================================================================
		void InsertUnchecked(int index, MagickImage^ image);
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

					std::list<Magick::Image>::iterator iter = _Collection->Images->begin();
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
	internal:
		//===========================================================================================
		void Merge(Magick::Image* mergedImage, LayerMethod layerMethod);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImage class.
		///</summary>
		MagickImageCollection();
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImage class using the specified images.
		///</summary>
		MagickImageCollection(IEnumerable<MagickImage^>^ images);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImageCollection class using the specified blob.
		///</summary>
		///<param name="data">The byte array to read the image data from.</param>
		///<exception cref="MagickException"/>
		MagickImageCollection(array<Byte>^ data);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImageCollection class using the specified blob.
		///</summary>
		///<param name="data">The byte array to read the image data from.</param>
		///<param name="readSettings">The settings to use when reading the image.</param>
		///<exception cref="MagickException"/>
		MagickImageCollection(array<Byte>^ data, MagickReadSettings^ readSettings);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImageCollection class using the specified filename.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<exception cref="MagickException"/>
		MagickImageCollection(String^ fileName);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImageCollection class using the specified filename
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<param name="readSettings">The settings to use when reading the image.</param>
		///<exception cref="MagickException"/>
		MagickImageCollection(String^ fileName, MagickReadSettings^ readSettings);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImageCollection class using the specified stream.
		///</summary>
		///<param name="stream">The stream to read the image data from.</param>
		///<exception cref="MagickException"/>
		MagickImageCollection(Stream^ stream);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImageCollection class using the specified stream.
		///</summary>
		///<param name="stream">The stream to read the image data from.</param>
		///<param name="readSettings">The settings to use when reading the image.</param>
		///<exception cref="MagickException"/>
		MagickImageCollection(Stream^ stream, MagickReadSettings^ readSettings);
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
				if (index < 0 || index > (int)Images->size() - 1)
					return nullptr;

				std::list<Magick::Image>::const_iterator iter = Images->begin();
				std::advance(iter, index);

				return gcnew MagickImage(*iter);
			}
			virtual void set(int index, MagickImage^ value) sealed
			{
				if (index < 0 || index >= (int)Images->size() || value == nullptr)
					return;

				std::list<Magick::Image>::iterator iter = Images->begin();
				std::advance(iter, index);

				Images->erase(iter);

				InsertUnchecked(index, value);
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
				return (int)Images->size();
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
		/// Returns the warning that was raised during the read operation.
		///</summary>
		property MagickWarningException^ ReadWarning
		{
			MagickWarningException^ get()
			{
				return _ReadWarning;
			}
		}
		//===========================================================================================
		static explicit operator array<Byte>^ (MagickImageCollection^ collection)
		{
			Throw::IfNull("collection", collection);

			return collection->ToByteArray();
		}
		///==========================================================================================
		///<summary>
		/// Adds an image to the collection.
		///</summary>
		///<param name="item">The image to add.</param>
		virtual void Add(MagickImage^ item);
		///==========================================================================================
		///<summary>
		/// Adds an image with the specified file name to the collection.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<exception cref="MagickException"/>
		void Add(String^ fileName);
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
		/// Merge this collection into a single image.
		/// This is useful for combining Photoshop layers into a single image.
		///</summary>
		///<param name="layerMethod">The layer method to use.</param>
		///<exception cref="MagickException"/>
		MagickImage^ Merge(LayerMethod layerMethod);
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
		///<param name="index">The index to insert the image.</param>
		///<param name="item">The image to insert.</param>
		virtual void Insert(int index, MagickImage^ item);
		///==========================================================================================
		///<summary>
		/// Inserts an image with the specified file name into the collection.
		///</summary>
		///<param name="index">The index to insert the image.</param>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		virtual void Insert(int index, String^ fileName);
		///==========================================================================================
		///<summary>
		/// Read all image frames.
		///</summary>
		///<param name="data">The byte array to read the image data from.</param>
		///<returns>If a warning was raised while reading the image that warning will be returned.</returns>
		///<exception cref="MagickException"/>
		MagickWarningException^ Read(array<Byte>^ data);
		///==========================================================================================
		///<summary>
		/// Read all image frames.
		///</summary>
		///<param name="data">The byte array to read the image data from.</param>
		///<param name="readSettings">The settings to use when reading the image.</param>
		///<returns>If a warning was raised while reading the image that warning will be returned.</returns>
		///<exception cref="MagickException"/>
		MagickWarningException^ Read(array<Byte>^ data, MagickReadSettings^ readSettings);
		///==========================================================================================
		///<summary>
		/// Read all image frames.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<returns>If a warning was raised while reading the image that warning will be returned.</returns>
		///<exception cref="MagickException"/>
		MagickWarningException^ Read(String^ fileName);
		///==========================================================================================
		///<summary>
		/// Read all image frames.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<param name="readSettings">The settings to use when reading the image.</param>
		///<returns>If a warning was raised while reading the image that warning will be returned.</returns>
		///<exception cref="MagickException"/>
		MagickWarningException^ Read(String^ fileName, MagickReadSettings^ readSettings);
		///==========================================================================================
		///<summary>
		/// Read all image frames.
		///</summary>
		///<param name="stream">The stream to read the image data from.</param>
		///<returns>If a warning was raised while reading the image that warning will be returned.</returns>
		///<exception cref="MagickException"/>
		MagickWarningException^ Read(Stream^ stream);
		///==========================================================================================
		///<summary>
		/// Read all image frames.
		///</summary>
		///<param name="stream">The stream to read the image data from.</param>
		///<param name="readSettings">The settings to use when reading the image.</param>
		///<returns>If a warning was raised while reading the image that warning will be returned.</returns>
		///<exception cref="MagickException"/>
		MagickWarningException^ Read(Stream^ stream, MagickReadSettings^ readSettings);
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
		///==========================================================================================
		///<summary>
		/// Resets the page property of every image in the collection.
		///</summary>
		void RePage();
		///==========================================================================================
		///<summary>
		/// Converts this instance to a byte array.
		///</summary>
		array<Byte>^ ToByteArray();
		///==========================================================================================
		///<summary>
		/// Writes the imagse to the specified stream. If the output image's file format does not
		/// allow multi-image files multiple files will be written.
		///</summary>
		///<param name="stream">The stream to write the images to.</param>
		void Write(Stream^ stream);
		///==========================================================================================
		///<summary>
		/// Writes the images to the specified file name. If the output image's file format does not
		/// allow multi-image files multiple files will be written.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		void Write(String^ fileName);
		//===========================================================================================
	};
	//==============================================================================================
}