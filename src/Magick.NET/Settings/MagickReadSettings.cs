// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Globalization;

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
/// Class that contains setting for when an image is being read.
/// </summary>
public sealed class MagickReadSettings : MagickSettings, IMagickReadSettings<QuantumType>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MagickReadSettings"/> class.
    /// </summary>
    public MagickReadSettings()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickReadSettings"/> class with the specified defines.
    /// </summary>
    /// <param name="readDefines">The read defines to set.</param>
    public MagickReadSettings(IReadDefines readDefines)
        => SetDefines(readDefines);

    internal MagickReadSettings(MagickSettings settings)
        => CopyFrom(settings);

    internal MagickReadSettings(IMagickReadSettings<QuantumType> settings)
    {
        Copy(settings);

        ApplyDefines();
        ApplyDimensions();
        ApplyFrame();
    }

    /// <summary>
    /// Gets or sets the defines that should be set before the image is read.
    /// </summary>
    public IReadDefines? Defines { get; set; }

    /// <summary>
    /// Gets or sets the specified area to extract from the image.
    /// </summary>
    public IMagickGeometry? ExtractArea
    {
        get => Extract;
        set => Extract = value;
    }

    /// <summary>
    /// Gets or sets the index of the image to read from a multi layer/frame image.
    /// </summary>
    public uint? FrameIndex { get; set; }

    /// <summary>
    /// Gets or sets the number of images to read from a multi layer/frame image.
    /// </summary>
    public uint? FrameCount { get; set; }

    /// <summary>
    /// Gets or sets the height.
    /// </summary>
    public uint? Height { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the exif profile should be used to update some of the
    /// properties of the image (e.g. <see cref="IMagickImage.Density"/>, <see cref="IMagickImage.Orientation"/>).
    /// </summary>
    public bool SyncImageWithExifProfile
    {
        get
        {
            var value = GetOption("exif:sync-image");
            if (value is null)
                return true;

            return bool.Parse(value);
        }
        set => SetOption("exif:sync-image", value.ToString());
    }

    /// <summary>
    /// Gets or sets a value indicating whether the tiff profile should be used to update some of the
    /// properties of the image (e.g. <see cref="IMagickImage.Density"/>, <see cref="IMagickImage.Orientation"/>).
    /// </summary>
    public bool SyncImageWithTiffProperties
    {
        get
        {
            var value = GetOption("tiff:sync-image");
            if (value is null)
                return true;

            return bool.Parse(value);
        }
        set => SetOption("tiff:sync-image", value.ToString());
    }

    /// <summary>
    /// Gets or sets a value indicating whether the monochrome reader shoul be used. This is
    /// supported by: <see cref="MagickFormat.Pcl"/>, <see cref="MagickFormat.Pdf"/> ,
    /// <see cref="MagickFormat.Ps"/>  and <see cref="MagickFormat.Xps"/>.
    /// </summary>
    public bool UseMonochrome
    {
        get => Monochrome;
        set => Monochrome = value;
    }

    /// <summary>
    /// Gets or sets the width.
    /// </summary>
    public uint? Width { get; set; }

    private static string GetDefineKey(IDefine define)
    {
        if (define.Format == MagickFormat.Unknown)
            return define.Name;

        return EnumHelper.GetName(define.Format) + ":" + define.Name;
    }

    private void ApplyDefines()
    {
        if (Defines is null)
            return;

        foreach (var define in Defines.Defines)
        {
            SetOption(GetDefineKey(define), define.Value);
        }
    }

    private void ApplyDimensions()
    {
        if (Width.HasValue && Height.HasValue)
            Size = Width + "x" + Height;
        else if (Width.HasValue)
            Size = Width + "x";
        else if (Height.HasValue)
            Size = "x" + Height;
    }

    private void ApplyFrame()
    {
        if (!FrameIndex.HasValue && !FrameCount.HasValue)
            return;

        Scene = FrameIndex ?? 0;
        NumberScenes = FrameCount ?? 1;

        if (FrameIndex.HasValue && (!FrameCount.HasValue || FrameCount.Value == 1))
            Scenes = FrameIndex.Value.ToString(CultureInfo.InvariantCulture);
        else
            Scenes = string.Format(CultureInfo.InvariantCulture, "{0}-{1}", Scene, Scene + NumberScenes);
    }

    private void Copy(IMagickReadSettings<QuantumType> settings)
    {
        CopyFrom((MagickSettings)settings);

        Defines = settings.Defines;
        FrameIndex = settings.FrameIndex;
        FrameCount = settings.FrameCount;
        Height = settings.Height;
        Width = settings.Width;
    }
}
