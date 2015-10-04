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

using System;

namespace ImageMagick
{
  ///<summary>
  /// Class that contains data for the Read event.
  ///</summary>
  public sealed class ScriptReadEventArgs : EventArgs
  {
    internal ScriptReadEventArgs(string id, MagickReadSettings settings)
    {
      Id = id;
      Settings = settings;
    }

    ///<summary>
    /// The ID of the image.
    ///</summary>
    public string Id
    {
      get;
      private set;
    }

    ///<summary>
    /// The image that was read.
    ///</summary>
    public MagickImage Image
    {
      get;
      set;
    }

    ///<summary>
    /// The read settings for the image.
    ///</summary>
    public MagickReadSettings Settings
    {
      get;
      private set;
    }
  }
}