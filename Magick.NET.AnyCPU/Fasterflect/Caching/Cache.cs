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
#if DOT_NET_4
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Diagnostics;
    
    [DebuggerStepThrough]
    internal sealed class Cache<TKey, TValue>
    {
        private readonly IDictionary<TKey, object> entries;

		#region Constructors
		public Cache()
		{
			entries = new ConcurrentDictionary<TKey, object>();
		}
		public Cache(IEqualityComparer<TKey> equalityComparer)
		{
			entries = new ConcurrentDictionary<TKey, object>(equalityComparer);
		}
		#endregion

		#region Properties
		/// <summary>
		/// Returns the number of entries currently stored in the cache. Accessing this property
		/// causes a check of all entries in the cache to ensure collected entries are not counted.
		/// </summary>
		public int Count
		{
			get { return ClearCollected(); }
		}
		#endregion

		#region Indexers
		/// <summary>
		/// Indexer for accessing or adding cache entries.
		/// </summary>
		public TValue this[TKey key]
		{
			get { return Get(key); }
			set { Insert(key, value, CacheStrategy.Temporary); }
		}

		/// <summary>
		/// Indexer for adding a cache item using the specified strategy.
		/// </summary>
		public TValue this[TKey key, CacheStrategy strategy]
		{
			set { Insert(key, value, strategy); }
		}
		#endregion

		#region Insert Methods
		/// <summary>
		/// Insert a collectible object into the cache.
		/// </summary>
		/// <param name="key">The cache key used to reference the item.</param>
		/// <param name="value">The object to be inserted into the cache.</param>
		public void Insert(TKey key, TValue value)
		{
			Insert(key, value, CacheStrategy.Temporary);
		}

		/// <summary>
		/// Insert an object into the cache using the specified cache strategy (lifetime management).
		/// </summary>
		/// <param name="key">The cache key used to reference the item.</param>
		/// <param name="value">The object to be inserted into the cache.</param>
		/// <param name="strategy">The strategy to apply for the inserted item (use Temporary for objects 
		/// that are collectible and Permanent for objects you wish to keep forever).</param>
		public void Insert(TKey key, TValue value, CacheStrategy strategy)
		{
			entries[key] = strategy == CacheStrategy.Temporary 
				? new WeakReference(value) 
				: value as object;
		}
		#endregion

		#region Get Methods
		/// <summary>
		/// Retrieves an entry from the cache using the given key.
		/// </summary>
		/// <param name="key">The cache key of the item to retrieve.</param>
		/// <returns>The retrieved cache item or null if not found.</returns>
		public TValue Get(TKey key)
		{
			object entry;
			entries.TryGetValue(key, out entry);
			var wr = entry as WeakReference;
			return (TValue)(wr != null ? wr.Target : entry);
		}
		#endregion

		#region Remove Methods
		/// <summary>
		/// Removes the object associated with the given key from the cache.
		/// </summary>
		/// <param name="key">The cache key of the item to remove.</param>
		/// <returns>True if an item removed from the cache and false otherwise.</returns>
		public bool Remove(TKey key)
		{
			return entries.Remove(key);
		}
		#endregion

		#region Clear Methods
		/// <summary>
		/// Removes all entries from the cache.
		/// </summary>
		public void Clear()
		{
			entries.Clear();
		}

		/// <summary>
		/// Process all entries in the cache and remove entries that refer to collected entries.
		/// </summary>
		/// <returns>The number of live cache entries still in the cache.</returns>
		private int ClearCollected()
		{
			IList<TKey> keys = entries.Where(kvp => kvp.Value is WeakReference && !(kvp.Value as WeakReference).IsAlive).Select(kvp => kvp.Key).ToList();
			keys.ForEach(k => entries.Remove(k));
			return entries.Count;
		}
		#endregion

		#region ToString
		/// <summary>
		/// This method returns a string with information on the cache contents (number of contained objects).
		/// </summary>
		public override string ToString()
		{
			int count = ClearCollected();
			return count > 0 ? String.Format("Cache contains {0} live objects.", count) : "Cache is empty.";
		}
		#endregion
    }
