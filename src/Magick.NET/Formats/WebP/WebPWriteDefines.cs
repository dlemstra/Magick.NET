// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

namespace ImageMagick.Formats;

/// <summary>
/// Class for defines that are used when a <see cref="MagickFormat.WebP"/> image is written.
/// </summary>
public sealed class WebPWriteDefines : IWriteDefines
{
    /// <summary>
    /// Gets or sets the encoding of the alpha plane (webp:alpha-compression).
    /// </summary>
    public WebPAlphaCompression? AlphaCompression { get; set; }

    /// <summary>
    /// Gets or sets the predictive filtering method for the alpha plane (webp:alpha-filtering).
    /// </summary>
    public WebPAlphaFiltering? AlphaFiltering { get; set; }

    /// <summary>
    /// Gets or sets the compression value for alpha compression between 0 and 100. Lossless compression of alpha is achieved using a value of 100, while the lower values result in a lossy compression (webp:alpha-quality).
    /// </summary>
    public int? AlphaQuality { get; set; }

    /// <summary>
    /// Gets or sets a value indicating wether the algorithm should spend additional time optimizing the filtering strength to reach a well-balanced quality (webp:auto-filter).
    /// </summary>
    public bool? AutoFilter { get; set; }

    /// <summary>
    /// Gets or sets a value indicating wether a similar compression to that of JPEG but with less degradation should be used. (webp:emulate-jpeg-size).
    /// </summary>
    public bool? EmulateJpegSize { get; set; }

    /// <summary>
    /// Gets or sets a value indicating wether RGB values should be preserved in transparent area. It's disabled by default to help compressibility.
    /// </summary>
    public bool? Exact { get; set; }

    /// <summary>
    /// Gets or sets strength of the filter sharpness, between 0 and 7 (least sharp) (webp:filter-sharpness).
    /// </summary>
    public int? FilterSharpness { get; set; }

    /// <summary>
    /// Gets or sets strength of the deblocking filter, between 0 (no filtering) and 100 (maximum filtering). A value of 0 turns off any filtering. Higher values increase the strength of the filtering process applied after decoding the image. The higher the value, the smoother the image appears. Typical values are usually in the range of 20 to 50 (webp:filter-strength).
    /// </summary>
    public int? FilterStrength { get; set; }

    /// <summary>
    /// Gets or sets the filter type. (webp:filter-type).
    /// </summary>
    public WebPFilterType? FilterType { get; set; }

    /// <summary>
    /// Gets the format where the defines are for.
    /// </summary>
    public MagickFormat Format
        => MagickFormat.WebP;

    /// <summary>
    /// Gets or sets the hint about the image type. (webp:image-hint).
    /// </summary>
    public WebPImageHint? ImageHint { get; set; }

    /// <summary>
    /// Gets or sets a value indicating wether the image should be encoded without any loss (webp:lossless).
    /// </summary>
    public bool? Lossless { get; set; }

    /// <summary>
    /// Gets or sets a value indicating wether the memory usage should be reduced (webp:low-memory).
    /// </summary>
    public bool? LowMemory { get; set; }

    /// <summary>
    /// Gets or sets the compression method to use. It controls the trade off between encoding speed and the compressed file size and quality. Possible values range from 0 to 6. Default value is 4. When higher values are utilized, the encoder spends more time inspecting additional encoding possibilities and decide on the quality gain. Lower value might result in faster processing time at the expense of larger file size and lower compression quality (webp:method).
    /// </summary>
    public int? Method { get; set; }

    /// <summary>
    /// Gets or sets the near lossless encoding, between 0 (max-loss) and 100 (off) (webp:near-lossless).
    /// </summary>
    [Obsolete("This property will be removed in the next major release, this was removed from ImageMagick.")]
    public int? NearLossless { get; set; }

    /// <summary>
    /// Gets or sets the partition limit. Choose 0 for no quality degradation and 100 for maximum degradation (webp:partition-limit).
    /// </summary>
    public int? PartitionLimit { get; set; }

