// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ImageMagick
{
    /// <summary>
    /// Class that can be used to chain path actions.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    [SuppressMessage("Naming", "CA1710", Justification = "No need to use Collection suffix.")]
    public partial interface IPaths<TQuantumType> : IEnumerable<IPath>
        where TQuantumType : struct
    {
        /// <summary>
        /// Converts this instance to a <see cref="IDrawables{TQuantumType}"/> instance.
        /// </summary>
        /// <returns>A new <see cref="Drawables"/> instance.</returns>
        IDrawables<TQuantumType> Drawables();
    }
}
