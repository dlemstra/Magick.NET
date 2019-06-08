// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;

namespace ImageMagick
{
    /// <summary>
    /// Class that contains variables for a script.
    /// </summary>
    public sealed class ScriptVariables
    {
        private static readonly Regex _Names = new Regex("\\{[$](?<name>[0-9a-zA-Z_-]{1,16})\\}", RegexOptions.Compiled);

        private readonly Dictionary<string, object> _variables;

        internal ScriptVariables(XmlDocument script)
        {
            _variables = new Dictionary<string, object>();
            GetNames(script.DocumentElement);
        }

        /// <summary>
        /// Gets the names of the variables.
        /// </summary>
        public IEnumerable<string> Names
        {
            get
            {
                return _variables.Keys;
            }
        }

        /// <summary>
        /// Get or sets the specified variable.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        public object this[string name]
        {
            get { return Get(name); }
            set { Set(name, value); }
        }

        /// <summary>
        /// Returns the value of the variable with the specified name.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <returns>Am <see cref="object"/>.</returns>
        public object Get(string name)
        {
            Throw.IfNullOrEmpty(nameof(name), name);
            Throw.IfFalse(nameof(name), _variables.ContainsKey(name), "Invalid variable name: {0}", name);

            return _variables[name];
        }

        /// <summary>
        /// Set the value of the variable with the specified name.
        /// </summary>
        /// <param name="name">The name of the variable.</param>
        /// <param name="value">The value of the variable.</param>
        public void Set(string name, object value)
        {
            Throw.IfNullOrEmpty(nameof(name), name);
            Throw.IfFalse(nameof(name), _variables.ContainsKey(name), "Invalid variable name: {0}", name);

            _variables[name] = value;
        }

        internal double[] GetDoubleArray(XmlElement element)
        {
            return GetArray<double>("double", element);
        }

        internal float[] GetSingleArray(XmlElement element)
        {
            return GetArray<float>("float", element);
        }

        internal string[] GetStringArray(XmlElement element)
        {
            return GetArray<string>("string", element);
        }

        internal bool TryGetValue<T>(XmlAttribute attribute, out T value)
        {
            if (attribute == null)
            {
                value = default(T);
                return true;
            }

            string[] names = GetNames(attribute.Value);
            if (names == null)
            {
                value = default(T);
                return false;
            }

            if (typeof(T) == typeof(string))
            {
                string newValue = attribute.Value;
                foreach (string name in names)
                {
                    newValue = newValue.Replace(newValue, MagickConverter.Convert<string>(_variables[name]));
                }

                value = (T)(object)newValue;
                return true;
            }
            else
            {
                string name = names[0];

                if (TypeHelper.IsValueType(typeof(T)))
                    Throw.IfNull(nameof(attribute), _variables[name], "The variable {0} should be set.", name);

                value = MagickConverter.Convert<T>(_variables[name]);
                return true;
            }
        }

        private static string[] GetNames(string value)
        {
            if (value.Length < 3)
                return null;

            MatchCollection matches = _Names.Matches(value);
            if (matches.Count == 0)
                return null;

            string[] result = new string[matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                result[i] = matches[i].Groups["name"].Value;
            }

            return result;
        }

        private T[] GetArray<T>(string typeName, XmlElement element)
        {
            if (element == null)
                return null;

            XmlAttribute attribute = element.Attributes["variable"];
            if (attribute == null)
                return null;

            string[] names = GetNames(attribute.Value);
            if (names == null)
                return null;

            var name = names[0];
            if (!_variables.TryGetValue(name, out var value))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Invalid variable name: {0}.", name));
            }

            if (value is T[])
            {
                return (T[])value;
            }

            throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The value of variable '{0}' is not a {1}[].", name, typeName));
        }

        private void GetNames(XmlElement element)
        {
            if (element == null)
            {
                return;
            }

            foreach (XmlAttribute attribute in element.Attributes)
            {
                string[] names = GetNames(attribute.Value);
                if (names == null)
                    continue;

                foreach (string name in names)
                {
                    _variables[name] = null;
                }
            }

            foreach (XmlNode child in element.ChildNodes)
            {
                if (child.GetType() == typeof(XmlElement))
                    GetNames((XmlElement)child);
            }
        }
    }
}