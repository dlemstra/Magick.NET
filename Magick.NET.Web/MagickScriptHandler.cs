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
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.SessionState;
using System.Xml.XPath;

namespace ImageMagick.Web
{
	///=============================================================================================
	/// <summary>
	/// IHttpHandler that can be used to send a scripted image to the response.
	/// </summary>
	public class MagickScriptHandler : IHttpHandler, IRequiresSessionState
	{
		//===========================================================================================
		private MagickFormatInfo _FormatInfo;
		private static readonly ReaderWriterLockSlim _Lock = new ReaderWriterLockSlim();
		private IUrlResolver _UrlResolver;
		private static readonly string _Version = GetVersion();
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
		private bool CanUseCache(string cacheFileName)
		{
			_Lock.EnterReadLock();

			try
			{
				if (!File.Exists(cacheFileName))
					return false;

				DateTime fileDate = File.GetLastWriteTime(_UrlResolver.FileName);
				DateTime cacheDate = File.GetLastWriteTime(cacheFileName);
				return fileDate <= cacheDate;
			}
			finally
			{
				_Lock.ExitReadLock();
			}
		}
		//===========================================================================================
		private string GetCacheFileName(IXPathNavigable xml)
		{
			string cacheDirectory = MagickWebSettings.CacheDirectory + CalculateMD5(xml.CreateNavigator().OuterXml) + "\\";

			if (!Directory.Exists(cacheDirectory))
				Directory.CreateDirectory(cacheDirectory);

			return cacheDirectory + CalculateMD5(_UrlResolver.FileName) + "." + _UrlResolver.Format;
		}
		//===========================================================================================
		private static string GetVersion()
		{
			if (!MagickWebSettings.ShowVersion)
				return null;

			Object version = typeof(MagickScriptHandler).Assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)[0];
			return ((AssemblyFileVersionAttribute)version).Version;
		}
		//===========================================================================================
		private void OnScriptRead(object sender, ScriptReadEventArgs arguments)
		{
			arguments.Image = new MagickImage(_UrlResolver.FileName, arguments.Settings);
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
		//===========================================================================================
		private void WriteFile(HttpContext context)
		{
			WriteFile(context, _UrlResolver.FileName);
		}
		//===========================================================================================
		private static void WriteFile(HttpContext context, string fileName)
		{
			_Lock.EnterReadLock();

			try
			{
				if (Write304(context, File.GetLastWriteTime(fileName)))
					return;

				context.Response.TransmitFile(fileName);
			}
			finally
			{
				_Lock.ExitReadLock();
			}
		}
		//===========================================================================================
		private bool WriteScriptedFile(HttpContext context)
		{
			IXPathNavigable xml = _UrlResolver.Script;
			if (xml == null)
				return false;

			string cacheFileName = GetCacheFileName(xml);
			if (CanUseCache(cacheFileName))
			{
				WriteFile(context, cacheFileName);
				return true;
			}

			MagickScript script = new MagickScript(xml);
			script.Read += OnScriptRead;

			using (MagickImage image = script.Execute())
			{
				image.Format = _UrlResolver.Format;
				WriteToCache(image, cacheFileName);
				WriteFile(context, cacheFileName);
			}

			return true;
		}
		//===========================================================================================
		private static void WriteToCache(MagickImage image, string cacheFileName)
		{
			string tempFile = Path.GetTempFileName();

			try
			{
				image.Write(tempFile);

				_Lock.EnterWriteLock();

				if (File.Exists(cacheFileName))
					File.Delete(cacheFileName);

				File.Move(tempFile, cacheFileName);
			}
			finally
			{
				_Lock.ExitWriteLock();

				if (File.Exists(tempFile))
					File.Delete(tempFile);
			}
		}
		//===========================================================================================
		internal MagickScriptHandler(IUrlResolver urlResolver)
		{
			_UrlResolver = urlResolver;
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

			context.Response.ContentType = _FormatInfo.MimeType;

			if (!string.IsNullOrEmpty(_Version))
				context.Response.AddHeader("X-Magick", _Version);

			if (!WriteScriptedFile(context))
				WriteFile(context);
		}
		///==========================================================================================
		/// <summary>
		/// Determines if the UrlResolver is initialized properly.
		/// </summary>
		public bool IsValid
		{
			get
			{
				if (string.IsNullOrEmpty(_UrlResolver.FileName))
					return false;

				if (!File.Exists(_UrlResolver.FileName))
					return false;

				_FormatInfo = MagickNET.GetFormatInformation(_UrlResolver.Format);

				if (_FormatInfo == null || string.IsNullOrEmpty(_FormatInfo.MimeType))
					return false;

				return true;
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}
