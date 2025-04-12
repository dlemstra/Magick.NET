// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Threading;
using Xunit;

namespace Magick.NET.Tests;

[CollectionDefinition(nameof(IsolatedUnitTest), DisableParallelization = true)]
public class IsolatedUnitTest
{
    private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

    public static void Execute(Action action)
    {
        _semaphore.Wait();
        try
        {
            action();
        }
        finally
        {
            _semaphore.Release();
        }
    }
}
