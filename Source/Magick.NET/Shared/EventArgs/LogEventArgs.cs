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

namespace ImageMagick
{
    /// <summary>
    /// EventArgs for Log events.
    /// </summary>
    public sealed class LogEventArgs : EventArgs
    {
        internal LogEventArgs(LogEvents eventType, string message)
        {
            EventType = eventType;
            Message = message;
        }

        /// <summary>
        /// Gets the type of the log message.
        /// </summary>
        public LogEvents EventType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the type of the log message.
        /// </summary>
        public string Message
        {
            get;
            private set;
        }
    }
}