//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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

namespace ImageMagick.Web
{
  /// <summary>
  /// Specifies the mode to use for client caching.
  /// </summary>
  public enum CacheControlMode
  {
    /// <summary>
    /// Does not add a max-age to the response.
    /// </summary>
    NoControl,

    /// <summary>
    /// Adds a Cache-Control: max-age=&gt;nnn&lt; header to the response based on the value
    /// specified in the CacheControlMaxAge property.
    /// </summary>
    UseMaxAge
  }
}
