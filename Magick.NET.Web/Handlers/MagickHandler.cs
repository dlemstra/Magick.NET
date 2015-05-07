//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.SessionState;

namespace ImageMagick.Web.Handlers
{
	///=============================================================================================
	/// <summary>
	/// Base class for IHttpHandlers that use the IUrlResolver class.
	/// </summary>
	public abstract class MagickHandler : IHttpHandler, IRequiresSessionState
	{
		//===========================================================================================
		private static readonly ReaderWriterLockSlim _Lock = new ReaderWriterLockSlim();
		private static readonly string _Version = InitializeVersion();
		//===========================================================================================
		private static void AddCacheControlHeader(HttpResponse response)
		{
			if (MagickWebSettings.ClientCache.CacheControlMode == CacheControlMode.NoControl)
				return;

			response.Cache.SetMaxAge(MagickWebSettings.ClientCache.CacheControlMaxAge);
			response.Cache.SetCacheability(HttpCacheability.Public);
		}
		//===========================================================================================
		private static string CalculateMD5(string value)
		{
			using (MD5 md5 = MD5.Create())
			{
				byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(value));

				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < data.Length; i++)
				{
					sb.Append(data[i].ToString("X2", CultureInfo.InvariantCulture));
				}

				return sb.ToString();
			}
		}
		//===========================================================================================
		private static string InitializeVersion()
		{
			if (!MagickWebSettings.ShowVersion)
				return null;

			Object version = typeof(MagickHandler).Assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)[0];
			return ((AssemblyFileVersionAttribute)version).Version;
		}
		//===========================================================================================
		private static bool Write304(HttpContext content, DateTime fileDate)
		{
			DateTime modificationDate = new DateTime(fileDate.Year, fileDate.Month, fileDate.Day, fileDate.Hour, fileDate.Minute, fileDate.Second);
			if (modificationDate > DateTime.Now)
				modificationDate = DateTime.Now;

			modificationDate = modificationDate.ToUniversalTime();

			string modifiedSince = null;
			try
			{
				content.Response.Cache.SetLastModified(modificationDate);
				modifiedSince = content.Request.Headers["If-Modified-Since"];
			}
			catch (ThreadAbortException)
			{
			}

			if (string.IsNullOrEmpty(modifiedSince))
				return false;

			string since;
			int index = modifiedSince.IndexOf(";", StringComparison.OrdinalIgnoreCase);

			if (index >= 0)
				since = modifiedSince.Substring(0, index);
			else
				since = modifiedSince;

			DateTime modifiedDate;
			bool success = DateTime.TryParseExact(since, "r", CultureInfo.InvariantCulture, DateTimeStyles.None, out modifiedDate);

			if (success && modifiedDate == modificationDate)
			{
				try
				{
					content.Response.StatusCode = 304;
					return true;
				}
				catch (ThreadAbortException)
				{
				}
			}

			return false;
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickHandler class.
		/// </summary>
		protected MagickHandler(IUrlResolver urlResolver, MagickFormatInfo formatInfo)
		{
			UrlResolver = urlResolver;
			FormatInfo = formatInfo;
		}
		///==========================================================================================
		/// <summary>
		/// Writes the file to the response.
		/// </summary>
		protected abstract void WriteFile(HttpContext context);
		///==========================================================================================
		/// <summary>
		/// Returns the format information for the file that was resolved with the IUrlResolver.
		/// </summary>
		protected MagickFormatInfo FormatInfo
		{
			get;
			private set;
		}
		///==========================================================================================
		/// <summary>
		/// Returns the IUrlResolver that was used.
		/// </summary>
		protected IUrlResolver UrlResolver
		{
			get;
			private set;
		}
		///==========================================================================================
		/// <summary>
		/// Returns true if the cache file is newer then the file name that was resolved by the
		/// IUrlResolver.
		/// </summary>
		protected bool CanUseCache(string cacheFileName)
		{
			_Lock.EnterReadLock();

			try
			{
				if (!File.Exists(cacheFileName))
					return false;

				DateTime fileDate = File.GetLastWriteTime(UrlResolver.FileName);
				DateTime cacheDate = File.GetLastWriteTime(cacheFileName);
				return fileDate <= cacheDate;
			}
			finally
			{
				_Lock.ExitReadLock();
			}
		}
		///==========================================================================================
		/// <summary>
		/// Returns the file name that can be used to cache the result.
		/// </summary>
		/// <param name="directoryName">The name of the subdirectory to store the files in.</param>
		/// <param name="subdirectoryKey">The key that will be used to create MD5 hash and that
		/// will be used as a sub directory.</param>
		/// <returns></returns>
		protected string GetCacheFileName(string directoryName, string subdirectoryKey)
		{
			string cacheDirectory = MagickWebSettings.CacheDirectory + directoryName + "\\" + CalculateMD5(subdirectoryKey) + "\\";

			if (!Directory.Exists(cacheDirectory))
				Directory.CreateDirectory(cacheDirectory);

			return cacheDirectory + CalculateMD5(UrlResolver.FileName) + "." + UrlResolver.Format;
		}
		///==========================================================================================
		/// <summary>
		/// Returns the file name for a temporary file.
		/// </summary>
		/// <returns></returns>
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		protected static string GetTempFileName()
		{
			return MagickWebSettings.TempDirectory + Guid.NewGuid();
		}
		///==========================================================================================
		/// <summary>
		/// Moves to the specified source file name to the destination file name. This is happening
		/// in a lock to avoid problems when an other request is reading the file.
		/// </summary>
		protected static void MoveToCache(string fileName, string cacheFileName)
		{
			try
			{
				_Lock.EnterWriteLock();

				if (File.Exists(cacheFileName))
					File.Delete(cacheFileName);

				File.Move(fileName, cacheFileName);
			}
			finally
			{
				_Lock.ExitWriteLock();
			}
		}
		///==========================================================================================
		/// <summary>
		/// Writes the specified file to the response.
		/// </summary>
		protected static void WriteFile(HttpContext context, string fileName)
		{
			if (context == null)
				return;

			_Lock.EnterReadLock();

			try
			{
				AddCacheControlHeader(context.Response);

				if (Write304(context, File.GetLastWriteTime(fileName)))
					return;

				context.Response.TransmitFile(fileName);
			}
			finally
			{
				_Lock.ExitReadLock();
			}
		}
		///==========================================================================================
		/// <summary>
		/// Gets a value indicating whether another request can use the IHttpHandler instance.
		/// </summary>
		public bool IsReusable
		{
			get
			{
				return false;
			}
		}
		///==========================================================================================
		/// <summary>
		/// Enables processing of HTTP Web requests by a custom HttpHandler that implements the
		/// IHttpHandler interface.
		/// </summary>
		/// <param name="context">An HttpContext object that provides references to the intrinsic
		/// server objects (for example, Request, Response, Session, and Server) used to service
		/// HTTP requests.</param>
		public void ProcessRequest(HttpContext context)
		{
			if (context == null)
				return;

			context.Response.ContentType = FormatInfo.MimeType;

			if (!string.IsNullOrEmpty(_Version))
				context.Response.AddHeader("X-Magick", _Version);

			WriteFile(context);
		}
		//===========================================================================================
	}
	//==============================================================================================
}
