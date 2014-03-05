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

using Fasterflect.Emitter;

namespace Fasterflect
{
    /// <summary>
    /// Extension methods for working with types.
    /// </summary>
    public static class ValueTypeExtensions
    {
        ///<summary>
        /// Returns a wrapper <see cref="ValueTypeHolder"/> instance if <paramref name="obj"/> 
        /// is a value type.  Otherwise, returns <paramref name="obj"/>.
        ///</summary>
        ///<param name="obj">An object to be examined.</param>
        ///<returns>A wrapper <seealso cref="ValueTypeHolder"/> instance if <paramref name="obj"/>
        /// is a value type, or <paramref name="obj"/> itself if it's a reference type.</returns>
        public static object WrapIfValueType( this object obj )
        {
            return obj.GetType().IsValueType ? new ValueTypeHolder( obj ) : obj;
        }

        ///<summary>
        /// Returns a wrapped object if <paramref name="obj"/> is an instance of <see cref="ValueTypeHolder"/>.
        ///</summary>
        ///<param name="obj">An object to be "erased".</param>
        ///<returns>The object wrapped by <paramref name="obj"/> if the latter is of type <see cref="ValueTypeHolder"/>.  Otherwise,
        /// return <paramref name="obj"/>.</returns>
        public static object UnwrapIfWrapped( this object obj )
        {
            var holder = obj as ValueTypeHolder;
            return holder == null ? obj : holder.Value;
        }

        /// <summary>
        /// Determines whether <paramref name="obj"/> is a wrapped object (instance of <see cref="ValueTypeHolder"/>).
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <returns>Returns true if <paramref name="obj"/> is a wrapped object (instance of <see cref="ValueTypeHolder"/>).</returns>
        public static bool IsWrapped( this object obj )
        {
            return obj as ValueTypeHolder != null;
        }
    }
}