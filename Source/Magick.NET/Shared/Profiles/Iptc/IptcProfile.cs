//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace ImageMagick
{
    /// <summary>
    /// Class that can be used to access an Iptc profile.
    /// </summary>
    public sealed class IptcProfile : ImageProfile
    {
        private Collection<IptcValue> _Values;

        private void Initialize()
        {
            if (_Values != null)
                return;

            _Values = new Collection<IptcValue>();

            if (Data == null || Data[0] != 0x1c)
                return;

            int i = 0;
            while (i + 4 < Data.Length)
            {
                if (Data[i++] != 28)
                    continue;

                i++;

                IptcTag tag = (IptcTag)Data[i++];

                short count = ByteConverter.ToShort(Data, ref i);

                byte[] data = new byte[count];
                if ((count > 0) && (i + count <= Data.Length))
                    Buffer.BlockCopy(Data, i, data, 0, count);
                _Values.Add(new IptcValue(tag, data));

                i += count;
            }
        }

        /// <summary>
        /// Updates the data of the profile.
        /// </summary>
        protected override void UpdateData()
        {
            int length = 0;
            foreach (IptcValue value in Values)
            {
                length += value.Length + 5;
            }

            Data = new byte[length];

            int i = 0;
            foreach (IptcValue value in Values)
            {
                Data[i++] = 28;
                Data[i++] = 2;
                Data[i++] = (byte)value.Tag;
                Data[i++] = (byte)(value.Length >> 8);
                Data[i++] = (byte)value.Length;
                if (value.Length > 0)
                {
                    Buffer.BlockCopy(value.ToByteArray(), 0, Data, i, value.Length);
                    i += value.Length;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IptcProfile"/> class.
        /// </summary>
        public IptcProfile()
          : base("iptc")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IptcProfile"/> class.
        /// </summary>
        /// <param name="data">The byte array to read the iptc profile from.</param>
        public IptcProfile(byte[] data)
          : base("iptc", data)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IptcProfile"/> class.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the iptc profile file, or the relative
        /// iptc profile file name.</param>
        public IptcProfile(string fileName)
          : base("iptc", fileName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IptcProfile"/> class.
        /// </summary>
        /// <param name="stream">The stream to read the iptc profile from.</param>
        public IptcProfile(Stream stream)
          : base("iptc", stream)
        {
        }

        /// <summary>
        /// Gets the values of this iptc profile.
        /// </summary>
        public IEnumerable<IptcValue> Values
        {
            get
            {
                Initialize();
                return _Values;
            }
        }

        /// <summary>
        /// Returns the value with the specified tag.
        /// </summary>
        /// <param name="tag">The tag of the iptc value.</param>
        /// <returns>The value with the specified tag.</returns>
        public IptcValue GetValue(IptcTag tag)
        {
            foreach (IptcValue iptcValue in Values)
            {
                if (iptcValue.Tag == tag)
                    return iptcValue;
            }

            return null;
        }

        /// <summary>
        /// Removes the value with the specified tag.
        /// </summary>
        /// <param name="tag">The tag of the iptc value.</param>
        /// <returns>True when the value was fount and removed.</returns>
        public bool RemoveValue(IptcTag tag)
        {
            Initialize();

            for (int i = 0; i < _Values.Count; i++)
            {
                if (_Values[i].Tag == tag)
                {
                    _Values.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Changes the encoding for all the values,
        /// </summary>
        /// <param name="encoding">The encoding to use when storing the bytes.</param>
        public void SetEncoding(Encoding encoding)
        {
            Throw.IfNull(nameof(encoding), encoding);

            foreach (IptcValue value in Values)
            {
                value.Encoding = encoding;
            }
        }

        /// <summary>
        /// Sets the value of the specified tag.
        /// </summary>
        /// <param name="tag">The tag of the iptc value.</param>
        /// <param name="encoding">The encoding to use when storing the bytes.</param>
        /// <param name="value">The value.</param>
        public void SetValue(IptcTag tag, Encoding encoding, string value)
        {
            Throw.IfNull(nameof(encoding), encoding);

            foreach (IptcValue iptcValue in Values)
            {
                if (iptcValue.Tag == tag)
                {
                    iptcValue.Encoding = encoding;
                    iptcValue.Value = value;
                    return;
                }
            }

            _Values.Add(new IptcValue(tag, encoding, value));
        }

        /// <summary>
        /// Sets the value of the specified tag.
        /// </summary>
        /// <param name="tag">The tag of the iptc value.</param>
        /// <param name="value">The value.</param>
        public void SetValue(IptcTag tag, string value)
        {
            SetValue(tag, Encoding.UTF8, value);
        }
    }
}