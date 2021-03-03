// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
    /// <summary>
    /// Class that can be used to chain path actions.
    /// </summary>
    public sealed partial class Paths : IPaths<QuantumType>
    {
        private readonly Drawables? _drawables;
        private readonly Collection<IPath> _paths;

        /// <summary>
        /// Initializes a new instance of the <see cref="Paths"/> class.
        /// </summary>
        public Paths()
        {
            _paths = new Collection<IPath>();
        }

        internal Paths(Drawables drawables)
          : this()
        {
            _drawables = drawables;
        }

        /// <summary>
        /// Converts this instance to a <see cref="IDrawables{TQuantumType}"/> instance.
        /// </summary>
        /// <returns>A new <see cref="Drawables"/> instance.</returns>
        public IDrawables<QuantumType> Drawables()
        {
            if (_drawables == null)
                return new Drawables().Path(this);

            return _drawables.Path(this);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that iterates through the collection.</returns>
        public IEnumerator<IPath> GetEnumerator()
            => _paths.GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that iterates through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
            => _paths.GetEnumerator();
    }
}
