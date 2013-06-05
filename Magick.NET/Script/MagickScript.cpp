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

using namespace System::Collections;
using namespace System::Xml::Schema;
using namespace System::Reflection;

namespace ImageMagick
{
	//==============================================================================================
	MagickGeometry^ MagickScript::CreateMagickGeometry(XmlElement^ element)
	{
		if (element == nullptr || !element->HasAttributes)
			return nullptr;

		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			if (attribute->Name == "height")
				arguments["height"] = XmlHelper::GetValue<int>(attribute);
			else if (attribute->Name == "percentageWidth")
				arguments["percentageWidth"] = XmlHelper::GetValue<Percentage>(attribute);
			else if (attribute->Name == "percentageHeight")
				arguments["percentageHeight"] = XmlHelper::GetValue<Percentage>(attribute);
			else if (attribute->Name == "value")
				arguments["value"] = attribute->Value;
			else if (attribute->Name == "width")
				arguments["width"] = XmlHelper::GetValue<int>(attribute);
			else if (attribute->Name == "x")
				arguments["x"] = XmlHelper::GetValue<int>(attribute);
			else if (attribute->Name == "y")
				arguments["y"] = XmlHelper::GetValue<int>(attribute);
			else
				throw gcnew NotImplementedException(attribute->Name);
		}

		if (OnlyContains(arguments, "value"))
			return gcnew MagickGeometry((String^)arguments["value"]);
		if (OnlyContains(arguments, "width", "height"))
			return gcnew MagickGeometry((int)arguments["width"], (int)arguments["height"]);
		if (OnlyContains(arguments, "percentageWidth", "percentageHeight"))
			return gcnew MagickGeometry((Percentage)arguments["percentageWidth"], (Percentage)arguments["percentageHeight"]);
		if (OnlyContains(arguments, "x", "y", "width", "height"))
			return gcnew MagickGeometry((int)arguments["x"], (int)arguments["y"], (int)arguments["width"], (int)arguments["height"]);
		if (OnlyContains(arguments, "x", "y", "percentageWidth", "percentageHeight"))
			return gcnew MagickGeometry((int)arguments["x"], (int)arguments["y"], (Percentage)arguments["percentageWidth"], (Percentage)arguments["percentageHeight"]);

