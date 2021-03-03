// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace Magick.NET.Tests
{
    internal static class TestHelper
    {
        private static readonly object _lock = new object();

        public static void ExecuteInsideLock(Action action)
        {
            lock (_lock)
            {
                action();
            }
        }
    }
}
