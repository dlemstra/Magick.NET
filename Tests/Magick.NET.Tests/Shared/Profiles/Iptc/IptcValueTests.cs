﻿// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using System.Linq;
using System.Text;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class IptcValueTests
    {
        [TestMethod]
        public void Encoding_SetToNull_NotChanged()
        {
            IptcValue value = GetIptcValue();

            value.Encoding = null;

            Assert.IsNotNull(value.Encoding);
        }

        [TestMethod]
        public void Test_Encoding()
        {
            IptcValue value = GetIptcValue();

            Assert.AreEqual("Communications", value.Value);

            value.Encoding = Encoding.UTF32;
            Assert.AreNotEqual("Communications", value.Value);

            value.Value = "Communications";
            Assert.AreEqual("Communications", value.Value);

            value.Encoding = Encoding.UTF8;
            Assert.AreNotEqual("Communications", value.Value);
        }

        [TestMethod]
        public void Test_IEquatable()
        {
            IptcValue first = GetIptcValue();
            IptcValue second = GetIptcValue();

            Assert.IsTrue(first == second);
            Assert.IsTrue(first.Equals(second));
            Assert.IsTrue(first.Equals((object)second));
        }

        [TestMethod]
        public void Test_Properties()
        {
            IptcValue value = GetIptcValue();

            Assert.AreEqual(IptcTag.Caption, value.Tag);
            Assert.AreEqual("Communications", value.ToString());
            Assert.AreEqual("Communications", value.Value);
            Assert.AreEqual(14, value.ToByteArray().Length);
        }

        [TestMethod]
        public void Test_ToString()
        {
            IptcValue value = GetIptcValue();

            Assert.AreEqual("Communications", value.ToString());
            Assert.AreEqual("Communications", value.ToString(Encoding.UTF8));
            Assert.AreNotEqual("Communications", value.ToString(Encoding.UTF32));

            value.Encoding = Encoding.UTF32;
            value.Value = "Test";
            Assert.AreEqual("Test", value.ToString());
            Assert.AreEqual("Test", value.ToString(Encoding.UTF32));
            Assert.AreNotEqual("Test", value.ToString(Encoding.UTF8));

            value.Value = string.Empty;
            Assert.AreEqual(string.Empty, value.ToString());
            value.Value = "Test";
            Assert.AreEqual("Test", value.ToString());
            value.Value = null;
            Assert.AreEqual(string.Empty, value.ToString());
        }

        private static IptcValue GetIptcValue()
        {
            using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
            {
                IptcProfile profile = image.GetIptcProfile();
                return profile.Values.ElementAt(1);
            }
        }
    }
}
