using UnityEngine.Assertions;
using UnityEngine;
using System;
using System.Collections.Generic;
using LostInSin.Runtime.Infrastructure.ApplicationState;
using LostInSin.Runtime.Infrastructure.ApplicationState.Signals;
using LostInSin.Runtime.Infrastructure.Data;
using LostInSin.Runtime.Infrastructure.Templates;

namespace LostInSin.Runtime.Infrastructure.MemoryPool
{
	public class PoolManager : SignalListener
	{
		private static readonly Dictionary<Type, object> _monoPools = new();
		private static readonly Dictionary<Type, object> _genericPools = new();
		private readonly PoolConfig _config;

		public static event Action<AppStateID, AppStateID> OnAppStateChanged;

		public PoolManager(PoolConfig config)
		{
			_config = config;
		}

		protected override void SubscribeToEvents()
		{
			_signalBus.Subscribe<AppStateChangedSignal>(OnAppStateChangedSignal);
		}

		private void OnAppStateChangedSignal(AppStateChangedSignal signal)
		{
			OnAppStateChanged?.Invoke(signal.newState, signal.oldState);
		}

		protected override void UnsubscribeFromEvents()
		{
			_signalBus.Unsubscribe<AppStateChangedSignal>(OnAppStateChangedSignal);
		}

		public override void Initialize()
		{
			base.Initialize();

			foreach (PoolEntry entry in _config.PoolEntries)
			{
				if (entry.IsMonoBehaviour && entry.Prefab != null && entry.classType != null)
				{
					// Get the specific MonoBehaviour type from the prefab
					Type monoType = entry.classType;

					// Create GenericMonoPool<T> where T is the MonoBehaviour type
					Type generic = typeof(GenericMonoPool<>);
					Type poolType = generic.MakeGenericType(monoType);

					PoolParams poolParams = new(entry.InitialSize,
					                            entry.DefaultCapacity,
					                            entry.MaximumSize,
					                            entry.ManagePoolOnSceneChange,
					                            entry.LifetimeSceneID);

					// Use the constructor that takes a GameObject parameter
					object pool = Activator.CreateInstance(poolType, entry.Prefab, poolParams);

					_monoPools[monoType] = pool;
				}
				else if (!entry.IsMonoBehaviour && entry.classType != null)
				{
					Type type = entry.classType;

					// Create GenericPool<T> where T is the class type
					Type generic = typeof(GenericPool<>);
					Type poolType = generic.MakeGenericType(type);

					PoolParams poolParams = new(entry.InitialSize,
					                            entry.DefaultCapacity,
					                            entry.MaximumSize,
					                            entry.ManagePoolOnSceneChange,
					                            entry.LifetimeSceneID);

					object pool = Activator.CreateInstance(poolType, new object[] { poolParams });

					_genericPools[type] = pool;
				}
			}
		}

		public static T GetMono<T>() where T : MonoBehaviour, IPoolable
		{
			if (_monoPools.TryGetValue(typeof(T), out object poolObj))
			{
				GenericMonoPool<T> pool = poolObj as GenericMonoPool<T>;
				Assert.IsNotNull(pool, "Pool object is null.");

				return pool.Get();
			}

			throw new InvalidOperationException($"No pool found for type {typeof(T)}.");
		}

		public static T GetPure<T>() where T : class, IPoolable
		{
			if (_genericPools.TryGetValue(typeof(T), out object poolObj))
			{
				GenericPool<T> pool = poolObj as GenericPool<T>;
				Assert.IsNotNull(pool, "Pool object is null.");
				return pool.Get();
			}

			throw new InvalidOperationException($"No pool found for type {typeof(T)}.");
		}

		public static void ReleaseMono<T>(T obj) where T : MonoBehaviour, IPoolable
		{
			if (_monoPools.TryGetValue(typeof(T), out object poolObj))
			{
				GenericMonoPool<T> pool = poolObj as GenericMonoPool<T>;
				Assert.IsNotNull(pool, "Pool object is null.");

				pool.Release(obj);
			}
			else
				Debug.LogError($"No pool found for type {typeof(T)}.");
		}

		public static void ReleasePure<T>(T obj) where T : class, IPoolable
		{
			if (_genericPools.TryGetValue(typeof(T), out object poolObj))
			{
				GenericPool<T> pool = poolObj as GenericPool<T>;
				Assert.IsNotNull(pool, "Pool object is null.");

				pool.Release(obj);
			}
			else
				Debug.LogError($"No pool found for type {typeof(T)}.");
		}
	}
}