    /// <summary>
    /// Gets or sets progressive decoding: choose 0 to 3 (webp:partitions).
    /// </summary>
    public int? Partitions { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of passes to target compression size or PSNR (webp:pass).
    /// </summary>
    public int? Pass { get; set; }

    /// <summary>
    /// Gets or sets the preprocessing filter (webp:preprocessing).
    /// </summary>
    public WebPPreprocessing? Preprocessing { get; set; }

    /// <summary>
    /// Gets or sets  the maximum number of segments to use, choose from 1 to 4 (webp:segment).
    /// </summary>
    public int? Segment { get; set; }

    /// <summary>
    /// Gets or sets a value indicating wether the compressed picture should be exported back (webp:show-compressed).
    /// </summary>
    public bool? ShowCompressed { get; set; }

    /// <summary>
    /// Gets or sets he amplitude of the spatial noise shaping. Spatial noise shaping (SNS) refers to a general collection of built-in algorithms used to decide which area of the picture should use relatively less bits, and where else to better transfer these bits. The possible range goes from 0 (algorithm is off) to 100 (the maximal effect). The default value is 80 (webp:sns-strength).
    /// </summary>
    public int? SnsStrength { get; set; }

    /// <summary>
    /// Gets or sets the desired minimal distortion (webp:target-psnr).
    /// </summary>
    public double? TargetPsnr { get; set; }

    /// <summary>
    /// Gets or sets the target size (in bytes) to try and reach for the compressed output. The compressor makes several passes of partial encoding in order to get as close as possible to this target. (webp:target-size).
    /// </summary>
    public int? TargetSize { get; set; }

    /// <summary>
    /// Gets or sets a value indicating wether multi-threaded encoding should be enabled (webp:thread-level).
    /// </summary>
    public bool? ThreadLevel { get; set; }

    /// <summary>
    /// Gets or sets a value indicating wether sharp (and slow) RGB->YUV conversion should be used. (webp:use-sharp-yuv).
    /// </summary>
    public bool? UseSharpYuv { get; set; }

    /// <summary>
    /// Gets the defines that should be set as a define on an image.
    /// </summary>
    public IEnumerable<IDefine> Defines
    {
        get
        {
            if (AlphaCompression.HasValue)
                yield return new MagickDefine(Format, "alpha-compression", (int)AlphaCompression.Value);

            if (AlphaFiltering.HasValue)
                yield return new MagickDefine(Format, "alpha-filtering", (int)AlphaFiltering.Value);

            if (AlphaQuality.HasValue)
                yield return new MagickDefine(Format, "alpha-quality", AlphaQuality.Value);

            if (AutoFilter.HasValue)
                yield return new MagickDefine(Format, "auto-filter", AutoFilter.Value);

            if (EmulateJpegSize.HasValue)
                yield return new MagickDefine(Format, "emulate-jpeg-size", EmulateJpegSize.Value);

            if (Exact.HasValue)
                yield return new MagickDefine(Format, "exact", Exact.Value);

            if (FilterStrength.HasValue)
                yield return new MagickDefine(Format, "filter-strength", FilterStrength.Value);

            if (FilterSharpness.HasValue)
                yield return new MagickDefine(Format, "filter-sharpness", FilterSharpness.Value);

            if (FilterType.HasValue)
                yield return new MagickDefine(Format, "filter-type", (int)FilterType.Value);

            if (ImageHint.HasValue)
                yield return new MagickDefine(Format, "image-hint", ImageHint.Value);

            if (Lossless.HasValue)
                yield return new MagickDefine(Format, "lossless", Lossless.Value);

            if (LowMemory.HasValue)
                yield return new MagickDefine(Format, "low-memory", LowMemory.Value);

            if (Method.HasValue)
                yield return new MagickDefine(Format, "method", Method.Value);

            if (PartitionLimit.HasValue)
                yield return new MagickDefine(Format, "partition-limit", PartitionLimit.Value);

            if (Partitions.HasValue)
                yield return new MagickDefine(Format, "partitions", Partitions.Value);

            if (Pass.HasValue)
                yield return new MagickDefine(Format, "pass", Pass.Value);

            if (Preprocessing.HasValue)
                yield return new MagickDefine(Format, "preprocessing", (int)Preprocessing.Value);

            if (Segment.HasValue)
                yield return new MagickDefine(Format, "segment", Segment.Value);

            if (ShowCompressed.HasValue)
                yield return new MagickDefine(Format, "show-compressed", ShowCompressed.Value);

            if (SnsStrength.HasValue)
                yield return new MagickDefine(Format, "sns-strength", SnsStrength.Value);

            if (TargetPsnr.HasValue)
                yield return new MagickDefine(Format, "target-psnr", TargetPsnr.Value);

            if (TargetSize.HasValue)
                yield return new MagickDefine(Format, "target-size", TargetSize.Value);

            if (ThreadLevel.HasValue)
                yield return new MagickDefine(Format, "thread-level", ThreadLevel.Value);

            if (UseSharpYuv.HasValue)
                yield return new MagickDefine(Format, "use-sharp-yuv", UseSharpYuv.Value);
        }
    }
}
