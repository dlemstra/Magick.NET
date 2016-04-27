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

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ImageMagick
{
  /// <summary>
  /// Class that can be used to chain path actions.
  /// </summary>
  public sealed partial class Paths : IEnumerable<IPath>
  {
    private Drawables _Drawables;
    private Collection<IPath> _Paths;

    IEnumerator IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    internal Paths(Drawables drawables)
      : this()
    {
      _Drawables = drawables;
    }

    /// <summary>
    /// Initializes a new instance of the Paths class.
    /// </summary>
    public Paths()
    {
      _Paths = new Collection<IPath>();
    }

    ///<summary>
    /// Converts the specified Paths to a DrawablePath instance.
    ///</summary>
    public static implicit operator Drawables(Paths paths)
    {
      if (ReferenceEquals(paths, null))
        return null;

      if (paths._Drawables == null)
        return new Drawables().Path(paths);

      return paths._Drawables.Path(paths);
    }

    /// <summary>
    ///  Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns></returns>
    public IEnumerator<IPath> GetEnumerator()
    {
      return _Paths.GetEnumerator();
    }
  }
}