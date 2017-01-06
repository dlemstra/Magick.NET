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
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

namespace ImageMagick.Web
{
  /// <summary>
  /// Class that contains the settings for an url resolver.
  /// </summary>
  public class UrlResolverSettings : ConfigurationElement
  {
    private delegate IUrlResolver IUrlResolverConstructor();
    private IUrlResolverConstructor _Constructor;

    [ConfigurationProperty("type", IsRequired = true)]
    internal string TypeName
    {
      get
      {
        return (string)this["type"];
      }
    }

    internal IUrlResolver CreateInstance()
    {
      return _Constructor();
    }

    /// <summary>
    /// Called after deserialization.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "IUrlResolver", Justification = "This is the correct spelling.")]
    protected override void PostDeserialize()
    {
      base.PostDeserialize();

      UrlResolverType = Type.GetType(TypeName, true, true);

      if (!typeof(IUrlResolver).IsAssignableFrom(UrlResolverType))
        throw new ConfigurationErrorsException("The type '" + TypeName + "' does not implement the interface " + nameof(IUrlResolver));

      ConstructorInfo ctor = UrlResolverType.GetConstructor(new Type[] { });
      NewExpression newExp = Expression.New(ctor);
      _Constructor = (IUrlResolverConstructor)Expression.Lambda(typeof(IUrlResolverConstructor), newExp).Compile();
    }

    /// <summary>
    /// Gets the type of the url resolver.
    /// </summary>
    public Type UrlResolverType
    {
      get;
      private set;
    }
  }
}
