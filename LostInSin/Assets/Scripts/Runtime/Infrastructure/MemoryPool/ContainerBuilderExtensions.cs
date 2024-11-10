using LostInSin.Runtime.Infrastructure.Data;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Infrastructure.MemoryPool
{
	public static class ContainerBuilderExtensions
	{
		public static void RegisterPoolManager(this IContainerBuilder builder, PoolConfig poolConfig)
		{
			builder.RegisterEntryPoint<PoolManager>().WithParameter(poolConfig).AsSelf();
		}
	}
}