// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick
{
    internal abstract class ConstNativeInstance : NativeHelper
    {
        private IntPtr _instance = IntPtr.Zero;

        public IntPtr Instance
        {
            get
            {
                if (_instance == IntPtr.Zero)
                    throw new ObjectDisposedException(TypeName);

                return _instance;
            }

            set => _instance = value;
        }

        public bool HasInstance => _instance != IntPtr.Zero;

        protected abstract string TypeName { get; }
    }
}