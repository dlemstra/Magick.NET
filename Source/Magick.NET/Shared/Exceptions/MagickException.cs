// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.Collections.Generic;

namespace ImageMagick
{
    /// <summary>
    /// Encapsulation of the ImageMagick exception object.
    /// </summary>
    [Serializable]
    public abstract class MagickException : Exception
    {
        [NonSerialized]
        private List<MagickException> _RelatedExceptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        internal MagickException(string message)
          : base(message)
        {
        }

        /// <summary>
        /// Gets the exceptions that are related to this exception.
        /// </summary>
        public IEnumerable<MagickException> RelatedExceptions
        {
            get
            {
                if (_RelatedExceptions == null)
                    return new MagickException[0];

                return _RelatedExceptions;
            }
        }

        internal void SetRelatedException(List<MagickException> relatedExceptions)
        {
            _RelatedExceptions = relatedExceptions;
        }
    }
}
