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
			_signalBus.Subscribe<LoadSceneSignal>(OnLoadSceneMessage);
		}

		protected override void UnsubscribeFromEvents()
		{
			_signalBus.Unsubscribe<LoadSceneSignal>(OnLoadSceneMessage);
		}

		private void OnLoadSceneMessage(LoadSceneSignal signal)
		{
			_signalBus.Fire(new ChangeAppStateSignal(AppStateID.Loading));
			AsyncOperation operation = SceneManager.LoadSceneAsync(signal.sceneID);

			if (_applicationSettings.ShowLoadingScreen)
			{
				_signalBus.Fire(new LoadingStartedSignal(operation));
			}

			DOTween.KillAll();
			GC.Collect();
		}
	}
}