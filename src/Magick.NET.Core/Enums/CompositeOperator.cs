// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

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
    /// Change mask.
    /// </summary>
    ChangeMask,

    /// <summary>
    /// Clear.
    /// </summary>
    Clear,

    /// <summary>
    /// Color burn.
    /// </summary>
    ColorBurn,

    /// <summary>
    /// Color dodge.
    /// </summary>
    ColorDodge,

    /// <summary>
    /// Colorize.
    /// </summary>
    Colorize,

    /// <summary>
    /// Copy black.
    /// </summary>
    CopyBlack,

    /// <summary>
    /// Copy blue.
    /// </summary>
    CopyBlue,

    /// <summary>
    /// Copy.
    /// </summary>
    Copy,

    /// <summary>
    /// Copy cyan.
    /// </summary>
    CopyCyan,

    /// <summary>
    /// Copy green.
    /// </summary>
    CopyGreen,

    /// <summary>
    /// Copy magenta.
    /// </summary>
    CopyMagenta,

    /// <summary>
    /// Copy alpha.
    /// </summary>
    CopyAlpha,

    /// <summary>
    /// Copy red.
    /// </summary>
    CopyRed,

    /// <summary>
    /// Copy yellow.
    /// </summary>
    CopyYellow,

    /// <summary>
    /// Darken.
    /// </summary>
    Darken,

    /// <summary>
    /// Darken intensity.
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
    /// Divide dst.
    /// </summary>
    DivideDst,

    /// <summary>
    /// Divide src.
    /// </summary>
    DivideSrc,

    /// <summary>
    /// Dst atop.
    /// </summary>
    DstAtop,

    /// <summary>
    /// Dst.
    /// </summary>
    Dst,

    /// <summary>
    /// Dst in.
    /// </summary>
    DstIn,

    /// <summary>
    /// Dst out.
    /// </summary>
    DstOut,

    /// <summary>
    /// Dst over.
    /// </summary>
    DstOver,

    /// <summary>
    /// Exclusion.
    /// </summary>
    Exclusion,

    /// <summary>
    /// Hard light.
    /// </summary>
    HardLight,

    /// <summary>
    /// Hard mix.
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
    /// Lighten intensity.
    /// </summary>
    LightenIntensity,

    /// <summary>
    /// Linear burn.
    /// </summary>
    LinearBurn,

    /// <summary>
    /// Linear dodge.
    /// </summary>
    LinearDodge,

    /// <summary>
    /// Linear light.
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
    /// Minus dst.
    /// </summary>
    MinusDst,

    /// <summary>
    /// Minus src.
    /// </summary>
    MinusSrc,

    /// <summary>
    /// Modulate.
    /// </summary>
    Modulate,

    /// <summary>
    /// Modulus add.
    /// </summary>
    ModulusAdd,

    /// <summary>
    /// Modulus subtract.
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
    /// Pegtop light.
    /// </summary>
    PegtopLight,

    /// <summary>
    /// Pin light.
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
    /// Soft light.
    /// </summary>
    SoftLight,

    /// <summary>
    /// Src atop.
    /// </summary>
    SrcAtop,

    /// <summary>
    /// Src.
    /// </summary>
    Src,

    /// <summary>
    /// Src in.
    /// </summary>
    SrcIn,

    /// <summary>
    /// Src out.
    /// </summary>
    SrcOut,

    /// <summary>
    /// Src over.
    /// </summary>
    SrcOver,

    /// <summary>
    /// Threshold.
    /// </summary>
    Threshold,

    /// <summary>
    /// Vivid light.
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
    /// Soft burn.
    /// </summary>
    SoftBurn,

    /// <summary>
    /// Soft dodge.
    /// </summary>
    SoftDodge,

    /// <summary>
    /// Stamp.
    /// </summary>
    Stamp,

    /// <summary>
    /// Root-mean-square error.
    /// </summary>
    RMSE,

    /// <summary>
    /// Saliency blend.
    /// </summary>
    SaliencyBlend,

    /// <summary>
    /// Seamless blend.
    /// </summary>
    SeamlessBlend,
}
