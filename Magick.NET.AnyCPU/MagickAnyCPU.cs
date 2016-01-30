//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.IO;

namespace ImageMagick
{
  ///<summary>
  /// Class that can be used to initialize the AnyCPU version of Magick.NET.
  ///</summary>
  public static class MagickAnyCPU
  {
    private static string _CacheDirectory = Path.GetTempPath();

    ///<summary>
    /// Sets the directory that will be used by Magick.NET to store the embedded assemblies.
    ///</summary>
    public static string CacheDirectory
    {
      get
      {
        return _CacheDirectory;
      }
      set
      {
        if (!Directory.Exists(value))
          throw new InvalidOperationException("The specified directory does not exist.");
        _CacheDirectory = value;
      }
    }
  }
}
