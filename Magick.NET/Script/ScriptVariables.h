//=================================================================================================
// Copyright 2013-2014 Dirk Lemstra <https://magick.codeplex.com/>
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
#pragma once

using namespace System::Collections::Generic;
using namespace System::Xml;
using namespace System::Text::RegularExpressions;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that contains variables for a script
	///</summary>
	public ref class ScriptVariables sealed
	{
		//===========================================================================================
	private:
		//===========================================================================================
		static initonly Regex^ _Names = gcnew Regex("\\{[$](?<name>[0-9a-zA-Z_-]{1,16})\\}", RegexOptions::Compiled);
		//===========================================================================================
		Dictionary<String^, Object^>^ _Variables;
		//===========================================================================================
		void GetNames(XmlElement^ script);
		//===========================================================================================
		static array<String^>^ GetNames(String^ value);
		//===========================================================================================
	internal:
		//===========================================================================================
		ScriptVariables(XmlDocument^ script);
		//===========================================================================================
		array<double>^ GetDoubleArray(XmlElement^ element);
		//===========================================================================================
		generic <class T>
		T GetValue(XmlAttribute^ attribute);
		//===========================================================================================
		generic <class T>
		T GetValue(XmlElement^ element, String^ attribute);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Get or set the specified variable.
		///</summary>
		property Object^ default[String^]
		{
			Object^ get(String^ name);
			void set(String^ name, Object^ value);
		}
		///==========================================================================================
		///<summary>
		/// The names of the variables.
		///</summary>
		property IEnumerable<String^>^ Names
		{
			IEnumerable<String^>^ get();
		}
		///==========================================================================================
		///<summary>
		/// Returns the value of the variable with the specified name.
		///</summary>
		///<param name="name">The name of the variable</param>
		Object^ Get(String^ name);
		///==========================================================================================
		///<summary>
		/// Set the value of the variable with the specified name.
		///</summary>
		///<param name="name">The name of the variable</param>
		void Set(String^ name, Object^ value);
		//===========================================================================================
	};
	//==============================================================================================
}