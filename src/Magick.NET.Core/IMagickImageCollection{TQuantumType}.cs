// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.Collections.Generic;
using System.IO;

namespace ImageMagick
{
    /// <summary>
    /// Represents the collection of images.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    public interface IMagickImageCollection<TQuantumType> : IDisposable, IList<IMagickImage<TQuantumType>>
    {
        /// <summary>
        /// Event that will we raised when a warning is thrown by ImageMagick.
        /// </summary>
        event EventHandler<WarningEventArgs> Warning;

        /// <summary>
        /// Adds an image with the specified file name to the collection.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Add(string fileName);

        /// <summary>
        /// Adds the image(s) from the specified byte array to the collection.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void AddRange(byte[] data);

        /// <summary>
        /// Adds the image(s) from the specified byte array to the collection.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void AddRange(byte[] data, IMagickReadSettings<TQuantumType> readSettings);

        /// <summary>
        /// Adds a Clone of the specified images to this collection.
        /// </summary>
        /// <param name="images">The images to add to the collection.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void AddRange(IEnumerable<IMagickImage<TQuantumType>> images);

        /// <summary>
        /// Adds the image(s) from the specified file name to the collection.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void AddRange(string fileName);

        /// <summary>
        /// Adds the image(s) from the specified file name to the collection.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void AddRange(string fileName, IMagickReadSettings<TQuantumType> readSettings);

        /// <summary>
        /// Adds the image(s) from the specified stream to the collection.
        /// </summary>
        /// <param name="stream">The stream to read the images from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void AddRange(Stream stream);

        /// <summary>
        /// Adds the image(s) from the specified stream to the collection.
        /// </summary>
        /// <param name="stream">The stream to read the images from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void AddRange(Stream stream, IMagickReadSettings<TQuantumType> readSettings);

        /// <summary>
        /// Creates a single image, by appending all the images in the collection horizontally (+append).
        /// </summary>
        /// <returns>A single image, by appending all the images in the collection horizontally (+append).</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> AppendHorizontally();

        /// <summary>
        /// Creates a single image, by appending all the images in the collection vertically (-append).
        /// </summary>
        /// <returns>A single image, by appending all the images in the collection vertically (-append).</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> AppendVertically();

        /// <summary>
        /// Merge a sequence of images. This is useful for GIF animation sequences that have page
        /// offsets and disposal methods.
        /// </summary>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Coalesce();

        /// <summary>
        /// Creates a clone of the current image collection.
        /// </summary>
        /// <returns>A clone of the current image collection.</returns>
        IMagickImageCollection<TQuantumType> Clone();

        /// <summary>
        /// Combines the images into a single image. The typical ordering would be
        /// image 1 => Red, 2 => Green, 3 => Blue, etc.
        /// </summary>
        /// <returns>The images combined into a single image.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Combine();

        /// <summary>
        /// Combines the images into a single image. The grayscale value of the pixels of each image
        /// in the sequence is assigned in order to the specified channels of the combined image.
        /// The typical ordering would be image 1 => Red, 2 => Green, 3 => Blue, etc.
        /// </summary>
        /// <param name="colorSpace">The image colorspace.</param>
        /// <returns>The images combined into a single image.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Combine(ColorSpace colorSpace);

        /// <summary>
        /// Perform complex mathematics on an image sequence.
        /// </summary>
        /// <param name="complexSettings">The complex settings.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Complex(IComplexSettings complexSettings);

        /// <summary>
        /// Break down an image sequence into constituent parts. This is useful for creating GIF or
        /// MNG animation sequences.
        /// </summary>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Deconstruct();

        /// <summary>
        /// Evaluate image pixels into a single image. All the images in the collection must be the
        /// same size in pixels.
        /// </summary>
        /// <param name="evaluateOperator">The operator.</param>
        /// <returns>The resulting image of the evaluation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Evaluate(EvaluateOperator evaluateOperator);

        /// <summary>
        /// Use the virtual canvas size of first image. Images which fall outside this canvas is clipped.
        /// This can be used to 'fill out' a given virtual canvas.
        /// </summary>
        /// <returns>The resulting image of the flatten operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Flatten();

        /// <summary>
        /// Flatten this collection into a single image.
        /// This is useful for combining Photoshop layers into a single image.
        /// </summary>
        /// <param name="backgroundColor">The background color of the output image.</param>
        /// <returns>The resulting image of the flatten operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Flatten(IMagickColor<TQuantumType> backgroundColor);

        /// <summary>
        /// Inserts an image with the specified file name into the collection.
        /// </summary>
        /// <param name="index">The index to insert the image.</param>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        void Insert(int index, string fileName);

        /// <summary>
        /// Remap image colors with closest color from reference image.
        /// </summary>
        /// <param name="image">The image to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Map(IMagickImage<TQuantumType> image);

        /// <summary>
        /// Remap image colors with closest color from reference image.
        /// </summary>
        /// <param name="image">The image to use.</param>
        /// <param name="settings">Quantize settings.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Map(IMagickImage<TQuantumType> image, IQuantizeSettings settings);

        /// <summary>
        /// Merge all layers onto a canvas just large enough to hold all the actual images. The virtual
        /// canvas of the first image is preserved but otherwise ignored.
        /// </summary>
        /// <returns>The resulting image of the merge operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Merge();

        /// <summary>
        /// Create a composite image by combining the images with the specified settings.
        /// </summary>
        /// <param name="settings">The settings to use.</param>
        /// <returns>The resulting image of the montage operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Montage(IMontageSettings<TQuantumType> settings);

        /// <summary>
        /// The Morph method requires a minimum of two images. The first image is transformed into
        /// the second by a number of intervening images as specified by frames.
        /// </summary>
        /// <param name="frames">The number of in-between images to generate.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Morph(int frames);

        /// <summary>
        /// Start with the virtual canvas of the first image, enlarging left and right edges to contain
        /// all images. Images with negative offsets will be clipped.
        /// </summary>
        /// <returns>The resulting image of the mosaic operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Mosaic();

        /// <summary>
        /// Compares each image the GIF disposed forms of the previous image in the sequence. From
        /// this it attempts to select the smallest cropped image to replace each frame, while
        /// preserving the results of the GIF animation.
        /// </summary>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Optimize();

        /// <summary>
        /// OptimizePlus is exactly as Optimize, but may also add or even remove extra frames in the
        /// animation, if it improves the total number of pixels in the resulting GIF animation.
        /// </summary>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void OptimizePlus();

        /// <summary>
        /// Compares each image the GIF disposed forms of the previous image in the sequence. Any
        /// pixel that does not change the displayed result is replaced with transparency.
        /// </summary>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void OptimizeTransparency();

        /// <summary>
        /// Read only metadata and not the pixel data from all image frames.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Ping(byte[] data);

        /// <summary>
        /// Reads only metadata and not the pixel data from all image frames.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Ping(byte[] data, int offset, int count);

        /// <summary>
        /// Reads only metadata and not the pixel data from all image frames.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Ping(byte[] data, int offset, int count, IMagickReadSettings<TQuantumType> readSettings);

        /// <summary>
        /// Read only metadata and not the pixel data from all image frames.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Ping(byte[] data, IMagickReadSettings<TQuantumType> readSettings);

        /// <summary>
        /// Read only metadata and not the pixel data from all image frames.
        /// </summary>
        /// <param name="file">The file to read the frames from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Ping(FileInfo file);

        /// <summary>
        /// Read only metadata and not the pixel data from all image frames.
        /// </summary>
        /// <param name="file">The file to read the frames from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Ping(FileInfo file, IMagickReadSettings<TQuantumType> readSettings);

        /// <summary>
        /// Read only metadata and not the pixel data from all image frames.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Ping(Stream stream);

        /// <summary>
        /// Read only metadata and not the pixel data from all image frames.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Ping(Stream stream, IMagickReadSettings<TQuantumType> readSettings);

        /// <summary>
        /// Read only metadata and not the pixel data from all image frames.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Ping(string fileName);

        /// <summary>
        /// Read only metadata and not the pixel data from all image frames.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Ping(string fileName, IMagickReadSettings<TQuantumType> readSettings);

        /// <summary>
        /// Returns a new image where each pixel is the sum of the pixels in the image sequence after applying its
        /// corresponding terms (coefficient and degree pairs).
        /// </summary>
        /// <param name="terms">The list of polynomial coefficients and degree pairs and a constant.</param>
        /// <returns>A new image where each pixel is the sum of the pixels in the image sequence after applying its
        /// corresponding terms (coefficient and degree pairs).</returns>
        IMagickImage<TQuantumType> Polynomial(double[] terms);

        /// <summary>
        /// Quantize images (reduce number of colors).
        /// </summary>
        /// <returns>The resulting image of the quantize operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickErrorInfo Quantize();

        /// <summary>
        /// Quantize images (reduce number of colors).
        /// </summary>
        /// <param name="settings">Quantize settings.</param>
        /// <returns>The resulting image of the quantize operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickErrorInfo Quantize(IQuantizeSettings settings);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(byte[] data);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(byte[] data, int offset, int count);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <param name="format">The format to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(byte[] data, int offset, int count, MagickFormat format);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(byte[] data, int offset, int count, IMagickReadSettings<TQuantumType> readSettings);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="format">The format to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(byte[] data, MagickFormat format);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(byte[] data, IMagickReadSettings<TQuantumType> readSettings);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="file">The file to read the frames from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(FileInfo file);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="file">The file to read the frames from.</param>
        /// <param name="format">The format to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(FileInfo file, MagickFormat format);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="file">The file to read the frames from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(FileInfo file, IMagickReadSettings<TQuantumType> readSettings);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(Stream stream);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="format">The format to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(Stream stream, MagickFormat format);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(Stream stream, IMagickReadSettings<TQuantumType> readSettings);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(string fileName);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="format">The format to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(string fileName, MagickFormat format);

        /// <summary>
        /// Read all image frames.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(string fileName, IMagickReadSettings<TQuantumType> readSettings);

        /// <summary>
        /// Resets the page property of every image in the collection.
        /// </summary>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void RePage();

        /// <summary>
        /// Reverses the order of the images in the collection.
        /// </summary>
        void Reverse();

        /// <summary>
        /// Smush images from list into single image in horizontal direction.
        /// </summary>
        /// <param name="offset">Minimum distance in pixels between images.</param>
        /// <returns>The resulting image of the smush operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> SmushHorizontal(int offset);

        /// <summary>
        /// Smush images from list into single image in vertical direction.
        /// </summary>
        /// <param name="offset">Minimum distance in pixels between images.</param>
        /// <returns>The resulting image of the smush operation.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> SmushVertical(int offset);

        /// <summary>
        /// Converts this instance to a <see cref="byte"/> array.
        /// </summary>
        /// <returns>A <see cref="byte"/> array.</returns>
        byte[] ToByteArray();

        /// <summary>
        /// Converts this instance to a <see cref="byte"/> array.
        /// </summary>
        /// <param name="defines">The defines to set.</param>
        /// <returns>A <see cref="byte"/> array.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        byte[] ToByteArray(IWriteDefines defines);

        /// <summary>
        /// Converts this instance to a <see cref="byte"/> array.
        /// </summary>
        /// <returns>A <see cref="byte"/> array.</returns>
        /// <param name="format">The format to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        byte[] ToByteArray(MagickFormat format);

        /// <summary>
        /// Converts this instance to a base64 <see cref="string"/>.
        /// </summary>
        /// <returns>A base64 <see cref="string"/>.</returns>
        string ToBase64();

        /// <summary>
        /// Converts this instance to a base64 string.
        /// </summary>
        /// <param name="format">The format to use.</param>
        /// <returns>A base64 <see cref="string"/>.</returns>
        string ToBase64(MagickFormat format);

        /// <summary>
        /// Determine the overall bounds of all the image layers just as in <see cref="Merge()"/>,
        /// then adjust the the canvas and offsets to be relative to those bounds,
        /// without overlaying the images.
        /// </summary>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void TrimBounds();

        /// <summary>
        /// Writes the images to the specified file. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="file">The file to write the image to.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Write(FileInfo file);

        /// <summary>
        /// Writes the images to the specified file. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="file">The file to write the image to.</param>
        /// <param name="defines">The defines to set.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Write(FileInfo file, IWriteDefines defines);

        /// <summary>
        /// Writes the images to the specified file. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="file">The file to write the image to.</param>
        /// <param name="format">The format to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Write(FileInfo file, MagickFormat format);

        /// <summary>
        /// Writes the imagse to the specified stream. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="stream">The stream to write the images to.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Write(Stream stream);

        /// <summary>
        /// Writes the imagse to the specified stream. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="stream">The stream to write the images to.</param>
        /// <param name="defines">The defines to set.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Write(Stream stream, IWriteDefines defines);

        /// <summary>
        /// Writes the image to the specified stream.
        /// </summary>
        /// <param name="stream">The stream to write the image data to.</param>
        /// <param name="format">The format to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Write(Stream stream, MagickFormat format);

        /// <summary>
        /// Writes the images to the specified file name. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Write(string fileName);

        /// <summary>
        /// Writes the images to the specified file name. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="defines">The defines to set.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Write(string fileName, IWriteDefines defines);

        /// <summary>
        /// Writes the images to the specified file name. If the output image's file format does not
        /// allow multi-image files multiple files will be written.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="format">The format to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Write(string fileName, MagickFormat format);
    }
}