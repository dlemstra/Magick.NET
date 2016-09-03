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
using System.Collections.Generic;
using System.Globalization;

namespace ImageMagick.Defines
{
  ///<summary>
  /// Base class that can create defines.
  ///</summary>
  public abstract class DefineCreator : IDefines
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="DefineCreator"/> class.
    /// </summary>
    /// <param name="format">The format where the defines are for.</param>
    protected DefineCreator(MagickFormat format)
    {
      Format = format;
    }

    /// <summary>
    /// Create a define with the specified name and value.
    /// </summary>
    /// <param name="name">The name of the define.</param>
    /// <param name="value">The value of the define.</param>
    /// <returns></returns>
    protected MagickDefine CreateDefine(string name, bool value)
    {
      return new MagickDefine(Format, name, value.ToString());
    }

    /// <summary>
    /// Create a define with the specified name and value.
    /// </summary>
    /// <param name="name">The name of the define.</param>
    /// <param name="value">The value of the define.</param>
    /// <returns></returns>
    protected MagickDefine CreateDefine(string name, int value)
    {
      return new MagickDefine(Format, name, value.ToString(CultureInfo.InvariantCulture));
    }

    /// <summary>
    /// Create a define with the specified name and value.
    /// </summary>
    /// <param name="name">The name of the define.</param>
    /// <param name="value">The value of the define.</param>
    /// <returns></returns>
    protected MagickDefine CreateDefine(string name, MagickGeometry value)
    {
      if (value == null)
        return null;

      return new MagickDefine(Format, name, value.ToString());
    }

    /// <summary>
    /// Create a define with the specified name and value.
    /// </summary>
    /// <param name="name">The name of the define.</param>
    /// <param name="value">The value of the define.</param>
    /// <returns></returns>
    protected MagickDefine CreateDefine(string name, string value)
    {
      return new MagickDefine(Format, name, value);
    }

    /// <summary>
    /// Create a define with the specified name and value.
    /// </summary>
    /// <param name="name">The name of the define.</param>
    /// <param name="value">The value of the define.</param>
    /// <returns></returns>
    protected MagickDefine CreateDefine<TEnum>(string name, TEnum value)
    {
      return new MagickDefine(Format, name, Enum.GetName(typeof(TEnum), value));
    }

    ///<summary>
    /// The format where the defines are for.
    ///</summary>
    protected MagickFormat Format
    {
      get;
      private set;
    }

    ///<summary>
    /// The defines that should be set as a define on an image
    ///</summary>
    public abstract IEnumerable<IDefine> Defines
    {
      get;
    }
  }
}