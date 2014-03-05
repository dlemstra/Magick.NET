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

namespace Fasterflect.Caching
{
	/// <summary>
	/// An enumeration of the supported caching strategies.
	/// </summary>
	internal enum CacheStrategy
	{
		/// <summary>
		/// This value indicates that caching is disabled.
		/// </summary>
		None,
		/// <summary>
		/// This value indicates that caching is enabled, and that cached objects may be
		/// collected and released at will by the garbage collector. This is the default value. 
		/// </summary>
		Temporary,
		/// <summary>
		/// This value indicates that caching is enabled, and that cached objects may not
		/// be garbage collected. The developer must manually ensure that objects are 
		/// removed from the cache when they are no longer needed.
		/// </summary>
		Permanent
	}
}