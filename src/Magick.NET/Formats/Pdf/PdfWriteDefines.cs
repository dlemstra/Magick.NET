// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

namespace ImageMagick.Formats;

/// <summary>
/// Class for defines that are used when a <see cref="MagickFormat.Pdf"/> image is written.
/// </summary>
public sealed class PdfWriteDefines : IWriteDefines
{
    /// <summary>
    /// Gets or sets the author of the pdf document (pdf:author).
    /// </summary>
    public string? Author { get; set; }

    /// <summary>
    /// Gets or sets the creation time of the pdf document (pdf:create-epoch).
    /// </summary>
    public DateTime? CreationTime { get; set; }

    /// <summary>
    /// Gets or sets the creator of the pdf document (pdf:creator).
    /// </summary>
    public string? Creator { get; set; }

    /// <summary>
    /// Gets the format where the defines are for.
    /// </summary>
    public MagickFormat Format
        => MagickFormat.Pdf;

    /// <summary>
    /// Gets or sets the keywords of the pdf document (pdf:keywords).
    /// </summary>
    public string? Keywords { get; set; }

    /// <summary>
    /// Gets or sets the modification time of the pdf document (pdf:modify-epoch).
    /// </summary>
    public DateTime? ModificationTime { get; set; }

    /// <summary>
    /// Gets or sets the producer of the pdf document (pdf:producer).
    /// </summary>
    public string? Producer { get; set; }

    /// <summary>
    /// Gets or sets the subject of the pdf document (pdf:subject).
    /// </summary>
    public string? Subject { get; set; }

    /// <summary>
    /// Gets or sets a value indicating wether a thumbnail should be added to the pdf document (pdf:thumbnail).
    /// </summary>
    public bool? Thumbnail { get; set; }

    /// <summary>
    /// Gets or sets the title of the pdf document (pdf:title).
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets the defines that should be set as a define on an image.
    /// </summary>
    public IEnumerable<IDefine> Defines
    {
        get
        {
            if (Author?.Length > 0)
                yield return new MagickDefine(Format, "author", Author);

            if (CreationTime is not null)
                yield return new MagickDefine(Format, "create-epoch", ToUnixTimeSeconds(CreationTime.Value));

            if (Creator?.Length > 0)
                yield return new MagickDefine(Format, "creator", Creator);

            if (Keywords?.Length > 0)
                yield return new MagickDefine(Format, "keywords", Keywords);

            if (ModificationTime is not null)
                yield return new MagickDefine(Format, "modify-epoch", ToUnixTimeSeconds(ModificationTime.Value));

            if (Producer?.Length > 0)
                yield return new MagickDefine(Format, "producer", Producer);

            if (Subject?.Length > 0)
                yield return new MagickDefine(Format, "subject", Subject);

            if (Thumbnail.HasValue)
                yield return new MagickDefine(Format, "thumbnail", Thumbnail.Value);

            if (Title?.Length > 0)
                yield return new MagickDefine(Format, "title", Title);
        }
    }

    private static long ToUnixTimeSeconds(DateTime value)
    {
        var dateTimeOffset = (DateTimeOffset)value.ToUniversalTime();
        return dateTimeOffset.ToUnixTimeSeconds();
    }
}
