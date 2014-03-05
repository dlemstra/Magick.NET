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

namespace Fasterflect
{
    /// <summary>
    /// A delegate to retrieve the value of an instance field or property of an object.
    /// </summary>
    /// <param name="obj">
    /// The object whose field's or property's value is to be retrieved.
    /// Use <c>null</c> for static field or property.
    /// </param>
    /// <returns>The value of the instance field or property.</returns>
    public delegate object MemberGetter( object obj );

    /// <summary>
    /// A delegate to set the value of an instance field or property of an object.
    /// </summary>
    /// <param name="obj">
    /// The object whose field's or property's value is to be set.
    /// Use <c>null</c> for static field or property.
    /// </param>
    /// <param name="value">The value to be set to the field or property.</param>
    public delegate void MemberSetter( object obj, object value );

    /// <summary>
    /// A delegate to set an element of an array.
    /// </summary>
    /// <param name="array">The array whose element is to be set.</param>
    /// <param name="index">The index of the element to be set.</param>
    /// <param name="value">The value to set to the element.</param>
    public delegate void ArrayElementSetter( object array, int index, object value );

    /// <summary>
    /// A delegate to retrieve an element of an array.
    /// </summary>
    /// <param name="array">The array whose element is to be retrieved</param>
    /// <param name="index">The index of the element to be retrieved</param>
    /// <returns>The element at <paramref name="index"/></returns>
    public delegate object ArrayElementGetter( object array, int index );

    /// <summary>
    /// A delegate to invoke an instance method or indexer of an object.
    /// </summary>
    /// <param name="obj">
    /// The object whose method or indexer is to be invoked on.
    /// Use <c>null</c> for static method.</param>
    /// <param name="parameters">The properly-ordered parameter list of the method/indexer.  
    /// For indexer-set operation, the parameter array include parameters for the indexer plus
    /// the value to be set to the indexer.</param>
    /// <returns>The return value of the method or indexer.  Null is returned if the method has no
    /// return type or if it's a indexer-set operation.</returns>
    public delegate object MethodInvoker( object obj, params object[] parameters );

    /// <summary>
    /// A delegate to invoke the constructor of a type.
    /// </summary>
    /// <param name="parameters">The properly-ordered parameter list of the constructor.</param>
    /// <returns>An instance of type whose constructor is invoked.</returns>
    public delegate object ConstructorInvoker( params object[] parameters );

    /// <summary>
    /// A delegate to copy values of instance members (fields, properties, or both) from one object to another.
    /// </summary>
    /// <param name="source">The object whose instance members' values will be read.</param>
    /// <param name="target">The object whose instance members' values will be written.</param>
    public delegate void ObjectMapper( object source, object target );
}