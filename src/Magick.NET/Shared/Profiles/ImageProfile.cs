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

using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ImageMagick
{
    /// <summary>
    /// Class that contains an image profile.
    /// </summary>
    public class ImageProfile : IImageProfile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageProfile"/> class.
        /// </summary>
        /// <param name="name">The name of the profile.</param>
        /// <param name="data">A byte array containing the profile.</param>
        public ImageProfile(string name, byte[] data)
        {
            Throw.IfNullOrEmpty(nameof(name), name);
            Throw.IfNull(nameof(data), data);

            Name = name;
            Data = Copy(data);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageProfile"/> class.
        /// </summary>
        /// <param name="name">The name of the profile.</param>
        /// <param name="stream">A stream containing the profile.</param>
        public ImageProfile(string name, Stream stream)
        {
            Throw.IfNullOrEmpty(nameof(name), name);

            Name = name;

            var bytes = new Bytes(stream);
            Data = bytes.Data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageProfile"/> class.
        /// </summary>
        /// <param name="name">The name of the profile.</param>
        /// <param name="fileName">The fully qualified name of the profile file, or the relative profile file name.</param>
        public ImageProfile(string name, string fileName)
        {
            Throw.IfNullOrEmpty(nameof(name), name);
            Throw.IfNullOrEmpty(nameof(fileName), fileName);

            Name = name;

            var filePath = FileHelper.CheckForBaseDirectory(fileName);
            Data = File.ReadAllBytes(filePath);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageProfile"/> class.
        /// </summary>
        /// <param name="name">The name of the profile.</param>
        protected ImageProfile(string name)
        {
            Throw.IfNullOrEmpty(nameof(name), name);
            Name = name;
        }

        /// <summary>
        /// Gets the name of the profile.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the data of this profile.
        /// </summary>
        [SuppressMessage("Performance", "CA1819:Properties should not return arrays", Justification = "The property needs to be an array.")]
        protected byte[] Data { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="ImageProfile"/>.
        /// </summary>
        /// <param name="obj">The object to compare this <see cref="ImageProfile"/> with.</param>
        /// <returns>True when the specified object is equal to the current <see cref="ImageProfile"/>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return Equals(obj as IImageProfile);
        }

        /// <summary>
        /// Determines whether the specified image compare is equal to the current <see cref="ImageProfile"/>.
        /// </summary>
        /// <param name="other">The image profile to compare this <see cref="ImageProfile"/> with.</param>
        /// <returns>True when the specified image compare is equal to the current <see cref="ImageProfile"/>.</returns>
        public bool Equals(IImageProfile other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (Name != other.Name)
                return false;

            UpdateData();

            var data = other.ToByteArray();
            if (data.Length == 0)
                return Data is null || Data.Length == 0;

            if (Data.Length != data.Length)
                return false;

            for (int i = 0; i < Data.Length; i++)
            {
                if (Data[i] != data[i])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Serves as a hash of this type.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode() => Data.GetHashCode() ^ Name.GetHashCode();

        /// <summary>
        /// Converts this instance to a byte array.
        /// </summary>
        /// <returns>A <see cref="byte"/> array.</returns>
        public byte[] ToByteArray()
        {
            UpdateData();
            return Copy(Data);
        }

        /// <summary>
        /// Updates the data of the profile.
        /// </summary>
        protected virtual void UpdateData()
        {
        }

        private static byte[] Copy(byte[] data)
        {
            if (data == null || data.Length == 0)
                return new byte[0];

            var result = new byte[data.Length];
            data.CopyTo(result, 0);
            return result;
        }
    }
}
