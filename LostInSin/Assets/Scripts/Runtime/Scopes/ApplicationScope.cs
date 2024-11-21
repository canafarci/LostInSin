using LostInSin.Runtime.CrossScene;
using LostInSin.Runtime.CrossScene.Audio;
using LostInSin.Runtime.CrossScene.Currency;
using LostInSin.Runtime.CrossScene.Data;
using LostInSin.Runtime.CrossScene.LoadingScreen;
using LostInSin.Runtime.CrossScene.LoadingScreen.Signals;
using LostInSin.Runtime.CrossScene.Signals;
using LostInSin.Runtime.Gameplay.Characters.Visuals;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations;
using LostInSin.Runtime.Gameplay.Characters.Visuals.Animations.Data;
using LostInSin.Runtime.Infrastructure.ApplicationState;
using LostInSin.Runtime.Infrastructure.Data;
using LostInSin.Runtime.Infrastructure.MemoryPool;
using LostInSin.Runtime.Infrastructure.Signals;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace LostInSin.Runtime.Scopes
{
	public class ApplicationScope : LifetimeScope
	{
		[SerializeField] private ApplicationSettings ApplicationSettings;
		[SerializeField] private AudioDataSo AudioDataSO;
		[SerializeField] private AudioView AudioView;
		[SerializeField] private PoolConfig PoolConfig;
		[SerializeField] private CurrencyConfig CurrencyConfig;
		[SerializeField] private CharacterAnimationClipsData CharacterAnimationClipsData;
		[SerializeField] private CharacterAnimationData CharacterAnimationData;


		protected override void Configure(IContainerBuilder builder)
		{
			RegisterInstances(builder);
			RegisterEntryPoints(builder);
			RegisterServices(builder);
			RegisterSignals(builder);
			RegisterLoadingScreen(builder);
			RegisterAnimationHelpers(builder);
		}

		private void RegisterAnimationHelpers(IContainerBuilder builder)
		{
			builder.RegisterEntryPoint<AnimationConstants>().AsSelf();
		}

		private void RegisterInstances(IContainerBuilder builder)
		{
			builder.RegisterInstance(ApplicationSettings);
			builder.RegisterInstance(AudioDataSO);
			builder.RegisterInstance(AudioView);
			builder.RegisterInstance(CurrencyConfig);
			builder.RegisterInstance(CharacterAnimationClipsData);
			builder.RegisterInstance(CharacterAnimationData);
		}

		private static void RegisterEntryPoints(IContainerBuilder builder)
		{
			builder.RegisterEntryPoint<SceneLoader>().AsSelf();
			builder.RegisterEntryPoint<AudioController>().AsSelf();
		}

		private void RegisterServices(IContainerBuilder builder)
		{
			builder.Register<IAudioModel, AudioModel>(Lifetime.Singleton);
			builder.Register<AudioMediator>(Lifetime.Singleton).AsSelf();
			builder.Register<ICurrencyModel, CurrencyModel>(Lifetime.Singleton);

			builder.Register<IGameplayPersistentData, GameplayPersistentData>(Lifetime.Singleton);

			builder.RegisterSignalBus();
			builder.RegisterPoolManager(PoolConfig);
			builder.RegisterAppController();
		}

		private void RegisterSignals(IContainerBuilder builder)
		{
			//cross scene
			builder.DeclareSignal<LoadSceneSignal>();
			builder.DeclareSignal<ChangeAudioSettingsSignal>();
			builder.DeclareSignal<ChangeHapticActivationSignal>();
			builder.DeclareSignal<PlayAudioSignal>();
			builder.DeclareSignal<CurrencyChangedSignal>();
		}

		private void RegisterLoadingScreen(IContainerBuilder builder)
		{
			if (!ApplicationSettings.ShowLoadingScreen) return;

			builder.RegisterComponentInNewPrefab(ApplicationSettings.LoadingScreenPrefab, Lifetime.Scoped)
				.DontDestroyOnLoad();
			builder.RegisterEntryPoint<LoadingScreenController>();
			builder.DeclareSignal<LoadingStartedSignal>();
			builder.DeclareSignal<LoadingFinishedSignal>();
		}
	}
}