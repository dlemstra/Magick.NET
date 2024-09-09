// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if !NETCOREAPP

using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

[assembly: TestCollectionOrderer("Magick.NET.Tests.TestCollectionOrderer", "Magick.NET.Tests")]

namespace Magick.NET.Tests;

public sealed class TestCollectionOrderer : ITestCollectionOrderer
{
    public IEnumerable<ITestCollection>? OrderTestCollections(IEnumerable<ITestCollection> testCollections)
    {
        if (!TestInitializer.Initialize())
            return null;

        return testCollections;
    }
}

#endif
