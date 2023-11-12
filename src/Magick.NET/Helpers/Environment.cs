// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

internal static partial class Environment
{
    private static readonly object _lock = new object();
    private static bool _initialized;

    public static void Initialize()
    {
        if (_initialized)
            return;

        lock (_lock)
        {
            if (_initialized)
                return;

            NativeEnvironment.Initialize();
            _initialized = true;
        }
    }

    public static string? GetEnv(string name)
        => NativeEnvironment.GetEnv(name);

    public static void SetEnv(string name, string value)
        => NativeEnvironment.SetEnv(name, value);
}
