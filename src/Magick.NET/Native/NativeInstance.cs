// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

internal abstract class NativeInstance : NativeHelper, INativeInstance
{
    private IntPtr _instance = IntPtr.Zero;

    public static INativeInstance Zero
        => new ZeroInstance();

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

    public bool IsDisposed
        => _instance == IntPtr.Zero;

    protected abstract string TypeName { get; }

    public void Dispose()
    {
        Instance = IntPtr.Zero;
        GC.SuppressFinalize(this);
    }

    protected abstract void Dispose(IntPtr instance);

    protected void CheckException(IntPtr exception, IntPtr result)
    {
        var magickException = MagickExceptionHelper.Create(exception);
        if (magickException is null)
            return;

        if (magickException is MagickErrorException)
        {
            if (result != IntPtr.Zero)
                Dispose(result);
            throw magickException;
        }

        RaiseWarning(magickException);

        if (result == IntPtr.Zero)
            throw new MagickErrorException("The operation returned null but did not raise an exception.");
    }

    private class ZeroInstance : INativeInstance
    {
        public IntPtr Instance
            => IntPtr.Zero;

        public void Dispose()
        {
        }
    }
}
