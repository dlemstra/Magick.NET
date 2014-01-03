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

using System;
using System.Xml.XPath;

namespace ImageMagick.Web
{
	///=============================================================================================
	/// <summary>
	/// Defines an interface that is used to resolve a file and script from the specified request.
	/// </summary>
	public interface IUrlResolver
	{
		///==========================================================================================
		/// <summary>
		/// The name of the file.
		/// </summary>
		string FileName
		{
			get;
		}
		///==========================================================================================
		/// <summary>
		/// The format of the output image.
		/// </summary>
		MagickFormat Format
		{
			get;
		}
		///==========================================================================================
		/// <summary>
		/// The script to use.
		/// </summary>
		IXPathNavigable Script
		{
			get;
		}
		///==========================================================================================
		/// <summary>
		/// Returns true if the specified url could be resolved to a file name and script.
		/// </summary>
		/// <param name="url">The url to resolve.</param>
		bool Resolve(Uri url);
		//===========================================================================================
	}
	//============================================================================================== 
}
