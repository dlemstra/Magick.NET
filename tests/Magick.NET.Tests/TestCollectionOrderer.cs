// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if !NETCOREAPP

using System.Collections.Generic;
using Magick.NET.Tests;
using Xunit;
using Xunit.Sdk;
using Xunit.v3;

[assembly: TestCollectionOrderer(typeof(TestCollectionOrderer))]

namespace Magick.NET.Tests;

public sealed class TestCollectionOrderer : ITestCollectionOrderer
{
    public IReadOnlyCollection<TTestCollection> OrderTestCollections<TTestCollection>(IReadOnlyCollection<TTestCollection> testCollections)
        where TTestCollection : ITestCollection
    {
        if (!TestInitializer.Initialize())
            return [];

        return testCollections;
    }
}

#endif
