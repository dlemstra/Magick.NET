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
#include "Stdafx.h"
#include "MagickImageInfo.h"

using namespace System::Collections::ObjectModel;

namespace ImageMagick
{
	//==============================================================================================
	MagickReadSettings^ MagickImageInfo::CreateReadSettings()
	{
		MagickReadSettings^ settings = gcnew MagickReadSettings();
		settings->Ping = true;
		return settings;
	}
	//==============================================================================================
	IEnumerable<MagickImageInfo^>^ MagickImageInfo::Enumerate(std::list<Magick::Image>* images)
	{
		Collection<MagickImageInfo^>^ result = gcnew Collection<MagickImageInfo^>();

		for (std::list<Magick::Image>::iterator iter = images->begin(), end = images->end(); iter != end; ++iter)
		{
			MagickImageInfo^ info = gcnew MagickImageInfo();
			info->Initialize(&*iter);
			result->Add(info);
		}

		return result;
	}
	//==============================================================================================
	void MagickImageInfo::Initialize(Magick::Image* image)
	{
		try
		{
			_ColorSpace = (ImageMagick::ColorSpace)image->colorSpace();
			_Format = EnumHelper::Parse<MagickFormat>(Marshaller::Marshal(image->magick()), MagickFormat::Unknown);
			_FileName = Marshaller::Marshal(image->baseFilename());
			_Height = Convert::ToInt32(image->size().height());
			_ResolutionX = image->xResolution();
			_ResolutionY = image->yResolution();
			_Width = Convert::ToInt32(image->size().width());
		}
		catch(Magick::Exception& exception)
		{
			MagickException::Throw(exception);
		}
	}
	//==============================================================================================
	MagickImageInfo::MagickImageInfo(array<Byte>^ data)
	{
		Read(data);
	}
	//==============================================================================================
	MagickImageInfo::MagickImageInfo(String^ fileName)
	{
		Read(fileName);
	}
	//==============================================================================================
	MagickImageInfo::MagickImageInfo(Stream^ stream)
	{
		Read(stream);
	}
	//==============================================================================================
	ColorSpace MagickImageInfo::ColorSpace::get()
	{
		return _ColorSpace;
	}
	//==============================================================================================
	String^ MagickImageInfo::FileName::get()
	{
		return _FileName;
	}
	//==============================================================================================
	MagickFormat MagickImageInfo::Format::get()
	{
		return _Format;
	}
	//==============================================================================================
	int MagickImageInfo::Height::get()
	{
		return _Height;
	}
	//==============================================================================================
	double MagickImageInfo::ResolutionX::get()
	{
		return _ResolutionX;
	}
	//==============================================================================================
	double MagickImageInfo::ResolutionY::get()
	{
		return _ResolutionY;
	}
	//==============================================================================================
	int MagickImageInfo::Width::get()
	{
		return _Width;
	}
	//===========================================================================================
	bool MagickImageInfo::operator == (MagickImageInfo^ left, MagickImageInfo^ right)
	{
		return Object::Equals(left, right);
	}
	//===========================================================================================
	bool MagickImageInfo::operator != (MagickImageInfo^ left, MagickImageInfo^ right)
	{
		return !Object::Equals(left, right);
	}
	//===========================================================================================
	bool MagickImageInfo::operator > (MagickImageInfo^ left, MagickImageInfo^ right)
	{
		if (ReferenceEquals(left, nullptr))
			return ReferenceEquals(right, nullptr);

		return left->CompareTo(right) == 1;
	}
	//===========================================================================================
	bool MagickImageInfo::operator < (MagickImageInfo^ left, MagickImageInfo^ right)
	{
		if (ReferenceEquals(left, nullptr))
			return !ReferenceEquals(right, nullptr);

		return left->CompareTo(right) == -1;
	}
	//===========================================================================================
	bool MagickImageInfo::operator >= (MagickImageInfo^ left, MagickImageInfo^ right)
	{
		if (ReferenceEquals(left, nullptr))
			return ReferenceEquals(right, nullptr);

		return left->CompareTo(right) >= 0;
	}
	//===========================================================================================
	bool MagickImageInfo::operator <= (MagickImageInfo^ left, MagickImageInfo^ right)
	{
		if (ReferenceEquals(left, nullptr))
			return !ReferenceEquals(right, nullptr);

		return left->CompareTo(right) <= 0;
	}
	//==============================================================================================
	int MagickImageInfo::CompareTo(MagickImageInfo^ other)
	{
		if (ReferenceEquals(other, nullptr))
			return 1;

		int left = (this->Width * this->Height);
		int right = (other->Width * other->Height);

		if (left == right)
			return 0;

		return left < right ? -1 : 1;
	}
	//==============================================================================================
	bool MagickImageInfo::Equals(Object^ obj)
	{
		if (ReferenceEquals(this, obj))
			return true;

		return Equals(dynamic_cast<MagickImageInfo^>(obj));
	}
	//==============================================================================================
	bool MagickImageInfo::Equals(MagickImageInfo^ other)
	{
		if (ReferenceEquals(other, nullptr))
			return false;

		if (ReferenceEquals(this, other))
			return true;

		return
			this->_ColorSpace == other->_ColorSpace &&
			this->_Format == other->_Format &&
			this->_Height  == other->_Height &&
			this->_ResolutionX  == other->_ResolutionX &&
			this->_ResolutionY  == other->_ResolutionY &&
			this->_Width == other->_Width;
	}
	//==============================================================================================
	int MagickImageInfo::GetHashCode()
	{
		return
			this->_ColorSpace.GetHashCode() ^
			this->_Format.GetHashCode() ^
			this->_Height.GetHashCode() ^
			this->_ResolutionX.GetHashCode() ^
			this->_ResolutionY.GetHashCode() ^
			this->_Width.GetHashCode();
	}
	//==============================================================================================
	void MagickImageInfo::Read(array<Byte>^ data)
	{
		Magick::Image* image = new Magick::Image();
		MagickReader::Read(image, data, CreateReadSettings());
		Initialize(image);
		delete image;
	}
	//==============================================================================================
	void MagickImageInfo::Read(String^ fileName)
	{
		Magick::Image* image = new Magick::Image();
		MagickReader::Read(image, fileName, CreateReadSettings());
		Initialize(image);
		delete image;
	}
	//==============================================================================================
	void MagickImageInfo::Read(Stream^ stream)
	{
		Magick::Image* image = new Magick::Image();
		MagickReader::Read(image, stream, CreateReadSettings());
		Initialize(image);
		delete image;
	}
	//==============================================================================================
	IEnumerable<MagickImageInfo^>^ MagickImageInfo::ReadCollection(array<Byte>^ data)
	{
		std::list<Magick::Image>* images = new std::list<Magick::Image>();
		try
		{
			MagickReader::Read(images, data, CreateReadSettings());
			return Enumerate(images);
		}
		finally
		{
			delete images;
		}
	}
	//==============================================================================================
	IEnumerable<MagickImageInfo^>^ MagickImageInfo::ReadCollection(String^ fileName)
	{
		std::list<Magick::Image>* images = new std::list<Magick::Image>();
		try
		{
			MagickReader::Read(images, fileName, CreateReadSettings());
			return Enumerate(images);
		}
		finally
		{
			delete images;
		}
	}
	//==============================================================================================
	IEnumerable<MagickImageInfo^>^ MagickImageInfo::ReadCollection(Stream^ stream)
	{
		std::list<Magick::Image>* images = new std::list<Magick::Image>();
		try
		{
			MagickReader::Read(images, stream, CreateReadSettings());
			return Enumerate(images);
		}
		finally
		{
			delete images;
		}
	}
	//==============================================================================================
}