// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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
    /// Class that can be used to create various instances.
    /// </summary>
    public sealed partial class MagickFactory : IMagickFactory<QuantumType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickFactory"/> class.
        /// </summary>
        public MagickFactory()
        {
            Color = new MagickColorFactory();
            Drawables = new DrawablesFactory();
            Geometry = new MagickGeometryFactory();
            Image = new MagickImageFactory();
            ImageCollection = new MagickImageCollectionFactory();
            ImageInfo = new MagickImageInfoFactory();
            Matrix = new MatrixFactory();
            Settings = new SettingsFactory();
        }

        /// <summary>
        /// Gets a factory that can be used to create <see cref="IMagickColorFactory{TQuantumType}"/> instances.
        /// </summary>
        public IMagickColorFactory<QuantumType> Color { get; }

        /// <summary>
        /// Gets a factory that can be used to create <see cref="IDrawables{QuantumType}"/> instances.
        /// </summary>
        public IDrawablesFactory<QuantumType> Drawables { get; }

        /// <summary>
        /// Gets a factory that can be used to create <see cref="IMagickGeometry"/> instances.
        /// </summary>
        public IMagickGeometryFactory Geometry { get; }

        /// <summary>
        /// Gets a factory that can be used to create <see cref="IMagickImage{QuantumType}"/> instances.
        /// </summary>
        public IMagickImageFactory<QuantumType> Image { get; }

        /// <summary>
        /// Gets a factory that can be used to create <see cref="IMagickImageCollection{QuantumType}"/> instances.
        /// </summary>
        public IMagickImageCollectionFactory<QuantumType> ImageCollection { get; }

        /// <summary>
        /// Gets a factory that can be used to create <see cref="IMagickImageInfo"/> instances.
        /// </summary>
        public IMagickImageInfoFactory ImageInfo { get; }

        /// <summary>
        /// Gets a factory that can be used to create various matrix instances.
        /// </summary>
        public IMatrixFactory Matrix { get; }

        /// <summary>
        /// Gets the quantum information of this image.
        /// </summary>
        public IQuantumInfo<QuantumType> QuantumInfo
            => ImageMagick.QuantumInfo.Instance;

        /// <summary>
        /// Gets a factory that can be used to create various settings.
        /// </summary>
        public ISettingsFactory<QuantumType> Settings { get; }
    }
}
