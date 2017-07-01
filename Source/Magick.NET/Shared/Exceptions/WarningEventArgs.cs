// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System;

namespace ImageMagick
{
    /// <summary>
    /// Arguments for the Warning event.
    /// </summary>
    public sealed class WarningEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WarningEventArgs"/> class.
        /// </summary>
        /// <param name="exception">The MagickWarningException that was thrown.</param>
        public WarningEventArgs(MagickWarningException exception)
        {
            Exception = exception;
        }

        /// <summary>
        /// Gets the message of the exception
        /// </summary>
        public string Message
        {
            get
            {
                return Exception.Message;
            }
        }

        /// <summary>
        /// Gets the MagickWarningException that was thrown
        /// </summary>
        public MagickWarningException Exception
        {
            get;
            private set;
        }
    }
}