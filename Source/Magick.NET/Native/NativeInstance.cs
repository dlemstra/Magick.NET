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

using System;

namespace ImageMagick
{
    internal abstract class NativeInstance : NativeHelper, INativeInstance, IDisposable
    {
        private IntPtr _instance = IntPtr.Zero;

        public static INativeInstance Zero => new ZeroInstance();

        public IntPtr Instance
        {
            get
            {
                if (_instance == IntPtr.Zero)
                    throw new ObjectDisposedException(TypeName);

                return _instance;
            }

            set
            {
                if (_instance != IntPtr.Zero)
                    Dispose(_instance);
                _instance = value;
            }
        }

        public bool IsDisposed => _instance == IntPtr.Zero;

        protected abstract string TypeName
        {
            get;
        }

        public void Dispose()
        {
            Instance = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        protected abstract void Dispose(IntPtr instance);

        protected void CheckException(IntPtr exception, IntPtr result)
        {
            MagickException magickException = MagickExceptionHelper.Create(exception);
            if (MagickExceptionHelper.IsError(magickException))
            {
                if (result != IntPtr.Zero)
                    Dispose(result);
                throw magickException;
            }

            RaiseWarning(magickException);
        }

        private class ZeroInstance : INativeInstance
        {
            public IntPtr Instance => IntPtr.Zero;

            public void Dispose()
            {
            }
        }
    }
}
