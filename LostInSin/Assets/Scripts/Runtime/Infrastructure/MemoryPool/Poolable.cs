namespace LostInSin.Runtime.Infrastructure.MemoryPool
{
	public abstract class Poolable : IPoolable
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