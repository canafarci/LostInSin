using System;
using System.Collections.Generic;
using LostInSin.Runtime.Infrastructure.Data;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer.Unity;

namespace LostInSin.Runtime.Infrastructure.MemoryPool
{
	public class PoolManager : IInitializable
	{
		private readonly Dictionary<Type, object> _monoPools = new();
		private readonly Dictionary<Type, object> _genericPools = new();
		private readonly PoolConfig _config;

		public PoolManager(PoolConfig config)
		{
			_config = config;
		}

		public void Initialize()
		{
			foreach (PoolEntry entry in _config.PoolEntries)
				if (entry.IsMonoBehaviour && entry.Prefab != null)
				{
					// Get the specific MonoBehaviour type from the prefab
					Type monoType = entry.classType;

					// Create GenericMonoPool<T> where T is the MonoBehaviour type
					Type generic = typeof(GenericMonoPool<>);
					Type poolType = generic.MakeGenericType(monoType);

					// Use the constructor that takes a GameObject parameter
					var pool = Activator.CreateInstance(poolType, new object[] { entry.Prefab });

					_monoPools[monoType] = pool;
				}
				else if (!entry.IsMonoBehaviour && entry.classType != null)
				{
					Type type = entry.classType;

					// Create GenericPool<T> where T is the class type
					Type generic = typeof(GenericPool<>);
					Type poolType = generic.MakeGenericType(type);

					var pool = Activator.CreateInstance(poolType);

					_genericPools[type] = pool;
				}
		}

		public T GetMono<T>() where T : MonoBehaviour
		{
			if (_monoPools.TryGetValue(typeof(T), out var poolObj))
			{
				GenericMonoPool<T> pool = poolObj as GenericMonoPool<T>;
				Assert.IsNotNull(pool, "Pool object is null.");

				return pool.Get();
			}

			throw new InvalidOperationException($"No pool found for type {typeof(T)}.");
		}

		public T GetPure<T>() where T : class, IPoolable
		{
			if (_genericPools.TryGetValue(typeof(T), out var poolObj))
			{
				GenericPool<T> pool = poolObj as GenericPool<T>;
				Assert.IsNotNull(pool, "Pool object is null.");
				return pool.Get();
			}

			throw new InvalidOperationException($"No pool found for type {typeof(T)}.");
		}

		public void ReleaseMono<T>(T obj) where T : MonoBehaviour
		{
			if (_monoPools.TryGetValue(typeof(T), out var poolObj))
			{
				GenericMonoPool<T> pool = poolObj as GenericMonoPool<T>;
				Assert.IsNotNull(pool, "Pool object is null.");

				pool.Release(obj);
			}
			else
			{
				Debug.LogError($"No pool found for type {typeof(T)}.");
			}
		}

		public void ReleasePure<T>(T obj) where T : class, IPoolable
		{
			if (_genericPools.TryGetValue(typeof(T), out var poolObj))
			{
				GenericPool<T> pool = poolObj as GenericPool<T>;
				Assert.IsNotNull(pool, "Pool object is null.");

				pool.Release(obj);
			}
			else
			{
				Debug.LogError($"No pool found for type {typeof(T)}.");
			}
		}
	}
}