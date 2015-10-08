//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageMagick
{
  ///<summary>
  /// Represents the collection of images.
  ///</summary>
  public sealed class MagickImageCollection : IDisposable, IList<MagickImage>
  {
    private List<MagickImage> _Images;
    private Wrapper.MagickImageCollection _Instance;
    private EventHandler<WarningEventArgs> _WarningEvent;

    private IEnumerable<Wrapper.MagickImage> GetImageInstances()
    {
      foreach (MagickImage image in _Images)
      {
        yield return MagickImage.GetInstance(image);
      }
    }

    private void SetFormat(ImageFormat format)
    {
      SetFormat(MagickImage.GetFormat(format));
    }

    private void SetFormat(MagickFormat format)
    {
      foreach (MagickImage image in _Images)
      {
        image.Format = format;
      }
    }

    private void OnWarning(object sender, WarningEventArgs arguments)
    {
      if (_WarningEvent != null)
        _WarningEvent(this, arguments);
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
      Clear();
    }

    ///<summary>
    /// Initializes a new instance of the MagickImage class.
    ///</summary>
    public MagickImageCollection()
    {
      _Instance = new Wrapper.MagickImageCollection();
      _Images = new List<MagickImage>();
    }

    ///<summary>
    /// Initializes a new instance of the MagickImageCollection class using the specified byte array.
    ///</summary>
    ///<param name="data">The byte array to read the image data from.</param>
    ///<exception cref="MagickException"/>
    public MagickImageCollection(byte[] data)
      : this()
    {
      Read(data);
    }

    ///<summary>
    /// Initializes a new instance of the MagickImageCollection class using the specified byte array.
    ///</summary>
    ///<param name="data">The byte array to read the image data from.</param>
    ///<param name="readSettings">The settings to use when reading the image.</param>
    ///<exception cref="MagickException"/>
    public MagickImageCollection(byte[] data, MagickReadSettings readSettings)
      : this()
    {
      Read(data, readSettings);
    }

    ///<summary>
    /// Initializes a new instance of the MagickImage class using the specified images.
    ///</summary>
    ///<param name="images">The images to add to the collection.</param>
    ///<exception cref="MagickException"/>
    public MagickImageCollection(IEnumerable<MagickImage> images)
      : this()
    {
      Throw.IfNull("images", images);

      foreach (MagickImage image in images)
      {
        Add(image);
      }
    }

    ///<summary>
    /// Initializes a new instance of the MagickImage class using the specified file.
    ///</summary>
    ///<param name="file">The file to read the image from.</param>
    ///<exception cref="MagickException"/>
    public MagickImageCollection(FileInfo file)
      : this()
    {
      Read(file);
    }

    ///<summary>
    /// Initializes a new instance of the MagickImage class using the specified file.
    ///</summary>
    ///<param name="file">The file to read the image from.</param>
    ///<param name="readSettings">The settings to use when reading the image.</param>
    ///<exception cref="MagickException"/>
    public MagickImageCollection(FileInfo file, MagickReadSettings readSettings)
      : this()
    {
      Read(file, readSettings);
    }

    ///<summary>
    /// Initializes a new instance of the MagickImageCollection class using the specified stream.
    ///</summary>
    ///<param name="stream">The stream to read the image data from.</param>
    ///<exception cref="MagickException"/>
    public MagickImageCollection(Stream stream)
      : this()
    {
      Read(stream);
    }

    ///<summary>
    /// Initializes a new instance of the MagickImageCollection class using the specified stream.
    ///</summary>
    ///<param name="stream">The stream to read the image data from.</param>
    ///<param name="readSettings">The settings to use when reading the image.</param>
    ///<exception cref="MagickException"/>
    public MagickImageCollection(Stream stream, MagickReadSettings readSettings)
      : this()
    {
      Read(stream, readSettings);
    }

    ///<summary>
    /// Initializes a new instance of the MagickImageCollection class using the specified filename.
    ///</summary>
    ///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    ///<exception cref="MagickException"/>
    public MagickImageCollection(string fileName)
      : this()
    {
      Read(fileName);
    }

    ///<summary>
    /// Initializes a new instance of the MagickImageCollection class using the specified filename
    ///</summary>
    ///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    ///<param name="readSettings">The settings to use when reading the image.</param>
    ///<exception cref="MagickException"/>
    public MagickImageCollection(string fileName, MagickReadSettings readSettings)
      : this()
    {
      Read(fileName, readSettings);
    }

    ///<summary>
    /// Converts this instance to a byte array.
    ///</summary>
    public static explicit operator byte[] (MagickImageCollection collection)
    {
      Throw.IfNull("collection", collection);

      return collection.ToByteArray();
    }

    ///<summary>
    /// Event that will we raised when a warning is thrown by ImageMagick.
    ///</summary>
    public event EventHandler<WarningEventArgs> Warning
    {
      add
      {
        _Instance.Warning += OnWarning;
        _WarningEvent += value;
      }
      remove
      {
        _Instance.Warning -= OnWarning;
        _WarningEvent -= value;
      }
    }

    ///<summary>
    /// Gets or sets the image at the specified index.
    ///</summary>
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

    ///<summary>
    /// Returns the number of images in the collection.
    ///</summary>
    public int Count
    {
      get
      {
        return _Images.Count;
      }
    }

    ///<summary>
    ///Gets a value indicating whether the collection is read-only.
    ///</summary>
    public bool IsReadOnly
    {
      get
      {
        return false;
      }
    }

    ///<summary>
    /// Adds an image to the collection.
    ///</summary>
    ///<param name="item">The image to add.</param>
    public void Add(MagickImage item)
    {
      _Images.Add(item);
    }

    ///<summary>
    /// Adds an image with the specified file name to the collection.
    ///</summary>
    ///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    ///<exception cref="MagickException"/>
    public void Add(string fileName)
    {
      _Images.Add(new MagickImage(fileName));
    }

    ///<summary>
    /// Adds the image(s) from the specified byte array to the collection.
    ///</summary>
    ///<param name="data">The byte array to read the image data from.</param>
    ///<exception cref="MagickException"/>
    public void AddRange(byte[] data)
    {
      AddRange(data, null);
    }

    ///<summary>
    /// Adds the image(s) from the specified byte array to the collection.
    ///</summary>
    ///<param name="data">The byte array to read the image data from.</param>
    ///<param name="readSettings">The settings to use when reading the image.</param>
    ///<exception cref="MagickException"/>
    public void AddRange(byte[] data, MagickReadSettings readSettings)
    {
      foreach (Wrapper.MagickImage image in _Instance.Read(data, readSettings))
      {
        _Images.Add(MagickImage.Create(image));
      }
    }

    ///<summary>
    /// Adds a the specified images to this collection.
    ///</summary>
    ///<param name="images">The images to add to the collection.</param>
    ///<exception cref="MagickException"/>
    public void AddRange(IEnumerable<MagickImage> images)
    {
      Throw.IfNull("images", images);

      foreach (MagickImage image in images)
      {
        Add(image);
      }
    }

    ///<summary>
    /// Adds a Clone of the images from the specified collection to this collection.
    ///</summary>
    ///<param name="images">A collection of MagickImages.</param>
    ///<exception cref="MagickException"/>
    public void AddRange(MagickImageCollection images)
    {
      Throw.IfNull("images", images);

      int count = images.Count;
      for (int i = 0; i < count; i++)
      {
        Add(images[i].Clone());
      }
    }

    ///<summary>
    /// Adds the image(s) from the specified file name to the collection.
    ///</summary>
    ///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    ///<exception cref="MagickException"/>
    public void AddRange(string fileName)
    {
      AddRange(fileName, null);
    }

    ///<summary>
    /// Adds the image(s) from the specified file name to the collection.
    ///</summary>
    ///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    ///<param name="readSettings">The settings to use when reading the image.</param>
    ///<exception cref="MagickException"/>
    public void AddRange(string fileName, MagickReadSettings readSettings)
    {
      foreach (Wrapper.MagickImage image in _Instance.Read(fileName, readSettings))
      {
        _Images.Add(MagickImage.Create(image));
      }
    }

    ///<summary>
    /// Adds the image(s) from the specified stream to the collection.
    ///</summary>
    ///<param name="stream">The stream to read the images from.</param>
    ///<exception cref="MagickException"/>
    public void AddRange(Stream stream)
    {
      AddRange(stream, null);
    }

    ///<summary>
    /// Adds the image(s) from the specified stream to the collection.
    ///</summary>
    ///<param name="stream">The stream to read the images from.</param>
    ///<param name="readSettings">The settings to use when reading the image.</param>
    ///<exception cref="MagickException"/>
    public void AddRange(Stream stream, MagickReadSettings readSettings)
    {
      foreach (Wrapper.MagickImage image in _Instance.Read(stream, readSettings))
      {
        _Images.Add(MagickImage.Create(image));
      }
    }

    ///<summary>
    /// Creates a single image, by appending all the images in the collection horizontally.
    ///</summary>
    ///<exception cref="MagickException"/>
    public MagickImage AppendHorizontally()
    {
      Wrapper.MagickImage image = _Instance.Append(GetImageInstances(), false);
      return MagickImage.Create(image);
    }

    ///<summary>
    /// Creates a single image, by appending all the images in the collection vertically.
    ///</summary>
    ///<exception cref="MagickException"/>
    public MagickImage AppendVertically()
    {
      Wrapper.MagickImage image = _Instance.Append(GetImageInstances(), true);
      return MagickImage.Create(image);
    }

    ///<summary>
    /// Merge a sequence of images. This is useful for GIF animation sequences that have page
    /// offsets and disposal methods
    ///</summary>
    ///<exception cref="MagickException"/>
    public void Coalesce()
    {
      List<Wrapper.MagickImage> images = new List<Wrapper.MagickImage>(_Instance.Coalesce(GetImageInstances()));

      Clear();
      foreach (Wrapper.MagickImage image in images)
      {
        _Images.Add(MagickImage.Create(image));
      }
    }

    ///<summary>
    /// Determines whether the collection contains the specified image.
    ///</summary>
    ///<param name="item">The image to check.</param>
    public bool Contains(MagickImage item)
    {
      return _Images.Contains(item);
    }

    ///<summary>
    /// Removes all images from the collection.
    ///</summary>
    public void Clear()
    {
      foreach (MagickImage image in _Images)
      {
        if (image != null)
          image.Dispose();
      }

      _Images.Clear();
    }

    ///<summary>
    /// Combines one or more images into a single image. The typical ordering would be
    /// image 1 => Red, 2 => Green, 3 => Blue, etc.
    ///</summary>
    public MagickImage Combine()
    {
      return Combine(Channels.All);
    }

    ///<summary>
    /// Combines one or more images into a single image. The grayscale value of the pixels of each
    /// image in the sequence is assigned in order to the specified channels of the combined image.
    /// The typical ordering would be image 1 => Red, 2 => Green, 3 => Blue, etc.
    ///</summary>
    ///<param name="channels">The channel(s) to combine.</param>
    ///<exception cref="MagickException"/>
    public MagickImage Combine(Channels channels)
    {
      Wrapper.MagickImage image = _Instance.Combine(GetImageInstances(), channels);
      return MagickImage.Create(image);
    }

    ///<summary>
    /// Copies the images to an Array, starting at a particular Array index.
    ///</summary>
    ///<param name="array">The one-dimensional Array that is the destination.</param>
    ///<param name="arrayIndex">The zero-based index in 'destination' at which copying begins.</param>
    public void CopyTo(MagickImage[] array, int arrayIndex)
    {
      if (Count == 0)
        return;

      Throw.IfNull("array", array);
      Throw.IfOutOfRange("arrayIndex", arrayIndex, _Images.Count);
      Throw.IfOutOfRange("arrayIndex", arrayIndex, array.Length);

      int indexI = 0;
      int length = Math.Min(array.Length, _Images.Count);
      for (int indexA = arrayIndex; indexA < length; indexA++)
      {
        array[indexA] = _Images[indexI++].Clone();
      }
    }

    ///<summary>
    /// Break down an image sequence into constituent parts. This is useful for creating GIF or
    /// MNG animation sequences.
    ///</summary>
    ///<exception cref="MagickException"/>
    public void Deconstruct()
    {
      List<Wrapper.MagickImage> images = new List<Wrapper.MagickImage>(_Instance.Deconstruct(GetImageInstances()));

      Clear();
      foreach (Wrapper.MagickImage image in images)
      {
        _Images.Add(MagickImage.Create(image));
      }
    }

    /// <summary>
    /// Disposes the MagickImageCollection instance.
    /// </summary>
    public void Dispose()
    {
      Clear();
      GC.SuppressFinalize(this);
    }

    ///<summary>
    /// Evaluate image pixels into a single image. All the images in the collection must be the
    /// same size in pixels.
    ///</summary>
    ///<param name="evaluateOperator">The operator.</param>
    ///<exception cref="MagickException"/>
    public MagickImage Evaluate(EvaluateOperator evaluateOperator)
    {
      Wrapper.MagickImage image = _Instance.Evaluate(GetImageInstances(), evaluateOperator);
      return MagickImage.Create(image);
    }

    ///<summary>
    /// Flatten this collection into a single image.
    /// This is useful for combining Photoshop layers into a single image.
    ///</summary>
    ///<exception cref="MagickException"/>
    public MagickImage Flatten()
    {
      Wrapper.MagickImage image = _Instance.Flatten(GetImageInstances());
      return MagickImage.Create(image);
    }

    ///<summary>
    /// Applies a mathematical expression to the images.
    ///</summary>
    ///<param name="expression">The expression to apply.</param>
    ///<exception cref="MagickException"/>
    public MagickImage Fx(string expression)
    {
      Wrapper.MagickImage image = _Instance.Fx(GetImageInstances(), expression);
      return MagickImage.Create(image);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the images.
    /// </summary>
    /// <returns></returns>
    public IEnumerator<MagickImage> GetEnumerator()
    {
      return _Images.GetEnumerator();
    }

    ///<summary>
    /// Determines the index of the specified image.
    ///</summary>
    ///<param name="item">The image to check.</param>
    public int IndexOf(MagickImage item)
    {
      return _Images.IndexOf(item);
    }

    ///<summary>
    /// Inserts an image into the collection.
    ///</summary>
    ///<param name="index">The index to insert the image.</param>
    ///<param name="item">The image to insert.</param>
    public void Insert(int index, MagickImage item)
    {
      _Images.Insert(index, item);
    }

    ///<summary>
    /// Inserts an image with the specified file name into the collection.
    ///</summary>
    ///<param name="index">The index to insert the image.</param>
    ///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    public void Insert(int index, string fileName)
    {
      _Images.Insert(index, new MagickImage(fileName));
    }

    ///<summary>
    /// Remap image colors.
    ///</summary>
    ///<exception cref="MagickException"/>
    public void Map()
    {
      Map(new QuantizeSettings());
    }

    ///<summary>
    /// Remap image colors.
    ///</summary>
    ///<param name="settings">Quantize settings.</param>
    ///<exception cref="MagickException"/>
    public void Map(QuantizeSettings settings)
    {
      List<Wrapper.MagickImage> images = new List<Wrapper.MagickImage>(_Instance.Map(GetImageInstances(), settings));

      Clear();
      foreach (Wrapper.MagickImage image in images)
      {
        _Images.Add(MagickImage.Create(image));
      }
    }

    ///<summary>
    /// Merge this collection into a single image.
    /// This is useful for combining Photoshop layers into a single image.
    ///</summary>
    ///<exception cref="MagickException"/>
    public MagickImage Merge()
    {
      Wrapper.MagickImage image = _Instance.Merge(GetImageInstances());
      return MagickImage.Create(image);
    }

    ///<summary>
    /// The Morph method requires a minimum of two images. The first image is transformed into
    /// the second by a number of intervening images as specified by frames.
    ///</summary>
    ///<param name="frames">The number of in-between images to generate.</param>
    ///<exception cref="MagickException"/>
    public MagickImageCollection Morph(int frames)
    {
      MagickImageCollection collection = new MagickImageCollection();

      foreach (Wrapper.MagickImage image in _Instance.Morph(GetImageInstances(), frames))
      {
        collection.Add(MagickImage.Create(image));
      }

      return collection;
    }

    ///<summary>
    /// Create a composite image by combining the images with the specified settings.
    ///</summary>
    ///<param name="settings">The settings to use.</param>
    ///<exception cref="MagickException"/>
    public MagickImage Montage(MontageSettings settings)
    {
      Wrapper.MagickImage image = _Instance.Montage(GetImageInstances(), MontageSettings.GetInstance(settings));
      return MagickImage.Create(image);
    }

    ///<summary>
    /// Inlay the images to form a single coherent picture.
    ///</summary>
    ///<exception cref="MagickException"/>
    public MagickImage Mosaic()
    {
      Wrapper.MagickImage image = _Instance.Mosaic(GetImageInstances());
      return MagickImage.Create(image);
    }

    ///<summary>
    /// Compares each image the GIF disposed forms of the previous image in the sequence. From
    /// this it attempts to select the smallest cropped image to replace each frame, while
    /// preserving the results of the GIF animation.
    ///</summary>
    ///<exception cref="MagickException"/>
    public void Optimize()
    {
      List<Wrapper.MagickImage> images = new List<Wrapper.MagickImage>(_Instance.Optimize(GetImageInstances()));

      Clear();
      foreach (Wrapper.MagickImage image in images)
      {
        _Images.Add(MagickImage.Create(image));
      }
    }

    ///<summary>
    /// OptimizePlus is exactly as Optimize, but may also add or even remove extra frames in the
    /// animation, if it improves the total number of pixels in the resulting GIF animation.
    ///</summary>
    ///<exception cref="MagickException"/>
    public void OptimizePlus()
    {
      List<Wrapper.MagickImage> images = new List<Wrapper.MagickImage>(_Instance.OptimizePlus(GetImageInstances()));

      Clear();
      foreach (Wrapper.MagickImage image in images)
      {
        _Images.Add(MagickImage.Create(image));
      }
    }

    ///<summary>
    /// Compares each image the GIF disposed forms of the previous image in the sequence. Any
    /// pixel that does not change the displayed result is replaced with transparency. 
    ///</summary>
    ///<exception cref="MagickException"/>
    public void OptimizeTransparency()
    {
      List<Wrapper.MagickImage> images = new List<Wrapper.MagickImage>(_Instance.OptimizeTransparency(GetImageInstances()));

      Clear();
      foreach (Wrapper.MagickImage image in images)
      {
        _Images.Add(MagickImage.Create(image));
      }
    }

    ///<summary>
    /// Quantize images (reduce number of colors).
    ///</summary>
    ///<exception cref="MagickException"/>
    public MagickErrorInfo Quantize()
    {
      return Quantize(new QuantizeSettings());
    }

    ///<summary>
    /// Quantize images (reduce number of colors).
    ///</summary>
    ///<param name="settings">Quantize settings.</param>
    ///<exception cref="MagickException"/>
    public MagickErrorInfo Quantize(QuantizeSettings settings)
    {
      if (Count == 0)
        return null;

      Throw.IfNull("settings", settings);

      using (MagickImage colorMap = AppendHorizontally())
      {
        MagickErrorInfo result = colorMap.Quantize(settings);

        foreach (MagickImage image in _Images)
        {
          image.Map(colorMap);
        }

        return result;
      }
    }

    ///<summary>
    /// Read all image frames.
    ///</summary>
    ///<param name="data">The byte array to read the image data from.</param>
    ///<exception cref="MagickException"/>
    public void Read(byte[] data)
    {
      Read(data, null);
    }

    ///<summary>
    /// Read all image frames.
    ///</summary>
    ///<param name="data">The byte array to read the image data from.</param>
    ///<param name="readSettings">The settings to use when reading the image.</param>
    ///<exception cref="MagickException"/>
    public void Read(byte[] data, MagickReadSettings readSettings)
    {
      Clear();

      foreach (Wrapper.MagickImage image in _Instance.Read(data, readSettings))
      {
        _Images.Add(MagickImage.Create(image));
      }
    }

    ///<summary>
    /// Read all image frames.
    ///</summary>
    ///<param name="file">The file to read the frames from.</param>
    ///<exception cref="MagickException"/>
    public void Read(FileInfo file)
    {
      Throw.IfNull("file", file);
      Read(file.FullName);
    }

    ///<summary>
    /// Read all image frames.
    ///</summary>
    ///<param name="file">The file to read the frames from.</param>
    ///<param name="readSettings">The settings to use when reading the image.</param>
    ///<exception cref="MagickException"/>
    public void Read(FileInfo file, MagickReadSettings readSettings)
    {
      Throw.IfNull("file", file);

      Read(file.FullName, readSettings);
    }

    ///<summary>
    /// Read all image frames.
    ///</summary>
    ///<param name="stream">The stream to read the image data from.</param>
    ///<exception cref="MagickException"/>
    public void Read(Stream stream)
    {
      Read(stream, null);
    }

    ///<summary>
    /// Read all image frames.
    ///</summary>
    ///<param name="stream">The stream to read the image data from.</param>
    ///<param name="readSettings">The settings to use when reading the image.</param>
    ///<exception cref="MagickException"/>
    public void Read(Stream stream, MagickReadSettings readSettings)
    {
      Clear();

      foreach (Wrapper.MagickImage image in _Instance.Read(stream, readSettings))
      {
        _Images.Add(MagickImage.Create(image));
      }
    }

    ///<summary>
    /// Read all image frames.
    ///</summary>
    ///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    ///<exception cref="MagickException"/>
    public void Read(string fileName)
    {
      Read(fileName, null);
    }

    ///<summary>
    /// Read all image frames.
    ///</summary>
    ///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    ///<param name="readSettings">The settings to use when reading the image.</param>
    ///<exception cref="MagickException"/>
    public void Read(string fileName, MagickReadSettings readSettings)
    {
      Clear();

      string filePath = FileHelper.CheckForBaseDirectory(fileName);
      foreach (Wrapper.MagickImage image in _Instance.Read(filePath, readSettings))
      {
        _Images.Add(MagickImage.Create(image));
      }
    }

    ///<summary>
    /// Removes the first occurrence of the specified image from the collection.
    ///</summary>
    ///<param name="item">The image to remove.</param>
    public bool Remove(MagickImage item)
    {
      return _Images.Remove(item);
    }

    ///<summary>
    /// Removes the image at the specified index from the collection.
    ///</summary>
    ///<param name="index">The index of the image to remove.</param>
    public void RemoveAt(int index)
    {
      _Images.RemoveAt(index);
    }

    ///<summary>
    /// Resets the page property of every image in the collection.
    ///</summary>
    ///<exception cref="MagickException"/>
    public void RePage()
    {
      foreach (MagickImage image in _Images)
      {
        image.RePage();
      }
    }

    ///<summary>
    /// Reverses the order of the images in the collection.
    ///</summary>
    public void Reverse()
    {
      _Images.Reverse();
    }

    ///<summary>
    /// Smush images from list into single image in horizontal direction.
    ///</summary>
    ///<param name="offset">Minimum distance in pixels between images.</param>
    ///<exception cref="MagickException"/>
    public MagickImage SmushHorizontal(int offset)
    {
      Wrapper.MagickImage image = _Instance.Smush(GetImageInstances(), offset, false);
      return MagickImage.Create(image);
    }

    ///<summary>
    /// Smush images from list into single image in vertical direction.
    ///</summary>
    ///<param name="offset">Minimum distance in pixels between images.</param>
    ///<exception cref="MagickException"/>
    public MagickImage SmushVertical(int offset)
    {
      Wrapper.MagickImage image = _Instance.Smush(GetImageInstances(), offset, true);
      return MagickImage.Create(image);
    }

    ///<summary>
    /// Converts this instance to a base64 string.
    ///</summary>
    public string ToBase64()
    {
      byte[] bytes = ToByteArray();
      if (bytes == null)
        return "";

      return Convert.ToBase64String(bytes);
    }

    ///<summary>
    /// Converts this instance to a base64 string.
    ///</summary>
    ///<param name="format">The format to use.</param>
    public string ToBase64(MagickFormat format)
    {
      byte[] bytes = ToByteArray(format);
      if (bytes == null)
        return "";

      return Convert.ToBase64String(bytes);
    }

    ///<summary>
    /// Converts the images in this instance to a bitmap using ImageFormat.Tiff.
    ///</summary>
    public Bitmap ToBitmap()
    {
      return ToBitmap(ImageFormat.Tiff);
    }

    ///<summary>
    /// Converts the images in this instance to a bitmap using the specified ImageFormat.
    /// Supported formats are: Gif, Icon, Tiff.
    ///</summary>
    public Bitmap ToBitmap(ImageFormat imageFormat)
    {
      SetFormat(imageFormat);

      MemoryStream memStream = new MemoryStream();
      Write(memStream);
      memStream.Position = 0;
      // Do not dispose the memStream, the bitmap owns it.
      return new Bitmap(memStream);
    }

    ///<summary>
    /// Converts this instance to a byte array.
    ///</summary>
    public byte[] ToByteArray()
    {
      return _Instance.ToByteArray(GetImageInstances());
    }

    ///<summary>
    /// Converts this instance to a byte array.
    ///</summary>
    ///<param name="format">The format to use.</param>
    public Byte[] ToByteArray(MagickFormat format)
    {
      SetFormat(format);
      return ToByteArray();
    }

    ///<summary>
    /// Merge this collection into a single image.
    /// This is useful for combining Photoshop layers into a single image.
    ///</summary>
    ///<exception cref="MagickException"/>
    public MagickImage TrimBounds()
    {
      Wrapper.MagickImage image = _Instance.TrimBounds(GetImageInstances());
      return MagickImage.Create(image);
    }

    ///<summary>
    /// Writes the images to the specified file. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    ///</summary>
    ///<param name="file">The file to write the image to.</param>
    ///<exception cref="MagickException"/>
    public void Write(FileInfo file)
    {
      Throw.IfNull("file", file);

      Write(file.FullName);
      file.Refresh();
    }

    ///<summary>
    /// Writes the imagse to the specified stream. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    ///</summary>
    ///<param name="stream">The stream to write the images to.</param>
    ///<exception cref="MagickException"/>
    public void Write(Stream stream)
    {
      Throw.IfNull("stream", stream);

      _Instance.Write(GetImageInstances(), stream);
    }

    ///<summary>
    /// Writes the image to the specified stream.
    ///</summary>
    ///<param name="stream">The stream to write the image data to.</param>
    ///<param name="format">The format to use.</param>
    ///<exception cref="MagickException"/>
    public void Write(Stream stream, MagickFormat format)
    {
      SetFormat(format);
      Write(stream);
    }

    ///<summary>
    /// Writes the images to the specified file name. If the output image's file format does not
    /// allow multi-image files multiple files will be written.
    ///</summary>
    ///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
    ///<exception cref="MagickException"/>
    public void Write(string fileName)
    {
      string filePath = FileHelper.CheckForBaseDirectory(fileName);
      _Instance.Write(GetImageInstances(), filePath);
    }
  }
}