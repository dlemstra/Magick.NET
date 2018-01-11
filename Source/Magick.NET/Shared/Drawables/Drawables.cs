// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    /// Class that can be used to chain draw actions.
    /// </summary>
    public sealed partial class Drawables : IEnumerable<IDrawable>
    {
        private readonly Collection<IDrawable> _drawables;

        /// <summary>
        /// Initializes a new instance of the <see cref="Drawables"/> class.
        /// </summary>
        public Drawables()
        {
            _drawables = new Collection<IDrawable>();
        }

        /// <summary>
        /// Draw on the specified image.
        /// </summary>
        /// <param name="image">The image to draw on.</param>
        /// <returns>The current instance.</returns>
        public Drawables Draw(IMagickImage image)
        {
            Throw.IfNull("image", image);

            image.Draw(this);
            return this;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Creates a new <see cref="Paths"/> instance.
        /// </summary>
        /// <returns>A new <see cref="Paths"/> instance.</returns>
        public Paths Paths()
        {
            return new Paths(this);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator.</returns>
        public IEnumerator<IDrawable> GetEnumerator()
        {
            return _drawables.GetEnumerator();
        }
    }
}
