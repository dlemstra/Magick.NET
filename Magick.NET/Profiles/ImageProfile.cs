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
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ImageMagick
{
  ///<summary>
  /// Class that contains an image profile.
  ///</summary>
  public class ImageProfile : IEquatable<ImageProfile>
  {
    private static byte[] Copy(byte[] data)
    {
      Throw.IfNullOrEmpty("data", data);

      byte[] result = new byte[data.Length];
      data.CopyTo(result, 0);
      return result;
    }

    ///<summary>
    /// The data of this profile
    ///</summary>
    [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
    protected byte[] Data
    {
      get;
      set;
    }

    ///<summary>
    /// Initializes a new instance of the ImageProfile class.
    ///</summary>
    ///<param name="name">The name of the profile.</param>
    protected ImageProfile(string name)
    {
      Throw.IfNullOrEmpty("name", name);
      Name = name;
    }

    /// <summary>
    /// Updates the data of the profile.
    /// </summary>
    protected virtual void UpdateData()
    {
    }

    ///<summary>
    /// Initializes a new instance of the ImageProfile class.
    ///</summary>
    ///<param name="name">The name of the profile.</param>
    ///<param name="data">A byte array containing the profile.</param>
    public ImageProfile(string name, byte[] data)
    {
      Throw.IfNullOrEmpty("name", name);
      Throw.IfNull("data", data);

      Name = name;
      Data = Copy(data);
    }

    ///<summary>
    /// Initializes a new instance of the ImageProfile class.
    ///</summary>
    ///<param name="name">The name of the profile.</param>
    ///<param name="stream">A stream containing the profile.</param>
    public ImageProfile(string name, Stream stream)
    {
      Throw.IfNullOrEmpty("name", name);

      Name = name;
      Data = Wrapper.MagickReader.Read(stream);
    }

    ///<summary>
    /// Initializes a new instance of the ImageProfile class.
    ///</summary>
    ///<param name="name">The name of the profile.</param>
    ///<param name="fileName">The fully qualified name of the profile file, or the relative profile file name.</param>
    public ImageProfile(string name, string fileName)
    {
      Throw.IfNullOrEmpty("name", name);

      Name = name;

      string filePath = FileHelper.CheckForBaseDirectory(fileName);
      Data = Wrapper.MagickReader.Read(filePath);
    }

    ///<summary>
    /// The name of the profile.
    ///</summary>
    public string Name
    {
      get;
      private set;
    }

    /// <summary>
    /// Determines whether the specified ImageProfile instances are considered equal.
    /// </summary>
    /// <param name="left">The first ImageProfile to compare.</param>
    /// <param name="right"> The second ImageProfile to compare.</param>
    /// <returns></returns>
    public static bool operator ==(ImageProfile left, ImageProfile right)
    {
      return object.Equals(left, right);
    }

    /// <summary>
    /// Determines whether the specified ImageProfile instances are not considered equal.
    /// </summary>
    /// <param name="left">The first ImageProfile to compare.</param>
    /// <param name="right"> The second ImageProfile to compare.</param>
    /// <returns></returns>
    public static bool operator !=(ImageProfile left, ImageProfile right)
    {
      return !object.Equals(left, right);
    }

    ///<summary>
    /// Determines whether the specified object is equal to the current image profile.
    ///</summary>
    ///<param name="obj">The object to compare this image profile with.</param>
    public override bool Equals(object obj)
    {

      if (ReferenceEquals(this, obj))
        return true;

      return Equals(obj as ImageProfile);
    }

    ///<summary>
    /// Determines whether the specified image compare is equal to the current image profile.
    ///</summary>
    ///<param name="other">The image profile to compare this image profile with.</param>
    public bool Equals(ImageProfile other)
    {
      if (ReferenceEquals(other, null))
        return false;

      if (ReferenceEquals(this, other))
        return true;

      if (Name != other.Name)
        return false;

      UpdateData();

      if (ReferenceEquals(Data, null))
        return ReferenceEquals(other.Data, null);

      if (ReferenceEquals(other.Data, null))
        return false;

      if (Data.Length != other.Data.Length)
        return false;

      for (int i = 0; i < Data.Length; i++)
      {
        if (Data[i] != other.Data[i])
          return false;
      }

      return true;
    }

    ///<summary>
    /// Serves as a hash of this type.
    ///</summary>
    public override int GetHashCode()
    {
      return
        Data.GetHashCode() ^
        Name.GetHashCode();
    }

    ///<summary>
    /// Converts this instance to a byte array.
    ///</summary>

    public Byte[] ToByteArray()
    {
      UpdateData();
      return Copy(Data);
    }
  }
}