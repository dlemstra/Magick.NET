#region License
// Copyright 2010 Buu Nguyen, Morten Mertner
// 
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at 
// 
// http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
// 
// The latest version of this file can be found at http://fasterflect.codeplex.com/
#endregion

using System;

namespace Fasterflect
{
	/// <summary>
	/// This enumeration allows you to customize the XML output of the ToXml extensions.
	/// </summary>
	[Flags]
	public enum FormatOptions
	{
		/// <summary>
		/// This option specifies the empty set of options and does not affect the output.
		/// </summary>
		None = 0,
		/// <summary>
		/// If this option is specified the generated XML will include an XML document header.
		/// </summary>
		AddHeader = 1,
		/// <summary>
		/// If this option is specified a line feed will be emitted after every XML element.
		/// </summary>
		NewLineAfterElement = 2,
		/// <summary>
		/// If this option is specified nested tags will be indented either 1 tab character
		/// (the default) or 4 space characters.
		/// </summary>
		Indent = 4,
		/// <summary>
		/// If this option is specified indentation will use spaces instead of tabs.
		/// </summary>
		UseSpaces = 8,
		/// <summary>
		/// This option, which combines AddHeader, NewLineAfterElement and Indent, provides the 
		/// default set of options used. 
		/// </summary>
		Default = AddHeader | NewLineAfterElement | Indent
	}
}
