using System;
using LostInSin.Runtime.Infrastructure.Signals;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Infrastructure.Templates
{
	public abstract class SignalListener : IInitializable, IDisposable
	{
		[Inject] protected SignalBus SignalBus;

		public virtual void Initialize()
		{
			SubscribeToEvents();
		}

		protected abstract void SubscribeToEvents();
		protected abstract void UnsubscribeFromEvents();

		public virtual void Dispose()
		{
			UnsubscribeFromEvents();
		}
	}
}