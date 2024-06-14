// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;

namespace ImageMagick.Formats;

/// <summary>
/// The info of a <see cref="MagickFormat.Pdf"/> file.
/// </summary>
public sealed partial class PdfInfo
{
    private PdfInfo(int pageCount)
    {
        PageCount = pageCount;
    }

    /// <summary>
    /// Gets the page count of the file.
    /// </summary>
    public int PageCount { get; }

    /// <summary>
    /// Creates info from a <see cref="MagickFormat.Pdf"/> file.
    /// </summary>
    /// <param name="file">The pdf file to create the info from.</param>
    /// <returns>The info of a <see cref="MagickFormat.Pdf"/> file.</returns>
    public static PdfInfo Create(FileInfo file)
        => Create(file, string.Empty);

    /// <summary>
    /// Creates info from a <see cref="MagickFormat.Pdf"/> file.
    /// </summary>
    /// <param name="file">The pdf file to create the info from.</param>
    /// <param name="password">The password of the pdf file.</param>
    /// <returns>The info of a <see cref="MagickFormat.Pdf"/> file.</returns>
    public static PdfInfo Create(FileInfo file, string password)
    {
        Throw.IfNull(nameof(file), file);
        return Create(file.FullName, password);
    }

    /// <summary>
    /// Creates info from a <see cref="MagickFormat.Pdf"/> file.
    /// </summary>
    /// <param name="fileName">The pdf file to create the info from.</param>
    /// <returns>The info of a <see cref="MagickFormat.Pdf"/> file.</returns>
    public static PdfInfo Create(string fileName)
        => Create(fileName, string.Empty);

    /// <summary>
    /// Creates info from a <see cref="MagickFormat.Pdf"/> file.
    /// </summary>
    /// <param name="fileName">The pdf file to create the info from.</param>
    /// <param name="password">The password of the pdf file.</param>
    /// <returns>The info of a <see cref="MagickFormat.Pdf"/> file.</returns>
    public static PdfInfo Create(string fileName, string password)
    {
        var filePath = FileHelper.CheckForBaseDirectory(fileName);
        Throw.IfNullOrEmpty(nameof(fileName), filePath);
        filePath = filePath.Replace('\\', '/');

        Throw.IfNull(nameof(password), password);

        var pageCount = NativePdfInfo.PageCount(filePath, password);
        if (pageCount == 0)
            throw new MagickErrorException("Unable to determine the page count.");

        return new PdfInfo(pageCount);
    }
}
