// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

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
        private readonly Drawables _drawables;
        private readonly Collection<IPath> _paths;

        /// <summary>
        /// Initializes a new instance of the <see cref="Paths"/> class.
        /// </summary>
        public Paths()
        {
            _paths = new Collection<IPath>();
        }

        internal Paths(Drawables drawables)
          : this()
        {
            _drawables = drawables;
        }

        /// <summary>
        /// Converts the specified <see cref="Paths"/> to a <see cref="Drawables"/> instance.
        /// </summary>
        /// <param name="paths">The <see cref="Paths"/> to convert.</param>
        public static implicit operator Drawables(Paths paths)
        {
            if (paths is null)
                return null;

            if (paths._drawables == null)
                return new Drawables().Path(paths);

            return paths._drawables.Path(paths);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that iterates through the collection.</returns>
        public IEnumerator<IPath> GetEnumerator() => _paths.GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that iterates through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator() => _paths.GetEnumerator();
    }
}
