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
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml;

namespace ImageMagick
{
    /// <summary>
    /// Class that can be used to access an 8bim profile.
    /// </summary>
    public sealed class EightBimProfile : ImageProfile, IEightBimProfile
    {
        private Collection<IClipPath> _clipPaths;
        private int _height;
        private Collection<IEightBimValue> _values;
        private int _width;

        /// <summary>
        /// Initializes a new instance of the <see cref="EightBimProfile"/> class.
        /// </summary>
        /// <param name="data">The byte array to read the 8bim profile from.</param>
        public EightBimProfile(byte[] data)
          : base("8bim", data)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EightBimProfile"/> class.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the 8bim profile file, or the relative
        /// 8bim profile file name.</param>
        public EightBimProfile(string fileName)
          : base("8bim", fileName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EightBimProfile"/> class.
        /// </summary>
        /// <param name="stream">The stream to read the 8bim profile from.</param>
        public EightBimProfile(Stream stream)
          : base("8bim", stream)
        {
        }

        internal EightBimProfile(MagickImage image, byte[] data)
          : base("8bim", data)
        {
            _width = image.Width;
            _height = image.Height;
        }

        /// <summary>
        /// Gets the clipping paths this image contains.
        /// </summary>
        public IEnumerable<IClipPath> ClipPaths
        {
            get
            {
                Initialize();

                return _clipPaths;
            }
        }

        /// <summary>
        /// Gets the values of this 8bim profile.
        /// </summary>
        public IEnumerable<IEightBimValue> Values
        {
            get
            {
                Initialize();

                return _values;
            }
        }

        private ClipPath CreateClipPath(string name, int offset, int length)
        {
            var d = GetClipPath(offset, length);
            if (string.IsNullOrEmpty(d))
                return null;

            var doc = XmlHelper.CreateDocument();
            doc.CreateXmlDeclaration("1.0", "iso-8859-1", null);

            var svg = XmlHelper.CreateElement(doc, "svg");
            XmlHelper.SetAttribute(svg, "width", _width);
            XmlHelper.SetAttribute(svg, "height", _height);

            var g = XmlHelper.CreateElement(svg, "g");

            var path = XmlHelper.CreateElement(g, "path");
            XmlHelper.SetAttribute(path, "fill", "#00000000");
            XmlHelper.SetAttribute(path, "stroke", "#00000000");
            XmlHelper.SetAttribute(path, "stroke-width", "0");
            XmlHelper.SetAttribute(path, "stroke-antialiasing", "false");
            XmlHelper.SetAttribute(path, "d", d);

            return new ClipPath(name, doc.CreateNavigator());
        }

        private string GetClipPath(int offset, int length)
        {
            if (_width == 0 || _height == 0)
                return null;

            var reader = new ClipPathReader(_width, _height);
            return reader.Read(Data, offset, length);
        }

        private void Initialize()
        {
            if (_clipPaths != null)
                return;

            _clipPaths = new Collection<IClipPath>();
            _values = new Collection<IEightBimValue>();

            int i = 0;
            while (i < Data.Length)
            {
                if (Data[i++] != '8')
                    continue;
                if (Data[i++] != 'B')
                    continue;
                if (Data[i++] != 'I')
                    continue;
                if (Data[i++] != 'M')
                    continue;

                if (i + 7 > Data.Length)
                    return;

                var id = ByteConverter.ToShort(Data, ref i);
                var isClipPath = id > 1999 && id < 2998;

                string name = null;
                int length = Data[i++];
                if (length != 0)
                {
                    if (isClipPath && i + length < Data.Length)
                        name = Encoding.ASCII.GetString(Data, i, length);

                    i += length;
                }

                if ((length & 0x01) == 0)
                    i++;

                length = ByteConverter.ToUInt(Data, ref i);
                if (i + length > Data.Length)
                    return;

                if (length < 0)
                    return;

                if (length != 0)
                {
                    if (isClipPath)
                    {
                        var clipPath = CreateClipPath(name, i, length);
                        if (clipPath != null)
                            _clipPaths.Add(clipPath);
                    }

                    var data = new byte[length];
                    Array.Copy(Data, i, data, 0, length);
                    _values.Add(new EightBimValue(id, data));
                }

                i += length;
            }
        }
    }
}