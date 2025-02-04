using UnityEngine;

namespace LostInSin.Runtime.Infrastructure.MemoryPool
{
	public class MonoPoolable : MonoBehaviour, IPoolable
	{
		public virtual void OnCreated()
		{
		}

		public virtual void OnDestroyed()
		{
		}

		public virtual void OnReturnToPool()
		{
		}

		public virtual void OnGetFromPool()
		{
		}
	}
}