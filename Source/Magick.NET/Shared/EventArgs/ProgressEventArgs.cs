﻿// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    /// EventArgs for Progress events.
    /// </summary>
    public sealed class ProgressEventArgs : EventArgs
    {
        internal ProgressEventArgs(string origin, int offset, int extent)
        {
            Origin = origin;
            Progress = new Percentage(((offset + 1) / (double)extent) * 100);
        }

        /// <summary>
        /// Gets the originator of this event.
        /// </summary>
        public string Origin
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the rogress percentage.
        /// </summary>
        public Percentage Progress
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the current operation will be canceled.
        /// </summary>
        public bool Cancel
        {
            get;
            set;
        }
    }
}