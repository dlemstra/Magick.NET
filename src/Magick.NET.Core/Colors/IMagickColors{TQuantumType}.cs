// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;

namespace ImageMagick;

/// <summary>
/// Interface that contains the same colors as System.Drawing.Colors.
/// </summary>
/// <typeparam name="TQuantumType">The quantum type.</typeparam>
public partial interface IMagickColors<TQuantumType>
    where TQuantumType : struct, IConvertible
{
    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00000000.
    /// </summary>
    IMagickColor<TQuantumType> None { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00000000.
    /// </summary>
    IMagickColor<TQuantumType> Transparent { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F0F8FFFF.
    /// </summary>
    IMagickColor<TQuantumType> AliceBlue { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FAEBD7FF.
    /// </summary>
    IMagickColor<TQuantumType> AntiqueWhite { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00FFFFFF.
    /// </summary>
    IMagickColor<TQuantumType> Aqua { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #7FFFD4FF.
    /// </summary>
    IMagickColor<TQuantumType> Aquamarine { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F0FFFFFF.
    /// </summary>
    IMagickColor<TQuantumType> Azure { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F5F5DCFF.
    /// </summary>
    IMagickColor<TQuantumType> Beige { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFE4C4FF.
    /// </summary>
    IMagickColor<TQuantumType> Bisque { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #000000FF.
    /// </summary>
    IMagickColor<TQuantumType> Black { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFEBCDFF.
    /// </summary>
    IMagickColor<TQuantumType> BlanchedAlmond { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #0000FFFF.
    /// </summary>
    IMagickColor<TQuantumType> Blue { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #8A2BE2FF.
    /// </summary>
    IMagickColor<TQuantumType> BlueViolet { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #A52A2AFF.
    /// </summary>
    IMagickColor<TQuantumType> Brown { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #DEB887FF.
    /// </summary>
    IMagickColor<TQuantumType> BurlyWood { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #5F9EA0FF.
    /// </summary>
    IMagickColor<TQuantumType> CadetBlue { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #7FFF00FF.
    /// </summary>
    IMagickColor<TQuantumType> Chartreuse { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #D2691EFF.
    /// </summary>
    IMagickColor<TQuantumType> Chocolate { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF7F50FF.
    /// </summary>
    IMagickColor<TQuantumType> Coral { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #6495EDFF.
    /// </summary>
    IMagickColor<TQuantumType> CornflowerBlue { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFF8DCFF.
    /// </summary>
    IMagickColor<TQuantumType> Cornsilk { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #DC143CFF.
    /// </summary>
    IMagickColor<TQuantumType> Crimson { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00FFFFFF.
    /// </summary>
    IMagickColor<TQuantumType> Cyan { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00008BFF.
    /// </summary>
    IMagickColor<TQuantumType> DarkBlue { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #008B8BFF.
    /// </summary>
    IMagickColor<TQuantumType> DarkCyan { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #B8860BFF.
    /// </summary>
    IMagickColor<TQuantumType> DarkGoldenrod { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #A9A9A9FF.
    /// </summary>
    IMagickColor<TQuantumType> DarkGray { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #006400FF.
    /// </summary>
    IMagickColor<TQuantumType> DarkGreen { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #BDB76BFF.
    /// </summary>
    IMagickColor<TQuantumType> DarkKhaki { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #8B008BFF.
    /// </summary>
    IMagickColor<TQuantumType> DarkMagenta { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #556B2FFF.
    /// </summary>
    IMagickColor<TQuantumType> DarkOliveGreen { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF8C00FF.
    /// </summary>
    IMagickColor<TQuantumType> DarkOrange { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #9932CCFF.
    /// </summary>
    IMagickColor<TQuantumType> DarkOrchid { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #8B0000FF.
    /// </summary>
    IMagickColor<TQuantumType> DarkRed { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #E9967AFF.
    /// </summary>
    IMagickColor<TQuantumType> DarkSalmon { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #8FBC8BFF.
    /// </summary>
    IMagickColor<TQuantumType> DarkSeaGreen { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #483D8BFF.
    /// </summary>
    IMagickColor<TQuantumType> DarkSlateBlue { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #2F4F4FFF.
    /// </summary>
    IMagickColor<TQuantumType> DarkSlateGray { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00CED1FF.
    /// </summary>
    IMagickColor<TQuantumType> DarkTurquoise { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #9400D3FF.
    /// </summary>
    IMagickColor<TQuantumType> DarkViolet { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF1493FF.
    /// </summary>
    IMagickColor<TQuantumType> DeepPink { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00BFFFFF.
    /// </summary>
    IMagickColor<TQuantumType> DeepSkyBlue { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #696969FF.
    /// </summary>
    IMagickColor<TQuantumType> DimGray { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #1E90FFFF.
    /// </summary>
    IMagickColor<TQuantumType> DodgerBlue { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #B22222FF.
    /// </summary>
    IMagickColor<TQuantumType> Firebrick { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFFAF0FF.
    /// </summary>
    IMagickColor<TQuantumType> FloralWhite { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #228B22FF.
    /// </summary>
    IMagickColor<TQuantumType> ForestGreen { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF00FFFF.
    /// </summary>
    IMagickColor<TQuantumType> Fuchsia { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #DCDCDCFF.
    /// </summary>
    IMagickColor<TQuantumType> Gainsboro { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F8F8FFFF.
    /// </summary>
    IMagickColor<TQuantumType> GhostWhite { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFD700FF.
    /// </summary>
    IMagickColor<TQuantumType> Gold { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #DAA520FF.
    /// </summary>
    IMagickColor<TQuantumType> Goldenrod { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #808080FF.
    /// </summary>
    IMagickColor<TQuantumType> Gray { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #008000FF.
    /// </summary>
    IMagickColor<TQuantumType> Green { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #ADFF2FFF.
    /// </summary>
    IMagickColor<TQuantumType> GreenYellow { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F0FFF0FF.
    /// </summary>
    IMagickColor<TQuantumType> Honeydew { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF69B4FF.
    /// </summary>
    IMagickColor<TQuantumType> HotPink { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #CD5C5CFF.
    /// </summary>
    IMagickColor<TQuantumType> IndianRed { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #4B0082FF.
    /// </summary>
    IMagickColor<TQuantumType> Indigo { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFFFF0FF.
    /// </summary>
    IMagickColor<TQuantumType> Ivory { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F0E68CFF.
    /// </summary>
    IMagickColor<TQuantumType> Khaki { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #E6E6FAFF.
    /// </summary>
    IMagickColor<TQuantumType> Lavender { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFF0F5FF.
    /// </summary>
    IMagickColor<TQuantumType> LavenderBlush { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #7CFC00FF.
    /// </summary>
    IMagickColor<TQuantumType> LawnGreen { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFFACDFF.
    /// </summary>
    IMagickColor<TQuantumType> LemonChiffon { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #ADD8E6FF.
    /// </summary>
    IMagickColor<TQuantumType> LightBlue { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F08080FF.
    /// </summary>
    IMagickColor<TQuantumType> LightCoral { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #E0FFFFFF.
    /// </summary>
    IMagickColor<TQuantumType> LightCyan { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FAFAD2FF.
    /// </summary>
    IMagickColor<TQuantumType> LightGoldenrodYellow { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #90EE90FF.
    /// </summary>
    IMagickColor<TQuantumType> LightGreen { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #D3D3D3FF.
    /// </summary>
    IMagickColor<TQuantumType> LightGray { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFB6C1FF.
    /// </summary>
    IMagickColor<TQuantumType> LightPink { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFA07AFF.
    /// </summary>
    IMagickColor<TQuantumType> LightSalmon { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #20B2AAFF.
    /// </summary>
    IMagickColor<TQuantumType> LightSeaGreen { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #87CEFAFF.
    /// </summary>
    IMagickColor<TQuantumType> LightSkyBlue { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #778899FF.
    /// </summary>
    IMagickColor<TQuantumType> LightSlateGray { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #B0C4DEFF.
    /// </summary>
    IMagickColor<TQuantumType> LightSteelBlue { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFFFE0FF.
    /// </summary>
    IMagickColor<TQuantumType> LightYellow { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00FF00FF.
    /// </summary>
    IMagickColor<TQuantumType> Lime { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #32CD32FF.
    /// </summary>
    IMagickColor<TQuantumType> LimeGreen { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FAF0E6FF.
    /// </summary>
    IMagickColor<TQuantumType> Linen { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF00FFFF.
    /// </summary>
    IMagickColor<TQuantumType> Magenta { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #800000FF.
    /// </summary>
    IMagickColor<TQuantumType> Maroon { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #66CDAAFF.
    /// </summary>
    IMagickColor<TQuantumType> MediumAquamarine { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #0000CDFF.
    /// </summary>
    IMagickColor<TQuantumType> MediumBlue { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #BA55D3FF.
    /// </summary>
    IMagickColor<TQuantumType> MediumOrchid { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #9370DBFF.
    /// </summary>
    IMagickColor<TQuantumType> MediumPurple { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #3CB371FF.
    /// </summary>
    IMagickColor<TQuantumType> MediumSeaGreen { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #7B68EEFF.
    /// </summary>
    IMagickColor<TQuantumType> MediumSlateBlue { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00FA9AFF.
    /// </summary>
    IMagickColor<TQuantumType> MediumSpringGreen { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #48D1CCFF.
    /// </summary>
    IMagickColor<TQuantumType> MediumTurquoise { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #C71585FF.
    /// </summary>
    IMagickColor<TQuantumType> MediumVioletRed { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #191970FF.
    /// </summary>
    IMagickColor<TQuantumType> MidnightBlue { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F5FFFAFF.
    /// </summary>
    IMagickColor<TQuantumType> MintCream { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFE4E1FF.
    /// </summary>
    IMagickColor<TQuantumType> MistyRose { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFE4B5FF.
    /// </summary>
    IMagickColor<TQuantumType> Moccasin { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFDEADFF.
    /// </summary>
    IMagickColor<TQuantumType> NavajoWhite { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #000080FF.
    /// </summary>
    IMagickColor<TQuantumType> Navy { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FDF5E6FF.
    /// </summary>
    IMagickColor<TQuantumType> OldLace { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #808000FF.
    /// </summary>
    IMagickColor<TQuantumType> Olive { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #6B8E23FF.
    /// </summary>
    IMagickColor<TQuantumType> OliveDrab { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFA500FF.
    /// </summary>
    IMagickColor<TQuantumType> Orange { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF4500FF.
    /// </summary>
    IMagickColor<TQuantumType> OrangeRed { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #DA70D6FF.
    /// </summary>
    IMagickColor<TQuantumType> Orchid { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #EEE8AAFF.
    /// </summary>
    IMagickColor<TQuantumType> PaleGoldenrod { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #98FB98FF.
    /// </summary>
    IMagickColor<TQuantumType> PaleGreen { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #AFEEEEFF.
    /// </summary>
    IMagickColor<TQuantumType> PaleTurquoise { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #DB7093FF.
    /// </summary>
    IMagickColor<TQuantumType> PaleVioletRed { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFEFD5FF.
    /// </summary>
    IMagickColor<TQuantumType> PapayaWhip { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFDAB9FF.
    /// </summary>
    IMagickColor<TQuantumType> PeachPuff { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #CD853FFF.
    /// </summary>
    IMagickColor<TQuantumType> Peru { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFC0CBFF.
    /// </summary>
    IMagickColor<TQuantumType> Pink { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #DDA0DDFF.
    /// </summary>
    IMagickColor<TQuantumType> Plum { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #B0E0E6FF.
    /// </summary>
    IMagickColor<TQuantumType> PowderBlue { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #800080FF.
    /// </summary>
    IMagickColor<TQuantumType> Purple { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF0000FF.
    /// </summary>
    IMagickColor<TQuantumType> Red { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #BC8F8FFF.
    /// </summary>
    IMagickColor<TQuantumType> RosyBrown { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #4169E1FF.
    /// </summary>
    IMagickColor<TQuantumType> RoyalBlue { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #8B4513FF.
    /// </summary>
    IMagickColor<TQuantumType> SaddleBrown { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FA8072FF.
    /// </summary>
    IMagickColor<TQuantumType> Salmon { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F4A460FF.
    /// </summary>
    IMagickColor<TQuantumType> SandyBrown { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #2E8B57FF.
    /// </summary>
    IMagickColor<TQuantumType> SeaGreen { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFF5EEFF.
    /// </summary>
    IMagickColor<TQuantumType> SeaShell { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #A0522DFF.
    /// </summary>
    IMagickColor<TQuantumType> Sienna { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #C0C0C0FF.
    /// </summary>
    IMagickColor<TQuantumType> Silver { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #87CEEBFF.
    /// </summary>
    IMagickColor<TQuantumType> SkyBlue { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #6A5ACDFF.
    /// </summary>
    IMagickColor<TQuantumType> SlateBlue { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #708090FF.
    /// </summary>
    IMagickColor<TQuantumType> SlateGray { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFFAFAFF.
    /// </summary>
    IMagickColor<TQuantumType> Snow { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00FF7FFF.
    /// </summary>
    IMagickColor<TQuantumType> SpringGreen { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #4682B4FF.
    /// </summary>
    IMagickColor<TQuantumType> SteelBlue { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #D2B48CFF.
    /// </summary>
    IMagickColor<TQuantumType> Tan { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #008080FF.
    /// </summary>
    IMagickColor<TQuantumType> Teal { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #D8BFD8FF.
    /// </summary>
    IMagickColor<TQuantumType> Thistle { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF6347FF.
    /// </summary>
    IMagickColor<TQuantumType> Tomato { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #40E0D0FF.
    /// </summary>
    IMagickColor<TQuantumType> Turquoise { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #EE82EEFF.
    /// </summary>
    IMagickColor<TQuantumType> Violet { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F5DEB3FF.
    /// </summary>
    IMagickColor<TQuantumType> Wheat { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFFFFFFF.
    /// </summary>
    IMagickColor<TQuantumType> White { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F5F5F5FF.
    /// </summary>
    IMagickColor<TQuantumType> WhiteSmoke { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFFF00FF.
    /// </summary>
    IMagickColor<TQuantumType> Yellow { get; }

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #9ACD32FF.
    /// </summary>
    IMagickColor<TQuantumType> YellowGreen { get; }
}
