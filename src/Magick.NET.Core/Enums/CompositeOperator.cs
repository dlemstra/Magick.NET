// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Specifies the composite operators.
    /// </summary>
    public enum CompositeOperator
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined,

        /// <summary>
        /// Alpha.
        /// </summary>
        Alpha,

        /// <summary>
        /// Atop.
        /// </summary>
        Atop,

        /// <summary>
        /// Blend.
        /// </summary>
        Blend,

        /// <summary>
        /// Blur.
        /// </summary>
        Blur,

        /// <summary>
        /// Bumpmap.
        /// </summary>
        Bumpmap,

        /// <summary>
        /// ChangeMask.
        /// </summary>
        ChangeMask,

        /// <summary>
        /// Clear.
        /// </summary>
        Clear,

        /// <summary>
        /// ColorBurn.
        /// </summary>
        ColorBurn,

        /// <summary>
        /// ColorDodge.
        /// </summary>
        ColorDodge,

        /// <summary>
        /// Colorize.
        /// </summary>
        Colorize,

        /// <summary>
        /// CopyBlack.
        /// </summary>
        CopyBlack,

        /// <summary>
        /// CopyBlue.
        /// </summary>
        CopyBlue,

        /// <summary>
        /// Copy.
        /// </summary>
        Copy,

        /// <summary>
        /// CopyCyan.
        /// </summary>
        CopyCyan,

        /// <summary>
        /// CopyGreen.
        /// </summary>
        CopyGreen,

        /// <summary>
        /// CopyMagenta.
        /// </summary>
        CopyMagenta,

        /// <summary>
        /// CopyAlpha.
        /// </summary>
        CopyAlpha,

        /// <summary>
        /// CopyRed.
        /// </summary>
        CopyRed,

        /// <summary>
        /// CopyYellow.
        /// </summary>
        CopyYellow,

        /// <summary>
        /// Darken.
        /// </summary>
        Darken,

        /// <summary>
        /// DarkenIntensity.
        /// </summary>
        DarkenIntensity,

        /// <summary>
        /// Difference.
        /// </summary>
        Difference,

        /// <summary>
        /// Displace.
        /// </summary>
        Displace,

        /// <summary>
        /// Dissolve.
        /// </summary>
        Dissolve,

        /// <summary>
        /// Distort.
        /// </summary>
        Distort,

        /// <summary>
        /// DivideDst.
        /// </summary>
        DivideDst,

        /// <summary>
        /// DivideSrc.
        /// </summary>
        DivideSrc,

        /// <summary>
        /// DstAtop.
        /// </summary>
        DstAtop,

        /// <summary>
        /// Dst.
        /// </summary>
        Dst,

        /// <summary>
        /// DstIn.
        /// </summary>
        DstIn,

        /// <summary>
        /// DstOut.
        /// </summary>
        DstOut,

        /// <summary>
        /// DstOver.
        /// </summary>
        DstOver,

        /// <summary>
        /// Exclusion.
        /// </summary>
        Exclusion,

        /// <summary>
        /// HardLight.
        /// </summary>
        HardLight,

        /// <summary>
        /// HardMix.
        /// </summary>
        HardMix,

        /// <summary>
        /// Hue.
        /// </summary>
        Hue,

        /// <summary>
        /// In.
        /// </summary>
        In,

        /// <summary>
        /// Intensity.
        /// </summary>
        Intensity,

        /// <summary>
        /// Lighten.
        /// </summary>
        Lighten,

        /// <summary>
        /// LightenIntensity.
        /// </summary>
        LightenIntensity,

        /// <summary>
        /// LinearBurn.
        /// </summary>
        LinearBurn,

        /// <summary>
        /// LinearDodge.
        /// </summary>
        LinearDodge,

        /// <summary>
        /// LinearLight.
        /// </summary>
        LinearLight,

        /// <summary>
        /// Luminize.
        /// </summary>
        Luminize,

        /// <summary>
        /// Mathematics.
        /// </summary>
        Mathematics,

        /// <summary>
        /// MinusDst.
        /// </summary>
        MinusDst,

        /// <summary>
        /// MinusSrc.
        /// </summary>
        MinusSrc,

        /// <summary>
        /// Modulate.
        /// </summary>
        Modulate,

        /// <summary>
        /// ModulusAdd.
        /// </summary>
        ModulusAdd,

        /// <summary>
        /// ModulusSubtract.
        /// </summary>
        ModulusSubtract,

        /// <summary>
        /// Multiply.
        /// </summary>
        Multiply,

        /// <summary>
        /// No.
        /// </summary>
        No,

        /// <summary>
        /// Out.
        /// </summary>
        Out,

        /// <summary>
        /// Over.
        /// </summary>
        Over,

        /// <summary>
        /// Overlay.
        /// </summary>
        Overlay,

        /// <summary>
        /// PegtopLight.
        /// </summary>
        PegtopLight,

        /// <summary>
        /// PinLight.
        /// </summary>
        PinLight,

        /// <summary>
        /// Plus.
        /// </summary>
        Plus,

        /// <summary>
        /// Replace.
        /// </summary>
        Replace,

        /// <summary>
        /// Saturate.
        /// </summary>
        Saturate,

        /// <summary>
        /// Screen.
        /// </summary>
        Screen,

        /// <summary>
        /// SoftLight.
        /// </summary>
        SoftLight,

        /// <summary>
        /// SrcAtop.
        /// </summary>
        SrcAtop,

        /// <summary>
        /// Src.
        /// </summary>
        Src,

        /// <summary>
        /// SrcIn.
        /// </summary>
        SrcIn,

        /// <summary>
        /// SrcOut.
        /// </summary>
        SrcOut,

        /// <summary>
        /// SrcOver.
        /// </summary>
        SrcOver,

        /// <summary>
        /// Threshold.
        /// </summary>
        Threshold,

        /// <summary>
        /// VividLight.
        /// </summary>
        VividLight,

        /// <summary>
        /// Xor.
        /// </summary>
        Xor,

        /// <summary>
        /// Stereo.
        /// </summary>
        Stereo,

        /// <summary>
        /// Freeze.
        /// </summary>
        Freeze,

        /// <summary>
        /// Interpolate.
        /// </summary>
        Interpolate,

        /// <summary>
        /// Negate.
        /// </summary>
        Negate,

        /// <summary>
        /// Reflect.
        /// </summary>
        Reflect,

        /// <summary>
        /// SoftBurn.
        /// </summary>
        SoftBurn,

        /// <summary>
        /// SoftDodge.
        /// </summary>
        SoftDodge,

        /// <summary>
        /// Stamp.
        /// </summary>
        Stamp,

        /// <summary>
        /// RMSE.
        /// </summary>
        RMSE,
    }
}