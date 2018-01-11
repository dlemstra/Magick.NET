// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    internal static class XmlHelper
    {
        public static XmlElement CreateElement(XmlNode node, string name)
        {
            DebugThrow.IfNull(nameof(node), node);
            DebugThrow.IfNullOrEmpty(nameof(name), name);

            XmlDocument doc = node.GetType() == typeof(XmlDocument) ? (XmlDocument)node : node.OwnerDocument;
            XmlElement element = doc.CreateElement(name);
            node.AppendChild(element);
            return element;
        }

        public static T GetAttribute<T>(XmlElement element, string name)
        {
            if (element == null || !element.HasAttribute(name))
                return default(T);

            return MagickConverter.Convert<T>(element.GetAttribute(name));
        }

        public static T GetValue<T>(XmlAttribute attribute)
        {
            if (attribute == null)
                return default(T);

            return MagickConverter.Convert<T>(attribute.Value);
        }

        public static void SetAttribute<TType>(XmlElement element, string name, TType value)
        {
            DebugThrow.IfNull(nameof(element), element);
            DebugThrow.IfNullOrEmpty(nameof(name), name);

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