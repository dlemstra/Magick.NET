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
    internal abstract class NativeHelper
    {
        private EventHandler<WarningEventArgs> _warningEvent;

        public event EventHandler<WarningEventArgs> Warning
        {
            add
            {
                _warningEvent += value;
            }
            remove
            {
                _warningEvent -= value;
            }
        }

        protected void CheckException(IntPtr exception)
        {
            MagickException magickException = MagickExceptionHelper.Check(exception);
            RaiseWarning(magickException);
        }

        protected void RaiseWarning(MagickException exception)
        {
            if (_warningEvent == null)
                return;

            MagickWarningException warning = exception as MagickWarningException;
            if (warning != null)
                _warningEvent.Invoke(this, new WarningEventArgs(warning));
        }
    }
}
