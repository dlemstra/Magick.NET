// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Specifies the placement gravity.
    /// </summary>
    public enum Gravity
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined,

        /// <summary>
        /// Forget.
        /// </summary>
        Forget = Undefined,

        /// <summary>
        /// Northwest.
        /// </summary>
        Northwest = 1,

        /// <summary>
        /// North.
        /// </summary>
        North = 2,

        /// <summary>
        /// Northeast.
        /// </summary>
        Northeast = 3,

        /// <summary>
        /// West.
        /// </summary>
        West = 4,

        /// <summary>
        /// Center.
        /// </summary>
        Center = 5,

        /// <summary>
        /// East.
        /// </summary>
        East = 6,

        /// <summary>
        /// Southwest.
        /// </summary>
        Southwest = 7,

        /// <summary>
        /// South.
        /// </summary>
        South = 8,

        /// <summary>
        /// Southeast.
        /// </summary>
        Southeast = 9,
    }
}