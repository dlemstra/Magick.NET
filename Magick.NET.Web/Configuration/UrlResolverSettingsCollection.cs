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

using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace ImageMagick.Web
{
	///=============================================================================================
	/// <summary>
	/// Class that contains the settings for the url resolvers.
	/// </summary>
	[ConfigurationCollection(typeof(UrlResolverSettings), AddItemName = "urlResolver")]
	[SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface")]
	public class UrlResolverSettingsCollection : ConfigurationElementCollection
	{
		///==========================================================================================
		/// <summary>
		/// Initializes a new instance of the UrlResolverSettings class.
		/// </summary>
		/// <returns></returns>
		protected override ConfigurationElement CreateNewElement()
		{
			return new UrlResolverSettings();
		}
		///==========================================================================================
		/// <summary>
		/// Gets the element key for a specified UrlResolverSettings element.
		/// </summary>
		/// <param name="element">The UrlResolverSettings to return the key for.</param>
		/// <returns></returns>
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((UrlResolverSettings)element).TypeName;
		}
		//===========================================================================================
	}
	//==============================================================================================
}
