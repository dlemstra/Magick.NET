//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in 
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ImageMagick
{
  /// <summary>
  /// Represents the collection of images.
  /// </summary>
  public sealed partial class MagickImageCollection : IDisposable, IList<MagickImage>
  {
    private List<MagickImage> _Images;
    private EventHandler<WarningEventArgs> _Warning;
    private NativeMagickImageCollection _NativeInstance;

    private void AddImages(byte[] data, MagickReadSettings readSettings, bool ping)
    {
      Throw.IfNullOrEmpty(nameof(data), data);

      MagickSettings settings = CreateSettings(readSettings);
      settings.Ping = ping;

      IntPtr result = _NativeInstance.ReadBlob(settings, data, data.Length);
      AddImages(result, settings);
    }

    private void AddImages(string fileName, MagickReadSettings readSettings, bool ping)
    {
      string filePath = FileHelper.CheckForBaseDirectory(fileName);
      Throw.IfInvalidFileName(filePath);

      MagickSettings settings = CreateSettings(readSettings);
      settings.FileName = filePath;
      settings.Ping = ping;

      IntPtr result = _NativeInstance.ReadFile(settings);
      AddImages(result, settings);
    }

    private void AddImages(IntPtr result, MagickSettings settings)
    {
      foreach (MagickImage image in MagickImage.CreateList(result, settings))
      {
        _Images.Add(image);
      }
    }

    private void AttachImages()
    {
      for (int i = 0; i < _Images.Count - 1; i++)
      {
        _Images[i].SetNext(_Images[i + 1]);
      }
    }

    [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "PixelStorage")]
    private static MagickSettings CreateSettings(MagickReadSettings readSettings)
    {
      if (readSettings == null)
        return new MagickSettings();

      Throw.IfTrue(nameof(readSettings), readSettings.PixelStorage != null, "PixelStorage is not supported for images with multiple frames/layers.");

      return new MagickReadSettings(readSettings);
    }

    private void DetachImages()
    {
      for (int i = _Images.Count - 2; i > 0; i--)
      {
        _Images[i].SetNext(null);
      }
    }

    private void Dispose(bool disposing)
    {
      if (_NativeInstance != null)
        _NativeInstance.Warning -= OnWarning;

      if (disposing)
        Clear();
    }

    private void OnWarning(object sender, WarningEventArgs arguments)
    {
      if (_Warning != null)
        _Warning(this, arguments);
    }

    private void SetDefines([ValidatedNotNull] IWriteDefines defines)
    {
      foreach (MagickImage image in _Images)
      {
        image.Settings.SetDefines(defines);
      }
    }

    private void SetFormat(MagickFormat format)
    {
      foreach (MagickImage image in _Images)
      {
        image.Format = format;
      }
    }

    private void ThrowIfEmpty()
    {
      if (_Images.Count == 0)
        throw new InvalidOperationException("Operation requires at least one image.");
    }

    private void ThrowIfCountLowerThan(int count)
    {
      if (_Images.Count < count)
        throw new InvalidOperationException("Operation requires at least " + count + " images.");
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return _Images.GetEnumerator();
    }

    /// <summary>
    /// Finalizer
    /// </summary>
    ~MagickImageCollection()
    {
      Dispose(false);
    }

    /// <summary>
    /// Initializes a new instance of the MagickImage class.
    /// </summary>
    public MagickImageCollection()
    {
      _Images = new List<MagickImage>();
      _NativeInstance = new NativeMagickImageCollection();
      _NativeInstance.Warning += OnWarning;
    }

    /// <summary>
    /// Initializes a new instance of the MagickImageCollection class using the specified byte array.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <exception cref="MagickException"/>
    public MagickImageCollection(byte[] data)
      : this()
    {
      Read(data);
    }

    /// <summary>
    /// Initializes a new instance of the MagickImageCollection class using the specified byte array.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException"/>
    public MagickImageCollection(byte[] data, MagickReadSettings readSettings)
      : this()
    {
      Read(data, readSettings);
    }

    /// <summary>
    /// Initializes a new instance of the MagickImage class using the specified file.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <exception cref="MagickException"/>
    public MagickImageCollection(FileInfo file)
      : this()
    {
      Read(file);
    }

    /// <summary>
    /// Initializes a new instance of the MagickImage class using the specified file.
    /// </summary>
    /// <param name="file">The file to read the image from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException"/>
    public MagickImageCollection(FileInfo file, MagickReadSettings readSettings)
      : this()
    {
      Read(file, readSettings);
    }

    /// <summary>
    /// Initializes a new instance of the MagickImage class using the specified images.
    /// </summary>
    /// <param name="images">The images to add to the collection.</param>
    /// <exception cref="MagickException"/>
    public MagickImageCollection(IEnumerable<MagickImage> images)
      : this()
    {
      Throw.IfNull(nameof(images), images);

      foreach (MagickImage image in images)
      {
        Add(image);
      }
    }

    /// <summary>
    /// Initializes a new instance of the MagickImageCollection class using the specified stream.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException"/>
    public MagickImageCollection(Stream stream)
      : this()
    {
      AddRange(stream);
    }

    /// <summary>
    /// Initializes a new instance of the MagickImageCollection class using the specified stream.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException"/>
    public MagickImageCollection(Stream stream, MagickReadSettings readSettings)
      : this()
    {
      AddRange(stream, readSettings);
    }

    /// <summary>
    /// Initializes a new instance of the MagickImageCollection class using the specified filename.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException"/>
    public MagickImageCollection(string fileName)
      : this()
    {
      AddRange(fileName);
    }

    /// <summary>
    /// Initializes a new instance of the MagickImageCollection class using the specified filename
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException"/>
    public MagickImageCollection(string fileName, MagickReadSettings readSettings)
      : this()
    {
      AddRange(fileName, readSettings);
    }

    /// <summary>
    /// Converts this instance to a byte array.
    /// </summary>
    public static explicit operator byte[](MagickImageCollection collection)
    {
      Throw.IfNull(nameof(collection), collection);

      return collection.ToByteArray();
    }

    /// <summary>
    /// Event that will we raised when a warning is thrown by ImageMagick.
    /// </summary>
    public event EventHandler<WarningEventArgs> Warning
    {
      add
      {
        _Warning += value;
      }
      remove
      {
        _Warning -= value;
      }
    }

    /// <summary>
    /// Gets or sets the image at the specified index.
    /// </summary>
    public MagickImage this[int index]
    {
      get
      {
        return _Images[index];
      }
      set
      {
        _Images[index] = value;
      }
    }

    /// <summary>
    /// Returns the number of images in the collection.
    /// </summary>
    public int Count
    {
      get
      {
        return _Images.Count;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the collection is read-only.
    /// </summary>
    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    /// Adds an image to the collection.
    /// </summary>
    /// <param name="item">The image to add.</param>
    public void Add(MagickImage item)
    {
      _Images.Add(item);
    }

    /// <summary>
    /// Adds an image with the specified file name to the collection.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException"/>
    public void Add(string fileName)
    {
      _Images.Add(new MagickImage(fileName));
    }

    /// <summary>
    /// Adds the image(s) from the specified byte array to the collection.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <exception cref="MagickException"/>
    public void AddRange(byte[] data)
    {
      AddRange(data, null);
    }

    /// <summary>
    /// Adds the image(s) from the specified byte array to the collection.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException"/>
    public void AddRange(byte[] data, MagickReadSettings readSettings)
    {
      AddImages(data, readSettings, false);
    }

    /// <summary>
    /// Adds a the specified images to this collection.
    /// </summary>
    /// <param name="images">The images to add to the collection.</param>
    /// <exception cref="MagickException"/>
    public void AddRange(IEnumerable<MagickImage> images)
    {
      Throw.IfNull(nameof(images), images);

      foreach (MagickImage image in images)
      {
        Add(image);
      }
    }

    /// <summary>
    /// Adds a Clone of the images from the specified collection to this collection.
    /// </summary>
    /// <param name="images">A collection of MagickImages.</param>
    /// <exception cref="MagickException"/>
    public void AddRange(MagickImageCollection images)
    {
      Throw.IfNull(nameof(images), images);

      int count = images.Count;
      for (int i = 0; i < count; i++)
      {
        Add(images[i].Clone());
      }
    }

    /// <summary>
    /// Adds the image(s) from the specified file name to the collection.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException"/>
    public void AddRange(string fileName)
    {
      AddRange(fileName, null);
    }

    /// <summary>
    /// Adds the image(s) from the specified file name to the collection.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException"/>
    public void AddRange(string fileName, MagickReadSettings readSettings)
    {
      AddImages(fileName, readSettings, false);
    }

    /// <summary>
    /// Adds the image(s) from the specified stream to the collection.
    /// </summary>
    /// <param name="stream">The stream to read the images from.</param>
    /// <exception cref="MagickException"/>
    public void AddRange(Stream stream)
    {
      AddRange(stream, null);
    }

    /// <summary>
    /// Adds the image(s) from the specified stream to the collection.
    /// </summary>
    /// <param name="stream">The stream to read the images from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException"/>
    public void AddRange(Stream stream, MagickReadSettings readSettings)
    {
      AddRange(StreamHelper.ToByteArray(stream), readSettings);
    }

    /// <summary>
    /// Creates a single image, by appending all the images in the collection horizontally (+append).
    /// </summary>
    /// <exception cref="MagickException"/>
    public MagickImage AppendHorizontally()
    {
      ThrowIfEmpty();

      try
      {
        AttachImages();
        IntPtr image = _NativeInstance.Append(_Images[0], false);
        return MagickImage.Create(image, _Images[0].Settings);
      }
      finally
      {
        DetachImages();
      }
    }

    /// <summary>
    /// Creates a single image, by appending all the images in the collection vertically (-append).
    /// </summary>
    /// <exception cref="MagickException"/>
    public MagickImage AppendVertically()
    {
      ThrowIfEmpty();

      try
      {
        AttachImages();
        IntPtr image = _NativeInstance.Append(_Images[0], true);
        return MagickImage.Create(image, _Images[0].Settings);
      }
      finally
      {
        DetachImages();
      }
    }

    /// <summary>
    /// Merge a sequence of images. This is useful for GIF animation sequences that have page
    /// offsets and disposal methods
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Coalesce()
    {
      ThrowIfEmpty();

      MagickSettings settings = _Images[0].Settings.Clone();

      IntPtr images;
      try
      {
        AttachImages();
        images = _NativeInstance.Coalesce(_Images[0]);
      }
      finally
      {
        DetachImages();
      }

      Clear();
      foreach (MagickImage image in MagickImage.CreateList(images, settings))
        Add(image);
    }

    /// <summary>
    /// Removes all images from the collection.
    /// </summary>
    public void Clear()
    {
      foreach (MagickImage image in _Images)
      {
        if (image != null)
          image.Dispose();
      }

      _Images.Clear();
    }

    /// <summary>
    /// Creates a clone of the current image collection.
    /// </summary>
    public MagickImageCollection Clone()
    {
      MagickImageCollection result = new MagickImageCollection();
      foreach (MagickImage image in this)
        result.Add(image.Clone());

      return result;
    }

    /// <summary>
    /// Combines one or more images into a single image. The typical ordering would be
    /// image 1 => Red, 2 => Green, 3 => Blue, etc.
    /// </summary>
    public MagickImage Combine()
    {
      return Combine(Channels.All);
    }

    /// <summary>
    /// Combines one or more images into a single image. The grayscale value of the pixels of each
    /// image in the sequence is assigned in order to the specified channels of the combined image.
    /// The typical ordering would be image 1 => Red, 2 => Green, 3 => Blue, etc.
    /// </summary>
    /// <param name="channels">The channel(s) to combine.</param>
    /// <exception cref="MagickException"/>
    public MagickImage Combine(Channels channels)
    {
      ThrowIfEmpty();

      try
      {
        AttachImages();
        IntPtr image = _NativeInstance.Combine(_Images[0], channels);
        return MagickImage.Create(image, _Images[0].Settings);
      }
      finally
      {
        DetachImages();
      }
    }

    /// <summary>
    /// Determines whether the collection contains the specified image.
    /// </summary>
    /// <param name="item">The image to check.</param>
    public bool Contains(MagickImage item)
    {
      return _Images.Contains(item);
    }

    /// <summary>
    /// Copies the images to an Array, starting at a particular Array index.
    /// </summary>
    /// <param name="array">The one-dimensional Array that is the destination.</param>
    /// <param name="arrayIndex">The zero-based index in 'destination' at which copying begins.</param>
    public void CopyTo(MagickImage[] array, int arrayIndex)
    {
      if (_Images.Count == 0)
        return;

      Throw.IfNull(nameof(array), array);
      Throw.IfOutOfRange(nameof(arrayIndex), arrayIndex, _Images.Count);
      Throw.IfOutOfRange(nameof(arrayIndex), arrayIndex, array.Length);

      int indexI = 0;
      int length = Math.Min(array.Length, _Images.Count);
      for (int indexA = arrayIndex; indexA < length; indexA++)
      {
        array[indexA] = _Images[indexI++].Clone();
      }
    }

    /// <summary>
    /// Break down an image sequence into constituent parts. This is useful for creating GIF or
    /// MNG animation sequences.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Deconstruct()
    {
      ThrowIfEmpty();

      MagickSettings settings = _Images[0].Settings.Clone();

      IntPtr images;
      try
      {
        AttachImages();
        images = _NativeInstance.Deconstruct(_Images[0]);
      }
      finally
      {
        DetachImages();
      }

      Clear();
      foreach (MagickImage image in MagickImage.CreateList(images, settings))
        Add(image);
    }

    /// <summary>
    /// Disposes the MagickImageCollection instance.
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Evaluate image pixels into a single image. All the images in the collection must be the
    /// same size in pixels.
    /// </summary>
    /// <param name="evaluateOperator">The operator.</param>
    /// <exception cref="MagickException"/>
    public MagickImage Evaluate(EvaluateOperator evaluateOperator)
    {
      ThrowIfEmpty();

      try
      {
        AttachImages();
        IntPtr image = _NativeInstance.Evaluate(_Images[0], evaluateOperator);
        return MagickImage.Create(image, _Images[0].Settings);
      }
      finally
      {
        DetachImages();
      }
    }

    /// <summary>
    /// Flatten this collection into a single image.
    /// This is useful for combining Photoshop layers into a single image.
    /// </summary>
    /// <exception cref="MagickException"/>
    public MagickImage Flatten()
    {
      ThrowIfEmpty();

      try
      {
        AttachImages();
        IntPtr image = _NativeInstance.Flatten(_Images[0]);
        return MagickImage.Create(image, _Images[0].Settings);
      }
      finally
      {
        DetachImages();
      }
    }

    /// <summary>
    /// Returns an enumerator that iterates through the images.
    /// </summary>
    /// <returns></returns>
    public IEnumerator<MagickImage> GetEnumerator()
    {
      return _Images.GetEnumerator();
    }

    /// <summary>
    /// Determines the index of the specified image.
    /// </summary>
    /// <param name="item">The image to check.</param>
    public int IndexOf(MagickImage item)
    {
      return _Images.IndexOf(item);
    }

    /// <summary>
    /// Inserts an image into the collection.
    /// </summary>
    /// <param name="index">The index to insert the image.</param>
    /// <param name="item">The image to insert.</param>
    public void Insert(int index, MagickImage item)
    {
      _Images.Insert(index, item);
    }

    /// <summary>
    /// Inserts an image with the specified file name into the collection.
    /// </summary>
    /// <param name="index">The index to insert the image.</param>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    public void Insert(int index, string fileName)
    {
      _Images.Insert(index, new MagickImage(fileName));
    }

    /// <summary>
    /// Remap image colors with closest color from reference image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <exception cref="MagickException"/>
    public void Map(MagickImage image)
    {
      Map(image, new QuantizeSettings());
    }

    /// <summary>
    /// Remap image colors with closest color from reference image.
    /// </summary>
    /// <param name="image">The image to use.</param>
    /// <param name="settings">Quantize settings.</param>
    /// <exception cref="MagickException"/>
    public void Map(MagickImage image, QuantizeSettings settings)
    {
      ThrowIfEmpty();

      Throw.IfNull(nameof(image), image);
      Throw.IfNull(nameof(settings), settings);

      try
      {
        AttachImages();
        _NativeInstance.Map(_Images[0], settings, image);
      }
      finally
      {
        DetachImages();
      }
    }

    /// <summary>
    /// Merge this collection into a single image.
    /// This is useful for combining Photoshop layers into a single image.
    /// </summary>
    /// <exception cref="MagickException"/>
    public MagickImage Merge()
    {
      ThrowIfEmpty();

      try
      {
        AttachImages();
        IntPtr image = _NativeInstance.Merge(_Images[0], LayerMethod.Merge);
        return MagickImage.Create(image, _Images[0].Settings);
      }
      finally
      {
        DetachImages();
      }
    }

    /// <summary>
    /// Create a composite image by combining the images with the specified settings.
    /// </summary>
    /// <param name="settings">The settings to use.</param>
    /// <exception cref="MagickException"/>
    public MagickImage Montage(MontageSettings settings)
    {
      ThrowIfEmpty();

      Throw.IfNull(nameof(settings), settings);

      IntPtr images;
      try
      {
        AttachImages();
        if (!string.IsNullOrEmpty(settings.Label))
          _Images[0].Label = settings.Label;
        images = _NativeInstance.Montage(_Images[0], settings);
      }
      finally
      {
        DetachImages();
      }

      using (MagickImageCollection collection = new MagickImageCollection())
      {
        collection.AddRange(MagickImage.CreateList(images, _Images[0].Settings));
        if (settings.TransparentColor != null)
        {
          foreach (MagickImage image in collection)
          {
            image.Transparent(settings.TransparentColor);
          }
        }

        return collection.Merge();
      }
    }

    /// <summary>
    /// The Morph method requires a minimum of two images. The first image is transformed into
    /// the second by a number of intervening images as specified by frames.
    /// </summary>
    /// <param name="frames">The number of in-between images to generate.</param>
    /// <exception cref="MagickException"/>
    public void Morph(int frames)
    {
      ThrowIfCountLowerThan(2);

      MagickSettings settings = _Images[0].Settings.Clone();

      IntPtr images;
      try
      {
        AttachImages();
        images = _NativeInstance.Morph(_Images[0], frames);
      }
      finally
      {
        DetachImages();
      }

      Clear();
      foreach (MagickImage image in MagickImage.CreateList(images, settings))
        Add(image);
    }

    /// <summary>
    /// Inlay the images to form a single coherent picture.
    /// </summary>
    /// <exception cref="MagickException"/>
    public MagickImage Mosaic()
    {
      ThrowIfEmpty();

      try
      {
        AttachImages();
        IntPtr image = _NativeInstance.Merge(_Images[0], LayerMethod.Mosaic);
        return MagickImage.Create(image, _Images[0].Settings);
      }
      finally
      {
        DetachImages();
      }
    }

    /// <summary>
    /// Compares each image the GIF disposed forms of the previous image in the sequence. From
    /// this it attempts to select the smallest cropped image to replace each frame, while
    /// preserving the results of the GIF animation.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void Optimize()
    {
      ThrowIfEmpty();

      MagickSettings settings = _Images[0].Settings.Clone();

      IntPtr images;
      try
      {
        AttachImages();
        images = _NativeInstance.Optimize(_Images[0]);
      }
      finally
      {
        DetachImages();
      }

      Clear();
      foreach (MagickImage image in MagickImage.CreateList(images, settings))
        Add(image);
    }

    /// <summary>
    /// OptimizePlus is exactly as Optimize, but may also add or even remove extra frames in the
    /// animation, if it improves the total number of pixels in the resulting GIF animation.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void OptimizePlus()
    {
      ThrowIfEmpty();

      MagickSettings settings = _Images[0].Settings.Clone();

      IntPtr images;
      try
      {
        AttachImages();
        images = _NativeInstance.OptimizePlus(_Images[0]);
      }
      finally
      {
        DetachImages();
      }

      Clear();
      foreach (MagickImage image in MagickImage.CreateList(images, settings))
        Add(image);
    }

    /// <summary>
    /// Compares each image the GIF disposed forms of the previous image in the sequence. Any
    /// pixel that does not change the displayed result is replaced with transparency. 
    /// </summary>
    /// <exception cref="MagickException"/>
    public void OptimizeTransparency()
    {
      ThrowIfEmpty();

      try
      {
        AttachImages();
        _NativeInstance.OptimizeTransparency(_Images[0]);
      }
      finally
      {
        DetachImages();
      }
    }

    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <exception cref="MagickException"/>
    public void Ping(byte[] data)
    {
      Clear();
      AddImages(data, null, true);
    }

    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="file">The file to read the frames from.</param>
    /// <exception cref="MagickException"/>
    public void Ping(FileInfo file)
    {
      Throw.IfNull(nameof(file), file);

      Ping(file.FullName);
    }

    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException"/>
    public void Ping(Stream stream)
    {
      Ping(StreamHelper.ToByteArray(stream));
    }

    /// <summary>
    /// Read only metadata and not the pixel data from all image frames.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException"/>
    public void Ping(string fileName)
    {
      Clear();
      AddImages(fileName, null, true);
    }

    /// <summary>
    /// Quantize images (reduce number of colors).
    /// </summary>
    /// <exception cref="MagickException"/>
    public MagickErrorInfo Quantize()
    {
      return Quantize(new QuantizeSettings());
    }

    /// <summary>
    /// Quantize images (reduce number of colors).
    /// </summary>
    /// <param name="settings">Quantize settings.</param>
    /// <exception cref="MagickException"/>
    public MagickErrorInfo Quantize(QuantizeSettings settings)
    {
      ThrowIfEmpty();

      Throw.IfNull(nameof(settings), settings);

      try
      {
        AttachImages();
        _NativeInstance.Quantize(_Images[0], settings);
      }
      finally
      {
        DetachImages();
      }

      if (settings.MeasureErrors)
        return MagickImage.CreateErrorInfo(_Images[0]);
      else
        return null;
    }

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="file">The file to read the frames from.</param>
    /// <exception cref="MagickException"/>
    public void Read(FileInfo file)
    {
      Throw.IfNull(nameof(file), file);

      Read(file.FullName);
    }

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="file">The file to read the frames from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException"/>
    public void Read(FileInfo file, MagickReadSettings readSettings)
    {
      Throw.IfNull(nameof(file), file);

      Read(file.FullName, readSettings);
    }

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <exception cref="MagickException"/>
    public void Read(byte[] data)
    {
      Read(data, null);
    }

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="data">The byte array to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException"/>
    public void Read(byte[] data, MagickReadSettings readSettings)
    {
      Clear();
      AddImages(data, readSettings, false);
    }

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <exception cref="MagickException"/>
    public void Read(Stream stream)
    {
      Read(stream, null);
    }

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="stream">The stream to read the image data from.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException"/>
    public void Read(Stream stream, MagickReadSettings readSettings)
    {
      Read(StreamHelper.ToByteArray(stream), readSettings);
    }

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException"/>
    public void Read(string fileName)
    {
      Read(fileName, null);
    }

    /// <summary>
    /// Read all image frames.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="readSettings">The settings to use when reading the image.</param>
    /// <exception cref="MagickException"/>
    public void Read(string fileName, MagickReadSettings readSettings)
    {
      Clear();
      AddImages(fileName, readSettings, false);
    }

    /// <summary>
    /// Removes the first occurrence of the specified image from the collection.
    /// </summary>
    /// <param name="item">The image to remove.</param>
    public bool Remove(MagickImage item)
    {
      return _Images.Remove(item);
    }

    /// <summary>
    /// Removes the image at the specified index from the collection.
    /// </summary>
    /// <param name="index">The index of the image to remove.</param>
    public void RemoveAt(int index)
    {
      _Images.RemoveAt(index);
    }

    /// <summary>
    /// Resets the page property of every image in the collection.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void RePage()
    {
      foreach (MagickImage image in _Images)
      {
        image.RePage();
      }
    }

    /// <summary>
    /// Reverses the order of the images in the collection.
    /// </summary>
    public void Reverse()
    {
      _Images.Reverse();
    }

    /// <summary>
    /// Smush images from list into single image in horizontal direction.
    /// </summary>
    /// <param name="offset">Minimum distance in pixels between images.</param>
    /// <exception cref="MagickException"/>
    public MagickImage SmushHorizontal(int offset)
    {
      ThrowIfEmpty();

      try
      {
        AttachImages();
        IntPtr image = _NativeInstance.Smush(_Images[0], offset, false);
        return MagickImage.Create(image, _Images[0].Settings);
      }
      finally
      {
        DetachImages();
      }
    }

    /// <summary>
    /// Smush images from list into single image in vertical direction.
    /// </summary>
    /// <param name="offset">Minimum distance in pixels between images.</param>
    /// <exception cref="MagickException"/>
    public MagickImage SmushVertical(int offset)
    {
      ThrowIfEmpty();

      try
      {
        AttachImages();
        IntPtr image = _NativeInstance.Smush(_Images[0], offset, true);
        return MagickImage.Create(image, _Images[0].Settings);
      }
      finally
      {
        DetachImages();
      }
    }

    /// <summary>
    /// Converts this instance to a byte array.
    /// </summary>
    public byte[] ToByteArray()
    {
      using (MemoryStream memStream = new MemoryStream())
      {
        Write(memStream);
        return memStream.ToArray();
      }
    }

    /// <summary>
    /// Converts this instance to a byte array.
    /// </summary>
    /// <param name="format">The format to use.</param>
    public byte[] ToByteArray(MagickFormat format)
    {
      SetFormat(format);
      return ToByteArray();
    }

    /// <summary>
    /// Converts this instance to a base64 string.
    /// </summary>
    public string ToBase64()
    {
      byte[] bytes = ToByteArray();
      if (bytes == null)
        return "";

      return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// Converts this instance to a base64 string.
    /// </summary>
    /// <param name="format">The format to use.</param>
    public string ToBase64(MagickFormat format)
    {
      byte[] bytes = ToByteArray(format);
      if (bytes == null)
        return "";

      return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// Merge this collection into a single image.
    /// This is useful for combining Photoshop layers into a single image.
    /// </summary>
    /// <exception cref="MagickException"/>
    public void TrimBounds()
    {
      ThrowIfEmpty();

      try
      {
        AttachImages();
        _NativeInstance.Merge(_Images[0], LayerMethod.Trimbounds);
      }
      finally
      {
        DetachImages();
      }
    }

    /// <summary>
    /// Writes the images to the specified file. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <exception cref="MagickException"/>
    public void Write(FileInfo file)
    {
      Throw.IfNull(nameof(file), file);

      Write(file.FullName);
      file.Refresh();
    }

    /// <summary>
    /// Writes the images to the specified file. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="file">The file to write the image to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException"/>
    public void Write(FileInfo file, IWriteDefines defines)
    {
      SetDefines(defines);
      Write(file);
    }

    /// <summary>
    /// Writes the imagse to the specified stream. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="stream">The stream to write the images to.</param>
    /// <exception cref="MagickException"/>
    public void Write(Stream stream)
    {
      Throw.IfNull(nameof(stream), stream);

      if (_Images.Count == 0)
        return;

      MagickSettings settings = _Images[0].Settings.Clone();
      settings.FileName = null;

      try
      {
        AttachImages();

        UIntPtr length;
        IntPtr data = _NativeInstance.WriteBlob(_Images[0], settings, out length);
        MagickMemory.WriteBytes(data, length, stream);
      }
      finally
      {
        DetachImages();
      }
    }

    /// <summary>
    /// Writes the imagse to the specified stream. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="stream">The stream to write the images to.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException"/>
    public void Write(Stream stream, IWriteDefines defines)
    {
      SetDefines(defines);
      SetFormat(defines.Format);
      Write(stream);
    }

    /// <summary>
    /// Writes the image to the specified stream.
    /// </summary>
    /// <param name="stream">The stream to write the image data to.</param>
    /// <param name="format">The format to use.</param>
    /// <exception cref="MagickException"/>
    public void Write(Stream stream, MagickFormat format)
    {
      SetFormat(format);
      Write(stream);
    }

    /// <summary>
    /// Writes the images to the specified file name. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <exception cref="MagickException"/>
    public void Write(string fileName)
    {
      string filePath = FileHelper.CheckForBaseDirectory(fileName);

      if (_Images.Count == 0)
        return;

      MagickSettings settings = _Images[0].Settings.Clone();
      settings.FileName = fileName;

      try
      {
        AttachImages();
        _NativeInstance.WriteFile(_Images[0], settings);
      }
      finally
      {
        DetachImages();
      }
    }

    /// <summary>
    /// Writes the images to the specified file name. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    /// </summary>
    /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    /// <param name="defines">The defines to set.</param>
    /// <exception cref="MagickException"/>
    public void Write(string fileName, IWriteDefines defines)
    {
      SetDefines(defines);
      Write(fileName);
    }
  }
}