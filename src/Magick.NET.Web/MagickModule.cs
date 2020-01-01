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
using System.Configuration;
using System.Web;
using ImageMagick.Web.Handlers;

namespace ImageMagick.Web
{
    /// <summary>
    /// Httpmodule that uses various handlers to resize/optimize/compress images.
    /// </summary>
    public sealed class MagickModule : MagickModuleBase
    {
        private const string UrlKey = "ImageMagick.Web.MagickModule.Url";
        private readonly MagickWebSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickModule"/> class.
        /// </summary>
        public MagickModule()
          : this(MagickWebSettings.Instance)
        {
        }

        internal MagickModule(MagickWebSettings settings)
        {
            _settings = settings;
        }

        /// <inheritdoc/>
        protected override bool UsingIntegratedPipeline
        {
            get
            {
                return HttpRuntime.UsingIntegratedPipeline;
            }
        }

        private IEnumerable<IUrlResolver> UrlResolvers
        {
            get
            {
                foreach (UrlResolverSettings settings in _settings.UrlResolvers)
                {
                    yield return settings.CreateInstance();
                }
            }
        }

        internal override void OnBeginRequest(HttpContextBase context)
        {
            context.Items[UrlKey] = context.Request.Url;
        }

        internal override void OnPostAuthorizeRequest(HttpContextBase context)
        {
            IHttpHandler newHandler = HandleRequest(context);
            if (newHandler != null)
                context.RemapHandler(newHandler);
        }

        internal override void OnPostMapRequestHandler(HttpContextBase context)
        {
            IHttpHandler newHandler = HandleRequest(context);
            if (newHandler != null)
                context.Handler = newHandler;
        }

        internal override void Initialize()
        {
            if (_settings.UrlResolvers.Count == 0)
                throw new ConfigurationErrorsException("Define at least one url resolver.");

            InitOpenCL();
            InitResourceLimits();
        }

        private static bool IsValid(IScriptData scriptData)
        {
            if (scriptData == null)
                return false;

            if (scriptData.Script == null)
                return false;

            return true;
        }

        private IHttpHandler CreateHttpHandler(IUrlResolver urlResolver)
        {
            MagickFormatInfo formatInfo = MagickNET.GetFormatInformation(urlResolver.Format);
            if (formatInfo == null || !formatInfo.IsReadable)
                return null;

            IImageData imageData = ImageData.Create(urlResolver, formatInfo);
            if (!imageData.IsValid)
                return null;

            IScriptData scriptData = urlResolver as IScriptData;
            if (IsValid(scriptData))
                return new MagickScriptHandler(_settings, imageData, scriptData);

            if (HandlerHelper.CanOptimize(_settings, formatInfo))
                return new ImageOptimizerHandler(_settings, imageData);

            if (HandlerHelper.CanCompress(_settings, formatInfo))
                return new GzipHandler(_settings, imageData);

            return null;
        }

        private IHttpHandler HandleRequest(HttpContextBase context)
        {
            Uri url = (Uri)context.Items[UrlKey];

            foreach (IUrlResolver urlResolver in UrlResolvers)
            {
                if (urlResolver.Resolve(url))
                    return CreateHttpHandler(urlResolver);
            }

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
    }
}
