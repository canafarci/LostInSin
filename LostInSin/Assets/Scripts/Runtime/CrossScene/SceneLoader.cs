using System;
using DG.Tweening;
using LostInSin.Runtime.CrossScene.LoadingScreen.Signals;
using LostInSin.Runtime.CrossScene.Signals;
using LostInSin.Runtime.Infrastructure.ApplicationState;
using LostInSin.Runtime.Infrastructure.ApplicationState.Signals;
using LostInSin.Runtime.Infrastructure.Data;
using LostInSin.Runtime.Infrastructure.Templates;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace LostInSin.Runtime.CrossScene
{
	public class SceneLoader : SignalListener
	{
		[Inject] private ApplicationSettings _applicationSettings;

		protected override void SubscribeToEvents()
		{
			SignalBus.Subscribe<LoadSceneSignal>(OnLoadSceneMessage);
		}

		protected override void UnsubscribeFromEvents()
		{
			SignalBus.Unsubscribe<LoadSceneSignal>(OnLoadSceneMessage);
		}

		private void OnLoadSceneMessage(LoadSceneSignal signal)
		{
			AsyncOperation operation = SceneManager.LoadSceneAsync(signal.sceneID);
			SignalBus.Fire(new ChangeAppStateSignal(AppStateID.Loading));

			if (_applicationSettings.ShowLoadingScreen) SignalBus.Fire(new LoadingStartedSignal(operation));

			DOTween.KillAll();
			GC.Collect();
		}
	}
}