// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;

namespace ImageMagick;

/// <summary>
/// Interface that  describes an Exif profile.
/// </summary>
public interface IExifProfile : IImageProfile
{
    /// <summary>
    /// Gets or sets which parts will be written when the profile is added to an image.
    /// </summary>
    ExifParts Parts { get; set; }

    /// <summary>
    /// Gets the tags that where found but contained an invalid value.
    /// </summary>
    IReadOnlyList<ExifTag> InvalidTags { get; }

    /// <summary>
    /// Gets the length of the thumbnail data in the <see cref="byte"/> array of the profile.
    /// </summary>
    int ThumbnailLength { get; }

    /// <summary>
    /// Gets the offset of the thumbnail in the <see cref="byte"/> array of the profile.
    /// </summary>
    int ThumbnailOffset { get; }

    /// <summary>
    /// Gets the values of this exif profile.
    /// </summary>
    IReadOnlyList<IExifValue> Values { get; }

    /// <summary>
    /// Returns the value with the specified tag.
    /// </summary>
    /// <param name="tag">The tag of the exif value.</param>
    /// <returns>The value with the specified tag.</returns>
    /// <typeparam name="TValueType">The data type of the tag.</typeparam>
    IExifValue<TValueType>? GetValue<TValueType>(ExifTag<TValueType> tag);

    /// <summary>
    /// Removes the thumbnail in the exif profile.
    /// </summary>
    void RemoveThumbnail();

    /// <summary>
    /// Removes the value with the specified tag.
    /// </summary>
    /// <param name="tag">The tag of the exif value.</param>
    /// <returns>True when the value was fount and removed.</returns>
    bool RemoveValue(ExifTag tag);

    /// <summary>
    /// Loads the data from the profile and rewrites the profile data. This can be used
    /// to fix an incorrect profile. It can also be used for products that require a
    /// specific exif structure.
    /// </summary>
    void Rewrite();

    /// <summary>
    /// Sets the value of the specified tag.
    /// </summary>
    /// <param name="tag">The tag of the exif value.</param>
    /// <param name="value">The value.</param>
    /// <typeparam name="TValueType">The data type of the tag.</typeparam>
    void SetValue<TValueType>(ExifTag<TValueType> tag, TValueType value);
}
