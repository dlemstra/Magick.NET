// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace ImageMagick.Extensions
{
    internal static class GravityExtensions
    {
        private static IImmutableDictionary<Gravity, ReadOnlyMemory<string>> GravityToEdge { get; } =
            new Dictionary<Gravity, ReadOnlyMemory<string>>
            {
                [Gravity.North] = new[] { "north" },
                [Gravity.Northeast] = new[] { "north", "east" },
                [Gravity.East] = new[] { "east" },
                [Gravity.Southeast] = new[] { "south", "east" },
                [Gravity.South] = new[] { "south" },
                [Gravity.Southwest] = new[] { "south", "west" },
                [Gravity.West] = new[] { "west" },
                [Gravity.Northwest] = new[] { "north", "west" },
            }.ToImmutableDictionary();

        /// <summary>
        /// Converts the provided value into edge strings.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The edge strings composing the given value.</returns>
        public static ReadOnlyMemory<string> ToEdge(this Gravity value)
            => GravityToEdge.TryGetValue(value, out var edge) ?
            edge :
            ReadOnlyMemory<string>.Empty;
    }
}
