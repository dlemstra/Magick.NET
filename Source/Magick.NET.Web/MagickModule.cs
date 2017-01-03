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
using System.Configuration;
using System.IO;
using System.Web;
using ImageMagick.Web.Handlers;

namespace ImageMagick.Web
{
  /// <summary>
  /// Httpmodule that uses various handlers to resize/optimize/compress images.
  /// </summary>
  public sealed class MagickModule : IHttpModule
  {
    private const string UrlKey = "ImageMagick.Web.MagickModule.Url";

    private readonly MagickWebSettings _settings;

    private IEnumerable<IUrlResolver> ScriptUrlResolvers
    {
      get
      {
        foreach (UrlResolverSettings settings in _settings.UrlResolvers)
        {
          yield return settings.CreateInstance();
        }
      }
    }

    private IHttpHandler HandleRequest(HttpContext context)
    {
      Uri url = (Uri)context.Items[UrlKey];

      foreach (IUrlResolver scriptUrlResolver in ScriptUrlResolvers)
      {
        if (scriptUrlResolver.Resolve(url))
          return CreateHttpHandler(scriptUrlResolver);
      }

      return null;
    }

    private IHttpHandler CreateHttpHandler(IUrlResolver urlResolver)
    {
      if (string.IsNullOrEmpty(urlResolver.FileName) || !File.Exists(urlResolver.FileName))
        return null;

      MagickFormatInfo formatInfo = MagickNET.GetFormatInformation(urlResolver.Format);
      if (formatInfo == null || string.IsNullOrEmpty(formatInfo.MimeType))
        return null;

      if (urlResolver.Script != null)
        return new MagickScriptHandler(_settings, urlResolver, formatInfo);

      if (ImageOptimizerHandler.CanOptimize(_settings, formatInfo))
        return new ImageOptimizerHandler(_settings, urlResolver, formatInfo);

      if (GzipHandler.CanCompress(_settings, formatInfo))
        return new GzipHandler(_settings, urlResolver, formatInfo);

      return null;
    }

    private void InitOpenCL()
    {
      if (!_settings.UseOpenCL)
        OpenCL.IsEnabled = false;
    }

    private void InitResourceLimits()
    {
      if (_settings.ResourceLimits.Width != null)
        ResourceLimits.Width = (ulong)_settings.ResourceLimits.Width.Value;

      if (_settings.ResourceLimits.Height != null)
        ResourceLimits.Height = (ulong)_settings.ResourceLimits.Height.Value;
    }

    private void OnBeginRequest(object sender, EventArgs arguments)
    {
      HttpContext context = ((HttpApplication)sender).Context;
      context.Items[UrlKey] = context.Request.Url;
    }

    private void OnPostAuthorizeRequest(object sender, EventArgs arguments)
    {
      HttpContext context = ((HttpApplication)sender).Context;
      IHttpHandler newHandler = HandleRequest(context);
      if (newHandler != null)
        context.RemapHandler(newHandler);
    }

    private void OnPostMapRequestHandler(object sender, EventArgs arguments)
    {
      HttpContext context = ((HttpApplication)sender).Context;
      IHttpHandler newHandler = HandleRequest(context);
      if (newHandler != null)
        context.Handler = newHandler;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickModule"/> class.
    /// </summary>
    public MagickModule()
      : this(MagickWebSettings.Instance)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickModule"/> class.
    /// </summary>
    /// <param name="settings">The settings to use.</param>
    public MagickModule(MagickWebSettings settings)
    {
      _settings = settings;
    }

    /// <summary>
    /// Initializes the module and prepares it to handle requests.
    /// </summary>
    /// <param name="context">An HttpApplication that provides access to the methods, properties,
    /// and events common to all application objects within an ASP.NET application</param>
    public void Init(HttpApplication context)
    {
      if (context == null)
        return;

      if (_settings.UrlResolvers.Count == 0)
        throw new ConfigurationErrorsException("Define at least one url resolver.");

      context.BeginRequest += OnBeginRequest;

      if (HttpRuntime.UsingIntegratedPipeline)
        context.PostAuthorizeRequest += OnPostAuthorizeRequest;
      else
        context.PostMapRequestHandler += OnPostMapRequestHandler;

      InitOpenCL();
      InitResourceLimits();
    }

    /// <summary>
    /// Disposes of the resources (other than memory) used by the module.
    /// </summary>
    public void Dispose()
    {
    }
  }
}
