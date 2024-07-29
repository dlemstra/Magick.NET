// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.SourceGenerator;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick;

/// <summary>
/// Class that contains the same colors as System.Drawing.Colors.
/// </summary>
public partial class MagickColors : IMagickColors<QuantumType>
{
    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00000000.
    /// </summary>
    public static MagickColor None
        => MagickColor.FromRgba(0, 0, 0, 0);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00000000.
    /// </summary>
    public static MagickColor Transparent
        => MagickColor.FromRgba(0, 0, 0, 0);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F0F8FFFF.
    /// </summary>
    public static MagickColor AliceBlue
        => MagickColor.FromRgba(240, 248, 255, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FAEBD7FF.
    /// </summary>
    public static MagickColor AntiqueWhite
        => MagickColor.FromRgba(250, 235, 215, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00FFFFFF.
    /// </summary>
    public static MagickColor Aqua
        => MagickColor.FromRgba(0, 255, 255, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #7FFFD4FF.
    /// </summary>
    public static MagickColor Aquamarine
        => MagickColor.FromRgba(127, 255, 212, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F0FFFFFF.
    /// </summary>
    public static MagickColor Azure
        => MagickColor.FromRgba(240, 255, 255, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F5F5DCFF.
    /// </summary>
    public static MagickColor Beige
        => MagickColor.FromRgba(245, 245, 220, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFE4C4FF.
    /// </summary>
    public static MagickColor Bisque
        => MagickColor.FromRgba(255, 228, 196, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #000000FF.
    /// </summary>
    public static MagickColor Black
        => MagickColor.FromRgba(0, 0, 0, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFEBCDFF.
    /// </summary>
    public static MagickColor BlanchedAlmond
        => MagickColor.FromRgba(255, 235, 205, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #0000FFFF.
    /// </summary>
    public static MagickColor Blue
        => MagickColor.FromRgba(0, 0, 255, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #8A2BE2FF.
    /// </summary>
    public static MagickColor BlueViolet
        => MagickColor.FromRgba(138, 43, 226, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #A52A2AFF.
    /// </summary>
    public static MagickColor Brown
        => MagickColor.FromRgba(165, 42, 42, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #DEB887FF.
    /// </summary>
    public static MagickColor BurlyWood
        => MagickColor.FromRgba(222, 184, 135, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #5F9EA0FF.
    /// </summary>
    public static MagickColor CadetBlue
        => MagickColor.FromRgba(95, 158, 160, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #7FFF00FF.
    /// </summary>
    public static MagickColor Chartreuse
        => MagickColor.FromRgba(127, 255, 0, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #D2691EFF.
    /// </summary>
    public static MagickColor Chocolate
        => MagickColor.FromRgba(210, 105, 30, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF7F50FF.
    /// </summary>
    public static MagickColor Coral
        => MagickColor.FromRgba(255, 127, 80, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #6495EDFF.
    /// </summary>
    public static MagickColor CornflowerBlue
        => MagickColor.FromRgba(100, 149, 237, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFF8DCFF.
    /// </summary>
    public static MagickColor Cornsilk
        => MagickColor.FromRgba(255, 248, 220, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #DC143CFF.
    /// </summary>
    public static MagickColor Crimson
        => MagickColor.FromRgba(220, 20, 60, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00FFFFFF.
    /// </summary>
    public static MagickColor Cyan
        => MagickColor.FromRgba(0, 255, 255, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00008BFF.
    /// </summary>
    public static MagickColor DarkBlue
        => MagickColor.FromRgba(0, 0, 139, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #008B8BFF.
    /// </summary>
    public static MagickColor DarkCyan
        => MagickColor.FromRgba(0, 139, 139, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #B8860BFF.
    /// </summary>
    public static MagickColor DarkGoldenrod
        => MagickColor.FromRgba(184, 134, 11, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #A9A9A9FF.
    /// </summary>
    public static MagickColor DarkGray
        => MagickColor.FromRgba(169, 169, 169, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #006400FF.
    /// </summary>
    public static MagickColor DarkGreen
        => MagickColor.FromRgba(0, 100, 0, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #BDB76BFF.
    /// </summary>
    public static MagickColor DarkKhaki
        => MagickColor.FromRgba(189, 183, 107, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #8B008BFF.
    /// </summary>
    public static MagickColor DarkMagenta
        => MagickColor.FromRgba(139, 0, 139, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #556B2FFF.
    /// </summary>
    public static MagickColor DarkOliveGreen
        => MagickColor.FromRgba(85, 107, 47, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF8C00FF.
    /// </summary>
    public static MagickColor DarkOrange
        => MagickColor.FromRgba(255, 140, 0, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #9932CCFF.
    /// </summary>
    public static MagickColor DarkOrchid
        => MagickColor.FromRgba(153, 50, 204, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #8B0000FF.
    /// </summary>
    public static MagickColor DarkRed
        => MagickColor.FromRgba(139, 0, 0, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #E9967AFF.
    /// </summary>
    public static MagickColor DarkSalmon
        => MagickColor.FromRgba(233, 150, 122, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #8FBC8BFF.
    /// </summary>
    public static MagickColor DarkSeaGreen
        => MagickColor.FromRgba(143, 188, 139, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #483D8BFF.
    /// </summary>
    public static MagickColor DarkSlateBlue
        => MagickColor.FromRgba(72, 61, 139, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #2F4F4FFF.
    /// </summary>
    public static MagickColor DarkSlateGray
        => MagickColor.FromRgba(47, 79, 79, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00CED1FF.
    /// </summary>
    public static MagickColor DarkTurquoise
        => MagickColor.FromRgba(0, 206, 209, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #9400D3FF.
    /// </summary>
    public static MagickColor DarkViolet
        => MagickColor.FromRgba(148, 0, 211, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF1493FF.
    /// </summary>
    public static MagickColor DeepPink
        => MagickColor.FromRgba(255, 20, 147, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00BFFFFF.
    /// </summary>
    public static MagickColor DeepSkyBlue
        => MagickColor.FromRgba(0, 191, 255, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #696969FF.
    /// </summary>
    public static MagickColor DimGray
        => MagickColor.FromRgba(105, 105, 105, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #1E90FFFF.
    /// </summary>
    public static MagickColor DodgerBlue
        => MagickColor.FromRgba(30, 144, 255, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #B22222FF.
    /// </summary>
    public static MagickColor Firebrick
        => MagickColor.FromRgba(178, 34, 34, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFFAF0FF.
    /// </summary>
    public static MagickColor FloralWhite
        => MagickColor.FromRgba(255, 250, 240, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #228B22FF.
    /// </summary>
    public static MagickColor ForestGreen
        => MagickColor.FromRgba(34, 139, 34, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF00FFFF.
    /// </summary>
    public static MagickColor Fuchsia
        => MagickColor.FromRgba(255, 0, 255, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #DCDCDCFF.
    /// </summary>
    public static MagickColor Gainsboro
        => MagickColor.FromRgba(220, 220, 220, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F8F8FFFF.
    /// </summary>
    public static MagickColor GhostWhite
        => MagickColor.FromRgba(248, 248, 255, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFD700FF.
    /// </summary>
    public static MagickColor Gold
        => MagickColor.FromRgba(255, 215, 0, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #DAA520FF.
    /// </summary>
    public static MagickColor Goldenrod
        => MagickColor.FromRgba(218, 165, 32, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #808080FF.
    /// </summary>
    public static MagickColor Gray
        => MagickColor.FromRgba(128, 128, 128, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #008000FF.
    /// </summary>
    public static MagickColor Green
        => MagickColor.FromRgba(0, 128, 0, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #ADFF2FFF.
    /// </summary>
    public static MagickColor GreenYellow
        => MagickColor.FromRgba(173, 255, 47, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F0FFF0FF.
    /// </summary>
    public static MagickColor Honeydew
        => MagickColor.FromRgba(240, 255, 240, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF69B4FF.
    /// </summary>
    public static MagickColor HotPink
        => MagickColor.FromRgba(255, 105, 180, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #CD5C5CFF.
    /// </summary>
    public static MagickColor IndianRed
        => MagickColor.FromRgba(205, 92, 92, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #4B0082FF.
    /// </summary>
    public static MagickColor Indigo
        => MagickColor.FromRgba(75, 0, 130, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFFFF0FF.
    /// </summary>
    public static MagickColor Ivory
        => MagickColor.FromRgba(255, 255, 240, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F0E68CFF.
    /// </summary>
    public static MagickColor Khaki
        => MagickColor.FromRgba(240, 230, 140, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #E6E6FAFF.
    /// </summary>
    public static MagickColor Lavender
        => MagickColor.FromRgba(230, 230, 250, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFF0F5FF.
    /// </summary>
    public static MagickColor LavenderBlush
        => MagickColor.FromRgba(255, 240, 245, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #7CFC00FF.
    /// </summary>
    public static MagickColor LawnGreen
        => MagickColor.FromRgba(124, 252, 0, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFFACDFF.
    /// </summary>
    public static MagickColor LemonChiffon
        => MagickColor.FromRgba(255, 250, 205, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #ADD8E6FF.
    /// </summary>
    public static MagickColor LightBlue
        => MagickColor.FromRgba(173, 216, 230, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F08080FF.
    /// </summary>
    public static MagickColor LightCoral
        => MagickColor.FromRgba(240, 128, 128, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #E0FFFFFF.
    /// </summary>
    public static MagickColor LightCyan
        => MagickColor.FromRgba(224, 255, 255, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FAFAD2FF.
    /// </summary>
    public static MagickColor LightGoldenrodYellow
        => MagickColor.FromRgba(250, 250, 210, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #90EE90FF.
    /// </summary>
    public static MagickColor LightGreen
        => MagickColor.FromRgba(144, 238, 144, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #D3D3D3FF.
    /// </summary>
    public static MagickColor LightGray
        => MagickColor.FromRgba(211, 211, 211, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFB6C1FF.
    /// </summary>
    public static MagickColor LightPink
        => MagickColor.FromRgba(255, 182, 193, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFA07AFF.
    /// </summary>
    public static MagickColor LightSalmon
        => MagickColor.FromRgba(255, 160, 122, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #20B2AAFF.
    /// </summary>
    public static MagickColor LightSeaGreen
        => MagickColor.FromRgba(32, 178, 170, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #87CEFAFF.
    /// </summary>
    public static MagickColor LightSkyBlue
        => MagickColor.FromRgba(135, 206, 250, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #778899FF.
    /// </summary>
    public static MagickColor LightSlateGray
        => MagickColor.FromRgba(119, 136, 153, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #B0C4DEFF.
    /// </summary>
    public static MagickColor LightSteelBlue
        => MagickColor.FromRgba(176, 196, 222, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFFFE0FF.
    /// </summary>
    public static MagickColor LightYellow
        => MagickColor.FromRgba(255, 255, 224, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00FF00FF.
    /// </summary>
    public static MagickColor Lime
        => MagickColor.FromRgba(0, 255, 0, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #32CD32FF.
    /// </summary>
    public static MagickColor LimeGreen
        => MagickColor.FromRgba(50, 205, 50, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FAF0E6FF.
    /// </summary>
    public static MagickColor Linen
        => MagickColor.FromRgba(250, 240, 230, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF00FFFF.
    /// </summary>
    public static MagickColor Magenta
        => MagickColor.FromRgba(255, 0, 255, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #800000FF.
    /// </summary>
    public static MagickColor Maroon
        => MagickColor.FromRgba(128, 0, 0, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #66CDAAFF.
    /// </summary>
    public static MagickColor MediumAquamarine
        => MagickColor.FromRgba(102, 205, 170, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #0000CDFF.
    /// </summary>
    public static MagickColor MediumBlue
        => MagickColor.FromRgba(0, 0, 205, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #BA55D3FF.
    /// </summary>
    public static MagickColor MediumOrchid
        => MagickColor.FromRgba(186, 85, 211, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #9370DBFF.
    /// </summary>
    public static MagickColor MediumPurple
        => MagickColor.FromRgba(147, 112, 219, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #3CB371FF.
    /// </summary>
    public static MagickColor MediumSeaGreen
        => MagickColor.FromRgba(60, 179, 113, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #7B68EEFF.
    /// </summary>
    public static MagickColor MediumSlateBlue
        => MagickColor.FromRgba(123, 104, 238, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00FA9AFF.
    /// </summary>
    public static MagickColor MediumSpringGreen
        => MagickColor.FromRgba(0, 250, 154, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #48D1CCFF.
    /// </summary>
    public static MagickColor MediumTurquoise
        => MagickColor.FromRgba(72, 209, 204, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #C71585FF.
    /// </summary>
    public static MagickColor MediumVioletRed
        => MagickColor.FromRgba(199, 21, 133, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #191970FF.
    /// </summary>
    public static MagickColor MidnightBlue
        => MagickColor.FromRgba(25, 25, 112, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F5FFFAFF.
    /// </summary>
    public static MagickColor MintCream
        => MagickColor.FromRgba(245, 255, 250, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFE4E1FF.
    /// </summary>
    public static MagickColor MistyRose
        => MagickColor.FromRgba(255, 228, 225, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFE4B5FF.
    /// </summary>
    public static MagickColor Moccasin
        => MagickColor.FromRgba(255, 228, 181, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFDEADFF.
    /// </summary>
    public static MagickColor NavajoWhite
        => MagickColor.FromRgba(255, 222, 173, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #000080FF.
    /// </summary>
    public static MagickColor Navy
        => MagickColor.FromRgba(0, 0, 128, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FDF5E6FF.
    /// </summary>
    public static MagickColor OldLace
        => MagickColor.FromRgba(253, 245, 230, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #808000FF.
    /// </summary>
    public static MagickColor Olive
        => MagickColor.FromRgba(128, 128, 0, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #6B8E23FF.
    /// </summary>
    public static MagickColor OliveDrab
        => MagickColor.FromRgba(107, 142, 35, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFA500FF.
    /// </summary>
    public static MagickColor Orange
        => MagickColor.FromRgba(255, 165, 0, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF4500FF.
    /// </summary>
    public static MagickColor OrangeRed
        => MagickColor.FromRgba(255, 69, 0, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #DA70D6FF.
    /// </summary>
    public static MagickColor Orchid
        => MagickColor.FromRgba(218, 112, 214, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #EEE8AAFF.
    /// </summary>
    public static MagickColor PaleGoldenrod
        => MagickColor.FromRgba(238, 232, 170, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #98FB98FF.
    /// </summary>
    public static MagickColor PaleGreen
        => MagickColor.FromRgba(152, 251, 152, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #AFEEEEFF.
    /// </summary>
    public static MagickColor PaleTurquoise
        => MagickColor.FromRgba(175, 238, 238, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #DB7093FF.
    /// </summary>
    public static MagickColor PaleVioletRed
        => MagickColor.FromRgba(219, 112, 147, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFEFD5FF.
    /// </summary>
    public static MagickColor PapayaWhip
        => MagickColor.FromRgba(255, 239, 213, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFDAB9FF.
    /// </summary>
    public static MagickColor PeachPuff
        => MagickColor.FromRgba(255, 218, 185, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #CD853FFF.
    /// </summary>
    public static MagickColor Peru
        => MagickColor.FromRgba(205, 133, 63, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFC0CBFF.
    /// </summary>
    public static MagickColor Pink
        => MagickColor.FromRgba(255, 192, 203, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #DDA0DDFF.
    /// </summary>
    public static MagickColor Plum
        => MagickColor.FromRgba(221, 160, 221, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #B0E0E6FF.
    /// </summary>
    public static MagickColor PowderBlue
        => MagickColor.FromRgba(176, 224, 230, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #800080FF.
    /// </summary>
    public static MagickColor Purple
        => MagickColor.FromRgba(128, 0, 128, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF0000FF.
    /// </summary>
    public static MagickColor Red
        => MagickColor.FromRgba(255, 0, 0, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #BC8F8FFF.
    /// </summary>
    public static MagickColor RosyBrown
        => MagickColor.FromRgba(188, 143, 143, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #4169E1FF.
    /// </summary>
    public static MagickColor RoyalBlue
        => MagickColor.FromRgba(65, 105, 225, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #8B4513FF.
    /// </summary>
    public static MagickColor SaddleBrown
        => MagickColor.FromRgba(139, 69, 19, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FA8072FF.
    /// </summary>
    public static MagickColor Salmon
        => MagickColor.FromRgba(250, 128, 114, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F4A460FF.
    /// </summary>
    public static MagickColor SandyBrown
        => MagickColor.FromRgba(244, 164, 96, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #2E8B57FF.
    /// </summary>
    public static MagickColor SeaGreen
        => MagickColor.FromRgba(46, 139, 87, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFF5EEFF.
    /// </summary>
    public static MagickColor SeaShell
        => MagickColor.FromRgba(255, 245, 238, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #A0522DFF.
    /// </summary>
    public static MagickColor Sienna
        => MagickColor.FromRgba(160, 82, 45, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #C0C0C0FF.
    /// </summary>
    public static MagickColor Silver
        => MagickColor.FromRgba(192, 192, 192, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #87CEEBFF.
    /// </summary>
    public static MagickColor SkyBlue
        => MagickColor.FromRgba(135, 206, 235, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #6A5ACDFF.
    /// </summary>
    public static MagickColor SlateBlue
        => MagickColor.FromRgba(106, 90, 205, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #708090FF.
    /// </summary>
    public static MagickColor SlateGray
        => MagickColor.FromRgba(112, 128, 144, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFFAFAFF.
    /// </summary>
    public static MagickColor Snow
        => MagickColor.FromRgba(255, 250, 250, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00FF7FFF.
    /// </summary>
    public static MagickColor SpringGreen
        => MagickColor.FromRgba(0, 255, 127, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #4682B4FF.
    /// </summary>
    public static MagickColor SteelBlue
        => MagickColor.FromRgba(70, 130, 180, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #D2B48CFF.
    /// </summary>
    public static MagickColor Tan
        => MagickColor.FromRgba(210, 180, 140, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #008080FF.
    /// </summary>
    public static MagickColor Teal
        => MagickColor.FromRgba(0, 128, 128, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #D8BFD8FF.
    /// </summary>
    public static MagickColor Thistle
        => MagickColor.FromRgba(216, 191, 216, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF6347FF.
    /// </summary>
    public static MagickColor Tomato
        => MagickColor.FromRgba(255, 99, 71, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #40E0D0FF.
    /// </summary>
    public static MagickColor Turquoise
        => MagickColor.FromRgba(64, 224, 208, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #EE82EEFF.
    /// </summary>
    public static MagickColor Violet
        => MagickColor.FromRgba(238, 130, 238, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F5DEB3FF.
    /// </summary>
    public static MagickColor Wheat
        => MagickColor.FromRgba(245, 222, 179, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFFFFFFF.
    /// </summary>
    public static MagickColor White
        => MagickColor.FromRgba(255, 255, 255, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F5F5F5FF.
    /// </summary>
    public static MagickColor WhiteSmoke
        => MagickColor.FromRgba(245, 245, 245, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFFF00FF.
    /// </summary>
    public static MagickColor Yellow
        => MagickColor.FromRgba(255, 255, 0, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #9ACD32FF.
    /// </summary>
    public static MagickColor YellowGreen
        => MagickColor.FromRgba(154, 205, 50, 255);

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00000000.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.None
        => None;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00000000.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Transparent
        => Transparent;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F0F8FFFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.AliceBlue
        => AliceBlue;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FAEBD7FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.AntiqueWhite
        => AntiqueWhite;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00FFFFFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Aqua
        => Aqua;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #7FFFD4FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Aquamarine
        => Aquamarine;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F0FFFFFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Azure
        => Azure;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F5F5DCFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Beige
        => Beige;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFE4C4FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Bisque
        => Bisque;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #000000FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Black
        => Black;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFEBCDFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.BlanchedAlmond
        => BlanchedAlmond;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #0000FFFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Blue
        => Blue;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #8A2BE2FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.BlueViolet
        => BlueViolet;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #A52A2AFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Brown
        => Brown;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #DEB887FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.BurlyWood
        => BurlyWood;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #5F9EA0FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.CadetBlue
        => CadetBlue;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #7FFF00FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Chartreuse
        => Chartreuse;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #D2691EFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Chocolate
        => Chocolate;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF7F50FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Coral
        => Coral;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #6495EDFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.CornflowerBlue
        => CornflowerBlue;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFF8DCFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Cornsilk
        => Cornsilk;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #DC143CFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Crimson
        => Crimson;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00FFFFFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Cyan
        => Cyan;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00008BFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.DarkBlue
        => DarkBlue;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #008B8BFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.DarkCyan
        => DarkCyan;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #B8860BFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.DarkGoldenrod
        => DarkGoldenrod;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #A9A9A9FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.DarkGray
        => DarkGray;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #006400FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.DarkGreen
        => DarkGreen;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #BDB76BFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.DarkKhaki
        => DarkKhaki;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #8B008BFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.DarkMagenta
        => DarkMagenta;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #556B2FFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.DarkOliveGreen
        => DarkOliveGreen;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF8C00FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.DarkOrange
        => DarkOrange;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #9932CCFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.DarkOrchid
        => DarkOrchid;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #8B0000FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.DarkRed
        => DarkRed;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #E9967AFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.DarkSalmon
        => DarkSalmon;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #8FBC8BFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.DarkSeaGreen
        => DarkSeaGreen;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #483D8BFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.DarkSlateBlue
        => DarkSlateBlue;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #2F4F4FFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.DarkSlateGray
        => DarkSlateGray;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00CED1FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.DarkTurquoise
        => DarkTurquoise;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #9400D3FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.DarkViolet
        => DarkViolet;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF1493FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.DeepPink
        => DeepPink;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00BFFFFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.DeepSkyBlue
        => DeepSkyBlue;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #696969FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.DimGray
        => DimGray;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #1E90FFFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.DodgerBlue
        => DodgerBlue;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #B22222FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Firebrick
        => Firebrick;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFFAF0FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.FloralWhite
        => FloralWhite;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #228B22FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.ForestGreen
        => ForestGreen;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF00FFFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Fuchsia
        => Fuchsia;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #DCDCDCFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Gainsboro
        => Gainsboro;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F8F8FFFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.GhostWhite
        => GhostWhite;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFD700FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Gold
        => Gold;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #DAA520FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Goldenrod
        => Goldenrod;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #808080FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Gray
        => Gray;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #008000FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Green
        => Green;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #ADFF2FFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.GreenYellow
        => GreenYellow;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F0FFF0FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Honeydew
        => Honeydew;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF69B4FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.HotPink
        => HotPink;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #CD5C5CFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.IndianRed
        => IndianRed;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #4B0082FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Indigo
        => Indigo;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFFFF0FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Ivory
        => Ivory;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F0E68CFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Khaki
        => Khaki;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #E6E6FAFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Lavender
        => Lavender;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFF0F5FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.LavenderBlush
        => LavenderBlush;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #7CFC00FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.LawnGreen
        => LawnGreen;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFFACDFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.LemonChiffon
        => LemonChiffon;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #ADD8E6FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.LightBlue
        => LightBlue;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F08080FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.LightCoral
        => LightCoral;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #E0FFFFFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.LightCyan
        => LightCyan;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FAFAD2FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.LightGoldenrodYellow
        => LightGoldenrodYellow;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #90EE90FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.LightGreen
        => LightGreen;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #D3D3D3FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.LightGray
        => LightGray;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFB6C1FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.LightPink
        => LightPink;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFA07AFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.LightSalmon
        => LightSalmon;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #20B2AAFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.LightSeaGreen
        => LightSeaGreen;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #87CEFAFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.LightSkyBlue
        => LightSkyBlue;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #778899FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.LightSlateGray
        => LightSlateGray;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #B0C4DEFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.LightSteelBlue
        => LightSteelBlue;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFFFE0FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.LightYellow
        => LightYellow;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00FF00FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Lime
        => Lime;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #32CD32FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.LimeGreen
        => LimeGreen;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FAF0E6FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Linen
        => Linen;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF00FFFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Magenta
        => Magenta;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #800000FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Maroon
        => Maroon;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #66CDAAFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.MediumAquamarine
        => MediumAquamarine;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #0000CDFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.MediumBlue
        => MediumBlue;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #BA55D3FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.MediumOrchid
        => MediumOrchid;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #9370DBFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.MediumPurple
        => MediumPurple;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #3CB371FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.MediumSeaGreen
        => MediumSeaGreen;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #7B68EEFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.MediumSlateBlue
        => MediumSlateBlue;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00FA9AFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.MediumSpringGreen
        => MediumSpringGreen;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #48D1CCFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.MediumTurquoise
        => MediumTurquoise;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #C71585FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.MediumVioletRed
        => MediumVioletRed;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #191970FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.MidnightBlue
        => MidnightBlue;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F5FFFAFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.MintCream
        => MintCream;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFE4E1FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.MistyRose
        => MistyRose;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFE4B5FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Moccasin
        => Moccasin;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFDEADFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.NavajoWhite
        => NavajoWhite;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #000080FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Navy
        => Navy;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FDF5E6FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.OldLace
        => OldLace;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #808000FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Olive
        => Olive;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #6B8E23FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.OliveDrab
        => OliveDrab;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFA500FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Orange
        => Orange;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF4500FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.OrangeRed
        => OrangeRed;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #DA70D6FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Orchid
        => Orchid;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #EEE8AAFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.PaleGoldenrod
        => PaleGoldenrod;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #98FB98FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.PaleGreen
        => PaleGreen;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #AFEEEEFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.PaleTurquoise
        => PaleTurquoise;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #DB7093FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.PaleVioletRed
        => PaleVioletRed;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFEFD5FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.PapayaWhip
        => PapayaWhip;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFDAB9FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.PeachPuff
        => PeachPuff;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #CD853FFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Peru
        => Peru;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFC0CBFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Pink
        => Pink;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #DDA0DDFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Plum
        => Plum;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #B0E0E6FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.PowderBlue
        => PowderBlue;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #800080FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Purple
        => Purple;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF0000FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Red
        => Red;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #BC8F8FFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.RosyBrown
        => RosyBrown;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #4169E1FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.RoyalBlue
        => RoyalBlue;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #8B4513FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.SaddleBrown
        => SaddleBrown;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FA8072FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Salmon
        => Salmon;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F4A460FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.SandyBrown
        => SandyBrown;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #2E8B57FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.SeaGreen
        => SeaGreen;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFF5EEFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.SeaShell
        => SeaShell;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #A0522DFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Sienna
        => Sienna;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #C0C0C0FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Silver
        => Silver;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #87CEEBFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.SkyBlue
        => SkyBlue;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #6A5ACDFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.SlateBlue
        => SlateBlue;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #708090FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.SlateGray
        => SlateGray;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFFAFAFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Snow
        => Snow;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #00FF7FFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.SpringGreen
        => SpringGreen;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #4682B4FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.SteelBlue
        => SteelBlue;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #D2B48CFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Tan
        => Tan;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #008080FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Teal
        => Teal;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #D8BFD8FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Thistle
        => Thistle;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FF6347FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Tomato
        => Tomato;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #40E0D0FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Turquoise
        => Turquoise;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #EE82EEFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Violet
        => Violet;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F5DEB3FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Wheat
        => Wheat;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFFFFFFF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.White
        => White;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #F5F5F5FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.WhiteSmoke
        => WhiteSmoke;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #FFFF00FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.Yellow
        => Yellow;

    /// <summary>
    /// Gets a system-defined color that has an RGBA value of #9ACD32FF.
    /// </summary>
    IMagickColor<QuantumType> IMagickColors<QuantumType>.YellowGreen
        => YellowGreen;
}