#elif DOT_NET_35
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Diagnostics;

    [DebuggerStepThrough]
	internal sealed class Cache<TKey,TValue>
	{
		private readonly Dictionary<TKey, object> entries;
		private int owner;

        #region Constructors
		public Cache()
		{
			entries = new Dictionary<TKey,object>();
		}
		public Cache( IEqualityComparer<TKey> equalityComparer )
		{
			entries = new Dictionary<TKey,object>( equalityComparer );
		}
        #endregion

        #region Properties
		/// <summary>
		/// Returns the number of entries currently stored in the cache. Accessing this property
		/// causes a check of all entries in the cache to ensure collected entries are not counted.
		/// </summary>
		public int Count
		{
			get { return ClearCollected(); }
		}
        #endregion

        #region Indexers
		/// <summary>
		/// Indexer for accessing or adding cache entries.
		/// </summary>
		public TValue this[ TKey key ]
		{
			get { return Get( key ); }
			set { Insert( key, value, CacheStrategy.Temporary ); }
		}

		/// <summary>
		/// Indexer for adding a cache item using the specified strategy.
		/// </summary>
		public TValue this[ TKey key, CacheStrategy strategy ]
		{
			set { Insert( key, value, strategy ); }
		}
        #endregion

    #region Insert Methods
		/// <summary>
		/// Insert a collectible object into the cache.
		/// </summary>
		/// <param name="key">The cache key used to reference the item.</param>
		/// <param name="value">The object to be inserted into the cache.</param>
		public void Insert( TKey key, TValue value )
		{
			Insert( key, value, CacheStrategy.Temporary );
		}

		/// <summary>
		/// Insert an object into the cache using the specified cache strategy (lifetime management).
		/// </summary>
		/// <param name="key">The cache key used to reference the item.</param>
		/// <param name="value">The object to be inserted into the cache.</param>
		/// <param name="strategy">The strategy to apply for the inserted item (use Temporary for objects 
		/// that are collectible and Permanent for objects you wish to keep forever).</param>
		public void Insert( TKey key, TValue value, CacheStrategy strategy )
		{
			object entry = strategy == CacheStrategy.Temporary ? new WeakReference( value ) : value as object;
			int current = Thread.CurrentThread.ManagedThreadId;
			while( Interlocked.CompareExchange( ref owner, current, 0 ) != current ) { }
			entries[ key ] = entry;
			if( current != Interlocked.Exchange( ref owner, 0 ) )
				throw new UnauthorizedAccessException( "Thread had access to cache even though it shouldn't have." );
		}
        #endregion

        #region GetValue Methods
		/// <summary>
		/// Retrieves an entry from the cache using the given key.
		/// </summary>
		/// <param name="key">The cache key of the item to retrieve.</param>
		/// <returns>The retrieved cache item or null if not found.</returns>
		public TValue Get( TKey key )
        {
			int current = Thread.CurrentThread.ManagedThreadId;
			while( Interlocked.CompareExchange( ref owner, current, 0 ) != current ) { }
			object entry;
			entries.TryGetValue( key, out entry );
			if( current != Interlocked.Exchange( ref owner, 0 ) )
				throw new UnauthorizedAccessException( "Thread had access to cache even though it shouldn't have." );
			var wr = entry as WeakReference;
			return (TValue) (wr != null ? wr.Target : entry);
		}
        #endregion

        #region Remove Methods
		/// <summary>
		/// Removes the object associated with the given key from the cache.
		/// </summary>
		/// <param name="key">The cache key of the item to remove.</param>
		/// <returns>True if an item removed from the cache and false otherwise.</returns>
		public bool Remove( TKey key )
		{
			int current = Thread.CurrentThread.ManagedThreadId;
			while( Interlocked.CompareExchange( ref owner, current, 0 ) != current ) { }
			bool found = entries.Remove( key );
			if( current != Interlocked.Exchange( ref owner, 0 ) )
				throw new UnauthorizedAccessException( "Thread had access to cache even though it shouldn't have." );
			return found;
		}
        #endregion

        #region Clear Methods
		/// <summary>
		/// Removes all entries from the cache.
		/// </summary>
		public void Clear()
		{
			int current = Thread.CurrentThread.ManagedThreadId;
			while( Interlocked.CompareExchange( ref owner, current, 0 ) != current ) { }
			entries.Clear();
			if( current != Interlocked.Exchange( ref owner, 0 ) )
				throw new UnauthorizedAccessException( "Thread had access to cache even though it shouldn't have." );
		}

		/// <summary>
		/// Process all entries in the cache and remove entries that refer to collected entries.
		/// </summary>
		/// <returns>The number of live cache entries still in the cache.</returns>
		private int ClearCollected()
		{
			int current = Thread.CurrentThread.ManagedThreadId;
			while( Interlocked.CompareExchange( ref owner, current, 0 ) != current ) { }
			IList<TKey> keys = entries.Where( kvp => kvp.Value is WeakReference && ! (kvp.Value as WeakReference).IsAlive ).Select( kvp => kvp.Key ).ToList();
			keys.ForEach( k => entries.Remove( k ) );
			int count = entries.Count;
			if( current != Interlocked.Exchange( ref owner, 0 ) )
				throw new UnauthorizedAccessException( "Thread had access to cache even though it shouldn't have." );
			return count;
		}
        #endregion

        #region ToString
        /// <summary>
		/// This method returns a string with information on the cache contents (number of contained objects).
		/// </summary>
		public override string ToString()
		{
			int count = ClearCollected();
			return count > 0 ? String.Format( "Cache contains {0} live objects.", count ) : "Cache is empty.";
		}
        #endregion
	}
#else
	#error At least one of the compilation symbols DOT_NET_4 or DOT_NET_35 must be defined. 
#endif
}