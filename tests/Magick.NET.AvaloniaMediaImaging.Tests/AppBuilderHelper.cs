// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Threading;
using Avalonia;

namespace Magick.NET.AvaloniaMediaImaging.Tests;

internal static class AppBuilderHelper
{
    private static readonly SemaphoreSlim _lock = new(1);
    private static bool _setupDone;

    public static void Setup()
    {
        if (_setupDone)
            return;

        _lock.Wait();

        try
        {
            if (_setupDone)
                return;

            AppBuilder
                .Configure<Application>()
                .UsePlatformDetect()
                .SetupWithoutStarting();

            _setupDone = true;
        }
        finally
        {
            _lock.Release();
        }
    }
}
