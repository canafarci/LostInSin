using UnityEngine;
using UnityEngine.Assertions;

namespace LostInSin.Runtime.Infrastructure
{
	public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance = null;
		public static T instance => _instance;

		[SerializeField] private bool DontDestroyMonoOnLoad;

		private void Awake()
		{
			if (_instance)
			{
				Destroy(this);
			}

			//as this is an abstract class, we can safely cast its implementation to the derived type
			_instance = this as T;

			Assert.IsNotNull(_instance, $"Casting Singleton of type {typeof(T).Name} has failed!");

			if (DontDestroyMonoOnLoad)
				DontDestroyOnLoad(this);
		}

		private void OnDestroy()
		{
			ResetInstance();
		}

		//for scene specific mono singletons, clean up the instance when object is destroyed during scene switches 
		private void ResetInstance()
		{
			_instance = null;
		}
	}
}