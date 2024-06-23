// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick;

/// <summary>
/// Class that contains setting for quantize operations.
/// </summary>
public sealed partial class QuantizeSettings : IQuantizeSettings
{
    /// <summary>
    /// Initializes a new instance of the <see cref="QuantizeSettings"/> class.
    /// </summary>
    public QuantizeSettings()
    {
        Colors = 256;
        DitherMethod = ImageMagick.DitherMethod.Riemersma;
    }

    /// <summary>
    /// Gets or sets the maximum number of colors to quantize to.
    /// </summary>
    public int Colors { get; set; }

    /// <summary>
    /// Gets or sets the colorspace to quantize in.
    /// </summary>
    public ColorSpace ColorSpace { get; set; }

    /// <summary>
    /// Gets or sets the dither method to use.
    /// </summary>
    public DitherMethod? DitherMethod { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether errors should be measured.
    /// </summary>
    public bool MeasureErrors { get; set; }

    /// <summary>
    /// Gets or sets the quantization tree-depth.
    /// </summary>
    public int TreeDepth { get; set; }

    private static INativeInstance CreateNativeInstance(IQuantizeSettings settings)
    {
        var instance = NativeQuantizeSettings.Create();
        instance.SetColors((uint)settings.Colors);
        instance.SetColorSpace(settings.ColorSpace);
        instance.SetDitherMethod(settings.DitherMethod ?? ImageMagick.DitherMethod.No);
        instance.SetMeasureErrors(settings.MeasureErrors);
        instance.SetTreeDepth((uint)settings.TreeDepth);

        return instance;
    }
}