		throw gcnew ArgumentException("Invalid argument combination for '" + element->Name + "', allowed combinations are: [value] [width, height] [percentageWidth, percentageHeight] [x, y, width, height] [x, y, percentageWidth, percentageHeight].");
	}
	//==============================================================================================
	MagickImage^ MagickScript::CreateMagickImage(XmlElement^ element)
	{
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
	void MagickScript::Execute(XmlElement^ element, MagickImage^ image)
	{
#pragma region Generated Code.
		int hashCode = element->Name->GetHashCode();
		switch(hashCode)
		{
		case 67910424:
			ExecuteAdaptiveBlur(element, image);
			break;
		case 338213577:
			ExecuteAdaptiveThreshold(element, image);
			break;
		case 303795173:
			ExecuteAddNoise(element, image);
			break;
		case -629424942:
			ExecuteAddProfile(element, image);
			break;
		case 414349046:
			ExecuteAdjoin(element, image);
			break;
		case -281919255:
			ExecuteAnimationDelay(element, image);
			break;
		case -1860514769:
			ExecuteAnimationIterations(element, image);
			break;
		case -2100432769:
			ExecuteAnnotate(element, image);
			break;
		case -194167079:
			ExecuteAntiAlias(element, image);
			break;
		case -136546062:
			ExecuteAttribute(element, image);
			break;
		case -1650340647:
			ExecuteBackgroundColor(element, image);
			break;
		case 1191540639:
			ExecuteBlur(element, image);
			break;
		case 1255592519:
			ExecuteBorder(element, image);
			break;
		case -1096349590:
			ExecuteBorderColor(element, image);
			break;
		case -1385453605:
			ExecuteCDL(element, image);
			break;
		case 922132170:
			ExecuteCharcoal(element, image);
			break;
		case -1201797716:
			ExecuteChop(element, image);
			break;
		case -708211308:
			ExecuteChopHorizontal(element, image);
			break;
		case -360528937:
			ExecuteChopVertical(element, image);
			break;
		case -1991452456:
			ExecuteChromaBluePrimary(element, image);
			break;
		case -276869561:
			ExecuteChromaGreenPrimary(element, image);
			break;
		case -2034101721:
			ExecuteChromaRedPrimary(element, image);
			break;
		case 1542218040:
			ExecuteChromaWhitePoint(element, image);
			break;
		case 338384396:
			ExecuteClassType(element, image);
			break;
		case -333727766:
			ExecuteClipMask(element, image);
			break;
		case 1781198750:
			ExecuteColorAlpha(element, image);
			break;
		case -2057388205:
			ExecuteColorFuzz(element, image);
			break;
		case -1358772689:
			ExecuteColorize(element, image);
			break;
		case -651095416:
			ExecuteColorMap(element, image);
			break;
		case 1137081529:
			ExecuteColorMapSize(element, image);
			break;
		case 149560619:
			ExecuteColorSpace(element, image);
			break;
		case 1793387752:
			ExecuteColorType(element, image);
			break;
		case -498703581:
			ExecuteComment(element, image);
			break;
		case 38942766:
			ExecuteCompose(element, image);
			break;
		case 1767780564:
			ExecuteComposite(element, image);
			break;
		case 613907492:
			ExecuteContrast(element, image);
			break;
		case -1201404500:
			ExecuteCrop(element, image);
			break;
		case -1730384909:
			ExecuteCycleColormap(element, image);
			break;
		case 1962162158:
			ExecuteDensity(element, image);
			break;
		case -1153177276:
			ExecuteDepth(element, image);
			break;
		case 1763206769:
			ExecuteDespeckle(image);
			break;
		case 1581558402:
			ExecuteEdge(element, image);
			break;
		case 787225412:
			ExecuteEmboss(element, image);
			break;
		case -1196096376:
			ExecuteEndian(element, image);
			break;
		case 1802328816:
			ExecuteExifProfile(element, image);
			break;
		case 129350357:
			ExecuteExtent(element, image);
			break;
		case 753653940:
			ExecuteFillColor(element, image);
			break;
		case 101740205:
			ExecuteFillPattern(element, image);
			break;
		case -280615450:
			ExecuteFillRule(element, image);
			break;
		case 1839702968:
			ExecuteFilterType(element, image);
			break;
		case 1604614520:
			ExecuteFlashPixView(element, image);
			break;
		case -395490809:
			ExecuteFlip(image);
			break;
		case -1636590561:
			ExecuteFloodFill(element, image);
			break;
		case -1202059863:
			ExecuteFlop(image);
			break;
		case -400846578:
			ExecuteFont(element, image);
			break;
		case -773577746:
			ExecuteFontPointsize(element, image);
			break;
		case 446157247:
			ExecuteFormat(element, image);
			break;
		case -1538742576:
			ExecuteFrame(element, image);
			break;
		case -838682694:
			ExecuteFx(element, image);
			break;
		case 398185609:
			ExecuteGamma(element, image);
			break;
		case 961447514:
			ExecuteGaussianBlur(element, image);
			break;
		case -1357131272:
			ExecuteGifDisposeMethod(element, image);
			break;
		case 1396045834:
			ExecuteHaldClut(element, image);
			break;
		case -1064607174:
			ExecuteHasMatte(element, image);
			break;
		case -730298669:
			ExecuteImplode(element, image);
			break;
		case 1260863166:
			ExecuteInverseFourierTransform(element, image);
			break;
		case -1972557189:
			ExecuteIsMonochrome(element, image);
			break;
		case -371647226:
			ExecuteLabel(element, image);
			break;
		case 1232840130:
			ExecuteLevel(element, image);
			break;
		case -320267677:
			ExecuteLower(element, image);
			break;
		case 1743660820:
			ExecuteMagnify(image);
			break;
		case 227881117:
			ExecuteMap(element, image);
			break;
		case 1863232027:
			ExecuteMatteColor(element, image);
			break;
		case 1956417807:
			ExecuteMedianFilter(element, image);
			break;
		case -382317901:
			ExecuteMinify(image);
			break;
		case -883740819:
			ExecuteModulate(element, image);
			break;
		case 1654044696:
			ExecuteModulusDepth(element, image);
			break;
		case 1123602528:
			ExecuteMotionBlur(element, image);
			break;
		case 1150378859:
			ExecuteNegate(element, image);
			break;
		case 347442616:
			ExecuteNormalize(image);
			break;
		case 1095908874:
			ExecuteOilPaint(element, image);
			break;
		case 772283427:
			ExecuteOrientation(element, image);
			break;
		case 1581755031:
			ExecutePage(element, image);
			break;
		case 168267742:
			ExecuteQuality(element, image);
			break;
		case 1980898134:
			ExecuteQuantize(image);
			break;
		case -618086687:
			ExecuteQuantizeColors(element, image);
			break;
		case -1859077320:
			ExecuteQuantizeColorSpace(element, image);
			break;
		case -1866355241:
			ExecuteQuantizeDither(element, image);
			break;
		case -73716975:
			ExecuteQuantizeTreeDepth(element, image);
			break;
		case -231476150:
			ExecuteQuantumOperator(element, image);
			break;
		case -15727532:
			ExecuteRaise(element, image);
			break;
		case 1668950609:
			ExecuteRandomThreshold(element, image);
			break;
		case 431746803:
			ExecuteRead(element, image);
			break;
		case 1098406684:
			ExecuteReduceNoise(element, image);
			break;
		case -1860151891:
			ExecuteRemoveProfile(element, image);
			break;
		case -1232613242:
			ExecuteRenderingIntent(element, image);
			break;
		case -358332235:
			ExecuteResize(element, image);
			break;
		case -1927683852:
			ExecuteResolutionUnits(element, image);
			break;
		case 1201973228:
			ExecuteRoll(element, image);
			break;
		case 845266270:
			ExecuteRotate(element, image);
			break;
		case 741856984:
			ExecuteSample(element, image);
			break;
		case 763144525:
			ExecuteScale(element, image);
			break;
		case -1578101831:
			ExecuteSegment(element, image);
			break;
		case -1151678251:
			ExecuteSeparate(element, image);
			break;
		case -706012951:
			break;
		case -742414003:
			ExecuteShade(element, image);
			break;
		case -743790269:
			ExecuteShadow(element, image);
			break;
		case 1996399444:
			ExecuteSharpen(element, image);
			break;
		case 1933945165:
			ExecuteShave(element, image);
			break;
		case 9206722:
			ExecuteShear(element, image);
			break;
		case -1272467893:
			ExecuteSigmoidalContrast(element, image);
			break;
		case 1903740959:
			ExecuteSolarize(element, image);
			break;
		case 1166780862:
			ExecuteStegano(element, image);
			break;
		case 735476665:
			ExecuteStereo(element, image);
			break;
		case 1238029379:
			ExecuteStrip(image);
			break;
		case -263820959:
			ExecuteStrokeAntiAlias(element, image);
			break;
		case 447497766:
			ExecuteStrokeColor(element, image);
			break;
		case 2085887298:
			ExecuteStrokeDashOffset(element, image);
			break;
		case 1621917842:
			ExecuteStrokeLineCap(element, image);
			break;
		case 853037116:
			ExecuteStrokeLineJoin(element, image);
			break;
		case -1580131456:
			ExecuteStrokeMiterLimit(element, image);
			break;
		case 411143848:
			ExecuteStrokePattern(element, image);
			break;
		case 1828262320:
			ExecuteStrokeWidth(element, image);
			break;
		case -1950415764:
			ExecuteSwirl(element, image);
			break;
		case 697718607:
			ExecuteTextEncoding(element, image);
			break;
		case -686405780:
			ExecuteTexture(element, image);
			break;
		case -1291593385:
			ExecuteThreshold(element, image);
			break;
		case 1969921379:
			ExecuteTileName(element, image);
			break;
		case 1705654852:
			ExecuteTransform(element, image);
			break;
		case -999010064:
			ExecuteTransformOrigin(element, image);
			break;
		case -1631283828:
			ExecuteTransformReset(image);
			break;
		case 1098365163:
			ExecuteTransformRotation(element, image);
			break;
		case 384532169:
			ExecuteTransformScale(element, image);
			break;
		case 788978136:
			ExecuteTransformSkewX(element, image);
			break;
		case 789043672:
			ExecuteTransformSkewY(element, image);
			break;
		case 134890947:
			ExecuteTransparent(element, image);
			break;
		case 2098087467:
			ExecuteTransparentChroma(element, image);
			break;
		case -732542439:
			ExecuteTrim(image);
			break;
		case 167571987:
			ExecuteUnsharpmask(element, image);
			break;
		case 824852238:
			ExecuteVerbose(element, image);
			break;
		case -1187158202:
			ExecuteVirtualPixelMethod(element, image);
			break;
		case -1859589051:
			ExecuteWave(element, image);
			break;
		case 1973027817:
			ExecuteWrite(element, image);
			break;
		case -1539963451:
			ExecuteZoom(element, image);
			break;
		default:
			throw gcnew NotImplementedException(element->Name);
		}
#pragma endregion
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
			collection->Add(ExecuteRead(element, nullptr));
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
	MagickImage^ MagickScript::ExecuteCollection(XmlElement^ collectionElement)
	{
		MagickImageCollection^ collection = gcnew MagickImageCollection();

		MagickImage^ result;
		for each (XmlElement^ element_ in collectionElement->SelectNodes("*"))
		{
			result = Execute(element_, collection);
			if (result != nullptr)
				break;
		}

		delete collection;

		return result;
	}
	//==============================================================================================
	MagickImage^ MagickScript::ExecuteRead(XmlElement^ readElement, MagickImage^ image)
	{
		MagickImage^ readImage = image;

		if (readImage == nullptr)
			readImage = CreateMagickImage(readElement);

		for each (XmlElement^ element in readElement->SelectNodes("*"))
		{
			Execute(element, readImage);
		}

		return readImage;
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
	}
	//==============================================================================================
	bool MagickScript::OnlyContains(Hashtable^ arguments, ... array<Object^>^ keys)
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
#pragma region Generated Code.
	//==============================================================================================
	void MagickScript::ExecuteAdaptiveBlur(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case -1204484339: 
				arguments[-1204484339] = XmlHelper::GetAttribute<double>(element, "radius");
				break;
			case 1934517483: 
				arguments[1934517483] = XmlHelper::GetAttribute<double>(element, "sigma");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (arguments->Count == 0)
		{
			image->AdaptiveBlur();
			return;
		}
		if (OnlyContains(arguments, -1204484339, 1934517483))
		{
			image->AdaptiveBlur((double)arguments[-1204484339], (double)arguments[1934517483]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'adaptiveBlur', allowed combinations are: [] [radius, sigma]");
	}
	//==============================================================================================
	void MagickScript::ExecuteAdaptiveThreshold(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case 1910834845: 
				arguments[1910834845] = XmlHelper::GetAttribute<int>(element, "width");
				break;
			case 452723731: 
				arguments[452723731] = XmlHelper::GetAttribute<int>(element, "height");
				break;
			case 1082126080: 
				arguments[1082126080] = XmlHelper::GetAttribute<int>(element, "offset");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (OnlyContains(arguments, 1910834845, 452723731))
		{
			image->AdaptiveThreshold((int)arguments[1910834845], (int)arguments[452723731]);
			return;
		}
		if (OnlyContains(arguments, 1910834845, 452723731, 1082126080))
		{
			image->AdaptiveThreshold((int)arguments[1910834845], (int)arguments[452723731], (int)arguments[1082126080]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'adaptiveThreshold', allowed combinations are: [width, height] [width, height, offset]");
	}
	//==============================================================================================
	void MagickScript::ExecuteAddNoise(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case -929901517: 
				arguments[-929901517] = XmlHelper::GetAttribute<NoiseType>(element, "noiseType");
				break;
			case -1167029596: 
				arguments[-1167029596] = XmlHelper::GetAttribute<Channels>(element, "channels");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (OnlyContains(arguments, -929901517))
		{
			image->AddNoise((NoiseType)arguments[-929901517]);
			return;
		}
		if (OnlyContains(arguments, -929901517, -1167029596))
		{
			image->AddNoise((NoiseType)arguments[-929901517], (Channels)arguments[-1167029596]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'addNoise', allowed combinations are: [noiseType] [noiseType, channels]");
	}
	//==============================================================================================
	void MagickScript::ExecuteAddProfile(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case 1969920661: 
				arguments[1969920661] = XmlHelper::GetAttribute<String^>(element, "fileName");
				break;
			case 62725243: 
				arguments[62725243] = XmlHelper::GetAttribute<String^>(element, "name");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (OnlyContains(arguments, 1969920661))
		{
			image->AddProfile((String^)arguments[1969920661]);
			return;
		}
		if (OnlyContains(arguments, 62725243, 1969920661))
		{
			image->AddProfile((String^)arguments[62725243], (String^)arguments[1969920661]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'addProfile', allowed combinations are: [fileName] [name, fileName]");
	}
	//==============================================================================================
	void MagickScript::ExecuteAdjoin(XmlElement^ element, MagickImage^ image)
	{
		image->Adjoin = XmlHelper::GetAttribute<bool>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteAnimationDelay(XmlElement^ element, MagickImage^ image)
	{
		image->AnimationDelay = XmlHelper::GetAttribute<int>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteAnimationIterations(XmlElement^ element, MagickImage^ image)
	{
		image->AnimationIterations = XmlHelper::GetAttribute<int>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteAnnotate(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case -307131442: 
				arguments[-307131442] = XmlHelper::GetAttribute<String^>(element, "text");
				break;
			case -624915240: 
				arguments[-624915240] = XmlHelper::GetAttribute<Gravity>(element, "gravity");
				break;
			case 1647686709: 
				arguments[1647686709] = XmlHelper::GetAttribute<double>(element, "degrees");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		for each(XmlElement^ element in element->ChildNodes)
		{
			int hashCode = element->Name->GetHashCode();
			switch(hashCode)
			{
			case 2051627867: 
				arguments[2051627867] = CreateMagickGeometry(element);
				break;
			default:
				throw gcnew NotImplementedException(element->Name);
			}
		}
		if (OnlyContains(arguments, -307131442, -624915240))
		{
			image->Annotate((String^)arguments[-307131442], (Gravity)arguments[-624915240]);
			return;
		}
		if (OnlyContains(arguments, -307131442, 2051627867))
		{
			image->Annotate((String^)arguments[-307131442], (MagickGeometry^)arguments[2051627867]);
			return;
		}
		if (OnlyContains(arguments, -307131442, 2051627867, -624915240))
		{
			image->Annotate((String^)arguments[-307131442], (MagickGeometry^)arguments[2051627867], (Gravity)arguments[-624915240]);
			return;
		}
		if (OnlyContains(arguments, -307131442, 2051627867, -624915240, 1647686709))
		{
			image->Annotate((String^)arguments[-307131442], (MagickGeometry^)arguments[2051627867], (Gravity)arguments[-624915240], (double)arguments[1647686709]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'annotate', allowed combinations are: [text, gravity] [text, boundingArea] [text, boundingArea, gravity] [text, boundingArea, gravity, degrees]");
	}
	//==============================================================================================
	void MagickScript::ExecuteAntiAlias(XmlElement^ element, MagickImage^ image)
	{
		image->AntiAlias = XmlHelper::GetAttribute<bool>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteAttribute(XmlElement^ element, MagickImage^ image)
	{
		String^ name_ = XmlHelper::GetAttribute<String^>(element, "name");
		String^ value_ = XmlHelper::GetAttribute<String^>(element, "value");
		image->Attribute(name_, value_);
	}
	//==============================================================================================
	void MagickScript::ExecuteBackgroundColor(XmlElement^ element, MagickImage^ image)
	{
		image->BackgroundColor = XmlHelper::GetAttribute<MagickColor^>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteBlur(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case -1167029596: 
				arguments[-1167029596] = XmlHelper::GetAttribute<Channels>(element, "channels");
				break;
			case -1204484339: 
				arguments[-1204484339] = XmlHelper::GetAttribute<double>(element, "radius");
				break;
			case 1934517483: 
				arguments[1934517483] = XmlHelper::GetAttribute<double>(element, "sigma");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (arguments->Count == 0)
		{
			image->Blur();
			return;
		}
		if (OnlyContains(arguments, -1167029596))
		{
			image->Blur((Channels)arguments[-1167029596]);
			return;
		}
		if (OnlyContains(arguments, -1204484339, 1934517483))
		{
			image->Blur((double)arguments[-1204484339], (double)arguments[1934517483]);
			return;
		}
		if (OnlyContains(arguments, -1204484339, 1934517483, -1167029596))
		{
			image->Blur((double)arguments[-1204484339], (double)arguments[1934517483], (Channels)arguments[-1167029596]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'blur', allowed combinations are: [] [channels] [radius, sigma] [radius, sigma, channels]");
	}
	//==============================================================================================
	void MagickScript::ExecuteBorder(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case -245926651: 
				arguments[-245926651] = XmlHelper::GetAttribute<int>(element, "size");
				break;
			case 1910834845: 
				arguments[1910834845] = XmlHelper::GetAttribute<int>(element, "width");
				break;
			case 452723731: 
				arguments[452723731] = XmlHelper::GetAttribute<int>(element, "height");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (OnlyContains(arguments, -245926651))
		{
			image->Border((int)arguments[-245926651]);
			return;
		}
		if (OnlyContains(arguments, 1910834845, 452723731))
		{
			image->Border((int)arguments[1910834845], (int)arguments[452723731]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'border', allowed combinations are: [size] [width, height]");
	}
	//==============================================================================================
	void MagickScript::ExecuteBorderColor(XmlElement^ element, MagickImage^ image)
	{
		image->BorderColor = XmlHelper::GetAttribute<MagickColor^>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteCDL(XmlElement^ element, MagickImage^ image)
	{
		String^ fileName_ = XmlHelper::GetAttribute<String^>(element, "fileName");
		image->CDL(fileName_);
	}
	//==============================================================================================
	void MagickScript::ExecuteCharcoal(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case -1204484339: 
				arguments[-1204484339] = XmlHelper::GetAttribute<double>(element, "radius");
				break;
			case 1934517483: 
				arguments[1934517483] = XmlHelper::GetAttribute<double>(element, "sigma");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (arguments->Count == 0)
		{
			image->Charcoal();
			return;
		}
		if (OnlyContains(arguments, -1204484339, 1934517483))
		{
			image->Charcoal((double)arguments[-1204484339], (double)arguments[1934517483]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'charcoal', allowed combinations are: [] [radius, sigma]");
	}
	//==============================================================================================
	void MagickScript::ExecuteChop(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case -1607650283: 
				arguments[-1607650283] = XmlHelper::GetAttribute<int>(element, "xOffset");
				break;
			case 1910834845: 
				arguments[1910834845] = XmlHelper::GetAttribute<int>(element, "width");
				break;
			case -1607650250: 
				arguments[-1607650250] = XmlHelper::GetAttribute<int>(element, "yOffset");
				break;
			case 452723731: 
				arguments[452723731] = XmlHelper::GetAttribute<int>(element, "height");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		for each(XmlElement^ element in element->ChildNodes)
		{
			int hashCode = element->Name->GetHashCode();
			switch(hashCode)
			{
			case -1835407281: 
				arguments[-1835407281] = CreateMagickGeometry(element);
				break;
			default:
				throw gcnew NotImplementedException(element->Name);
			}
		}
		if (OnlyContains(arguments, -1835407281))
		{
			image->Chop((MagickGeometry^)arguments[-1835407281]);
			return;
		}
		if (OnlyContains(arguments, -1607650283, 1910834845, -1607650250, 452723731))
		{
			image->Chop((int)arguments[-1607650283], (int)arguments[1910834845], (int)arguments[-1607650250], (int)arguments[452723731]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'chop', allowed combinations are: [geometry] [xOffset, width, yOffset, height]");
	}
	//==============================================================================================
	void MagickScript::ExecuteChopHorizontal(XmlElement^ element, MagickImage^ image)
	{
		int offset_ = XmlHelper::GetAttribute<int>(element, "offset");
		int width_ = XmlHelper::GetAttribute<int>(element, "width");
		image->ChopHorizontal(offset_, width_);
	}
	//==============================================================================================
	void MagickScript::ExecuteChopVertical(XmlElement^ element, MagickImage^ image)
	{
		int offset_ = XmlHelper::GetAttribute<int>(element, "offset");
		int height_ = XmlHelper::GetAttribute<int>(element, "height");
		image->ChopVertical(offset_, height_);
	}
	//==============================================================================================
	void MagickScript::ExecuteChromaBluePrimary(XmlElement^ element, MagickImage^ image)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		double y_ = XmlHelper::GetAttribute<double>(element, "y");
		image->ChromaBluePrimary(x_, y_);
	}
	//==============================================================================================
	void MagickScript::ExecuteChromaGreenPrimary(XmlElement^ element, MagickImage^ image)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		double y_ = XmlHelper::GetAttribute<double>(element, "y");
		image->ChromaGreenPrimary(x_, y_);
	}
	//==============================================================================================
	void MagickScript::ExecuteChromaRedPrimary(XmlElement^ element, MagickImage^ image)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		double y_ = XmlHelper::GetAttribute<double>(element, "y");
		image->ChromaRedPrimary(x_, y_);
	}
	//==============================================================================================
	void MagickScript::ExecuteChromaWhitePoint(XmlElement^ element, MagickImage^ image)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		double y_ = XmlHelper::GetAttribute<double>(element, "y");
		image->ChromaWhitePoint(x_, y_);
	}
	//==============================================================================================
	void MagickScript::ExecuteClassType(XmlElement^ element, MagickImage^ image)
	{
		image->ClassType = XmlHelper::GetAttribute<ClassType>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteClipMask(XmlElement^ element, MagickImage^ image)
	{
		image->ClipMask = CreateMagickImage((XmlElement^)element->SelectSingleNode("read"));
	}
	//==============================================================================================
	void MagickScript::ExecuteColorAlpha(XmlElement^ element, MagickImage^ image)
	{
		MagickColor^ color_ = XmlHelper::GetAttribute<MagickColor^>(element, "color");
		image->ColorAlpha(color_);
	}
	//==============================================================================================
	void MagickScript::ExecuteColorFuzz(XmlElement^ element, MagickImage^ image)
	{
		image->ColorFuzz = XmlHelper::GetAttribute<double>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteColorize(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case 1586258015: 
				arguments[1586258015] = XmlHelper::GetAttribute<MagickColor^>(element, "color");
				break;
			case -1898387216: 
				arguments[-1898387216] = XmlHelper::GetAttribute<Percentage>(element, "alpha");
				break;
			case 438535429: 
				arguments[438535429] = XmlHelper::GetAttribute<Percentage>(element, "alphaRed");
				break;
			case -381038934: 
				arguments[-381038934] = XmlHelper::GetAttribute<Percentage>(element, "alphaGreen");
				break;
			case -1368753964: 
				arguments[-1368753964] = XmlHelper::GetAttribute<Percentage>(element, "alphaBlue");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (OnlyContains(arguments, 1586258015, -1898387216))
		{
			image->Colorize((MagickColor^)arguments[1586258015], (Percentage)arguments[-1898387216]);
			return;
		}
		if (OnlyContains(arguments, 1586258015, 438535429, -381038934, -1368753964))
		{
			image->Colorize((MagickColor^)arguments[1586258015], (Percentage)arguments[438535429], (Percentage)arguments[-381038934], (Percentage)arguments[-1368753964]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'colorize', allowed combinations are: [color, alpha] [color, alphaRed, alphaGreen, alphaBlue]");
	}
	//==============================================================================================
	void MagickScript::ExecuteColorMap(XmlElement^ element, MagickImage^ image)
	{
		int index_ = XmlHelper::GetAttribute<int>(element, "index");
		MagickColor^ color_ = XmlHelper::GetAttribute<MagickColor^>(element, "color");
		image->ColorMap(index_, color_);
	}
	//==============================================================================================
	void MagickScript::ExecuteColorMapSize(XmlElement^ element, MagickImage^ image)
	{
		image->ColorMapSize = XmlHelper::GetAttribute<int>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteColorSpace(XmlElement^ element, MagickImage^ image)
	{
		image->ColorSpace = XmlHelper::GetAttribute<ColorSpace>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteColorType(XmlElement^ element, MagickImage^ image)
	{
		image->ColorType = XmlHelper::GetAttribute<ColorType>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteComment(XmlElement^ element, MagickImage^ image)
	{
		image->Comment = XmlHelper::GetAttribute<String^>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteCompose(XmlElement^ element, MagickImage^ image)
	{
		image->Compose = XmlHelper::GetAttribute<CompositeOperator>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteComposite(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case -624915240: 
				arguments[-624915240] = XmlHelper::GetAttribute<Gravity>(element, "gravity");
				break;
			case 38942766: 
				arguments[38942766] = XmlHelper::GetAttribute<CompositeOperator>(element, "compose");
				break;
			case -842352680: 
				arguments[-842352680] = XmlHelper::GetAttribute<int>(element, "x");
				break;
			case -842352681: 
				arguments[-842352681] = XmlHelper::GetAttribute<int>(element, "y");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		for each(XmlElement^ element in element->ChildNodes)
		{
			int hashCode = element->Name->GetHashCode();
			switch(hashCode)
			{
			case 820750591: 
				arguments[820750591] = CreateMagickImage(element);
				break;
			case 1082126080: 
				arguments[1082126080] = CreateMagickGeometry(element);
				break;
			default:
				throw gcnew NotImplementedException(element->Name);
			}
		}
		if (OnlyContains(arguments, 820750591, -624915240))
		{
			image->Composite((MagickImage^)arguments[820750591], (Gravity)arguments[-624915240]);
			return;
		}
		if (OnlyContains(arguments, 820750591, 1082126080))
		{
			image->Composite((MagickImage^)arguments[820750591], (MagickGeometry^)arguments[1082126080]);
			return;
		}
		if (OnlyContains(arguments, 820750591, -624915240, 38942766))
		{
			image->Composite((MagickImage^)arguments[820750591], (Gravity)arguments[-624915240], (CompositeOperator)arguments[38942766]);
			return;
		}
		if (OnlyContains(arguments, 820750591, 1082126080, 38942766))
		{
			image->Composite((MagickImage^)arguments[820750591], (MagickGeometry^)arguments[1082126080], (CompositeOperator)arguments[38942766]);
			return;
		}
		if (OnlyContains(arguments, 820750591, -842352680, -842352681))
		{
			image->Composite((MagickImage^)arguments[820750591], (int)arguments[-842352680], (int)arguments[-842352681]);
			return;
		}
		if (OnlyContains(arguments, 820750591, -842352680, -842352681, 38942766))
		{
			image->Composite((MagickImage^)arguments[820750591], (int)arguments[-842352680], (int)arguments[-842352681], (CompositeOperator)arguments[38942766]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'composite', allowed combinations are: [image, gravity] [image, offset] [image, gravity, compose] [image, offset, compose] [image, x, y] [image, x, y, compose]");
	}
	//==============================================================================================
	void MagickScript::ExecuteContrast(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case -1188182954: 
				arguments[-1188182954] = XmlHelper::GetAttribute<bool>(element, "enhance");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (arguments->Count == 0)
		{
			image->Contrast();
			return;
		}
		if (OnlyContains(arguments, -1188182954))
		{
			image->Contrast((bool)arguments[-1188182954]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'contrast', allowed combinations are: [] [enhance]");
	}
	//==============================================================================================
	void MagickScript::ExecuteCrop(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case 1910834845: 
				arguments[1910834845] = XmlHelper::GetAttribute<int>(element, "width");
				break;
			case 452723731: 
				arguments[452723731] = XmlHelper::GetAttribute<int>(element, "height");
				break;
			case -624915240: 
				arguments[-624915240] = XmlHelper::GetAttribute<Gravity>(element, "gravity");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		for each(XmlElement^ element in element->ChildNodes)
		{
			int hashCode = element->Name->GetHashCode();
			switch(hashCode)
			{
			case -1835407281: 
				arguments[-1835407281] = CreateMagickGeometry(element);
				break;
			default:
				throw gcnew NotImplementedException(element->Name);
			}
		}
		if (OnlyContains(arguments, -1835407281))
		{
			image->Crop((MagickGeometry^)arguments[-1835407281]);
			return;
		}
		if (OnlyContains(arguments, 1910834845, 452723731))
		{
			image->Crop((int)arguments[1910834845], (int)arguments[452723731]);
			return;
		}
		if (OnlyContains(arguments, 1910834845, 452723731, -624915240))
		{
			image->Crop((int)arguments[1910834845], (int)arguments[452723731], (Gravity)arguments[-624915240]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'crop', allowed combinations are: [geometry] [width, height] [width, height, gravity]");
	}
	//==============================================================================================
	void MagickScript::ExecuteCycleColormap(XmlElement^ element, MagickImage^ image)
	{
		int amount_ = XmlHelper::GetAttribute<int>(element, "amount");
		image->CycleColormap(amount_);
	}
	//==============================================================================================
	void MagickScript::ExecuteDensity(XmlElement^ element, MagickImage^ image)
	{
		image->Density = CreateMagickGeometry((XmlElement^)element->SelectSingleNode("geometry"));
	}
	//==============================================================================================
	void MagickScript::ExecuteDepth(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case -801349223: 
				arguments[-801349223] = XmlHelper::GetAttribute<int>(element, "value");
				break;
			case -1167029596: 
				arguments[-1167029596] = XmlHelper::GetAttribute<Channels>(element, "channels");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (OnlyContains(arguments, -801349223))
		{
			image->Depth((int)arguments[-801349223]);
			return;
		}
		if (OnlyContains(arguments, -801349223, -1167029596))
		{
			image->Depth((int)arguments[-801349223], (Channels)arguments[-1167029596]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'depth', allowed combinations are: [value] [value, channels]");
	}
	//==============================================================================================
	void MagickScript::ExecuteDespeckle(MagickImage^ image)
	{
		image->Despeckle();
	}
	//==============================================================================================
	void MagickScript::ExecuteEdge(XmlElement^ element, MagickImage^ image)
	{
		double radius_ = XmlHelper::GetAttribute<double>(element, "radius");
		image->Edge(radius_);
	}
	//==============================================================================================
	void MagickScript::ExecuteEmboss(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case -1204484339: 
				arguments[-1204484339] = XmlHelper::GetAttribute<double>(element, "radius");
				break;
			case 1934517483: 
				arguments[1934517483] = XmlHelper::GetAttribute<double>(element, "sigma");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (arguments->Count == 0)
		{
			image->Emboss();
			return;
		}
		if (OnlyContains(arguments, -1204484339, 1934517483))
		{
			image->Emboss((double)arguments[-1204484339], (double)arguments[1934517483]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'emboss', allowed combinations are: [] [radius, sigma]");
	}
	//==============================================================================================
	void MagickScript::ExecuteEndian(XmlElement^ element, MagickImage^ image)
	{
		image->Endian = XmlHelper::GetAttribute<Endian>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteExifProfile(XmlElement^ element, MagickImage^ image)
	{
		String^ fileName_ = XmlHelper::GetAttribute<String^>(element, "fileName");
		image->ExifProfile(fileName_);
	}
	//==============================================================================================
	void MagickScript::ExecuteExtent(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case -624915240: 
				arguments[-624915240] = XmlHelper::GetAttribute<Gravity>(element, "gravity");
				break;
			case -1650340647: 
				arguments[-1650340647] = XmlHelper::GetAttribute<MagickColor^>(element, "backgroundColor");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		for each(XmlElement^ element in element->ChildNodes)
		{
			int hashCode = element->Name->GetHashCode();
			switch(hashCode)
			{
			case -1835407281: 
				arguments[-1835407281] = CreateMagickGeometry(element);
				break;
			default:
				throw gcnew NotImplementedException(element->Name);
			}
		}
		if (OnlyContains(arguments, -1835407281))
		{
			image->Extent((MagickGeometry^)arguments[-1835407281]);
			return;
		}
		if (OnlyContains(arguments, -1835407281, -624915240))
		{
			image->Extent((MagickGeometry^)arguments[-1835407281], (Gravity)arguments[-624915240]);
			return;
		}
		if (OnlyContains(arguments, -1835407281, -1650340647))
		{
			image->Extent((MagickGeometry^)arguments[-1835407281], (MagickColor^)arguments[-1650340647]);
			return;
		}
		if (OnlyContains(arguments, -1835407281, -624915240, -1650340647))
		{
			image->Extent((MagickGeometry^)arguments[-1835407281], (Gravity)arguments[-624915240], (MagickColor^)arguments[-1650340647]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'extent', allowed combinations are: [geometry] [geometry, gravity] [geometry, backgroundColor] [geometry, gravity, backgroundColor]");
	}
	//==============================================================================================
	void MagickScript::ExecuteFillColor(XmlElement^ element, MagickImage^ image)
	{
		image->FillColor = XmlHelper::GetAttribute<MagickColor^>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteFillPattern(XmlElement^ element, MagickImage^ image)
	{
		image->FillPattern = CreateMagickImage((XmlElement^)element->SelectSingleNode("read"));
	}
	//==============================================================================================
	void MagickScript::ExecuteFillRule(XmlElement^ element, MagickImage^ image)
	{
		image->FillRule = XmlHelper::GetAttribute<FillRule>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteFilterType(XmlElement^ element, MagickImage^ image)
	{
		image->FilterType = XmlHelper::GetAttribute<FilterType>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteFlashPixView(XmlElement^ element, MagickImage^ image)
	{
		image->FlashPixView = XmlHelper::GetAttribute<String^>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteFlip(MagickImage^ image)
	{
		image->Flip();
	}
	//==============================================================================================
	void MagickScript::ExecuteFloodFill(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case 1586258015: 
				arguments[1586258015] = XmlHelper::GetAttribute<MagickColor^>(element, "color");
				break;
			case -1096349590: 
				arguments[-1096349590] = XmlHelper::GetAttribute<MagickColor^>(element, "borderColor");
				break;
			case -842352680: 
				arguments[-842352680] = XmlHelper::GetAttribute<int>(element, "x");
				break;
			case -842352681: 
				arguments[-842352681] = XmlHelper::GetAttribute<int>(element, "y");
				break;
			case -1898387216: 
				arguments[-1898387216] = XmlHelper::GetAttribute<int>(element, "alpha");
				break;
			case 1995694144: 
				arguments[1995694144] = XmlHelper::GetAttribute<PaintMethod>(element, "paintMethod");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		for each(XmlElement^ element in element->ChildNodes)
		{
			int hashCode = element->Name->GetHashCode();
			switch(hashCode)
			{
			case 820750591: 
				arguments[820750591] = CreateMagickImage(element);
				break;
			case -1835407281: 
				arguments[-1835407281] = CreateMagickGeometry(element);
				break;
			default:
				throw gcnew NotImplementedException(element->Name);
			}
		}
		if (OnlyContains(arguments, 820750591, -1835407281))
		{
			image->FloodFill((MagickImage^)arguments[820750591], (MagickGeometry^)arguments[-1835407281]);
			return;
		}
		if (OnlyContains(arguments, 1586258015, -1835407281))
		{
			image->FloodFill((MagickColor^)arguments[1586258015], (MagickGeometry^)arguments[-1835407281]);
			return;
		}
		if (OnlyContains(arguments, 820750591, -1835407281, -1096349590))
		{
			image->FloodFill((MagickImage^)arguments[820750591], (MagickGeometry^)arguments[-1835407281], (MagickColor^)arguments[-1096349590]);
			return;
		}
		if (OnlyContains(arguments, 820750591, -842352680, -842352681))
		{
			image->FloodFill((MagickImage^)arguments[820750591], (int)arguments[-842352680], (int)arguments[-842352681]);
			return;
		}
		if (OnlyContains(arguments, 1586258015, -1835407281, -1096349590))
		{
			image->FloodFill((MagickColor^)arguments[1586258015], (MagickGeometry^)arguments[-1835407281], (MagickColor^)arguments[-1096349590]);
			return;
		}
		if (OnlyContains(arguments, 1586258015, -842352680, -842352681))
		{
			image->FloodFill((MagickColor^)arguments[1586258015], (int)arguments[-842352680], (int)arguments[-842352681]);
			return;
		}
		if (OnlyContains(arguments, 820750591, -842352680, -842352681, -1096349590))
		{
			image->FloodFill((MagickImage^)arguments[820750591], (int)arguments[-842352680], (int)arguments[-842352681], (MagickColor^)arguments[-1096349590]);
			return;
		}
		if (OnlyContains(arguments, 1586258015, -842352680, -842352681, -1096349590))
		{
			image->FloodFill((MagickColor^)arguments[1586258015], (int)arguments[-842352680], (int)arguments[-842352681], (MagickColor^)arguments[-1096349590]);
			return;
		}
		if (OnlyContains(arguments, -1898387216, -842352680, -842352681, 1995694144))
		{
			image->FloodFill((int)arguments[-1898387216], (int)arguments[-842352680], (int)arguments[-842352681], (PaintMethod)arguments[1995694144]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'floodFill', allowed combinations are: [image, geometry] [color, geometry] [image, geometry, borderColor] [image, x, y] [color, geometry, borderColor] [color, x, y] [image, x, y, borderColor] [color, x, y, borderColor] [alpha, x, y, paintMethod]");
	}
	//==============================================================================================
	void MagickScript::ExecuteFlop(MagickImage^ image)
	{
		image->Flop();
	}
	//==============================================================================================
	void MagickScript::ExecuteFont(XmlElement^ element, MagickImage^ image)
	{
		image->Font = XmlHelper::GetAttribute<String^>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteFontPointsize(XmlElement^ element, MagickImage^ image)
	{
		image->FontPointsize = XmlHelper::GetAttribute<double>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteFormat(XmlElement^ element, MagickImage^ image)
	{
		image->Format = XmlHelper::GetAttribute<MagickFormat>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteFrame(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case 1910834845: 
				arguments[1910834845] = XmlHelper::GetAttribute<int>(element, "width");
				break;
			case 452723731: 
				arguments[452723731] = XmlHelper::GetAttribute<int>(element, "height");
				break;
			case 218242056: 
				arguments[218242056] = XmlHelper::GetAttribute<int>(element, "innerBevel");
				break;
			case -943334853: 
				arguments[-943334853] = XmlHelper::GetAttribute<int>(element, "outerBevel");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		for each(XmlElement^ element in element->ChildNodes)
		{
			int hashCode = element->Name->GetHashCode();
			switch(hashCode)
			{
			case -1835407281: 
				arguments[-1835407281] = CreateMagickGeometry(element);
				break;
			default:
				throw gcnew NotImplementedException(element->Name);
			}
		}
		if (arguments->Count == 0)
		{
			image->Frame();
			return;
		}
		if (OnlyContains(arguments, -1835407281))
		{
			image->Frame((MagickGeometry^)arguments[-1835407281]);
			return;
		}
		if (OnlyContains(arguments, 1910834845, 452723731))
		{
			image->Frame((int)arguments[1910834845], (int)arguments[452723731]);
			return;
		}
		if (OnlyContains(arguments, 1910834845, 452723731, 218242056, -943334853))
		{
			image->Frame((int)arguments[1910834845], (int)arguments[452723731], (int)arguments[218242056], (int)arguments[-943334853]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'frame', allowed combinations are: [] [geometry] [width, height] [width, height, innerBevel, outerBevel]");
	}
	//==============================================================================================
	void MagickScript::ExecuteFx(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case 928723642: 
				arguments[928723642] = XmlHelper::GetAttribute<String^>(element, "expression");
				break;
			case -1167029596: 
				arguments[-1167029596] = XmlHelper::GetAttribute<Channels>(element, "channels");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (OnlyContains(arguments, 928723642))
		{
			image->Fx((String^)arguments[928723642]);
			return;
		}
		if (OnlyContains(arguments, 928723642, -1167029596))
		{
			image->Fx((String^)arguments[928723642], (Channels)arguments[-1167029596]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'fx', allowed combinations are: [expression] [expression, channels]");
	}
	//==============================================================================================
	void MagickScript::ExecuteGamma(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case -801349223: 
				arguments[-801349223] = XmlHelper::GetAttribute<double>(element, "value");
				break;
			case 73938860: 
				arguments[73938860] = XmlHelper::GetAttribute<double>(element, "gammeRed");
				break;
			case 301532703: 
				arguments[301532703] = XmlHelper::GetAttribute<double>(element, "gammeGreen");
				break;
			case -1732460095: 
				arguments[-1732460095] = XmlHelper::GetAttribute<double>(element, "gammeBlue");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (OnlyContains(arguments, -801349223))
		{
			image->Gamma((double)arguments[-801349223]);
			return;
		}
		if (OnlyContains(arguments, 73938860, 301532703, -1732460095))
		{
			image->Gamma((double)arguments[73938860], (double)arguments[301532703], (double)arguments[-1732460095]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'gamma', allowed combinations are: [value] [gammeRed, gammeGreen, gammeBlue]");
	}
	//==============================================================================================
	void MagickScript::ExecuteGaussianBlur(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case 1910834845: 
				arguments[1910834845] = XmlHelper::GetAttribute<double>(element, "width");
				break;
			case 1934517483: 
				arguments[1934517483] = XmlHelper::GetAttribute<double>(element, "sigma");
				break;
			case -1167029596: 
				arguments[-1167029596] = XmlHelper::GetAttribute<Channels>(element, "channels");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (OnlyContains(arguments, 1910834845, 1934517483))
		{
			image->GaussianBlur((double)arguments[1910834845], (double)arguments[1934517483]);
			return;
		}
		if (OnlyContains(arguments, 1910834845, 1934517483, -1167029596))
		{
			image->GaussianBlur((double)arguments[1910834845], (double)arguments[1934517483], (Channels)arguments[-1167029596]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'gaussianBlur', allowed combinations are: [width, sigma] [width, sigma, channels]");
	}
	//==============================================================================================
	void MagickScript::ExecuteGifDisposeMethod(XmlElement^ element, MagickImage^ image)
	{
		image->GifDisposeMethod = XmlHelper::GetAttribute<GifDisposeMethod>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteHaldClut(XmlElement^ element, MagickImage^ image)
	{
		MagickImage^ image_ = CreateMagickImage((XmlElement^)element->SelectSingleNode("read"));
		image->HaldClut(image_);
	}
	//==============================================================================================
	void MagickScript::ExecuteHasMatte(XmlElement^ element, MagickImage^ image)
	{
		image->HasMatte = XmlHelper::GetAttribute<bool>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteImplode(XmlElement^ element, MagickImage^ image)
	{
		double factor_ = XmlHelper::GetAttribute<double>(element, "factor");
		image->Implode(factor_);
	}
	//==============================================================================================
	void MagickScript::ExecuteInverseFourierTransform(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case 1482132450: 
				arguments[1482132450] = XmlHelper::GetAttribute<bool>(element, "magnitude");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		for each(XmlElement^ element in element->ChildNodes)
		{
			int hashCode = element->Name->GetHashCode();
			switch(hashCode)
			{
			case 820750591: 
				arguments[820750591] = CreateMagickImage(element);
				break;
			default:
				throw gcnew NotImplementedException(element->Name);
			}
		}
		if (OnlyContains(arguments, 820750591))
		{
			image->InverseFourierTransform((MagickImage^)arguments[820750591]);
			return;
		}
		if (OnlyContains(arguments, 820750591, 1482132450))
		{
			image->InverseFourierTransform((MagickImage^)arguments[820750591], (bool)arguments[1482132450]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'inverseFourierTransform', allowed combinations are: [image] [image, magnitude]");
	}
	//==============================================================================================
	void MagickScript::ExecuteIsMonochrome(XmlElement^ element, MagickImage^ image)
	{
		image->IsMonochrome = XmlHelper::GetAttribute<bool>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteLabel(XmlElement^ element, MagickImage^ image)
	{
		image->Label = XmlHelper::GetAttribute<String^>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteLevel(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case -364514606: 
				arguments[-364514606] = XmlHelper::GetAttribute<Magick::Quantum>(element, "blackPoint");
				break;
			case -1248320203: 
				arguments[-1248320203] = XmlHelper::GetAttribute<Magick::Quantum>(element, "whitePoint");
				break;
			case 1463591266: 
				arguments[1463591266] = XmlHelper::GetAttribute<double>(element, "midpoint");
				break;
			case -1167029596: 
				arguments[-1167029596] = XmlHelper::GetAttribute<Channels>(element, "channels");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (OnlyContains(arguments, -364514606, -1248320203))
		{
			image->Level((Magick::Quantum)arguments[-364514606], (Magick::Quantum)arguments[-1248320203]);
			return;
		}
		if (OnlyContains(arguments, -364514606, -1248320203, 1463591266))
		{
			image->Level((Magick::Quantum)arguments[-364514606], (Magick::Quantum)arguments[-1248320203], (double)arguments[1463591266]);
			return;
		}
		if (OnlyContains(arguments, -364514606, -1248320203, -1167029596))
		{
			image->Level((Magick::Quantum)arguments[-364514606], (Magick::Quantum)arguments[-1248320203], (Channels)arguments[-1167029596]);
			return;
		}
		if (OnlyContains(arguments, -364514606, -1248320203, 1463591266, -1167029596))
		{
			image->Level((Magick::Quantum)arguments[-364514606], (Magick::Quantum)arguments[-1248320203], (double)arguments[1463591266], (Channels)arguments[-1167029596]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'level', allowed combinations are: [blackPoint, whitePoint] [blackPoint, whitePoint, midpoint] [blackPoint, whitePoint, channels] [blackPoint, whitePoint, midpoint, channels]");
	}
	//==============================================================================================
	void MagickScript::ExecuteLower(XmlElement^ element, MagickImage^ image)
	{
		int size_ = XmlHelper::GetAttribute<int>(element, "size");
		image->Lower(size_);
	}
	//==============================================================================================
	void MagickScript::ExecuteMagnify(MagickImage^ image)
	{
		image->Magnify();
	}
	//==============================================================================================
	void MagickScript::ExecuteMap(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case 436976571: 
				arguments[436976571] = XmlHelper::GetAttribute<bool>(element, "dither");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		for each(XmlElement^ element in element->ChildNodes)
		{
			int hashCode = element->Name->GetHashCode();
			switch(hashCode)
			{
			case 820750591: 
				arguments[820750591] = CreateMagickImage(element);
				break;
			default:
				throw gcnew NotImplementedException(element->Name);
			}
		}
		if (OnlyContains(arguments, 820750591))
		{
			image->Map((MagickImage^)arguments[820750591]);
			return;
		}
		if (OnlyContains(arguments, 820750591, 436976571))
		{
			image->Map((MagickImage^)arguments[820750591], (bool)arguments[436976571]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'map', allowed combinations are: [image] [image, dither]");
	}
	//==============================================================================================
	void MagickScript::ExecuteMatteColor(XmlElement^ element, MagickImage^ image)
	{
		image->MatteColor = XmlHelper::GetAttribute<MagickColor^>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteMedianFilter(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case -1204484339: 
				arguments[-1204484339] = XmlHelper::GetAttribute<double>(element, "radius");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (arguments->Count == 0)
		{
			image->MedianFilter();
			return;
		}
		if (OnlyContains(arguments, -1204484339))
		{
			image->MedianFilter((double)arguments[-1204484339]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'medianFilter', allowed combinations are: [] [radius]");
	}
	//==============================================================================================
	void MagickScript::ExecuteMinify(MagickImage^ image)
	{
		image->Minify();
	}
	//==============================================================================================
	void MagickScript::ExecuteModulate(XmlElement^ element, MagickImage^ image)
	{
		Percentage brightness_ = XmlHelper::GetAttribute<Percentage>(element, "brightness");
		Percentage saturation_ = XmlHelper::GetAttribute<Percentage>(element, "saturation");
		Percentage hue_ = XmlHelper::GetAttribute<Percentage>(element, "hue");
		image->Modulate(brightness_, saturation_, hue_);
	}
	//==============================================================================================
	void MagickScript::ExecuteModulusDepth(XmlElement^ element, MagickImage^ image)
	{
		image->ModulusDepth = XmlHelper::GetAttribute<int>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteMotionBlur(XmlElement^ element, MagickImage^ image)
	{
		double radius_ = XmlHelper::GetAttribute<double>(element, "radius");
		double sigma_ = XmlHelper::GetAttribute<double>(element, "sigma");
		double angle_ = XmlHelper::GetAttribute<double>(element, "angle");
		image->MotionBlur(radius_, sigma_, angle_);
	}
	//==============================================================================================
	void MagickScript::ExecuteNegate(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case 721159152: 
				arguments[721159152] = XmlHelper::GetAttribute<bool>(element, "onlyGrayscale");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (arguments->Count == 0)
		{
			image->Negate();
			return;
		}
		if (OnlyContains(arguments, 721159152))
		{
			image->Negate((bool)arguments[721159152]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'negate', allowed combinations are: [] [onlyGrayscale]");
	}
	//==============================================================================================
	void MagickScript::ExecuteNormalize(MagickImage^ image)
	{
		image->Normalize();
	}
	//==============================================================================================
	void MagickScript::ExecuteOilPaint(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case -1204484339: 
				arguments[-1204484339] = XmlHelper::GetAttribute<double>(element, "radius");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (arguments->Count == 0)
		{
			image->OilPaint();
			return;
		}
		if (OnlyContains(arguments, -1204484339))
		{
			image->OilPaint((double)arguments[-1204484339]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'oilPaint', allowed combinations are: [] [radius]");
	}
	//==============================================================================================
	void MagickScript::ExecuteOrientation(XmlElement^ element, MagickImage^ image)
	{
		image->Orientation = XmlHelper::GetAttribute<OrientationType>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecutePage(XmlElement^ element, MagickImage^ image)
	{
		image->Page = CreateMagickGeometry((XmlElement^)element->SelectSingleNode("geometry"));
	}
	//==============================================================================================
	void MagickScript::ExecuteQuality(XmlElement^ element, MagickImage^ image)
	{
		image->Quality = XmlHelper::GetAttribute<int>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteQuantize(MagickImage^ image)
	{
		image->Quantize();
	}
	//==============================================================================================
	void MagickScript::ExecuteQuantizeColors(XmlElement^ element, MagickImage^ image)
	{
		image->QuantizeColors = XmlHelper::GetAttribute<int>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteQuantizeColorSpace(XmlElement^ element, MagickImage^ image)
	{
		image->QuantizeColorSpace = XmlHelper::GetAttribute<ColorSpace>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteQuantizeDither(XmlElement^ element, MagickImage^ image)
	{
		image->QuantizeDither = XmlHelper::GetAttribute<bool>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteQuantizeTreeDepth(XmlElement^ element, MagickImage^ image)
	{
		image->QuantizeTreeDepth = XmlHelper::GetAttribute<int>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteQuantumOperator(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case -1167029596: 
				arguments[-1167029596] = XmlHelper::GetAttribute<Channels>(element, "channels");
				break;
			case -39326037: 
				arguments[-39326037] = XmlHelper::GetAttribute<EvaluateOperator>(element, "evaluateOperator");
				break;
			case -801349223: 
				arguments[-801349223] = XmlHelper::GetAttribute<double>(element, "value");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		for each(XmlElement^ element in element->ChildNodes)
		{
			int hashCode = element->Name->GetHashCode();
			switch(hashCode)
			{
			case -1835407281: 
				arguments[-1835407281] = CreateMagickGeometry(element);
				break;
			default:
				throw gcnew NotImplementedException(element->Name);
			}
		}
		if (OnlyContains(arguments, -1167029596, -39326037, -801349223))
		{
			image->QuantumOperator((Channels)arguments[-1167029596], (EvaluateOperator)arguments[-39326037], (double)arguments[-801349223]);
			return;
		}
		if (OnlyContains(arguments, -1167029596, -1835407281, -39326037, -801349223))
		{
			image->QuantumOperator((Channels)arguments[-1167029596], (MagickGeometry^)arguments[-1835407281], (EvaluateOperator)arguments[-39326037], (double)arguments[-801349223]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'quantumOperator', allowed combinations are: [channels, evaluateOperator, value] [channels, geometry, evaluateOperator, value]");
	}
	//==============================================================================================
	void MagickScript::ExecuteRaise(XmlElement^ element, MagickImage^ image)
	{
		int size_ = XmlHelper::GetAttribute<int>(element, "size");
		image->Raise(size_);
	}
	//==============================================================================================
	void MagickScript::ExecuteRandomThreshold(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case -2144640805: 
				arguments[-2144640805] = XmlHelper::GetAttribute<Magick::Quantum>(element, "low");
				break;
			case 1919986319: 
				arguments[1919986319] = XmlHelper::GetAttribute<Magick::Quantum>(element, "high");
				break;
			case 58809550: 
				arguments[58809550] = XmlHelper::GetAttribute<Percentage>(element, "percentageLow");
				break;
			case 887982706: 
				arguments[887982706] = XmlHelper::GetAttribute<Percentage>(element, "percentageHigh");
				break;
			case -1167029596: 
				arguments[-1167029596] = XmlHelper::GetAttribute<Channels>(element, "channels");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (OnlyContains(arguments, -2144640805, 1919986319))
		{
			image->RandomThreshold((Magick::Quantum)arguments[-2144640805], (Magick::Quantum)arguments[1919986319]);
			return;
		}
		if (OnlyContains(arguments, 58809550, 887982706))
		{
			image->RandomThreshold((Percentage)arguments[58809550], (Percentage)arguments[887982706]);
			return;
		}
		if (OnlyContains(arguments, -2144640805, 1919986319, -1167029596))
		{
			image->RandomThreshold((Magick::Quantum)arguments[-2144640805], (Magick::Quantum)arguments[1919986319], (Channels)arguments[-1167029596]);
			return;
		}
		if (OnlyContains(arguments, 58809550, 887982706, -1167029596))
		{
			image->RandomThreshold((Percentage)arguments[58809550], (Percentage)arguments[887982706], (Channels)arguments[-1167029596]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'randomThreshold', allowed combinations are: [low, high] [percentageLow, percentageHigh] [low, high, channels] [percentageLow, percentageHigh, channels]");
	}
	//==============================================================================================
	void MagickScript::ExecuteReduceNoise(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case 823592379: 
				arguments[823592379] = XmlHelper::GetAttribute<int>(element, "order");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (arguments->Count == 0)
		{
			image->ReduceNoise();
			return;
		}
		if (OnlyContains(arguments, 823592379))
		{
			image->ReduceNoise((int)arguments[823592379]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'reduceNoise', allowed combinations are: [] [order]");
	}
	//==============================================================================================
	void MagickScript::ExecuteRemoveProfile(XmlElement^ element, MagickImage^ image)
	{
		String^ name_ = XmlHelper::GetAttribute<String^>(element, "name");
		image->RemoveProfile(name_);
	}
	//==============================================================================================
	void MagickScript::ExecuteRenderingIntent(XmlElement^ element, MagickImage^ image)
	{
		image->RenderingIntent = XmlHelper::GetAttribute<RenderingIntent>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteResize(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case 1117231853: 
				arguments[1117231853] = XmlHelper::GetAttribute<Percentage>(element, "percentage");
				break;
			case 1419665482: 
				arguments[1419665482] = XmlHelper::GetAttribute<Percentage>(element, "percentageWidth");
				break;
			case 453075322: 
				arguments[453075322] = XmlHelper::GetAttribute<Percentage>(element, "percentageHeight");
				break;
			case 1910834845: 
				arguments[1910834845] = XmlHelper::GetAttribute<int>(element, "width");
				break;
			case 452723731: 
				arguments[452723731] = XmlHelper::GetAttribute<int>(element, "height");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (OnlyContains(arguments, 1117231853))
		{
			image->Resize((Percentage)arguments[1117231853]);
			return;
		}
		if (OnlyContains(arguments, 1419665482, 453075322))
		{
			image->Resize((Percentage)arguments[1419665482], (Percentage)arguments[453075322]);
			return;
		}
		if (OnlyContains(arguments, 1910834845, 452723731))
		{
			image->Resize((int)arguments[1910834845], (int)arguments[452723731]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'resize', allowed combinations are: [percentage] [percentageWidth, percentageHeight] [width, height]");
	}
	//==============================================================================================
	void MagickScript::ExecuteResolutionUnits(XmlElement^ element, MagickImage^ image)
	{
		image->ResolutionUnits = XmlHelper::GetAttribute<Resolution>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteRoll(XmlElement^ element, MagickImage^ image)
	{
		int xOffset_ = XmlHelper::GetAttribute<int>(element, "xOffset");
		int yOffset_ = XmlHelper::GetAttribute<int>(element, "yOffset");
		image->Roll(xOffset_, yOffset_);
	}
	//==============================================================================================
	void MagickScript::ExecuteRotate(XmlElement^ element, MagickImage^ image)
	{
		double degrees_ = XmlHelper::GetAttribute<double>(element, "degrees");
		image->Rotate(degrees_);
	}
	//==============================================================================================
	void MagickScript::ExecuteSample(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case 1117231853: 
				arguments[1117231853] = XmlHelper::GetAttribute<Percentage>(element, "percentage");
				break;
			case 1419665482: 
				arguments[1419665482] = XmlHelper::GetAttribute<Percentage>(element, "percentageWidth");
				break;
			case 453075322: 
				arguments[453075322] = XmlHelper::GetAttribute<Percentage>(element, "percentageHeight");
				break;
			case 1910834845: 
				arguments[1910834845] = XmlHelper::GetAttribute<int>(element, "width");
				break;
			case 452723731: 
				arguments[452723731] = XmlHelper::GetAttribute<int>(element, "height");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (OnlyContains(arguments, 1117231853))
		{
			image->Sample((Percentage)arguments[1117231853]);
			return;
		}
		if (OnlyContains(arguments, 1419665482, 453075322))
		{
			image->Sample((Percentage)arguments[1419665482], (Percentage)arguments[453075322]);
			return;
		}
		if (OnlyContains(arguments, 1910834845, 452723731))
		{
			image->Sample((int)arguments[1910834845], (int)arguments[452723731]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'sample', allowed combinations are: [percentage] [percentageWidth, percentageHeight] [width, height]");
	}
	//==============================================================================================
	void MagickScript::ExecuteScale(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case 1117231853: 
				arguments[1117231853] = XmlHelper::GetAttribute<Percentage>(element, "percentage");
				break;
			case 1419665482: 
				arguments[1419665482] = XmlHelper::GetAttribute<Percentage>(element, "percentageWidth");
				break;
			case 453075322: 
				arguments[453075322] = XmlHelper::GetAttribute<Percentage>(element, "percentageHeight");
				break;
			case 1910834845: 
				arguments[1910834845] = XmlHelper::GetAttribute<int>(element, "width");
				break;
			case 452723731: 
				arguments[452723731] = XmlHelper::GetAttribute<int>(element, "height");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (OnlyContains(arguments, 1117231853))
		{
			image->Scale((Percentage)arguments[1117231853]);
			return;
		}
		if (OnlyContains(arguments, 1419665482, 453075322))
		{
			image->Scale((Percentage)arguments[1419665482], (Percentage)arguments[453075322]);
			return;
		}
		if (OnlyContains(arguments, 1910834845, 452723731))
		{
			image->Scale((int)arguments[1910834845], (int)arguments[452723731]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'scale', allowed combinations are: [percentage] [percentageWidth, percentageHeight] [width, height]");
	}
	//==============================================================================================
	void MagickScript::ExecuteSegment(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case 950178469: 
				arguments[950178469] = XmlHelper::GetAttribute<double>(element, "clusterThreshold");
				break;
			case 2112645885: 
				arguments[2112645885] = XmlHelper::GetAttribute<double>(element, "smoothingThreshold");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (arguments->Count == 0)
		{
			image->Segment();
			return;
		}
		if (OnlyContains(arguments, 950178469, 2112645885))
		{
			image->Segment((double)arguments[950178469], (double)arguments[2112645885]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'segment', allowed combinations are: [] [clusterThreshold, smoothingThreshold]");
	}
	//==============================================================================================
	void MagickScript::ExecuteSeparate(XmlElement^ element, MagickImage^ image)
	{
		Channels channels_ = XmlHelper::GetAttribute<Channels>(element, "channels");
		image->Separate(channels_);
	}
	//==============================================================================================
	void MagickScript::ExecuteShade(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case -1493779931: 
				arguments[-1493779931] = XmlHelper::GetAttribute<double>(element, "azimuth");
				break;
			case 774378973: 
				arguments[774378973] = XmlHelper::GetAttribute<double>(element, "elevation");
				break;
			case 73531561: 
				arguments[73531561] = XmlHelper::GetAttribute<bool>(element, "colorShading");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (arguments->Count == 0)
		{
			image->Shade();
			return;
		}
		if (OnlyContains(arguments, -1493779931, 774378973, 73531561))
		{
			image->Shade((double)arguments[-1493779931], (double)arguments[774378973], (bool)arguments[73531561]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'shade', allowed combinations are: [] [azimuth, elevation, colorShading]");
	}
	//==============================================================================================
	void MagickScript::ExecuteShadow(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case 1586258015: 
				arguments[1586258015] = XmlHelper::GetAttribute<MagickColor^>(element, "color");
				break;
			case -842352680: 
				arguments[-842352680] = XmlHelper::GetAttribute<int>(element, "x");
				break;
			case -842352681: 
				arguments[-842352681] = XmlHelper::GetAttribute<int>(element, "y");
				break;
			case 1934517483: 
				arguments[1934517483] = XmlHelper::GetAttribute<double>(element, "sigma");
				break;
			case -1898387216: 
				arguments[-1898387216] = XmlHelper::GetAttribute<Percentage>(element, "alpha");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (arguments->Count == 0)
		{
			image->Shadow();
			return;
		}
		if (OnlyContains(arguments, 1586258015))
		{
			image->Shadow((MagickColor^)arguments[1586258015]);
			return;
		}
		if (OnlyContains(arguments, -842352680, -842352681, 1934517483, -1898387216))
		{
			image->Shadow((int)arguments[-842352680], (int)arguments[-842352681], (double)arguments[1934517483], (Percentage)arguments[-1898387216]);
			return;
		}
		if (OnlyContains(arguments, -842352680, -842352681, 1934517483, -1898387216, 1586258015))
		{
			image->Shadow((int)arguments[-842352680], (int)arguments[-842352681], (double)arguments[1934517483], (Percentage)arguments[-1898387216], (MagickColor^)arguments[1586258015]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'shadow', allowed combinations are: [] [color] [x, y, sigma, alpha] [x, y, sigma, alpha, color]");
	}
	//==============================================================================================
	void MagickScript::ExecuteSharpen(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case -1167029596: 
				arguments[-1167029596] = XmlHelper::GetAttribute<Channels>(element, "channels");
				break;
			case -1204484339: 
				arguments[-1204484339] = XmlHelper::GetAttribute<double>(element, "radius");
				break;
			case 1934517483: 
				arguments[1934517483] = XmlHelper::GetAttribute<double>(element, "sigma");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (arguments->Count == 0)
		{
			image->Sharpen();
			return;
		}
		if (OnlyContains(arguments, -1167029596))
		{
			image->Sharpen((Channels)arguments[-1167029596]);
			return;
		}
		if (OnlyContains(arguments, -1204484339, 1934517483))
		{
			image->Sharpen((double)arguments[-1204484339], (double)arguments[1934517483]);
			return;
		}
		if (OnlyContains(arguments, -1204484339, 1934517483, -1167029596))
		{
			image->Sharpen((double)arguments[-1204484339], (double)arguments[1934517483], (Channels)arguments[-1167029596]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'sharpen', allowed combinations are: [] [channels] [radius, sigma] [radius, sigma, channels]");
	}
	//==============================================================================================
	void MagickScript::ExecuteShave(XmlElement^ element, MagickImage^ image)
	{
		int leftRight_ = XmlHelper::GetAttribute<int>(element, "leftRight");
		int topBottom_ = XmlHelper::GetAttribute<int>(element, "topBottom");
		image->Shave(leftRight_, topBottom_);
	}
	//==============================================================================================
	void MagickScript::ExecuteShear(XmlElement^ element, MagickImage^ image)
	{
		double xAngle_ = XmlHelper::GetAttribute<double>(element, "xAngle");
		double yAngle_ = XmlHelper::GetAttribute<double>(element, "yAngle");
		image->Shear(xAngle_, yAngle_);
	}
	//==============================================================================================
	void MagickScript::ExecuteSigmoidalContrast(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case 1996399444: 
				arguments[1996399444] = XmlHelper::GetAttribute<bool>(element, "sharpen");
				break;
			case 613907492: 
				arguments[613907492] = XmlHelper::GetAttribute<double>(element, "contrast");
				break;
			case 1463591266: 
				arguments[1463591266] = XmlHelper::GetAttribute<double>(element, "midpoint");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (OnlyContains(arguments, 1996399444, 613907492))
		{
			image->SigmoidalContrast((bool)arguments[1996399444], (double)arguments[613907492]);
			return;
		}
		if (OnlyContains(arguments, 1996399444, 613907492, 1463591266))
		{
			image->SigmoidalContrast((bool)arguments[1996399444], (double)arguments[613907492], (double)arguments[1463591266]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'sigmoidalContrast', allowed combinations are: [sharpen, contrast] [sharpen, contrast, midpoint]");
	}
	//==============================================================================================
	void MagickScript::ExecuteSolarize(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case -829976564: 
				arguments[-829976564] = XmlHelper::GetAttribute<double>(element, "factor");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (arguments->Count == 0)
		{
			image->Solarize();
			return;
		}
		if (OnlyContains(arguments, -829976564))
		{
			image->Solarize((double)arguments[-829976564]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'solarize', allowed combinations are: [] [factor]");
	}
	//==============================================================================================
	void MagickScript::ExecuteStegano(XmlElement^ element, MagickImage^ image)
	{
		MagickImage^ watermark_ = CreateMagickImage((XmlElement^)element->SelectSingleNode("read"));
		image->Stegano(watermark_);
	}
	//==============================================================================================
	void MagickScript::ExecuteStereo(XmlElement^ element, MagickImage^ image)
	{
		MagickImage^ rightImage_ = CreateMagickImage((XmlElement^)element->SelectSingleNode("read"));
		image->Stereo(rightImage_);
	}
	//==============================================================================================
	void MagickScript::ExecuteStrip(MagickImage^ image)
	{
		image->Strip();
	}
	//==============================================================================================
	void MagickScript::ExecuteStrokeAntiAlias(XmlElement^ element, MagickImage^ image)
	{
		image->StrokeAntiAlias = XmlHelper::GetAttribute<bool>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteStrokeColor(XmlElement^ element, MagickImage^ image)
	{
		image->StrokeColor = XmlHelper::GetAttribute<MagickColor^>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteStrokeDashOffset(XmlElement^ element, MagickImage^ image)
	{
		image->StrokeDashOffset = XmlHelper::GetAttribute<double>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteStrokeLineCap(XmlElement^ element, MagickImage^ image)
	{
		image->StrokeLineCap = XmlHelper::GetAttribute<LineCap>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteStrokeLineJoin(XmlElement^ element, MagickImage^ image)
	{
		image->StrokeLineJoin = XmlHelper::GetAttribute<LineJoin>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteStrokeMiterLimit(XmlElement^ element, MagickImage^ image)
	{
		image->StrokeMiterLimit = XmlHelper::GetAttribute<int>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteStrokePattern(XmlElement^ element, MagickImage^ image)
	{
		image->StrokePattern = CreateMagickImage((XmlElement^)element->SelectSingleNode("read"));
	}
	//==============================================================================================
	void MagickScript::ExecuteStrokeWidth(XmlElement^ element, MagickImage^ image)
	{
		image->StrokeWidth = XmlHelper::GetAttribute<double>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteSwirl(XmlElement^ element, MagickImage^ image)
	{
		double degrees_ = XmlHelper::GetAttribute<double>(element, "degrees");
		image->Swirl(degrees_);
	}
	//==============================================================================================
	void MagickScript::ExecuteTextEncoding(XmlElement^ element, MagickImage^ image)
	{
		image->TextEncoding = XmlHelper::GetAttribute<Encoding^>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteTexture(XmlElement^ element, MagickImage^ image)
	{
		MagickImage^ image_ = CreateMagickImage((XmlElement^)element->SelectSingleNode("read"));
		image->Texture(image_);
	}
	//==============================================================================================
	void MagickScript::ExecuteThreshold(XmlElement^ element, MagickImage^ image)
	{
		double value_ = XmlHelper::GetAttribute<double>(element, "value");
		image->Threshold(value_);
	}
	//==============================================================================================
	void MagickScript::ExecuteTileName(XmlElement^ element, MagickImage^ image)
	{
		image->TileName = XmlHelper::GetAttribute<String^>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteTransform(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlElement^ element in element->ChildNodes)
		{
			int hashCode = element->Name->GetHashCode();
			switch(hashCode)
			{
			case 1266452337: 
				arguments[1266452337] = CreateMagickGeometry(element);
				break;
			case -110859025: 
				arguments[-110859025] = CreateMagickGeometry(element);
				break;
			default:
				throw gcnew NotImplementedException(element->Name);
			}
		}
		if (OnlyContains(arguments, 1266452337))
		{
			image->Transform((MagickGeometry^)arguments[1266452337]);
			return;
		}
		if (OnlyContains(arguments, 1266452337, -110859025))
		{
			image->Transform((MagickGeometry^)arguments[1266452337], (MagickGeometry^)arguments[-110859025]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'transform', allowed combinations are: [imageGeometry] [imageGeometry, cropGeometry]");
	}
	//==============================================================================================
	void MagickScript::ExecuteTransformOrigin(XmlElement^ element, MagickImage^ image)
	{
		double x_ = XmlHelper::GetAttribute<double>(element, "x");
		double y_ = XmlHelper::GetAttribute<double>(element, "y");
		image->TransformOrigin(x_, y_);
	}
	//==============================================================================================
	void MagickScript::ExecuteTransformReset(MagickImage^ image)
	{
		image->TransformReset();
	}
	//==============================================================================================
	void MagickScript::ExecuteTransformRotation(XmlElement^ element, MagickImage^ image)
	{
		double angle_ = XmlHelper::GetAttribute<double>(element, "angle");
		image->TransformRotation(angle_);
	}
	//==============================================================================================
	void MagickScript::ExecuteTransformScale(XmlElement^ element, MagickImage^ image)
	{
		double scaleX_ = XmlHelper::GetAttribute<double>(element, "scaleX");
		double scaleY_ = XmlHelper::GetAttribute<double>(element, "scaleY");
		image->TransformScale(scaleX_, scaleY_);
	}
	//==============================================================================================
	void MagickScript::ExecuteTransformSkewX(XmlElement^ element, MagickImage^ image)
	{
		double skewX_ = XmlHelper::GetAttribute<double>(element, "skewX");
		image->TransformSkewX(skewX_);
	}
	//==============================================================================================
	void MagickScript::ExecuteTransformSkewY(XmlElement^ element, MagickImage^ image)
	{
		double skewY_ = XmlHelper::GetAttribute<double>(element, "skewY");
		image->TransformSkewY(skewY_);
	}
	//==============================================================================================
	void MagickScript::ExecuteTransparent(XmlElement^ element, MagickImage^ image)
	{
		MagickColor^ color_ = XmlHelper::GetAttribute<MagickColor^>(element, "color");
		image->Transparent(color_);
	}
	//==============================================================================================
	void MagickScript::ExecuteTransparentChroma(XmlElement^ element, MagickImage^ image)
	{
		MagickColor^ colorLow_ = XmlHelper::GetAttribute<MagickColor^>(element, "colorLow");
		MagickColor^ colorHigh_ = XmlHelper::GetAttribute<MagickColor^>(element, "colorHigh");
		image->TransparentChroma(colorLow_, colorHigh_);
	}
	//==============================================================================================
	void MagickScript::ExecuteTrim(MagickImage^ image)
	{
		image->Trim();
	}
	//==============================================================================================
	void MagickScript::ExecuteUnsharpmask(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case -1204484339: 
				arguments[-1204484339] = XmlHelper::GetAttribute<double>(element, "radius");
				break;
			case 1934517483: 
				arguments[1934517483] = XmlHelper::GetAttribute<double>(element, "sigma");
				break;
			case -1192575494: 
				arguments[-1192575494] = XmlHelper::GetAttribute<double>(element, "amount");
				break;
			case -1291593385: 
				arguments[-1291593385] = XmlHelper::GetAttribute<double>(element, "threshold");
				break;
			case -1167029596: 
				arguments[-1167029596] = XmlHelper::GetAttribute<Channels>(element, "channels");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (OnlyContains(arguments, -1204484339, 1934517483, -1192575494, -1291593385))
		{
			image->Unsharpmask((double)arguments[-1204484339], (double)arguments[1934517483], (double)arguments[-1192575494], (double)arguments[-1291593385]);
			return;
		}
		if (OnlyContains(arguments, -1204484339, 1934517483, -1192575494, -1291593385, -1167029596))
		{
			image->Unsharpmask((double)arguments[-1204484339], (double)arguments[1934517483], (double)arguments[-1192575494], (double)arguments[-1291593385], (Channels)arguments[-1167029596]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'unsharpmask', allowed combinations are: [radius, sigma, amount, threshold] [radius, sigma, amount, threshold, channels]");
	}
	//==============================================================================================
	void MagickScript::ExecuteVerbose(XmlElement^ element, MagickImage^ image)
	{
		image->Verbose = XmlHelper::GetAttribute<bool>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteVirtualPixelMethod(XmlElement^ element, MagickImage^ image)
	{
		image->VirtualPixelMethod = XmlHelper::GetAttribute<VirtualPixelMethod>(element, "value");
	}
	//==============================================================================================
	void MagickScript::ExecuteWave(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case 1437793759: 
				arguments[1437793759] = XmlHelper::GetAttribute<double>(element, "amplitude");
				break;
			case 1212500642: 
				arguments[1212500642] = XmlHelper::GetAttribute<double>(element, "length");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (arguments->Count == 0)
		{
			image->Wave();
			return;
		}
		if (OnlyContains(arguments, 1437793759, 1212500642))
		{
			image->Wave((double)arguments[1437793759], (double)arguments[1212500642]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'wave', allowed combinations are: [] [amplitude, length]");
	}
	//==============================================================================================
	void MagickScript::ExecuteZoom(XmlElement^ element, MagickImage^ image)
	{
		Hashtable^ arguments = gcnew Hashtable();
		for each(XmlAttribute^ attribute in element->Attributes)
		{
			int hashCode = attribute->Name->GetHashCode();
			switch(hashCode)
			{
			case 1117231853: 
				arguments[1117231853] = XmlHelper::GetAttribute<Percentage>(element, "percentage");
				break;
			case 1419665482: 
				arguments[1419665482] = XmlHelper::GetAttribute<Percentage>(element, "percentageWidth");
				break;
			case 453075322: 
				arguments[453075322] = XmlHelper::GetAttribute<Percentage>(element, "percentageHeight");
				break;
			case 1910834845: 
				arguments[1910834845] = XmlHelper::GetAttribute<int>(element, "width");
				break;
			case 452723731: 
				arguments[452723731] = XmlHelper::GetAttribute<int>(element, "height");
				break;
			default:
				throw gcnew NotImplementedException(attribute->Name);
			}
		}
		if (OnlyContains(arguments, 1117231853))
		{
			image->Zoom((Percentage)arguments[1117231853]);
			return;
		}
		if (OnlyContains(arguments, 1419665482, 453075322))
		{
			image->Zoom((Percentage)arguments[1419665482], (Percentage)arguments[453075322]);
			return;
		}
		if (OnlyContains(arguments, 1910834845, 452723731))
		{
			image->Zoom((int)arguments[1910834845], (int)arguments[452723731]);
			return;
		}
		throw gcnew ArgumentException("Invalid argument combination for 'zoom', allowed combinations are: [percentage] [percentageWidth, percentageHeight] [width, height]");
	}
	//==============================================================================================
#pragma endregion
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
			return ExecuteRead(element, nullptr);
		else if (element->Name == "collection")
			return ExecuteCollection(element);
		else
			throw gcnew NotImplementedException(element->Name);
	}
	//==============================================================================================
	void MagickScript::Execute(MagickImage^ image)
	{
		Throw::IfNull("image", image);

		XmlNodeList^ elements = _Script->SelectNodes("/msl/read");
		if (elements->Count != 1)
			throw gcnew InvalidOperationException("This method only works with a script that contains a single read operation.");

		ExecuteRead((XmlElement^)elements[0], image);
	}
	//==============================================================================================
}