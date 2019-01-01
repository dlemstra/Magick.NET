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

using System.Web;

namespace ImageMagick.Web
{
    /// <summary>
    /// Base class for the Httpmodule that uses various handlers to resize/optimize/compress images.
    /// </summary>
    public abstract class MagickModuleBase : IHttpModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickModuleBase"/> class.
        /// </summary>
        protected MagickModuleBase()
        {
        }

        /// <summary>
        /// Gets a value indicating whether an intergrated pipeline is used.
        /// </summary>
        protected abstract bool UsingIntegratedPipeline { get; }

        /// <summary>
        /// Initializes the module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">An HttpApplication that provides access to the methods, properties,
        /// and events common to all application objects within an ASP.NET application</param>
        public void Init(HttpApplication context)
        {
            if (context == null)
                return;

            context.BeginRequest += (sender, e) => OnBeginRequest(new HttpContextWrapper(((HttpApplication)sender).Context));

            if (UsingIntegratedPipeline)
                context.PostAuthorizeRequest += (sender, e) => OnPostAuthorizeRequest(new HttpContextWrapper(((HttpApplication)sender).Context));
            else
                context.PostMapRequestHandler += (sender, e) => OnPostMapRequestHandler(new HttpContextWrapper(((HttpApplication)sender).Context));

            Initialize();
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module.
        /// </summary>
        public void Dispose()
        {
        }

        internal abstract void OnBeginRequest(HttpContextBase context);

        internal abstract void OnPostAuthorizeRequest(HttpContextBase context);

        internal abstract void OnPostMapRequestHandler(HttpContextBase context);

        internal abstract void Initialize();
    }
}
