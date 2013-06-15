//=================================================================================================
// Copyright 2013 Dirk Lemstra <http://magick.codeplex.com/>
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
using System.IO;
using System.Xml.Schema;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
	//==============================================================================================
	[TestClass]
	public class MagickScriptsTests
	{
		//===========================================================================================
		private const string _Category = "MagickScript";
		//===========================================================================================
		private void CollectionScriptRead(object sender, ScriptReadEventArgs arguments)
		{
			switch (arguments.Id)
			{
				case "icon":
					arguments.Image = new MagickImage(Files.MagickNETIconPng, arguments.Settings);
					break;
				case "snakeware":
					arguments.Image = new MagickImage(Files.SnakewarePNG, arguments.Settings);
					break;
				default:
					throw new NotImplementedException(arguments.Id);
			}
		}
		//===========================================================================================
		private void EventsScriptRead(object sender, ScriptReadEventArgs arguments)
		{
			Assert.AreEqual("read.id", arguments.Id);
			arguments.Image = new MagickImage(Files.SnakewarePNG, arguments.Settings);
		}
		//===========================================================================================
		private void EventsScriptWrite(object sender, ScriptWriteEventArgs arguments)
		{
			Assert.AreEqual("write.id", arguments.Id);
		}
		//===========================================================================================
		private void ResizeScriptRead(object sender, ScriptReadEventArgs arguments)
		{
			arguments.Image = new MagickImage(Files.MagickNETIconPng);
		}
		//===========================================================================================
		private void Script_ReadNothing(object sender, ScriptReadEventArgs arguments)
		{
		}
		//===========================================================================================
		private static void TestScriptResizeResult(MagickImage result)
		{
			Assert.AreEqual("Magick.NET.Resize", result.Comment);
			Assert.AreEqual(64, result.Width);
			Assert.AreEqual(64, result.Height);
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Constructor()
		{
			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				new MagickScript((string)null);
			});

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				new MagickScript((Stream)null);
			});

			ExceptionAssert.Throws<ArgumentException>(delegate()
			{
				new MagickScript(Files.Missing);
			});

			ExceptionAssert.Throws<XmlSchemaValidationException>(delegate()
			{
				new MagickScript(Files.InvalidScript);
			});
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Execute_Collection()
		{
			MagickScript script = new MagickScript(Files.CollectionScript);
			script.Read += CollectionScriptRead;

			MagickImage image = script.Execute();

			Assert.IsNotNull(image);
			Assert.AreEqual(MagickFormat.Png, image.Format);
			Assert.AreEqual(128, image.Width);
			Assert.AreEqual(128, image.Height);
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Execute_Draw()
		{
			MagickScript script = new MagickScript(Files.DrawScript);

			using (MagickImage image = new MagickImage(Files.ImageMagickJPG))
			{
				script.Execute(image);
			}
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Execute_Events()
		{
			MagickScript script = new MagickScript(Files.EventsScript);

			ExceptionAssert.Throws<InvalidOperationException>(delegate()
			{
				script.Execute();
			});

			script.Read += Script_ReadNothing;
			ExceptionAssert.Throws<InvalidOperationException>(delegate()
			{
				script.Execute();
			});
			script.Read -= Script_ReadNothing;

			ExceptionAssert.Throws<InvalidOperationException>(delegate()
			{
				script.Read += EventsScriptRead;
				script.Read -= EventsScriptRead;
				script.Execute();
			});

			script.Read += EventsScriptRead;

			ExceptionAssert.Throws<InvalidOperationException>(delegate()
			{
				script.Execute();
			});

			ExceptionAssert.Throws<InvalidOperationException>(delegate()
			{
				script.Write += EventsScriptWrite;
				script.Write -= EventsScriptWrite;
				script.Execute();
			});

			script.Write += EventsScriptWrite;
			script.Execute();
		}
		//===========================================================================================
		[TestMethod, TestCategory(_Category)]
		public void Test_Execute_Resize()
		{
			MagickScript script = new MagickScript(Files.ResizeScript);

			using (MagickImage image = new MagickImage(Files.MagickNETIconPng))
			{
				script.Execute(image);
				TestScriptResizeResult(image);

				script.Read += ResizeScriptRead;
				using (MagickImage result = script.Execute())
				{
					TestScriptResizeResult(result);
				}
			}
		}
		//===========================================================================================
	}
	//==============================================================================================
}
