using System;
using System.Collections.Generic;
using VContainer;

namespace LostInSin.Runtime.Infrastructure.Signals
{
	public static class ContainerBuilderExtensions
	{
		private static readonly List<Type> DeclaredSignalTypes = new();

		public static void DeclareSignal<TSignal>(this IContainerBuilder builder)
		{
			Type signalType = typeof(TSignal);
			if (!DeclaredSignalTypes.Contains(signalType)) DeclaredSignalTypes.Add(signalType);
		}

		public static void RegisterSignalBus(this IContainerBuilder builder)
		{
			// Register the SignalBus with the declared signal types
			builder.Register<SignalBus>(resolver => new SignalBus(DeclaredSignalTypes), Lifetime.Singleton);

			// Clear the declared signals list to prevent issues if the container is built multiple times
			DeclaredSignalTypes.Clear();
		}
	}
}