using System;

namespace ImageMagick.Formats
{
    /// <summary>
    /// Specifies the chunks to be included or excluded in the PNG image.
    /// This is a flags enumeration, allowing a bitwise combination of its member values.
    /// </summary>
    [Flags]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "<Pending>")]
    public enum PngChunkFlags
    {
        /// <summary>
        /// No chunks specified.
        /// </summary>
        None = 0,

        /// <summary>
        /// Include or exclude all chunks.
        /// </summary>
        All = bKGD | cHRM | EXIF | gAMA | iCCP | iTXt | sRGB | tEXt | zCCP | zTXt | date,

        /// <summary>
        /// Include or exclude bKGD chunk.
        /// </summary>
        bKGD = 1 << 0, // 0000 0001

        /// <summary>
        /// Include or exclude cHRM chunk.
        /// </summary>
        cHRM = 1 << 1, // 0000 0010

        /// <summary>
        /// Include or exclude EXIF chunk.
        /// </summary>
        EXIF = 1 << 2, // 0000 0100

        /// <summary>
        /// Include or exclude gAMA chunk.
        /// </summary>
        gAMA = 1 << 3, // 0000 1000

        /// <summary>
        /// Include or exclude iCCP chunk.
        /// </summary>
        iCCP = 1 << 4, // 0001 0000

        /// <summary>
        /// Include or exclude iTXt chunk.
        /// </summary>
        iTXt = 1 << 5, // 0010 0000

        /// <summary>
        /// Include or exclude sRGB chunk.
        /// </summary>
        sRGB = 1 << 6, // 0100 0000

        /// <summary>
        /// Include or exclude tEXt chunk.
        /// </summary>
        tEXt = 1 << 7, // 1000 0000

        /// <summary>
        /// Include or exclude zCCP chunk.
        /// </summary>
        zCCP = 1 << 8, // 1 0000 0000

        /// <summary>
        /// Include or exclude zTXt chunk.
        /// </summary>
        zTXt = 1 << 9, // 10 0000 0000

        /// <summary>
        /// Include or exclude date chunk.
        /// </summary>
        date = 1 << 10, // 100 0000 0000
    }
}
