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
#include "Stdafx.h"
#include "..\Helpers\FileHelper.h"
#include "..\Helpers\XmlHelper.h"
#include "..\MagickImageCollection.h"
#include "MagickScript.h"

using namespace System::Xml::Schema;
using namespace System::Reflection;

namespace ImageMagick
{
	//==============================================================================================
	MagickImage^ MagickScript::CreateMagickImage(XmlElement^ element)
	{
		Throw::IfNull("element", element);

		MagickImage^ image = nullptr;
		MagickReadSettings^ settings = CreateMagickReadSettings((XmlElement^)element->SelectSingleNode("settings"));

		String^ fileName = element->GetAttribute("fileName");
		if (!String::IsNullOrEmpty(fileName))
		{
			if (settings != nullptr)
				image = gcnew MagickImage(fileName, settings);
			else
				image = gcnew MagickImage(fileName);
		}
		else
		{
			if (_ReadHandler == nullptr || _ReadHandler->GetInvocationList()->Length == 0)
				throw gcnew InvalidOperationException("The Read event should be bound when the fileName attribute is not set.");

			String^ id = element->GetAttribute("id");

			ScriptReadEventArgs^ eventArgs = gcnew ScriptReadEventArgs(id, settings);

			Read(this, eventArgs);

			if (eventArgs->Image == nullptr)
				throw gcnew InvalidOperationException("The Image property should not be null after the Read event has been raised.");

			image = eventArgs->Image;
		}

		return image;
	}
	//==============================================================================================
	MagickReadSettings^ MagickScript::CreateMagickReadSettings(XmlElement^ element)
	{
		if (element == nullptr || !element->HasAttributes)
			return nullptr;

		MagickReadSettings^ settings = gcnew MagickReadSettings();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "colorSpace")
				settings->ColorSpace = XmlHelper::GetValue<ColorSpace>(attribute);
			else if (attribute->Name == "height")
				settings->Height = XmlHelper::GetValue<Nullable<int>>(attribute);
			else if (attribute->Name == "width")
				settings->Width = XmlHelper::GetValue<Nullable<int>>(attribute);
		}

		settings->Density = CreateMagickGeometry((XmlElement^)element->SelectSingleNode("density"));

		return settings;
	}
	//==============================================================================================
	Collection<PathBase^>^ MagickScript::CreatePaths(XmlElement^ element)
	{
		Collection<PathBase^>^ paths = gcnew Collection<PathBase^>();

		for each (XmlElement^ elem in element->SelectNodes("*"))
		{
			ExecutePath(elem, paths);
		}

		return paths;
	}
	//==============================================================================================
	XmlReaderSettings^ MagickScript::CreateXmlReaderSettings()
	{
		XmlReaderSettings^ settings = gcnew XmlReaderSettings();
		settings->ValidationType = ValidationType::Schema;
		settings->ValidationFlags = XmlSchemaValidationFlags::ReportValidationWarnings;
		settings->IgnoreWhitespace = true;

		Stream^ resourceStream = Assembly::GetAssembly(MagickScript::typeid)->GetManifestResourceStream("MagickScript.xsd");
		try
		{
			XmlReader^ xmlReader = XmlReader::Create(resourceStream);
			settings->Schemas->Add("", xmlReader);
			delete xmlReader;
		}
		catch(XmlException^)
		{
			delete resourceStream;
			throw;
		}

		return settings;
	}
	//==============================================================================================
	MagickImage^ MagickScript::Execute(XmlElement^ element, MagickImageCollection^ collection)
	{
		if (element->Name == "appendHorizontally")
		{
			return collection->AppendHorizontally();
		}
		if (element->Name == "appendVertically")
		{
			return collection->AppendVertically();
		}
		if (element->Name == "merge")
		{
			LayerMethod layerMethod_ = XmlHelper::GetAttribute<LayerMethod>(element, "layerMethod");
			return collection->Merge(layerMethod_);
		}
		if (element->Name == "read")
		{
			collection->Add(ExecuteRead(element));
			return nullptr;
		}
		if (element->Name == "rePage")
		{
			collection->RePage();
			return nullptr;
		}
		if (element->Name == "write")
		{
			String^ fileName_ = XmlHelper::GetAttribute<String^>(element, "fileName");
			collection->Write(fileName_);
			return nullptr;
		}

		throw gcnew NotImplementedException(element->Name);
	}
	//==============================================================================================
	MagickImage^ MagickScript::ExecuteCollection(XmlElement^ element)
	{
		MagickImageCollection^ collection = gcnew MagickImageCollection();

		MagickImage^ result;
		for each (XmlElement^ elem in element->SelectNodes("*"))
		{
			result = Execute(elem, collection);
			if (result != nullptr)
				break;
		}

		delete collection;

		return result;
	}
	//==============================================================================================
	void MagickScript::ExecuteCopy(XmlElement^ element, MagickImage^ image)
	{
		ExecuteRead(element, image->Copy());
	}
	//==============================================================================================
	void MagickScript::ExecuteDraw(XmlElement^ element, MagickImage^ image)
	{
		Collection<Drawable^>^ drawables = gcnew Collection<Drawable^>();

		for each (XmlElement^ elem in element->SelectNodes("*"))
		{
			ExecuteDrawable(elem, drawables);
		}

		image->Draw(drawables);
	}
	//==============================================================================================
	MagickImage^ MagickScript::ExecuteRead(XmlElement^ element)
	{
		MagickImage^ image = CreateMagickImage(element);
		ExecuteRead(element, image);
		return image;
	}
	//==============================================================================================
	void MagickScript::ExecuteRead(XmlElement^ element, MagickImage^ image)
	{
		for each (XmlElement^ elem in element->SelectNodes("*[name() != 'settings']"))
		{
			ExecuteImage(elem, image);
		}
	}
	//==============================================================================================
	void MagickScript::ExecuteWrite(XmlElement^ element, MagickImage^ image)
	{
		String^ fileName = element->GetAttribute("fileName");
		if (!String::IsNullOrEmpty(fileName))
		{
			image->Write(fileName);
		}
		else
		{
			if (_WriteHandler == nullptr || _WriteHandler->GetInvocationList()->Length == 0)
				throw gcnew InvalidOperationException("The Write event should be bound when the fileName attribute is not set.");

			String^ id = element->GetAttribute("id");

			ScriptWriteEventArgs^ eventArgs = gcnew ScriptWriteEventArgs(id, image);
			Write(this, eventArgs);
		}
	}
	//==============================================================================================
	void MagickScript::Initialize(Stream^ stream)
	{
		Throw::IfNull("stream", stream);

		_Script = gcnew XmlDocument();
		XmlReader^ xmlReader = XmlReader::Create(stream, _ReaderSettings);
		_Script->Load(xmlReader);
		delete xmlReader;

		InitializeExecute();
	}
	//==============================================================================================
	bool MagickScript::OnlyContains(System::Collections::Hashtable^ arguments, ... array<Object^>^ keys)
	{
		if (arguments->Count != keys->Length)
			return false;

		for each (Object^ key in keys)
		{
			if (!arguments->ContainsKey(key))
				return false;
		}

		return true;
	}
	//==============================================================================================
	MagickScript::MagickScript(String^ fileName)
	{
		String^ filePath = FileHelper::CheckForBaseDirectory(fileName);
		Throw::IfInvalidFileName(filePath);

		FileStream^ stream = File::OpenRead(filePath);
		Initialize(stream);
		delete stream;
	}
	//==============================================================================================
	MagickScript::MagickScript(Stream^ stream)
	{
		Initialize(stream);
	}
	//==============================================================================================
	void MagickScript::Read::add(EventHandler<ScriptReadEventArgs^>^ handler)
	{
		_ReadHandler += handler;
	}
	//==============================================================================================
	void MagickScript::Read::raise(Object^ sender, ScriptReadEventArgs^ arguments)
	{
		_ReadHandler(sender, arguments);
	}
	//==============================================================================================
	void MagickScript::Read::remove(EventHandler<ScriptReadEventArgs^>^ handler)
	{
		_ReadHandler -= handler;
	}
	//==============================================================================================
	void MagickScript::Write::add(EventHandler<ScriptWriteEventArgs^>^ handler)
	{
		_WriteHandler += handler;
	}
	//==============================================================================================
	void MagickScript::Write::raise(Object^ sender, ScriptWriteEventArgs^ arguments)
	{
		_WriteHandler(sender, arguments);
	}
	//==============================================================================================
	void MagickScript::Write::remove(EventHandler<ScriptWriteEventArgs^>^ handler)
	{
		_WriteHandler -= handler;
	}
	//==============================================================================================
	MagickImage^ MagickScript::Execute()
	{
		XmlElement^ element = (XmlElement^)_Script->SelectSingleNode("/msl/*");

		if (element->Name == "read")
			return ExecuteRead(element);
		else if (element->Name == "collection")
			return ExecuteCollection(element);
		else
			throw gcnew NotImplementedException(element->Name);
	}
	//==============================================================================================
	void MagickScript::Execute(MagickImage^ image)
	{
		Throw::IfNull("image", image);

		XmlElement^ element = (XmlElement^)_Script->SelectSingleNode("/msl/read");
		if (element == nullptr)
			throw gcnew InvalidOperationException("This method only works with a script that contains a single read operation.");

		ExecuteRead(element, image);
	}
	//==============================================================================================
}