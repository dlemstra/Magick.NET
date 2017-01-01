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

using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;

namespace ImageMagick
{
  /// <summary>
  /// Class that contains variables for a script
  /// </summary>
  public sealed class ScriptVariables
  {
    private static readonly Regex _Names = new Regex("\\{[$](?<name>[0-9a-zA-Z_-]{1,16})\\}", RegexOptions.Compiled);

    private Dictionary<string, object> _Variables;

    private void GetNames(XmlElement element)
    {
      foreach (XmlAttribute attribute in element.Attributes)
      {
        string[] names = GetNames(attribute.Value);
        if (names == null)
          continue;

        foreach (string name in names)
        {
          _Variables[name] = null;
        }
      }

      foreach (XmlNode child in element.ChildNodes)
      {
        if (child.GetType() == typeof(XmlElement))
          GetNames((XmlElement)child);
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

    internal double[] GetDoubleArray(XmlElement element)
    {
      if (element == null)
        return null;

      XmlAttribute attribute = element.Attributes["variable"];
      if (attribute != null)
      {
        string[] names = GetNames(attribute.Value);
        if (names != null)
          return (double[])_Variables[names[0]];
      }

      double[] result = new double[element.ChildNodes.Count];
      int index = 0;
      foreach (XmlElement child in element.ChildNodes)
      {
        result[index++] = double.Parse(child.InnerText, CultureInfo.InvariantCulture);
      }

      return result;
    }

    internal string[] GetStringArray(XmlElement element)
    {
      if (element == null)
        return null;

      XmlAttribute attribute = element.Attributes["variable"];
      if (attribute != null)
      {
        string[] names = GetNames(attribute.Value);
        if (names != null)
          return (string[])_Variables[names[0]];
      }

      string[] result = new string[element.ChildNodes.Count];
      int index = 0;
      foreach (XmlElement child in element.ChildNodes)
      {
        result[index++] = child.InnerText;
      }

      return result;
    }

    internal T GetValue<T>(XmlAttribute attribute)
    {
      if (attribute == null)
        return default(T);

      string[] names = GetNames(attribute.Value);
      if (names == null)
        return XmlHelper.GetValue<T>(attribute);

      if (typeof(T) == typeof(string))
      {
        string newValue = attribute.Value;
        foreach (string name in names)
        {
          newValue = newValue.Replace(newValue, MagickConverter.Convert<string>(_Variables[name]));
        }

        return (T)(object)newValue;
      }
      else
      {
        string name = names[0];

        if (TypeHelper.IsValueType(typeof(T)))
          Throw.IfNull(nameof(attribute), _Variables[name], "The variable {0} should be set.", name);

        return MagickConverter.Convert<T>(_Variables[name]);
      }
    }

    internal T GetValue<T>(XmlElement element, string attribute)
    {
      return GetValue<T>(element.Attributes[attribute]);
    }

    internal ScriptVariables(XmlDocument script)
    {
      _Variables = new Dictionary<string, object>();
      GetNames(script.DocumentElement);
    }

    /// <summary>
    /// Get or sets the specified variable.
    /// </summary>
    /// <param name="name">The name of the variable.</param>
    public object this[string name]
    {
      get
      {
        return Get(name);
      }
      set
      {
        Set(name, value);
      }
    }

    /// <summary>
    /// Gets the names of the variables.
    /// </summary>
    public IEnumerable<string> Names
    {
      get
      {
        return _Variables.Keys;
      }
    }

    /// <summary>
    /// Returns the value of the variable with the specified name.
    /// </summary>
    /// <param name="name">The name of the variable</param>
    /// <returns>Am <see cref="object"/>.</returns>
    public object Get(string name)
    {
      Throw.IfNullOrEmpty(nameof(name), name);

      return _Variables[name];
    }

    /// <summary>
    /// Set the value of the variable with the specified name.
    /// </summary>
    /// <param name="name">The name of the variable</param>
    /// <param name="value">The value of the variable</param>
    public void Set(string name, object value)
    {
      Throw.IfNullOrEmpty(nameof(name), name);
      Throw.IfFalse(nameof(name), _Variables.ContainsKey(name), "Invalid variable name: {0}", value);

      _Variables[name] = value;
    }
  }
}