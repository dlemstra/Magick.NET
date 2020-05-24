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
using System.Globalization;
using System.Xml;

namespace ImageMagick
{
    internal static partial class XmlHelper
    {
        public static XmlElement CreateElement(XmlNode node, string name)
        {
            var doc = node.GetType() == typeof(XmlDocument) ? (XmlDocument)node : node.OwnerDocument;
            var element = doc.CreateElement(name);
            node.AppendChild(element);
            return element;
        }

        public static void SetAttribute<TType>(XmlElement element, string name, TType value)
        {
            XmlAttribute attribute;
            if (element.HasAttribute(name))
                attribute = element.Attributes[name];
            else
                attribute = element.Attributes.Append(element.OwnerDocument.CreateAttribute(name));

            if (typeof(TType) == typeof(string))
                attribute.Value = (string)(object)value;
            else
                attribute.Value = (string)Convert.ChangeType(value, typeof(string), CultureInfo.InvariantCulture);
        }
    }
}