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
#pragma once

#include "MagickImage.h"
#include "Enums\EvaluateOperator.h"
#include "Enums\LayerMethod.h"
#include "IO\MagickReader.h"
#include "MagickErrorInfo.h"
#include "Settings\MagickReadSettings.h"
#include "Settings\QuantizeSettings.h"

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
		List<MagickImage^>^ _Images;
		MagickWarningException^ _ReadWarning;
		EventHandler<WarningEventArgs^>^ _WarningEvent;
		//===========================================================================================
		!MagickImageCollection() 
		{ 
			Clear();
		}
		//===========================================================================================
		MagickImage^ Append(bool vertically);
		//===========================================================================================
		void CopyFrom(std::list<Magick::Image>* images);
		//===========================================================================================
		void CopyTo(std::list<Magick::Image>* images);
		//===========================================================================================
		void HandleException(const Magick::Exception& exception);
		//===========================================================================================
		void HandleException(MagickException^ exception);
		//===========================================================================================
		void HandleReadException(MagickException^ exception);
		//===========================================================================================
		void Optimize(LayerMethod optizeMethod);
		//===========================================================================================
		MagickImage^ Smush(bool vertically, int offset);
		//===========================================================================================
	internal:
		//===========================================================================================
		static List<MagickImage^>^ CreateList(std::list<Magick::Image>* images);
		//===========================================================================================
		void Merge(Magick::Image* image, LayerMethod method);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImage class.
		///</summary>
		MagickImageCollection();
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImageCollection class using the specified byte array.
		///</summary>
		///<param name="data">The byte array to read the image data from.</param>
		///<exception cref="MagickException"/>
		MagickImageCollection(array<Byte>^ data);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImageCollection class using the specified byte array.
		///</summary>
		///<param name="data">The byte array to read the image data from.</param>
		///<param name="readSettings">The settings to use when reading the image.</param>
		///<exception cref="MagickException"/>
		MagickImageCollection(array<Byte>^ data, MagickReadSettings^ readSettings);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImage class using the specified images.
		///</summary>
		///<param name="images">The images to add to the collection.</param>
		///<exception cref="MagickException"/>
		MagickImageCollection(IEnumerable<MagickImage^>^ images);
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
			virtual MagickImage^ get(int index) sealed;
			virtual void set(int index, MagickImage^ value) sealed;
		}
		///==========================================================================================
		///<summary>
		/// Returns the number of images in the collection.
		///</summary>
		property int Count 
		{
			virtual int get() sealed;
		}
		///==========================================================================================
		///<summary>
		/// Returns the number of images in the collection.
		///</summary>
		property bool IsReadOnly
		{
			virtual bool get() sealed;
		}
		///==========================================================================================
		///<summary>
		/// Returns the warning that was raised during the read operation.
		///</summary>
		property MagickWarningException^ ReadWarning
		{
			MagickWarningException^ get();
		}
		//===========================================================================================
		static explicit operator array<Byte>^ (MagickImageCollection^ collection)
		{
			Throw::IfNull("collection", collection);

			return collection->ToByteArray();
		}
		///==========================================================================================
		///<summary>
		/// Event that will we raised when a warning is thrown by ImageMagick.
		///</summary>
		event EventHandler<WarningEventArgs^>^ Warning
		{
			void add(EventHandler<WarningEventArgs^>^ handler);
			void remove(EventHandler<WarningEventArgs^>^ handler);
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
		/// Creates a single image, by appending all the images in the collection horizontally.
		///</summary>
		///<exception cref="MagickException"/>
		MagickImage^ AppendHorizontally();
		///==========================================================================================
		///<summary>
		/// Creates a single image, by appending all the images in the collection vertically.
		///</summary>
		///<exception cref="MagickException"/>
		MagickImage^ AppendVertically();
		///==========================================================================================
		///<summary>
		/// Removes all images from the collection.
		///</summary>
		virtual void Clear();
		///==========================================================================================
		///<summary>
		/// Merge a sequence of images. This is useful for GIF animation sequences that have page
		/// offsets and disposal methods
		///</summary>
		///<exception cref="MagickException"/>
		void Coalesce();
		///==========================================================================================
		///<summary>
		/// Determines whether the collection contains the specified image.
		///</summary>
		///<param name="item">The image to check.</param>
		virtual bool Contains(MagickImage^ item);
		///==========================================================================================
		///<summary>
		/// Combines one or more images into a single image. The typical ordering would be
		/// image 1 => Red, 2 => Green, 3 => Blue, etc.
		///</summary>
		MagickImage^ Combine();
		///==========================================================================================
		///<summary>
		/// Combines one or more images into a single image. The grayscale value of the pixels of each
		/// image in the sequence is assigned in order to the specified channels of the combined image.
		/// The typical ordering would be image 1 => Red, 2 => Green, 3 => Blue, etc.
		///</summary>
		///<param name="channels">The channel(s) to combine.</param>
		///<exception cref="MagickException"/>
		MagickImage^ Combine(Channels channels);
		///==========================================================================================
		///<summary>
		/// Copies the images to an Array, starting at a particular Array index.
		///</summary>
		///<param name="destination">The one-dimensional Array that is the destination.</param>
		///<param name="arrayIndex">The zero-based index in 'destination' at which copying begins.</param>
		virtual void CopyTo(array<MagickImage^>^ destination, int arrayIndex);
		///==========================================================================================
		///<summary>
		/// Break down an image sequence into constituent parts. This is useful for creating GIF or
		/// MNG animation sequences.
		///</summary>
		///<exception cref="MagickException"/>
		void Deconstruct();
		///==========================================================================================
		///<summary>
		/// Evaluate image pixels into a single image. All the images in the collection must be the
		/// same size in pixels.
		///</summary>
		///<param name="evaluateOperator">The operator.</param>
		///<exception cref="MagickException"/>
		MagickImage^ Evaluate(EvaluateOperator evaluateOperator);
		///==========================================================================================
		///<summary>
		/// Flatten this collection into a single image.
		/// This is useful for combining Photoshop layers into a single image.
		///</summary>
		///<exception cref="MagickException"/>
		MagickImage^ Flatten();
		///==========================================================================================
		///<summary>
		/// Applies a mathematical expression to the images.
		///</summary>
		///<param name="expression">The expression to apply.</param>
		///<exception cref="MagickException"/>
		MagickImage^ Fx(String^ expression);
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
		/// Merge this collection into a single image.
		/// This is useful for combining Photoshop layers into a single image.
		///</summary>
		///<exception cref="MagickException"/>
		MagickImage^ Merge();
		///==========================================================================================
		///<summary>
		/// The Morph method requires a minimum of two images. The first image is transformed into
		/// the second by a number of intervening images as specified by frames.
		///</summary>
		///<param name="frames">The number of in-between images to generate.</param>
		///<exception cref="MagickException"/>
		MagickImageCollection^ Morph(int frames);
		///==========================================================================================
		///<summary>
		/// Inlay the images to form a single coherent picture.
		///</summary>
		///<exception cref="MagickException"/>
		MagickImage^ Mosaic();
		///==========================================================================================
		///<summary>
		/// Compares each image the GIF disposed forms of the previous image in the sequence. From
		/// this it attempts to select the smallest cropped image to replace each frame, while
		/// preserving the results of the GIF animation.
		///</summary>
		///<exception cref="MagickException"/>
		void Optimize();
		///==========================================================================================
		///<summary>
		/// OptimizePlus is exactly as Optimize, but may also add or even remove extra frames in the
		/// animation, if it improves the total number of pixels in the resulting GIF animation.
		///</summary>
		///<exception cref="MagickException"/>
		void OptimizePlus();
		///==========================================================================================
		///<summary>
		/// Quantize images (reduce number of colors).
		///</summary>
		///<param name="settings">Quantize settings.</param>
		///<exception cref="MagickException"/>
		MagickErrorInfo^ Quantize(QuantizeSettings^ settings);
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
		///<exception cref="MagickException"/>
		void RePage();
		///==========================================================================================
		///<summary>
		/// Reverses the order of the images in the collection.
		///</summary>
		void Reverse();
		///==========================================================================================
		///<summary>
		/// Smush images from list into single image in horizontal direction.
		///</summary>
		///<param name="offset">Minimum distance in pixels between images.</param>
		///<exception cref="MagickException"/>
		MagickImage^ SmushHorizontal(int offset);
		///==========================================================================================
		///<summary>
		/// Smush images from list into single image in vertical direction.
		///</summary>
		///<param name="offset">Minimum distance in pixels between images.</param>
		///<exception cref="MagickException"/>
		MagickImage^ SmushVertical(int offset);
		///==========================================================================================
		///<summary>
		/// Converts this instance to a byte array.
		///</summary>
		array<Byte>^ ToByteArray();
		///==========================================================================================
		///<summary>
		/// Merge this collection into a single image.
		/// This is useful for combining Photoshop layers into a single image.
		///</summary>
		///<exception cref="MagickException"/>
		MagickImage^ TrimBounds();
		///==========================================================================================
		///<summary>
		/// Writes the imagse to the specified stream. If the output image's file format does not
		/// allow multi-image files multiple files will be written.
		///</summary>
		///<param name="stream">The stream to write the images to.</param>
		///<exception cref="MagickException"/>
		void Write(Stream^ stream);
		///==========================================================================================
		///<summary>
		/// Writes the images to the specified file name. If the output image's file format does not
		/// allow multi-image files multiple files will be written.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<exception cref="MagickException"/>
		void Write(String^ fileName);
		//===========================================================================================
	};
	//==============================================================================================
}