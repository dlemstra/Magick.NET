// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick.Formats
{
    /// <summary>
    /// Specifies jp2 progression orders.
    /// </summary>
    public enum Jp2ProgressionOrder
    {
        /// <summary>
        /// Layer-resolution-component-precinct order.
        /// </summary>
        LRCP = 0,

        /// <summary>
        /// Resolution-layer-component-precinct order.
        /// </summary>
        RLCP = 1,

        /// <summary>
        /// Resolution-precinct-component-layer order.
        /// </summary>
        RPCL = 2,

        /// <summary>
        /// Precinct-component-resolution-layer order.
        /// </summary>
        PCRL = 3,

        /// <summary>
        /// Component-precinct-resolution-layer order.
        /// </summary>
        CPRL = 4,
    }
}