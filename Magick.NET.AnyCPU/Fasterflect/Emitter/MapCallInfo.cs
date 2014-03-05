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
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Fasterflect.Emitter
{
    /// <summary>
    /// Stores all necessary information to construct a dynamic method for member mapping.
    /// </summary>
    [DebuggerStepThrough]
    internal class MapCallInfo : CallInfo
    {
    	public Type SourceType { get; private set; }
        public MemberTypes SourceMemberTypes { get; private set; }
        public MemberTypes TargetMemberTypes { get; private set; }
        public string[] Names { get; private set; }

    	public MapCallInfo( Type targetType, Type[] genericTypes, Flags bindingFlags, MemberTypes memberTypes, string name, Type[] parameterTypes, MemberInfo memberInfo, bool isReadOperation, Type sourceType, MemberTypes sourceMemberTypes, MemberTypes targetMemberTypes, string[] names ) : base( targetType, genericTypes, bindingFlags, memberTypes, name, parameterTypes, memberInfo, isReadOperation )
    	{
    		SourceType = sourceType;
    		SourceMemberTypes = sourceMemberTypes;
    		TargetMemberTypes = targetMemberTypes;
    		Names = names;
    	}

    	public override bool Equals( object obj )
        {
            var other = obj as MapCallInfo;
            if( other == null )
            {
                return false;
            }
            if( ! base.Equals( obj ) )
            {
                return false;
            }
            if( other.SourceType != SourceType ||
                other.SourceMemberTypes != SourceMemberTypes ||
                other.TargetMemberTypes != TargetMemberTypes ||
                (other.Names == null && Names != null) ||
				(other.Names != null && Names == null) ||
				(other.Names != null && Names != null && other.Names.Length != Names.Length) )
            {
                return false;
            }
            if( other.Names != null && Names != null )
            {
            	for( int i = 0; i < Names.Length; i++ )
            	{
            		if( Names[ i ] != other.Names[ i ] )
            		{
            			return false;
            		}
            	}
            }
    		return true;
        }

        public override int GetHashCode()
        {
        	int hash = base.GetHashCode() + SourceType.GetHashCode() * SourceMemberTypes.GetHashCode() * TargetMemberTypes.GetHashCode();
			for( int i = 0; i < Names.Length; i++ )
            {
                hash += Names[ i ].GetHashCode() * (i+1);
            }
            return hash;
        }        
    }
}