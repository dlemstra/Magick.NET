// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Class that can be used to create <see cref="IMagickColor{TQuantumType}"/> instances.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    public interface IDrawablesFactory<TQuantumType>
        where TQuantumType : struct
    {
        /// <summary>
        /// Initializes a new instance that implements <see cref="IDrawables{TQuantumType}"/>.
        /// </summary>
        /// <returns>A new <see cref="IDrawables{TQuantumType}"/> instance.</returns>
        IDrawables<TQuantumType> Create();
    }
}
