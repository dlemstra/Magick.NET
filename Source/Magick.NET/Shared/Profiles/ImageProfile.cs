﻿// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ImageMagick
{
    /// <summary>
    /// Class that contains an image profile.
    /// </summary>
    public class ImageProfile : IEquatable<ImageProfile>
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

            Bytes bytes = new Bytes(stream);
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

            string filePath = FileHelper.CheckForBaseDirectory(fileName);
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
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the data of this profile.
        /// </summary>
        [SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Subclasses need access to this array.")]
        protected byte[] Data
        {
            get;
            set;
        }

        /// <summary>
        /// Determines whether the specified <see cref="ImageProfile"/> instances are considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="ImageProfile"/> to compare.</param>
        /// <param name="right"> The second <see cref="ImageProfile"/> to compare.</param>
        public static bool operator ==(ImageProfile left, ImageProfile right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified <see cref="ImageProfile"/> instances are not considered equal.
        /// </summary>
        /// <param name="left">The first <see cref="ImageProfile"/> to compare.</param>
        /// <param name="right"> The second <see cref="ImageProfile"/> to compare.</param>
        public static bool operator !=(ImageProfile left, ImageProfile right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current <see cref="ImageProfile"/>.
        /// </summary>
        /// <param name="obj">The object to compare this <see cref="ImageProfile"/> with.</param>
        /// <returns>True when the specified object is equal to the current <see cref="ImageProfile"/>.</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj))
                return true;

            return Equals(obj as ImageProfile);
        }

        /// <summary>
        /// Determines whether the specified image compare is equal to the current <see cref="ImageProfile"/>.
        /// </summary>
        /// <param name="other">The image profile to compare this <see cref="ImageProfile"/> with.</param>
        /// <returns>True when the specified image compare is equal to the current <see cref="ImageProfile"/>.</returns>
        public bool Equals(ImageProfile other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (Name != other.Name)
                return false;

            UpdateData();

            if (Data is null)
                return other.Data is null;

            if (other.Data is null)
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

        /// <summary>
        /// Serves as a hash of this type.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            return
              Data.GetHashCode() ^
              Name.GetHashCode();
        }

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

            byte[] result = new byte[data.Length];
            data.CopyTo(result, 0);
            return result;
        }
    }
}
