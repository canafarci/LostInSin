using LostInSin.Runtime.Bootstrap;
using LostInSin.Runtime.Infrastructure.Data;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Scopes
{
	public class BootstrapScope : LifetimeScope
	{
		[SerializeField] private ApplicationSettings ApplicationSettings;
		protected override void Configure(IContainerBuilder builder)
		{
			builder.RegisterEntryPoint<BootstrapSceneEntryPoint>().AsSelf();
			
			builder.Register<AppInitializer>(Lifetime.Singleton).AsSelf();
		}
	}
}