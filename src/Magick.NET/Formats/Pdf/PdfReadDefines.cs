// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick.Formats;

/// <summary>
/// Class for defines that are used when a <see cref="MagickFormat.Pdf"/> image is read.
/// </summary>
public sealed class PdfReadDefines : IReadDefines
{
    /// <summary>
    /// Gets or sets the size where the image should be scaled to fit the page (pdf:fit-page).
    /// </summary>
    public IMagickGeometry? FitPage { get; set; }

    /// <summary>
    /// Gets the format where the defines are for.
    /// </summary>
    public MagickFormat Format
        => MagickFormat.Pdf;

    /// <summary>
    /// Gets or sets a value indicating whether annotations should be hidden (pdf:hide-annotations).
    /// </summary>
    public bool? HideAnnotations { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether all images are forced to be interpolated at full device resolution (pdf: interpolate).
    /// </summary>
    public bool? Interpolate { get; set; }

    /// <summary>
    /// Gets or sets the password that should be used to open the pdf (authenticate).
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether use of the cropbox should be forced (pdf:use-trimbox).
    /// </summary>
    public bool? UseCropBox { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether use of the trimbox should be forced (pdf:use-trimbox).
    /// </summary>
    public bool? UseTrimBox { get; set; }

    /// <summary>
    /// Gets the defines that should be set as a define on an image.
    /// </summary>
    public IEnumerable<IDefine> Defines
    {
        get
        {
            if (FitPage is not null)
                yield return new MagickDefine(Format, "fit-page", FitPage);

            if (HideAnnotations == true)
                yield return new MagickDefine(Format, "hide-annotations", HideAnnotations.Value);

            if (Interpolate == true)
                yield return new MagickDefine(Format, "interpolate", Interpolate.Value);

            if (Password is not null)
                yield return new MagickDefine("authenticate", Password);

            if (UseCropBox.HasValue)
                yield return new MagickDefine(Format, "use-cropbox", UseCropBox.Value);

            if (UseTrimBox.HasValue)
                yield return new MagickDefine(Format, "use-trimbox", UseTrimBox.Value);
        }
    }
}
