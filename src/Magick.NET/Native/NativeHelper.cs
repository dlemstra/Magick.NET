// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

internal abstract class NativeHelper
{
    private EventHandler<WarningEventArgs>? _warningEvent;

    public event EventHandler<WarningEventArgs> Warning
    {
        add => _warningEvent += value;
        remove => _warningEvent -= value;
    }

    protected void CheckException(IntPtr exception)
    {
        var magickException = MagickExceptionHelper.Check(exception);
        RaiseWarning(magickException);
    }

    protected void RaiseWarning(MagickException? exception)
    {
        if (_warningEvent is null)
            return;

        if (exception is MagickWarningException warning)
            _warningEvent.Invoke(this, new WarningEventArgs(warning));
    }
}
