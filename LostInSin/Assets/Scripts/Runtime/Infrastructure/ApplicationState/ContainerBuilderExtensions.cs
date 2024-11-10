using LostInSin.Runtime.Infrastructure.ApplicationState.Signals;
using LostInSin.Runtime.Infrastructure.Signals;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Infrastructure.ApplicationState
{
	public static class ContainerBuilderExtensions
	{
		public static void RegisterAppController(this IContainerBuilder builder)
		{
			builder.RegisterEntryPoint<ApplicationStateController>();

			builder.DeclareSignal<ChangeAppStateSignal>();
			builder.DeclareSignal<AppStateChangedSignal>();
		}
	}
}