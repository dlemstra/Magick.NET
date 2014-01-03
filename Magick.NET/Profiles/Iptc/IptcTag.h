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

#include "Stdafx.h"

namespace ImageMagick
{
	///=============================================================================================
	/// <summary>
	/// All iptc tags.
	/// </summary>
	public enum class IptcTag
	{
		Unknown = 0,
		Title = 5,
		EditStatus = 7,
		Priority = 10,
		Category = 15,
		SupplementalCategories = 20,
		FixtureIdentifier = 22,
		Keyword = 25,
		ReleaseDate = 30,
		ReleaseTime = 35,
		SpecialInstructions = 40,
		ReferenceService = 45,
		ReferenceDate = 47,
		ReferenceNumber = 50,
		CreatedDate = 55,
		CreatedTime = 60,
		OriginatingProgram = 65,
		ProgramVersion = 70,
		ObjectCycle = 75,
		Byline = 80,
		BylineTitle = 85,
		City = 90,
		ProvinceState = 95,
		CountryCode = 100,
		Country = 101,
		OriginalTransmissionReference = 103,
		Headline = 105,
		Credit = 110,
		Source = 115,
		CopyrightNotice = 116,
		Caption = 120,
		LocalCaption = 121,
		CaptionWriter = 122,
		CustomField1 = 200,
		CustomField2 = 201,
		CustomField3 = 202,
		CustomField4 = 203,
		CustomField5 = 204,
		CustomField6 = 205,
		CustomField7 = 206,
		CustomField8 = 207,
		CustomField9 = 208,
		CustomField10 = 209,
		CustomField11 = 210,
		CustomField12 = 211,
		CustomField13 = 212,
		CustomField14 = 213,
		CustomField15 = 214,
		CustomField16 = 215,
		CustomField17 = 216,
		CustomField18 = 217,
		CustomField19 = 218,
		CustomField20 = 219,
	};
	//==============================================================================================
}